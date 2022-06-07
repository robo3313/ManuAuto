using LaManuAuto.Data;
using LaManuAuto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaManuAuto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LaManuAutoContext _dbConnect;

        public HomeController(ILogger<HomeController> logger, LaManuAutoContext connexionDb)
        {
            _dbConnect = connexionDb;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var Tutorials = _dbConnect.Tutorials;
            return View(Tutorials);
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