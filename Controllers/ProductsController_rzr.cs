using Microsoft.AspNetCore.Mvc;

namespace MyFirstApi.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
