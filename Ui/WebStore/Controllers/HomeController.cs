using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValueService _valueService;

        public HomeController(IValueService valueService)
        {
            _valueService = valueService;
        }
        [SimpleActionFilter]
        public async Task<IActionResult> Index()
        {
            var values = await _valueService.GetAsync();
            return View(values);
            //ViewData["Title"] = "Домашняя страница";
            //return View();
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