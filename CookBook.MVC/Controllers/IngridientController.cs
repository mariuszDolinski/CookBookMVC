﻿using CookBook.Application.IngridientUtils.Commands.CreateIngridient;
using CookBook.Application.IngridientUtils.Queries.GetAllIngridients;
using CookBook.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    public class IngridientController : Controller
    {
        private readonly IMediator _mediator;

        public IngridientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var ingridients = await _mediator.Send(new GetAllIngridientsQuery());
            return View(ingridients);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngridientCommand command)
        {
            if(User.Identity == null || !User.Identity.IsAuthenticated)
            {
                this.SetNotification("warning", "Zaloguj się aby dodać nowe składniki.");
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.Where(e => e.Errors.Count() > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                var ingridients = await _mediator.Send(new GetAllIngridientsQuery());
                return View("Index", ingridients);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}
