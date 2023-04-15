using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
using CookBook.Application.RecipeUtils.Queries.GetRecipeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _mediator.Send(new GetAllRecipesQuery());
            return View(recipes);
        }

        #region Create Action
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeCommand command)
        {
            if(!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);  
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        [Route("recipe/{recipeId}/details")]
        public async Task<IActionResult> Details(int recipeId)
        {
            var dto = await _mediator.Send(new GetRecipeByIdQuery(recipeId));
            return View(dto);
        }


    }
}
