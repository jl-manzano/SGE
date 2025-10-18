using Microsoft.AspNetCore.Mvc;

namespace HolaMundoASPNetMVC.Controllers
{
    public class HomeController : Controller
    {
        // Acción para la página de inicio
        public IActionResult Index()
        {
            return View(); // Devuelve la vista Index.cshtml
        }
    }
}
