using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.DTOs;

namespace UI.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas con las personas.
    /// </summary>
    public class PersonaController : Controller
    {
        private readonly IPersonaUseCases _personaUseCases;

        /// <summary>
        /// Constructor que inicializa el controlador con los casos de uso de las personas.
        /// </summary>
        /// <param name="personaUseCases">Casos de uso para las personas.</param>
        public PersonaController(IPersonaUseCases personaUseCases)
        {
            _personaUseCases = personaUseCases;
        }

        /// <summary>
        /// Muestra la lista de todas las personas con sus departamentos.
        /// </summary>
        /// <returns>Vista con la lista de personas.</returns>
        public IActionResult Index()
        {
            try
            {
                return View(_personaUseCases.getPersonas());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar las personas: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra los detalles de una persona específica.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <returns>Vista con los detalles de la persona.</returns>
        public IActionResult Details(int id)
        {
            try
            {
                return View(_personaUseCases.getPersona(id));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los detalles de la persona: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra el formulario para crear una nueva persona.
        /// </summary>
        /// <returns>Vista con el formulario de creación.</returns>
        public ActionResult Create()
        {
            try
            {
                return View(_personaUseCases.getDepartamentos());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los departamentos: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Crea una nueva persona con los datos del formulario.
        /// </summary>
        /// <param name="persona">Persona a crear.</param>
        /// <returns>Redirige a la vista de índice o muestra el formulario si el modelo no es válido.</returns>
        [HttpPost]
        public ActionResult Create(Persona persona)
        {
            try
            {
                string mensaje;
                int res = _personaUseCases.addPersona(persona);
                if (res > 0)
                {
                    mensaje = "La persona se ha creado correctamente";
                }
                else
                {
                    mensaje = "La persona no se ha podido crear";
                }
                ViewBag.mensaje = mensaje;
                return View(_personaUseCases.getDepartamentos());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al crear la persona: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra el formulario para editar una persona específica.
        /// </summary>
        /// <param name="id">ID de la persona a editar.</param>
        /// <returns>Vista con el formulario de edición de persona.</returns>
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Departamentos = _personaUseCases.getDepartamentos();
                return View(_personaUseCases.getPersona(id).persona);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar la persona para editar: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Actualiza los datos de una persona con los datos del formulario.
        /// </summary>
        /// <param name="id">ID de la persona a actualizar.</param>
        /// <param name="persona">Nuevo objeto de persona con los datos actualizados.</param>
        /// <returns>Redirige a la vista de índice o muestra el formulario de edición si el modelo no es válido.</returns>
        [HttpPost]
        public IActionResult Edit(int id, Persona persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personaUseCases.updatePersona(id, persona);
                    return RedirectToAction("Index");
                }
                ViewBag.Departamentos = _personaUseCases.getDepartamentos();
                return View(persona);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al editar la persona: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra el formulario de confirmación para eliminar una persona.
        /// </summary>
        /// <param name="id">ID de la persona a eliminar.</param>
        /// <returns>Vista con el formulario de confirmación de eliminación.</returns>
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_personaUseCases.getPersona(id));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar la persona para eliminar: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Elimina una persona confirmada.
        /// </summary>
        /// <param name="id">ID de la persona a eliminar.</param>
        /// <returns>Redirige a la vista de índice de personas.</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _personaUseCases.deletePersona(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al eliminar la persona: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }
    }
}
