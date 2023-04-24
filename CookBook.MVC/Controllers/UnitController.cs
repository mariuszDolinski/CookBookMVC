using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
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
    }
}
