using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Infrastructure;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValueService _valueService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IValueService valueService, ILogger<HomeController> logger)
        {
            _valueService = valueService;
            _logger = logger;
        }

        [SimpleActionFilter]
        public async Task<IActionResult> Index()
        {

            _logger.LogInformation("Index action requested");
            _logger.LogTrace("Trace! winter is coming");
            _logger.LogInformation("Info! winter is coming");
            _logger.LogWarning("Warning! winter is coming");
            _logger.LogDebug("Debug! winter is coming");
            _logger.LogError("Error! winter is coming");
            _logger.LogCritical("Critical! winter is coming");

            var values = await _valueService.GetAsync();
            return View(values);

            //ViewData["Title"] = "Домашняя страница";
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}