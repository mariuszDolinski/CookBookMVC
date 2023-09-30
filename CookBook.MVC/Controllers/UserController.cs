using AutoMapper;
using CookBook.Application.RecipeUtils.Queries.GetAllRecipeCategories;
using CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using CookBook.Application.RecipeUtils.Commands.CreateRecipeCategory;
using CookBook.Application.ApplicationUser.Queries.GetAllUsers;
using CookBook.Application.ApplicationUser.Queries.GetAllRoles;
using Microsoft.IdentityModel.Tokens;
using CookBook.Application.ApplicationUser.Commands;

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllUsers(string roleName, string userName)
        {
            var allRoles = await _mediator.Send(new GetAllRolesQuery());

            ViewBag.AllRoles = allRoles;

            roleName ??= "";
            ViewBag.SearchedRole = roleName;

            userName ??= "";
            ViewBag.SearchedUser = userName;

            var allUsers = await _mediator.Send(new GetAllUsersQuery(roleName, userName));
            return View(allUsers);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/editRoles/{parameters}")]
        public async Task<IActionResult> SetRolesForUser(string parameters)
        {
            string[] paramArray = parameters.Split(';');
            string userName = paramArray[0];
            List<string> newRoles = new List<string>();
            if (paramArray.Length > 1)
            {
                for(int i = 1; i < paramArray.Length; i++)
                {
                    newRoles.Add(paramArray[i]);
                }
            }
            var result = await _mediator.Send(new ChangeUserRolesCommand(userName, newRoles));
            if(result != "")
            {
                return BadRequest(result);
            }
            else
            {
                return Ok("Role użytkownika zostały zmienione");
            }
        }
    }
}
