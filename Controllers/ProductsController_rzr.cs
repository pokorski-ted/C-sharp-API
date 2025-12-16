using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
