using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace VegasVacationVip.Controllers
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
        public IActionResult Package()
        {
            return View();
        }
        public IActionResult Hotels()
        {
            return View();
        }

        public IActionResult Gifts() {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}