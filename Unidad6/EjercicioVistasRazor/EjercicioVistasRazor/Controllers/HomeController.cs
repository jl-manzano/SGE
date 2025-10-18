using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // Acción para la vista principal
        public IActionResult Index()
        {
            // pasar la hora actual al ViewData
            ViewData["HoraActual"] = DateTime.Now.ToString("HH:mm");

            // comprobar si ya pasaron las 12 del mediodía
            ViewData["EsMediodia"] = DateTime.Now.Hour >= 12 ? "Sí, ya pasaron las 12" : "No, aún no son las 12";

            return View();
        }
    }
}
