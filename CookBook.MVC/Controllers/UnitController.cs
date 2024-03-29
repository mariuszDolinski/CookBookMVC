﻿using CookBook.Application.UnitUtils.Commands.CreateUnit;
using CookBook.Application.UnitUtils.Commands.DeleteUnits;
using CookBook.Application.UnitUtils.Commands.EditUnit;
using CookBook.Application.UnitUtils.Queries.GetAllUnits;
using CookBook.MVC.Extensions;
using CookBook.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.MVC.Controllers
{
    public class UnitController : Controller
    {
        private readonly IMediator _mediator;

        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search = "", string sortOrder = "", int page = 1, int pageSize = 5)
        {
            this.SetViewBagParams(search, sortOrder, pageSize);
            this.SetViewBagSortIcons(sortOrder);
            ViewBag.Code = ItemCodes.UNIT;

            var paginatedUnits = await _mediator.Send(new GetAllUnitsQuery(search, sortOrder, page, pageSize));
            var pages = new Pagination(paginatedUnits.TotalItems, page, pageSize);
            pages.SortOrder = sortOrder;
            pages.SearchPhrase = search;
            ViewBag.Pages = pages;

            var query = new ParamsQuery(sortOrder, page, pageSize);
            this.SetTempData(query);

            return View(@"ItemListViews/ListIndex", paginatedUnits.Items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUnitCommand command)
        {
            var query = this.GetTempData();

            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                this.SetNotification("warning", "Zaloguj się aby dodać nowe jednostki.");
                return this.CallRedirectToAction(query, nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                this.SetNotification("warning", errors[0]);
                return this.CallRedirectToAction(query, nameof(Index));
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Jednostka '{command.Name}' została dodana");
            return this.CallRedirectToAction(query, nameof(Index));
        }

        [HttpPut]
        [Route("unit/edit/{names}")]
        public async Task<IActionResult> Edit(string names)
        {
            return await this.EditItem(_mediator, new EditUnitCommand(), names);
        }

        [HttpDelete]
        [Route("unit/delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            return await this.DeleteItem(_mediator, new DeleteUnitCommand(), name);
        }
    }
}
