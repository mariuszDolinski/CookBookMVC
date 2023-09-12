using AutoMapper;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories;
using CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CookBook.MVC.Extensions;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
using CookBook.MVC.Models;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;

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
        public async Task<IActionResult> GetAllCategories(string search = "", string sortOrder = "", int page = 1, int pageSize = 5)
        {
            this.SetViewBagParams(search, sortOrder, pageSize);
            this.SetViewBagSortIcons(sortOrder);
            ViewBag.Code = ItemCodes.CATG;

            var paginatedCategories = await _mediator.Send(new GetAllRecipeCategoriesQuery(search, sortOrder, page, pageSize));
            var pages = new Pagination(paginatedCategories.TotalItems, page, pageSize, nameof(this.GetAllCategories));
            pages.SortOrder = sortOrder;
            pages.SearchPhrase = search;
            ViewBag.Pages = pages;

            var query = new ParamsQuery(sortOrder, page, pageSize);
            this.SetTempData(query);

            return View(@"ItemListViews/ListIndex", paginatedCategories.Items);
        }
    }
}
