using AutoMapper;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.Application.RecipeUtils.Commands.EditImage;
using CookBook.Application.RecipeUtils.Commands.EditRecipe;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
using CookBook.Application.RecipeUtils.Queries.GetRecipeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CookBook.MVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RecipeController(IMediator mediator, IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recipes = await _mediator.Send(new GetAllRecipesQuery());
            return View(recipes);
        }
        [Route("recipe/{recipeId}/details")]
        public async Task<IActionResult> Details(int recipeId)
        {
            var dto = await _mediator.Send(new GetRecipeByIdQuery(recipeId));
            return View(dto);
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

        #region Edit Actions
        [Route("recipe/{Id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _mediator.Send(new GetRecipeByIdQuery(id));
            var result = _mapper.Map<EditRecipeCommand>(dto);
            result.Id = id;
            return View(result);
        }
        [HttpPost]
        [Route("recipe/{Id}/edit")]
        public async Task<IActionResult> Edit(int id, EditRecipeCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Route("recipe/{Id}/edit/image")]
        public async Task<IActionResult> EditImage(int id)
        {
            var dto = await _mediator.Send(new GetRecipeByIdQuery(id));
            var result = _mapper.Map<EditImageCommand>(dto);
            result.Id = id;
            return View(result);
        }
        [HttpPost]
        [Route("recipe/{Id}/edit/image")]
        public async Task<IActionResult> EditImage(int id, EditImageCommand command)
        {
            if (!ModelState.IsValid)
            {
                var dto = await _mediator.Send(new GetRecipeByIdQuery(id));
                command.Name = dto.Name;
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction("Edit", new RouteValueDictionary(
                new
                {
                    Controller = "Recipe",
                    Action = "Edit",
                    id
                }));
        }
        #endregion


    }
}
