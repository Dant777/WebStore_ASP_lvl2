using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        [SimpleActionFilter]
        public IActionResult Index()
        {
            ViewData["Title"] = "Домашняя страница";
            return View();
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