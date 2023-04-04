using CookBook.Application.DtoModels;
using CookBook.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService) 
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeDto dto)
        {
            await _recipeService.CreateRecipe(dto);
            return RedirectToAction(nameof(Create));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
