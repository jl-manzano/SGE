using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Entities;
using Domain.DTOs;

public class HomeController : Controller
{
    private readonly ICategoriaUseCases _categoriaUseCases;
    private readonly IPlantaUseCases _plantaUseCases;

    // Constructor para inicializar los casos de uso
    public HomeController(ICategoriaUseCases categoriaUseCases, IPlantaUseCases plantaUseCases)
    {
        _categoriaUseCases = categoriaUseCases;
        _plantaUseCases = plantaUseCases;
    }

    // Acción para manejar la solicitud de la página de inicio
    public IActionResult Index(int? categoriaId)
    {
        // Obtener todas las categorías y asignarlas a ViewBag.Categorias
        List<Categoria> categorias = _categoriaUseCases.getCategorias();
        ViewBag.Categorias = categorias;  // Asegúrate de que ViewBag.Categorias es una lista de Categorías

        if (!categoriaId.HasValue)
        {
            return View();  // Si no se seleccionó categoría, simplemente mostrar la vista sin plantas
        }

        // Obtener las plantas correspondientes a la categoría seleccionada
        ListadoPlantasWithListadoCategoriasDTO dto = _plantaUseCases.getPlantasWithListadoCategoriasDTO(categoriaId.Value);

        return View(dto);  // Pasar el DTO a la vista
    }
}
