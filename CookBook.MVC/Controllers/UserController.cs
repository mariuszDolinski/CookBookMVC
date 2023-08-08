using AutoMapper;
using CookBook.Application.ApplicationUser.Queries.GetAllRecipeCategories;
using CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// return view with all user's recipes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userRecipes = await _mediator.Send(new GetAllUserRecipesQuery());
            return View(userRecipes);
        }
        /// <summary>
        /// return view with recipe categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Categories()
        {
            var categories = await _mediator.Send(new GetAllRecipeCategoriesQuery());
            return View(categories);
        }
    }
}
