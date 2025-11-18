using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult ListadoProductos()
        {
            return View();
        }
    }
}
