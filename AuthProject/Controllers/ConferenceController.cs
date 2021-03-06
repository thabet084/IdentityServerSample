﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Data.Models;

using AuthProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IAttendeApiService _api;

        public ConferenceController(IAttendeApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Organizer - Conference Overview";
            return View(await _api.GetAllConferences());
        }

        [Authorize(Policy ="CanAddConference")]
        public IActionResult Add()
        {
            ViewBag.Title = "Organizer - Add Conference";
            return View(new ConferenceModel());
        }

        [HttpPost]
        [Authorize(Policy = "CanAddConference")]
        public async Task<IActionResult> Add(ConferenceModel model)
        {
            if (ModelState.IsValid)
                await _api.AddConference(model);

            return RedirectToAction("Index");
        }
    }
}