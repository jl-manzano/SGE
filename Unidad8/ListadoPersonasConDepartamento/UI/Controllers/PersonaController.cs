using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.DTOs;

namespace UI.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaUseCases _personaUseCases;

        public PersonaController(IPersonaUseCases personaUseCases)
        {
            _personaUseCases = personaUseCases;
        }

        // Index - Muestra la lista de personas con los departamentos
        public IActionResult Index()
        {
            var personas = _personaUseCases.getPersonas(); // Recupera la lista de personas con sus departamentos
            return View(personas); // Pasa el modelo a la vista
        }

        // Details - Muestra los detalles de una persona específica
        public IActionResult Details(int id)
        {
            var personaDTO = _personaUseCases.getPersona(id); // Obtiene la persona con el nombre del departamento
            return View(personaDTO); // Pasa el DTO (PersonaWithNombreDepartamentoDTO) a la vista
        }

        public ActionResult Create()
        {
            return View(_personaUseCases.getDepartamentos());
        }

        [HttpPost]
        public ActionResult Create(Persona persona)
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

        // Edit (GET) - Muestra el formulario para editar una persona
        public IActionResult Edit(int id)
        {
            var personaDTO = _personaUseCases.getPersona(id); // Obtiene la persona con el nombre del departamento
            ViewBag.Departamentos = _personaUseCases.getDepartamentos(); // Carga los departamentos
            return View(personaDTO.persona); // Pasa solo la persona al formulario de edición
        }

        // Edit (POST) - Actualiza los datos de una persona
        [HttpPost]
        public IActionResult Edit(int id, Persona persona)
        {
            if (ModelState.IsValid)
            {
                _personaUseCases.updatePersona(id, persona); // Llama al caso de uso para actualizar la persona
                return RedirectToAction("Index"); // Redirige a la lista de personas
            }
            ViewBag.Departamentos = _personaUseCases.getDepartamentos(); // Recarga los departamentos si el modelo no es válido
            return View(persona); // Muestra la vista con el modelo de persona
        }

        // Delete (GET) - Muestra el formulario para confirmar la eliminación de una persona
        public IActionResult Delete(int id)
        {
            var personaDTO = _personaUseCases.getPersona(id); // Obtiene la persona con el nombre del departamento
            return View(personaDTO); // Pasa el DTO (PersonaWithNombreDepartamentoDTO) a la vista
        }

        // Delete (POST) - Elimina una persona
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _personaUseCases.deletePersona(id); // Llama al caso de uso para eliminar la persona
            return RedirectToAction("Index"); // Redirige a la lista de personas
        }
    }
}
