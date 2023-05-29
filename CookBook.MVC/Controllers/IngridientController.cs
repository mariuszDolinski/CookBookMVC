using CookBook.Application.IngridientUtils.Commands.CreateIngridient;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
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

            var ingridientsDto = await _mediator.Send(new GetAllIngridientsQuery
                (search, sortOrder, page, pageSize));

            var pages = new Pagination(ingridientsDto.TotalItems, page, pageSize);
            pages.SortOrder = sortOrder;
            pages.SearchPhrase = search;
            ViewBag.Pages = pages;

            var query = new ParamsQuery(sortOrder, page, pageSize);
            this.SetTempData(query);

            return View(ingridientsDto.Items);
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
                this.SetNotification("error", errors[0]);
                return this.CallRedirectToAction(query,nameof(Index));
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Składnik '{command.Name}' został dodany");
            return this.CallRedirectToAction(query, nameof(Index));
        }
    }
}
