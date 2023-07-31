using AutoMapper;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.IngridientUtils.Queries.GetIngridient;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient;
using CookBook.Application.RecipeUtils.Commands.DeleteRecipe;
using CookBook.Application.RecipeUtils.Commands.DeleteRecipeIngridient;
using CookBook.Application.RecipeUtils.Commands.EditImage;
using CookBook.Application.RecipeUtils.Commands.EditRecipe;
using CookBook.Application.RecipeUtils.Commands.EditRecipeIngridient;
using CookBook.Application.RecipeUtils.Queries.AdvancedSearch;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
using CookBook.Application.RecipeUtils.Queries.GetRecipeById;
using CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RecipeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 6)
        {
            this.SetViewBagParams(search, "", pageSize);
            var recipes = await _mediator.Send(new GetAllRecipesQuery(search, page, pageSize));

            var pages = new Pagination(recipes.TotalItems, page, pageSize);
            pages.SearchPhrase = search;
            ViewBag.Pages = pages;

            var dto = new RecipeIndexDto()
            {
                Recipes = recipes.Items
            };
            dto.IsInSearchMode = (search == null) ? false : true;
            return View(dto);
        }

        [AllowAnonymous]
        [HttpGet]
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
            if (!ModelState.IsValid)
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
            return RedirectToAction("Index", "User");
            //return RedirectToAction("Details", new RouteValueDictionary(
            //    new
            //    {
            //        Controller = "Recipe",
            //        Action = "Details",
            //        recipeId = id
            //    }));
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

        [HttpDelete]
        [Route("recipe/{recipeId}")]
        public async Task<IActionResult> Delete(int recipeId)
        {
            await _mediator.Send(new DeleteRecipeCommand(recipeId));
            return Ok();
        }

        #region Recipe Ingridients
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
            if (!isAuthor)
            {
                return BadRequest("Użytkownik nie jest autorem przepisu! Składnik nie został dodany.");
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("recipe/{recipeId}/recipeIngridients")]
        public async Task<IActionResult> GetRecipeIngridients(int recipeId)
        {
            var data = await _mediator.Send(new GetRecipeIngridientsQuery() { RecipeId = recipeId });
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("recipe/getDatalists")]
        public async Task<IActionResult> GetDatalists()
        {
            var ingridients = await _mediator.Send(new GetAllIngridientsQuery());
            var units = await _mediator.Send(new GetAllUnitsQuery());
            DatalistsDto data = new()
            {
                Ingridients = ingridients.Items.Select(x => x.Name!).ToList(),
                Units = units.Items.Select(x => x.Name!).ToList()
            };
            return Ok(data);
        }

        [HttpDelete]
        [Route("recipeIngridient/{ingId}")]
        public async Task<IActionResult> DeleteIngridient(int ingId)
        {
            await _mediator.Send(new DeleteRecipeIngridientCommand() { recipeIngridientId = ingId });

            return Ok();
        }

        [HttpPut]
        [Route("recipeIngridient/{ingId}")]
        public async Task<IActionResult> EditRecipeIngridient(int ingId, EditRecipeIngridientCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                return BadRequest(errors[0]);
            }
            command.Id = ingId;
            var isAuthor = await _mediator.Send(command);
            if (!isAuthor)
            {
                return BadRequest("Użytkownik nie jest autorem przepisu! Składnik nie został zmieniony.");
            }
            return Ok();
        }
        #endregion

        #region Advanced search actions
        [AllowAnonymous]
        public ActionResult AdvancedSearch()
        {
            string[]? ingridients = (string[]?)TempData["searchIng"];
            return View(ingridients);
        }

        [HttpGet]
        [Route("search/addIngToList")]
        [AllowAnonymous]
        public async Task<IActionResult> AddIngridientToSearchList(string ingridient)
        {
            var result = await _mediator.Send(new GetIngridientQuery(ingridient));
            switch (result)
            {
                case 0: return BadRequest("Nie wybrano składnika");
                case 1: return Ok();
                case 2: return BadRequest("Podany składnik nie istnieje");
                default: return BadRequest("Nieznany błąd");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("recipe/search/advanced")]
        public async Task<IActionResult> SearchByIngridients(string[] args)
        {
            if(args.Length < 2)
            {
                return BadRequest("Brak składników do wyszukania");
            }
            string[] ingridients = new string[args.Length - 1];
            int mode = 1;
            for(int i=0; i<args.Length; i++)
            {
                if (i < args.Length - 1)
                {
                    ingridients[i] = args[i];
                }
                else
                {
                    if(!int.TryParse(args[i], out mode))
                    {
                        return BadRequest("Błąd danych");
                    }
                }
            }
            TempData["searchIng"] = ingridients;
            var result = await _mediator.Send(new AdvancedSearchQuery(ingridients, mode));
            RecipeAdvancedSearchDto dto = new RecipeAdvancedSearchDto()
            {
                Recipes = result,
                IngridientsToSearch = ingridients,
                Mode = mode
            };
            return View(dto);
        }
        #endregion
    }
}
