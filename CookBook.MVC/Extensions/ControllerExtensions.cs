using CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CookBook.Application.Commons;

namespace CookBook.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetNotification(this Controller controller, string type, string message)
        {
            var notification = new Notification(type, message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }

        public static void SetViewBagParams(this Controller controller, string? search, string sortOrder, int pageSize)
        {
            controller.ViewBag.NameSortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            controller.ViewBag.DateSortOrder = (sortOrder == "date_asc") ? "date_desc" : "date_asc";
            controller.ViewBag.AuthorSortOrder = (sortOrder == "author_asc") ? "author_desc" : "author_asc";
            controller.ViewBag.PageSize = pageSize;
            controller.ViewBag.SearchPhrase = string.IsNullOrEmpty(search) ? "" : search;
        }
        public static void SetViewBagSortIcons(this Controller controller, string sortOrder)
        {
            switch (sortOrder)
            {
                case "date_desc":
                    controller.ViewBag.NameIconClass = "";
                    controller.ViewBag.DateIconClass = "bi bi-arrow-down";
                    controller.ViewBag.AuthorIconClass = "";
                    break;
                case "date_asc":
                    controller.ViewBag.NameIconClass = "";
                    controller.ViewBag.DateIconClass = "bi bi-arrow-up";
                    controller.ViewBag.AuthorIconClass = "";
                    break;
                case "author_desc":
                    controller.ViewBag.NameIconClass = "";
                    controller.ViewBag.DateIconClass = "";
                    controller.ViewBag.AuthorIconClass = "bi bi-arrow-down";
                    break;
                case "author_asc":
                    controller.ViewBag.NameIconClass = "";
                    controller.ViewBag.DateIconClass = "";
                    controller.ViewBag.AuthorIconClass = "bi bi-arrow-up";
                    break;
                case "desc":
                    controller.ViewBag.NameIconClass = "bi bi-arrow-down";
                    controller.ViewBag.DateIconClass = "";
                    controller.ViewBag.AuthorIconClass = "";
                    break;
                default:
                    controller.ViewBag.NameIconClass = "bi bi-arrow-up";
                    controller.ViewBag.DateIconClass = "";
                    controller.ViewBag.AuthorIconClass = "";
                    break;
            }
        }
        public static void SetTempData(this Controller controller, ParamsQuery query)
        {
            controller.TempData["sortOrder"] = query.SortOrder;
            controller.TempData["page"] = query.Page;
            controller.TempData["pageSize"] = query.PageSize;
        }
        public static ParamsQuery GetTempData(this Controller controller)
        {
            var query = new ParamsQuery();

            if (controller.TempData["page"] != null)
            {
                query.SortOrder = controller.TempData["sortOrder"];
                query.Page = controller.TempData["page"];
                query.PageSize = controller.TempData["pageSize"];
            }

            return query;
        }

        public static RedirectToActionResult CallRedirectToAction(this Controller controller, 
            ParamsQuery query, string action)
        {
            return controller.RedirectToAction(action,
                   new
                   {
                       sortOrder = query.SortOrder,
                       page = query.Page,
                       pageSize = query.PageSize
                   });
        }

        #region metody do populacji kategorii w widokach
        public static async Task<IActionResult> ViewWithCategories(this Controller controller,
            IMediator mediator, CreateRecipeCommand command)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.Items.ToList();
            return controller.View(command);
        }
        public static async Task<IActionResult> ViewWithCategories(this Controller controller,
            IMediator mediator, RecipeDto command)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.Items.ToList();
            return controller.View(command);
        }
        public static async Task<IActionResult> ViewWithCategories(this Controller controller,
            IMediator mediator)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.Items.ToList();
            return controller.View();
        }
        public static async Task<IActionResult> SearchViewWithCategories(this Controller controller,
            IMediator mediator, SearchParams parameters)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.Items.ToList();
            return controller.View(parameters);
        }
        #endregion

        #region metody implementujące wspólne akcje dla widoku ItemList
        public static async Task<IActionResult> EditItem(this Controller controller, IMediator mediator,
            IEditItemCommand command, string names)
        {
            string[] oldNewName = names.Split(';');

            if (oldNewName.Length != 2)
            {
                return controller.BadRequest("Dane nie zostały poprawnie przesłane");
            }

            command.Name = oldNewName[1];
            command.OldName = oldNewName[0];
            var message = (string?)(await mediator.Send(command));

            if(message == null)
            {
                return controller.BadRequest("Nieznany błąd.");
            }
            if (message.Length > 0)
            {
                return controller.BadRequest(message);
            }

            return controller.Ok();
        }
        public static async Task<IActionResult> DeleteItem(this Controller controller, IMediator mediator,
            IItemListDto command, string name)
        {
            command.Name = name;
            var message = (string?)(await mediator.Send(command));

            if (message == null)
            {
                return controller.BadRequest("Nieznany błąd.");
            }
            if (message.Length > 0)
            {
                return controller.BadRequest(message);
            }

            return controller.Ok();
        }

        #endregion
    }
}
