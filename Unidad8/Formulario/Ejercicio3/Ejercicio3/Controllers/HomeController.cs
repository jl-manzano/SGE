using System.Diagnostics;
using Ejercicio3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio3.Controllers
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
            var persona = new clsPersona(1, "Juan", "Pérez", 30);

            return View(persona);

        }

        [HttpPost]
        public IActionResult Editar(clsPersona persona)
        {
            return View("PersonaModificada", persona);
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
