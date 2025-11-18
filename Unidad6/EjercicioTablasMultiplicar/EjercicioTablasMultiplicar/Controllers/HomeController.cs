using Microsoft.AspNetCore.Mvc;

namespace EjercicioTablasMultiplicar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
