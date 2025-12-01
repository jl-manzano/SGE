using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaUseCases _personaUseCases;

        public PersonaController(IPersonaUseCases personaUseCases)
        {
           _personaUseCases = personaUseCases;
        }

        public IActionResult Index()
        {
            return View(_personaUseCases.getPersonas());
        }

        public IActionResult Details(int id)
        {
            return View(_personaUseCases.getPersona(id));
        }

        public IActionResult Create()
        {
            ViewBag.Departamentos = _personaUseCases.getDepartamentos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Domain.Entities.Persona persona)
        {
            if (ModelState.IsValid)
            {
                _personaUseCases.addPersona(persona);
                return RedirectToAction("Index");
            }
            ViewBag.Departamentos = _personaUseCases.getDepartamentos();
            return View(persona);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Departamentos = _personaUseCases.getDepartamentos();
            return View(_personaUseCases.getPersona(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Domain.Entities.Persona persona)
        {
            if (ModelState.IsValid)
            {
                _personaUseCases.updatePersona(id, persona);
                return RedirectToAction("Index");
            }
            ViewBag.Departamentos = _personaUseCases.getDepartamentos();
            return View(persona);
        }

        public IActionResult Delete(int id)
        {
            return View(_personaUseCases.getPersona(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _personaUseCases.deletePersona(id);
            return RedirectToAction("Index");
        }

    }
}
