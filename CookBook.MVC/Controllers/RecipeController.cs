using AutoMapper;
using CookBook.Application.IngridientUtils.Commands.CreateIngridient;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.Application.IngridientUtils.Queries.GetIngridient;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
using CookBook.Application.RecipeUtils.Commands.CreateRecipeCategory;
using CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient;
using CookBook.Application.RecipeUtils.Commands.DeleteRecipe;
using CookBook.Application.RecipeUtils.Commands.DeleteRecipeIngridient;
using CookBook.Application.RecipeUtils.Commands.EditImage;
using CookBook.Application.RecipeUtils.Commands.EditRecipe;
using CookBook.Application.RecipeUtils.Commands.EditRecipeIngridient;
using CookBook.Application.RecipeUtils.Queries.AdvancedSearch;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipes;
using CookBook.Application.RecipeUtils.Queries.GetRecipeById;
using CookBook.Application.RecipeUtils.Queries.GetRecipeCategoryById;
using CookBook.Application.RecipeUtils.Queries.GetRecipeIngridients;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
using CookBook.Domain.Entities;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            var recipeCategory = await _mediator.Send(new GetRecipeCategoryByIdQuery(dto.CategoryId));
            ViewBag.CategoryName = recipeCategory.Name;
            return View(dto);
        }

        #region Create Actions
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await this.ViewWithCategories(_mediator, command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return await this.ViewWithCategories(_mediator);
        }
        #endregion

        #region Edit Actions
        [HttpGet]
        [Route("recipe/{Id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetRecipeByIdQuery(id));

            if (!result.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            //var result = _mapper.Map<EditRecipeCommand>(dto);
            result.Id = id;
            return await this.ViewWithCategories(_mediator, result);
        }
        [HttpPost]
        [Route("recipe/{Id}/edit")]
        public async Task<IActionResult> Edit(EditRecipeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await this.ViewWithCategories(_mediator, command);
            }
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
        //widok zaaansowanego wyszukiwania
        [AllowAnonymous]
        public async Task<IActionResult> AdvancedSearch(string searchName, string ings, string categories, string others)
        {
            if(ings is null)
            {
                return await this.ViewWithCategories(_mediator);
            }

            SearchParams parameters = new SearchParams();

            //odtwarzamy listę składników do wyszukania
            if (int.TryParse(ings[0].ToString(), out int ingMode))
            {
                if(ings.Length < 3)
                {
                    parameters.IngridientsList = "";
                }
                else
                {
                    parameters.IngridientsList = ings.Substring(2);
                }
                parameters.Mode = ingMode;
            }
            else
            {
                return BadRequest("Lista składników do wyszukania nie została prawidłowo wczytana.");
            }

            parameters.SearchName = searchName;
            parameters.ChoosenCategories = categories;
            parameters.OtherFilters = others;

            return await this.SearchViewWithCategories(_mediator, parameters);
        }

        //akcja do dodawania składników do listy do wyszukania
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

        //akcja do zaawansowanego wyszukiwania
        [HttpGet]
        [AllowAnonymous]
        [Route("recipe/search/advanced")]
        public async Task<IActionResult> SearchByIngridients(string searchName, string categories, 
            string ings, string others)
        {
            string[]? categoryNames = categories.IsNullOrEmpty() ? null : categories.Split(";");

            string[]? ingridients = (ings == "0") ? null : ings.Split(";");

            int othersCount = others.Count(c => c == ';') + 1;
            bool[] otherFilters = new bool[othersCount];
            if(othersCount == 1)
            {
                otherFilters[0] = bool.Parse(others);
            }
            else
            {
                string[] otherString = others.Split(";");
                for (int i = 0; i < otherString.Length; i++)
                {
                    otherFilters[i] = bool.Parse(otherString[i]);
                }
            }

            var result = await _mediator.Send(new AdvancedSearchQuery(searchName, ingridients, 
                categoryNames, otherFilters));
            return Ok(result);
        }
        #endregion
    }
}
