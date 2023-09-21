using CookBook.Application.RecipeUtils.Commands.CreateRecipeCategory;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RecipeCategoryController : Controller
    {
        private readonly IMediator _mediator;

        public RecipeCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search = "", string sortOrder = "", int page = 1, int pageSize = 5)
        {
            this.SetViewBagParams(search, sortOrder, pageSize);
            this.SetViewBagSortIcons(sortOrder);
            ViewBag.Code = ItemCodes.CATG;

            var paginatedCategories = await _mediator.Send(new GetAllRecipeCategoriesQuery(search, sortOrder, page, pageSize));
            var pages = new Pagination(paginatedCategories.TotalItems, page, pageSize, nameof(this.Index));
            pages.SortOrder = sortOrder;
            pages.SearchPhrase = search;
            ViewBag.Pages = pages;

            var query = new ParamsQuery(sortOrder, page, pageSize);
            this.SetTempData(query);

            return View(@"ItemListViews/ListIndex", paginatedCategories.Items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeCategoryCommand command)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                this.SetNotification("warning", "Zaloguj się aby dodać nowe kategorie.");
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                this.SetNotification("warning", errors[0]);
                return RedirectToAction(nameof(Index));
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Kategoria '{command.Name}' została dodana");
            return RedirectToAction(nameof(Index));
        }
    }
}
