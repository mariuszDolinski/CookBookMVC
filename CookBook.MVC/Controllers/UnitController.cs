using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.UnitUtils.Commands.CreateUnit;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
using CookBook.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    public class UnitController : Controller
    {
        private readonly IMediator _mediator;

        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var units = await _mediator.Send(new GetAllUnitsQuery());
            return View(units);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUnitCommand command)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                this.SetNotification("warning", "Zaloguj się aby dodać nowe jednostki.");
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var units = await _mediator.Send(new GetAllUnitsQuery());
                return View("Index", units);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Create()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
