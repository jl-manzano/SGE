using Microsoft.AspNetCore.Mvc;

namespace HolaMundoASPNetMVC.Controllers
{
    public class HomeController : Controller
    {
        // Acci�n para la p�gina de inicio
        public IActionResult Index()
        {
            return View(); // Devuelve la vista Index.cshtml
        }
    }
}
