using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
    /// <summary>
    /// Controlador principal de la aplicación, maneja la página de inicio y la privacidad.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor que inicializa el controlador con el logger.
        /// </summary>
        /// <param name="logger">Instancia de logger para registrar eventos.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Muestra la página de inicio.
        /// </summary>
        /// <returns>Vista de la página de inicio.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Muestra la página de privacidad.
        /// </summary>
        /// <returns>Vista de la página de privacidad.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Muestra la página de error si ocurre un problema en la aplicación.
        /// </summary>
        /// <returns>Vista de la página de error.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
