using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaUseCases _categoriaUseCases;

        // Constructor que inicializa el caso de uso de categorías
        public CategoriaController(ICategoriaUseCases categoriaUseCases)
        {
            _categoriaUseCases = categoriaUseCases;
        }

        // Acción para mostrar todas las categorías
        public IActionResult Index()
        {
            // Obtener todas las categorías mediante el caso de uso
            List<Categoria> categorias = _categoriaUseCases.getCategorias();
            return View(categorias);  // Pasar las categorías a la vista
        }

        // Acción para mostrar los detalles de una categoría específica
        public IActionResult Details(int id)
        {
            // Obtener la categoría específica
            Categoria categoria = _categoriaUseCases.getCategoria(id);
            return View(categoria);  // Pasar la categoría a la vista
        }
    }
}
