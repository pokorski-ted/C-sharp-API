using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyFirstApi.Controllers
{
    public class ProductsCrudController : Controller
    {
        private readonly IProductService _products;

        public ProductsCrudController(IProductService products)
        {
            _products = products;
        }

        // GET: /ProductsCrud
        public async Task<IActionResult> Index()
        {
            var items = await _products.GetAllAsync();
            return View(items);
        }

        // GET: /ProductsCrud/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ProductsCrud/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _products.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: /ProductsCrud/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _products.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /ProductsCrud/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var result = await _products.UpdateAsync(id, updated);
            if (result == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: /ProductsCrud/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _products.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /ProductsCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _products.DeleteAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
