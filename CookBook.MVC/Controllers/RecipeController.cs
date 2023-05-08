using AutoMapper;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient;
using CookBook.Application.RecipeUtils.Commands.EditImage;
using CookBook.Application.RecipeUtils.Commands.EditRecipe;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
using CookBook.Application.RecipeUtils.Queries.GetRecipeById;
using CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<IActionResult> Create(CreateRecipeCommand command)
        {
            if(!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);  
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
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

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            var result = _mapper.Map<EditRecipeCommand>(dto);
            result.Id = id;
            return View(result);
        }
        [HttpPost]
        [Route("recipe/{Id}/edit")]
        public async Task<IActionResult> Edit(int id, EditRecipeCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Details", new RouteValueDictionary(
                new
                {
                    Controller = "Recipe",
                    Action = "Details",
                    recipeId = id
                }));
        }

        [Route("recipe/{Id}/edit/image")]
        public async Task<IActionResult> EditImage(int id)
        {
            var dto = await _mediator.Send(new GetRecipeByIdQuery(id));
            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }
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

        [HttpPost]
        [Route("recipe/recipeIngridient")]
        public async Task<IActionResult> CreateRecipeIngridient(CreateRecipeIngridientCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                return BadRequest(errors[0]);
            }
            var isAuthor = await _mediator.Send(command);

            return Ok();
        }
        [HttpGet]
        [Route("recipe/{recipeId}/recipeIngridients")]
        public async Task<IActionResult> GetRecipeIngridients(int recipeId)
        {
            var data = await _mediator.Send(new GetRecipeIngridientsQuery() { RecipeId = recipeId });
            return Ok(data);    
        }
        [HttpGet]
        [Route("recipe/getDatalists")]
        public async Task<IActionResult> GetDatalists()
        {
            var ingridients = await _mediator.Send(new GetAllIngridientsQuery());
            var units = await _mediator.Send(new GetAllUnitsQuery());
            DatalistsDto data = new ()
            { 
               Ingridients = ingridients.Select(x => x.Name!).ToList(),
               Units = units.Select(x => x.Name!).ToList()
            };
            return Ok(data);
        }
    }
}
