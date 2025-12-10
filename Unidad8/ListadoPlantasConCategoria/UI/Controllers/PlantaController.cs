using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class PlantaController : Controller
    {
        private readonly IPlantaUseCases _plantaUseCases;
        private readonly ICategoriaUseCases _categoriaUseCases;

        // Constructor que inicializa los casos de uso de plantas y categorías
        public PlantaController(IPlantaUseCases plantaUseCases, ICategoriaUseCases categoriaUseCases)
        {
            _plantaUseCases = plantaUseCases;
            _categoriaUseCases = categoriaUseCases;
        }

        // Acción para mostrar las plantas de una categoría seleccionada
        public IActionResult Index(int? categoriaId)
        {
            if (!categoriaId.HasValue)
            {
                // Si no hay categoría seleccionada, mostrar todas las categorías
                List<Categoria> categorias = _categoriaUseCases.getCategorias();
                ViewBag.Categorias = categorias;
                return View();
            }

            // Si hay categoría seleccionada, mostrar las plantas de esa categoría
            ListadoPlantasWithListadoCategoriasDTO dto = _plantaUseCases.getPlantasWithListadoCategoriasDTO(categoriaId.Value);
            List<Categoria> categoriasList = _categoriaUseCases.getCategorias();
            ViewBag.Categorias = categoriasList;
            return View(dto);
        }

        // Acción para asignar un precio a una planta
        public IActionResult Edit(int id)
        {
            PlantaWithNombreCategoriaDTO planta = _plantaUseCases.getPlanta(id);
            return View(planta);  // Mostrar el formulario de edición de precio
        }

        // Acción POST para guardar el nuevo precio de la planta
        [HttpPost]
        public IActionResult Edit(int id, double nuevoPrecio)
        {
            int resultado = _plantaUseCases.asignarPrecio(id, nuevoPrecio);
            if (resultado > 0)
            {
                TempData["Mensaje"] = "Precio actualizado correctamente.";
            }
            else
            {
                TempData["Mensaje"] = "El nuevo precio debe ser mayor que el actual.";
            }

            // Redirigir a la vista de plantas de la categoría seleccionada
            PlantaWithNombreCategoriaDTO planta = _plantaUseCases.getPlanta(id);
            return RedirectToAction("Index", new { categoriaId = planta.Planta.IdCategoria });
        }
    }
}
