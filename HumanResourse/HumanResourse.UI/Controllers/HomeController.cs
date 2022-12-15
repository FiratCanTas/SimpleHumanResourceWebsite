using HumanResourse.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HumanResourse.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //OLSNNNNN ATTIKKK

            //bak bakalım
           //sonn
           //fct
           //fct deneme2
           //Deneme Efe
           //Deneme tuba
        }

        public IActionResult Index()
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