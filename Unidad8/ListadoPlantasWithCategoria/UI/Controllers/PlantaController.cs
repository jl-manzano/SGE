using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class PlantaController : Controller
    {
        private readonly IPlantaUseCases _plantaUseCases;
        public PlantaController(IPlantaUseCases plantaUseCases)
        {
            _plantaUseCases = plantaUseCases;
        }

        // Acción Index (GET) - Mostrar plantas con las categorías (DTO combinado)
        public IActionResult Index()
        {
            // Pasamos el DTO combinado a la vista
            return View(_plantaUseCases.getListadoCategoriasWithListadoPlantasPorCategoria(null));
        }

        // Acción Index (POST) - Filtrado por categoría
        [HttpPost]
        public IActionResult Index(int idCategoria)
        {
            // Pasamos el DTO combinado a la vista
            return View(_plantaUseCases.getListadoCategoriasWithListadoPlantasPorCategoria(idCategoria));
        }
        public IActionResult Edit(int id)
        {
            return View(_plantaUseCases.getPlantaById(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, string nuevoPrecio)  // ← string, no double
        {
            // Reemplazar coma por punto
            nuevoPrecio = nuevoPrecio.Replace(",", ".");

            // Convertir a double usando cultura invariante
            double precio = double.Parse(nuevoPrecio, System.Globalization.CultureInfo.InvariantCulture);

            int resultado = _plantaUseCases.editarPrecio(id, precio);

            if (resultado == 0)
            {
                TempData["Error"] = "El precio debe ser mayor al precio actual.";
                return RedirectToAction("Edit", new { id = id });
            }

            TempData["Success"] = "Precio actualizado correctamente.";
            return RedirectToAction("Index");
        }
    }
}
