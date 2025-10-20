using System.Diagnostics;
using Ejercicio3.Models;
using Microsoft.AspNetCore.Mvc;
using Ejercicio3.Models.DAL;

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
            return View();
        }

        public IActionResult PersonaPosTres()
        {
            // crear una lista de personas
            var personas = new List<clsPersona>
            {
                new clsPersona(1, "José", "Domínguez", 30),
                new clsPersona(2, "Ana", "Gómez", 25),
                new clsPersona(3, "Carlos", "Pérez", 35),
                new clsPersona(4, "María", "López", 28),
                new clsPersona(5, "Pedro", "Martínez", 40)
            };

            // obtener la persona en la posición 3 (índice 2)
            var persona = personas.ElementAtOrDefault(2);

            // verificar si la persona fue encontrada y pasarla a ViewBag
            if (persona != null)
            {
                ViewBag.PersonaSeleccionada = persona;
            }
            else
            {
                ViewBag.PersonaSeleccionada = "No se encontró la persona en la posición indicada.";
            }

            // pasar la lista de personas a la vista
            return View(persona);
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
