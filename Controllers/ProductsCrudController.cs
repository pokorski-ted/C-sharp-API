using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Data;
using MyFirstApi.Models;
using System.Linq;

namespace MyFirstApi.Controllers
{
    public class ProductsCrudController : Controller
    {
        // GET: /ProductsCrud
        public IActionResult Index()
        {
            var products = ProductStore.Products;
            return View(products);
        }

        // GET: /ProductsCrud/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ProductsCrud/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var nextId = ProductStore.Products.Any()
                ? ProductStore.Products.Max(p => p.Id) + 1
                : 1;

            product.Id = nextId;
            ProductStore.Products.Add(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: /ProductsCrud/Edit/5
        public IActionResult Edit(int id)
        {
            var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /ProductsCrud/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product updated)
        {
            if (!ModelState.IsValid)
            {
                return View(updated);
            }

            var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            product.Name = updated.Name;

            return RedirectToAction(nameof(Index));
        }

        // GET: /ProductsCrud/Delete/5
        public IActionResult Delete(int id)
        {
            var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /ProductsCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            ProductStore.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
