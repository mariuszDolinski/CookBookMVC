using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
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

        public async Task<IActionResult> Index()
        {
            var ingridients = await _mediator.Send(new GetAllIngridientsQuery());
            return View(ingridients);
        }
    }
}
