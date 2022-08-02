using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(CarDto carParams) =>
            View();

        public async Task<IActionResult> Privacy(CarDto carParams) =>
            View();
    }
}