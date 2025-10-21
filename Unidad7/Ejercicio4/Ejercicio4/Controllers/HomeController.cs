using System.Diagnostics;
using Ejercicio4.Models;
using Microsoft.AspNetCore.Mvc;
using Ejercicio4.Models.DAL;

namespace Ejercicio4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // acción que renderiza la vista principal
        public IActionResult Index()
        {
            return View();
        }

        // lista de departamentos
        private static List<clsDepartamento> departamentos = new List<clsDepartamento>
        {
            new clsDepartamento(1, "Recursos Humanos"),
            new clsDepartamento(2, "Tecnología"),
            new clsDepartamento(3, "Marketing")
        };

        // lista de personas
        private static List<clsPersona> personas = new List<clsPersona>
        {
            new clsPersona(1, "José", "Domínguez", 30, departamentos[0]),
            new clsPersona(2, "Ana", "Gómez", 25, departamentos[1]),
            new clsPersona(3, "Carlos", "Pérez", 35, departamentos[2]),
            new clsPersona(4, "María", "López", 28, departamentos[1]),
            new clsPersona(5, "Pedro", "Martínez", 40, departamentos[0])
        };

        // acción para editar los datos de una persona
        public IActionResult EditarPersona()
        {
            // seleccionar aleatoriamente una persona de la lista
            Random random = new Random();
            var persona = personas[random.Next(personas.Count)]; // selecciona una persona al azar

            // pasar los departamentos a la vista
            ViewBag.Departamentos = departamentos;

            // pasar la persona seleccionada a la vista
            return View(persona);
        }

        // acción para guardar los datos modificados de una persona
        [HttpPost]
        public IActionResult GuardarPersona(clsPersona persona)
        {
            // verificar si el modelo es válido (validación del formulario)
            if (ModelState.IsValid)
            {
                // buscar la persona original por ID
                var personaExistente = personas.FirstOrDefault(p => p.Id == persona.Id);

                // si la persona existe, actualizar sus datos
                if (personaExistente != null)
                {
                    personaExistente.Nombre = persona.Nombre;
                    personaExistente.Apellidos = persona.Apellidos;
                    personaExistente.Edad = persona.Edad;
                    personaExistente.Departamento = persona.Departamento;
                }

                // redirigir al usuario a la página principal después de guardar
                return RedirectToAction("Index");
            }

            // si los datos no son válidos, devolver la vista de edición con errores
            ViewBag.Departamentos = departamentos; // pasar los departamentos nuevamente a la vista
            return View("EditarPersona", persona); // volver a mostrar el formulario de edición
        }

        // acción de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // acción para manejar los errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
