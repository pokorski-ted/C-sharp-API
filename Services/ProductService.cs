using Microsoft.EntityFrameworkCore;
using CRUD_API.Data;
using CRUD_API.Models;
using Microsoft.Extensions.Logging;
using CRUD_API.Services;

namespace CRUD_API.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProductService> _logger;

        public ProductService(AppDbContext db, ILogger<ProductService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync called.");
            return await _db.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("GetByIdAsync called with invalid id: {Id}", id);
                return null;
            }
                var product = await _db.Products.FindAsync(id);

            if (product == null)
                 _logger.LogWarning("GetByIdAsync: no product found for id: {Id}",id);
            else
                _logger.LogInformation("GetByIdAsync: found product for id: {Id}, name {Name}", id, product);
            
            return product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _logger.LogInformation("CreateAsync called with name: {Name}", product?.Name);

            if (product == null)
            {
                _logger.LogWarning("GetByIdAsync: no product found for product: {product}", product?.Name);
                throw new ArgumentNullException(nameof(product));
            }
            else
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                _logger.LogInformation("CreateAsync: created product id {Id}, name {Name}", product.Id, product.Name);
            }
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product updated)
        {
            _logger.LogInformation("UpdateAsync called for id {Id} with name: {Name}", id, updated?.Name);
            var existing = await _db.Products.FindAsync(id);
            if (existing == null || updated == null)
            {
                _logger.LogWarning("UpdateAsync: no product found for id: {Id}", id);
                return null;
            }

            existing.Name = updated.Name;
            await _db.SaveChangesAsync();

            _logger.LogInformation("UpdateAsync: updated product id {Id} to name {Name}", existing.Id, existing.Name);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync called for id {Id}", id);

            var existing = await _db.Products.FindAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("DeleteAsync: no product found for id: {Id}", id);
                return false;
            }    
            
            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
            _logger.LogInformation("DeleteAsync: deleted product id {Id}, name {Name}", existing.Id, existing.Name);
            return true;
        }
    }
}
