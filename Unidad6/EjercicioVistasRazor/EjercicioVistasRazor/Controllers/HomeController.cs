using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // Acci�n para la vista principal
        public IActionResult Index()
        {
            // pasar la hora actual al ViewData
            ViewData["HoraActual"] = DateTime.Now.ToString("HH:mm");

            // comprobar si ya pasaron las 12 del mediod�a
            ViewData["EsMediodia"] = DateTime.Now.Hour >= 12 ? "S�, ya pasaron las 12" : "No, a�n no son las 12";

            return View();
        }
    }
}
