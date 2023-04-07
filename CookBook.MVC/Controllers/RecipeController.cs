using CookBook.Application.DtoModels;
using CookBook.Application.Services;
using CookBook.Application.Services.Interfaces;
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

        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetAllRecipes();
            return View(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeDto dto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            await _recipeService.CreateRecipe(dto);       
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
