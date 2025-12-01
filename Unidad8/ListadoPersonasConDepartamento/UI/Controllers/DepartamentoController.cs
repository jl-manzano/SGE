using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoUseCases _departamentoUseCases;

        public DepartamentoController(IDepartamentoUseCases departamentoUseCases)
        {
           _departamentoUseCases = departamentoUseCases;
        }

        public IActionResult Index()
        {
            return View(_departamentoUseCases.getDepartamentos());
        }

        public IActionResult Details(int id)
        {
            return View(_departamentoUseCases.getDepartamento(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Domain.Entities.Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _departamentoUseCases.addDepartamento(departamento);
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        public IActionResult Edit(int id)
        {
            return View(_departamentoUseCases.getDepartamento(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Domain.Entities.Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _departamentoUseCases.updateDepartamento(id, departamento);
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        public IActionResult Delete(int id)
        {
            return View(_departamentoUseCases.getDepartamento(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _departamentoUseCases.deleteDepartamento(id);
            return RedirectToAction("Index");
        }

    }
}
