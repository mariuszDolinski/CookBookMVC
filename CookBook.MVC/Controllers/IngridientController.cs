using CookBook.Application.IngridientUtils.Commands.CreateIngridient;
using CookBook.Application.IngridientUtils.Commands.DeleteIngridient;
using CookBook.Application.IngridientUtils.Commands.EditIngridient;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridientsCategories;
using CookBook.Application.RecipeUtils;
using CookBook.Domain.Entities;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    public class IngridientController : Controller
    {
        private readonly IMediator _mediator;

        public IngridientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search = "", string sortOrder = "", int page = 1, int pageSize = 5)
        {
            this.SetViewBagParams(search, sortOrder, pageSize);
            this.SetViewBagSortIcons(sortOrder);
            ViewBag.Code = ItemCodes.INGR;

            var ingridientsDto = await _mediator.Send(new GetAllIngridientsQuery
                (search, sortOrder, page, pageSize));

            var pages = new Pagination(ingridientsDto.TotalItems, page, pageSize);
            pages.SortOrder = sortOrder;
            pages.SearchPhrase = search;
            ViewBag.Pages = pages;

            var query = new ParamsQuery(sortOrder, page, pageSize);
            this.SetTempData(query);

            return View(@"ItemListViews/ListIndex", ingridientsDto.Items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngridientCommand command)
        {
            var query = this.GetTempData();

            if(User.Identity == null || !User.Identity.IsAuthenticated)
            {
                this.SetNotification("warning", "Zaloguj się aby dodać nowe składniki.");
                return this.CallRedirectToAction(query, nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                this.SetNotification("warning", errors[0]);
                return this.CallRedirectToAction(query, nameof(Index));
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Składnik '{command.Name}' został dodany");
            return this.CallRedirectToAction(query, nameof(Index));
        }

        [HttpPut]
        [Route("ingridient/edit/{names}")]
        public async Task<IActionResult> Edit(string names)
        {
            return await this.EditItem(_mediator, new EditIngridientCommand(), names);
        }

        [HttpDelete]
        [Route("ingridient/delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            return await this.DeleteItem(_mediator, new DeleteIngridientCommand(), name);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ingridient/GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetAllIngridientsCategoriesQuery());
            DatalistsDto data = new()
            {
                Ingridients = categories.Items.Select(x => x.Name!).ToList()
            };
            return Ok(data);
        }
    }
}
