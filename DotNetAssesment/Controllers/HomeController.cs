using System.Diagnostics;
using DotNetAssesment.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssesment.Controllers
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

    }
}