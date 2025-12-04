using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

namespace UI.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas con los departamentos.
    /// </summary>
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoUseCases _departamentoUseCases;

        /// <summary>
        /// Constructor que inicializa el controlador con los casos de uso de los departamentos.
        /// </summary>
        /// <param name="departamentoUseCases">Casos de uso para los departamentos.</param>
        public DepartamentoController(IDepartamentoUseCases departamentoUseCases)
        {
            _departamentoUseCases = departamentoUseCases;
        }

        /// <summary>
        /// Muestra la lista de todos los departamentos.
        /// </summary>
        /// <returns>Vista con la lista de departamentos.</returns>
        public IActionResult Index()
        {
            try
            {
                return View(_departamentoUseCases.getDepartamentos());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los departamentos: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra los detalles de un departamento específico.
        /// </summary>
        /// <param name="id">ID del departamento.</param>
        /// <returns>Vista con los detalles del departamento.</returns>
        public IActionResult Details(int id)
        {
            try
            {
                return View(_departamentoUseCases.getDepartamento(id));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los detalles del departamento: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra el formulario para crear un nuevo departamento.
        /// </summary>
        /// <returns>Vista del formulario de creación.</returns>
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar el formulario de creación del departamento: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Crea un nuevo departamento con los datos del formulario.
        /// </summary>
        /// <param name="departamento">Departamento a crear.</param>
        /// <returns>Redirige a la vista de índice o muestra el formulario de creación si el modelo no es válido.</returns>
        [HttpPost]
        public IActionResult Create(Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departamentoUseCases.addDepartamento(departamento);
                    return RedirectToAction("Index");
                }
                return View(departamento);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al crear el departamento: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra el formulario para editar un departamento específico.
        /// </summary>
        /// <param name="id">ID del departamento a editar.</param>
        /// <returns>Vista con el formulario de edición del departamento.</returns>
        public IActionResult Edit(int id)
        {
            try
            {
                return View(_departamentoUseCases.getDepartamento(id));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar el departamento para editar: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Actualiza un departamento con los datos del formulario.
        /// </summary>
        /// <param name="id">ID del departamento a actualizar.</param>
        /// <param name="departamento">Nuevo departamento con los datos actualizados.</param>
        /// <returns>Redirige a la vista de índice o muestra el formulario de edición si el modelo no es válido.</returns>
        [HttpPost]
        public IActionResult Edit(int id, Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departamentoUseCases.updateDepartamento(id, departamento);
                    return RedirectToAction("Index");
                }
                return View(departamento);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al editar el departamento: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Muestra el formulario de confirmación para eliminar un departamento.
        /// </summary>
        /// <param name="id">ID del departamento a eliminar.</param>
        /// <returns>Vista con el formulario de confirmación de eliminación.</returns>
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_departamentoUseCases.getDepartamento(id));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar el departamento para eliminar: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }

        /// <summary>
        /// Elimina un departamento confirmado.
        /// </summary>
        /// <param name="id">ID del departamento a eliminar.</param>
        /// <returns>Redirige a la vista de índice de departamentos.</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _departamentoUseCases.deleteDepartamento(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al eliminar el departamento: " + ex.Message;
                return View("~/Views/Home/Error.cshtml");
            }
        }
    }
}
