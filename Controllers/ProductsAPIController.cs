using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Data;
using MyFirstApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MyFirstApi.Controllers

{

    [ApiController]

    //Ignore the controller name.
    //[Route("api/[controller]")]
    //ALWAYS use /api/products as the route
    [Route("api/products")]

    public class ProductsApiController : ControllerBase

    {

        private readonly AppDbContext _db;

        public ProductsApiController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]

        public async Task<ActionResult<List<Product>>> Get()

        {

            var products = await _db.Products.AsNoTracking().ToListAsync();
            return products;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be a positive integer.");

            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound($"No product found with Id = {id}.");

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product request)
        {
            try
            {
                _db.Products.Add(request);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product request)
        {
            if (id <= 0)
                return BadRequest("Id must be a positive integer.");

            // Validation for 'request' is still automatic due to [ApiController] + data annotations
            try
            {
                var product = await _db.Products.FindAsync(id);
                if (product == null)
                    return NotFound($"No product found with Id = {id}.");

                product.Name = request.Name;

                return product;
            }
            catch
            {
                return StatusCode(500, $"An error occurred while updating product with Id = {id}.");
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> Delete(int id)

        {
            if (id <= 0)
                return BadRequest("Id must be a positive integer.");

            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound($"No product found with Id = {id}.");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return StatusCode(200, $"Successfully deleted product with Id = {id}.");
        }
    }


}