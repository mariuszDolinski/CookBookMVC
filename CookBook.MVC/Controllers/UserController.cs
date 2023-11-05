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
using CookBook.Application.ApplicationUser;

namespace CookBook.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public UserController(IMediator mediator, IMapper mapper, IUserContext userContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userContext = userContext;
        }

        /// <summary>
        /// return view with user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userContext.GetCurrentUserDetails();
            if (user == null)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            return View(user!);
        }

        /// <summary>
        /// return view with all user's recipes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AllUserRecipes()
        {
            var userRecipes = await _mediator.Send(new GetAllUserRecipesQuery());
            return View(userRecipes);
        }
        /// <summary>
        /// Return all users in role specified as roleName (if given) and name contains search phrase userName (if given)
        /// </summary>
        /// <param name="roleName">if given, returns all users in that role</param>
        /// <param name="userName">if given, returns all users which name contains this string</param>
        /// <returns></returns>
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
