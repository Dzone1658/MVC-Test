using System.Diagnostics;
using Employee_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Employee_CRUD.Controllers
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
            return View( );
        }

        public IActionResult Privacy()
        {
            return View( );
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }

        public IActionResult Login(UserViewModel userViewModel)
        {

            return View(userViewModel);
        }

        public IActionResult Register(UserViewModel userViewModel)
        {
            return View(userViewModel);
        }
    }
}
