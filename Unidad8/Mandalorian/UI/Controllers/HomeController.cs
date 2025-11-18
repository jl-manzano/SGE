using System.Diagnostics;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using UI.Models;
using Domain.Interfaces;
using Domain.UseCases;
using Domain.DTO;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IListadoMisionesUseCase _listadoMisionesUseCase;

        public HomeController(ILogger<HomeController> logger, IListadoMisionesUseCase listadoMisionesUseCase)
        {
            _logger = logger;
            _listadoMisionesUseCase = listadoMisionesUseCase;
        }

        public IActionResult Index()
        {
                 return View(_listadoMisionesUseCase.getMisiones());
        }

        [HttpPost]
        public IActionResult MisionSeleccionada(int idMision)
        {
            return View(_listadoMisionesUseCase.getListadoConMisionSeleccionada(idMision));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}