using CookBook.Application.IngridientUtils.Commands.CreateIngridient;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.IngridientUtils.Queries.IngridientsCount;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Net.Sockets;

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

            var ingridientsCount = await _mediator.Send(new IngridientsCountQuery(search));
            var pages = new Pagination(ingridientsCount, page, pageSize);
            pages.SortOrder = sortOrder;
            pages.SearchPhrase = search;

            var query = new ParamsQuery(sortOrder, page, pageSize);
            this.SetTempData(query);

            ViewBag.Pages = pages;

            var ingridients = await _mediator.Send(new GetAllIngridientsQuery(search, sortOrder, page, pageSize));

            return View(ingridients);
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
