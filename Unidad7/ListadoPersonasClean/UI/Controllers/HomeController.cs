using Domain.Entities;
using Domain.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IListadoPersonasUseCases _useCaseListadoPersonas;

        public HomeController(ILogger<HomeController> logger, IListadoPersonasUseCases useCaseListadoPersonas)
        {
            _logger = logger;
            _useCaseListadoPersonas = useCaseListadoPersonas;
        }

        public IActionResult Index()
        {
            // Llamamos al caso de uso para obtener las personas
            var personas = _useCaseListadoPersonas.obtenerListadoPersonas();
            return View(personas);
        }

        [HttpPost]
        public IActionResult Create(Persona persona)
        {
            if (!ModelState.IsValid)
                return View(persona); // Si el modelo no es válido, retornamos la vista con el modelo

            _useCaseListadoPersonas.AgregarPersona(persona);  // Método en el repositorio para agregar la persona
            return RedirectToAction(nameof(Index));  // Redirige a la lista de personas después de la creación
        }

        [HttpPost]
        public IActionResult Edit(int id, Persona persona)
        {
            if (id != persona.Id)
                return RedirectToAction(nameof(Index));  // Si el id no coincide, redirige a la lista de personas

            if (ModelState.IsValid)
                _useCaseListadoPersonas.ActualizarPersona(persona);  // Actualiza la persona en el repositorio

            return RedirectToAction(nameof(Details), new { id = persona.Id });  // Redirige a la vista de detalles
        }

        public IActionResult Details(int id)
        {
            return View(_useCaseListadoPersonas.obtenerPersonaId(id));
        }

        public IActionResult Delete(int id)
        {
            var persona = _useCaseListadoPersonas.obtenerPersonaId(id);
            if (persona == null)
                return RedirectToAction(nameof(Index));  // Si no se encuentra, redirige a la lista de personas

            return View(persona);  // Muestra la confirmación de eliminación
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
