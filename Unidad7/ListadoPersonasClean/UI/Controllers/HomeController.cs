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

        public IActionResult Details(int id)
        {
            return View(_useCaseListadoPersonas.obtenerPersonaId(id));
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            _useCaseListadoPersonas.insertarPersona(persona);
            return RedirectToAction(nameof(Index));
        }

        // GET: Home/Edit/5
        public IActionResult Edit(int id)
        {
            var persona = _useCaseListadoPersonas.obtenerPersonaId(id);
            if (persona == null)
                return RedirectToAction(nameof(Index));

            return View(persona);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Persona persona)
        {
            if (id != persona.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            _useCaseListadoPersonas.actualizarPersona(persona);
            return RedirectToAction(nameof(Index));
        }

        // GET: Muestra la página de confirmación para eliminar
        public IActionResult Delete(int id)
        {
            var persona = _useCaseListadoPersonas.obtenerPersonaId(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Elimina el registro
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var persona = _useCaseListadoPersonas.obtenerPersonaId(id);
            if (persona == null)
            {
                return NotFound();
            }

            _useCaseListadoPersonas.eliminarPersona(id);
            return RedirectToAction(nameof(Index));
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
