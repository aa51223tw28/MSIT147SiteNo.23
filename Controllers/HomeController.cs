﻿using Microsoft.AspNetCore.Mvc;
using MSIT147Site.Models;
using System.Diagnostics;

namespace MSIT147Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult First()
        {
            return View();
        }

        public IActionResult TravelImage()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AjaxEvent()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}