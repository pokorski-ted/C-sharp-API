using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Data;
using MyFirstApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyFirstApi.Controllers
{
    public class ProductsCrudController : Controller
    {

        private readonly AppDbContext _db;

        public ProductsCrudController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /ProductsCrud
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.AsNoTracking().ToListAsync();
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
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /ProductsCrud/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.FindAsync(id);
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

            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updated.Name;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /ProductsCrud/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /ProductsCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
