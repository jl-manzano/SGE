using System.Diagnostics;
using EjerciciosUnidad7.Models;
using Microsoft.AspNetCore.Mvc;
using EjerciciosUnidad7.Models.Entities;

namespace EjerciciosUnidad7.Controllers
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
            int hora = DateTime.Now.Hour;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");

            if (hora < 12)
            {
                ViewBag.Mensaje = "Buenos días";
            }
            else if (hora < 21)
            {
                ViewBag.Mensaje = "Buenas tardes";
            }
            else
            {
                ViewBag.Mensaje = "Buenas noches";
            }

            ViewBag.Fecha = fecha;

            Persona persona = new Persona(1, "José Luis");

            ViewBag.pe

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
