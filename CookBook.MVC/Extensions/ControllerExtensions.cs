using CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CookBook.Domain.Utils;

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
        public static RedirectToActionResult CallRedirectToAction(this Controller controller, ParamsQuery query, string action)
        {
            return controller.RedirectToAction(action,
                   new
                   {
                       sortOrder = query.SortOrder,
                       page = query.Page,
                       pageSize = query.PageSize
                   });
        }
        //metody do zwracania widoku Create dla przepisu z przesyłem listy kategorii przez ViewBag
        #region metody do populacji kategorii w widokach
        public static async Task<IActionResult> ViewWithCategories(this Controller controller, 
            IMediator mediator, CreateRecipeCommand command)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.ToList();
            return controller.View(command);
        }
        public static async Task<IActionResult> ViewWithCategories(this Controller controller,
            IMediator mediator, RecipeDto command)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.ToList();
            return controller.View(command);
        }
        public static async Task<IActionResult> ViewWithCategories(this Controller controller,
            IMediator mediator)
        {
            var categories = await mediator.Send(new GetAllRecipeCategoriesQuery());
            controller.ViewBag.Categories = categories.ToList();
            return controller.View();
        }
        #endregion
        //metoda ustawiająca wartości pól w klasie RecipesQueryParams
        public static RecipesQueryParams SetQueryParams(this Controller controller, string? searchPhrase, int pageNaumber, int pageSize,
            string[]? ingList, int advancedMode, string category, bool isVege)
        {
            return new RecipesQueryParams()
            {
                SearchPhrase = searchPhrase,
                PageNumber = pageNaumber,
                PageSize = pageSize,
                IngList = ingList,
                AdvancedSearchMode = advancedMode,
                Category = category,
                IsVegetarian = isVege
            };
        }
    }
}
