using EjerciciosUnidad7.Models.Entities.DAL;
using EjerciciosUnidad7.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EjerciciosUnidad7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // accedemos a las listas estáticas de departamentos y personas a través de las clases ListadoDepartamentos y ListadoPersonas
        private List<Departamento> departamentos = new ListadoDepartamentos().Lista;
        private List<Persona> personas = new ListadoPersonas().Lista;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // acción que renderiza la vista principal
        public IActionResult Index()
        {
            return View();
        }

        // acción para editar los datos de una persona (selecciona aleatoriamente)
        public IActionResult EditarPersona()
        {
            Random random = new Random();
            var persona = personas[random.Next(personas.Count)]; // selecciona una persona al azar

            // pasar los departamentos a la vista
            ViewBag.Departamentos = departamentos;

            // pasar la persona seleccionada a la vista
            return View(persona);
        }

        // acción para guardar los datos modificados de una persona
        [HttpPost]
        public IActionResult GuardarPersona(Persona persona)
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
                    personaExistente.IdDepartamento = persona.IdDepartamento;
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
        [ResponseCache(Duration = 0] ()
