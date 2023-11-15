using AutoMapper;
using CookBook.Application.ApplicationUser.Queries.GetAllUserRecipes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CookBook.Application.ApplicationUser.Queries.GetAllUsers;
using CookBook.Application.ApplicationUser.Queries.GetAllRoles;
using Microsoft.IdentityModel.Tokens;
using CookBook.Application.ApplicationUser.Commands;
using CookBook.Application.ApplicationUser;
using CookBook.Application.ApplicationUser.Queries.GetUserByName;
using CookBook.Application.IngridientUtils.Queries.GetAllUserIngridients;
using CookBook.Application.UnitUtils.Queries.GetAllUserUnits;

namespace CookBook.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        //private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public UserController(IMediator mediator, IUserContext userContext)
        {
            _mediator = mediator;
            //_mapper = mapper;
            _userContext = userContext;
        }

        /// <summary>
        /// return view with user profile (current logged  in user if userName is not given)
        /// </summary>
        /// <param name="userName">user userName (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string userName)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            userName ??= string.Empty;
            var user = await _mediator.Send(new GetUserByNameQuery(userName));

            if(user.UserName == "nn")
            {
                return BadRequest("Użytkownik nie istnieje");
            }
            bool isCurrentUser = user.UserName == currentUser.UserName;
            var userRecipes = await _mediator.Send(new GetAllUserRecipesQuery(user.UserName, isCurrentUser));
            var userIngridients = await _mediator.Send(new GetAllUserIngridientsQuery(user.UserName));
            var userUnits = await _mediator.Send(new GetAllUserUnitsQuery(user.UserName));
            user.UserRecipes = userRecipes;
            user.UserIngridients = userIngridients;
            user.UserUnits = userUnits;
            ViewBag.IsProfile = "Y";

            ViewBag.IsUserCurrent = (user.UserName == currentUser.UserName) ? "Y" : "N";

            return View(user);
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

        /// <summary>
        /// Set roles for user
        /// </summary>
        /// <param name="parameters">contains userName and new roles separate by semicolon
        /// ie. userName;role1;role2... </param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("currentUser/editRoles/{parameters}")]
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
