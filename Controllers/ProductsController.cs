using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstApi.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class ProductsController : ControllerBase

    {

        private static readonly List<Product> _product = new()
        {
            new Product {Id = 1, Name = "Apple"},
            new Product {Id = 2, Name = "Banana"},
            new Product {Id = 3, Name = "Orange"}
        };

        [HttpGet]

        public ActionResult<List<Product>> Get()

        {

            return _product;

        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be a positive integer.");

            var product = _product.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"No product found with Id = {id}.");

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product request)
        {
            try
            {
                // Safer ID calculation (handles empty list)
                var nextId = _product.Any()
                    ? _product.Max(p => p.Id) + 1
                    : 1;

                request.Id = nextId;
                _product.Add(request);

                return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
            }
            catch
            {
                // In a real app you'd log the exception
                return StatusCode(500, "An error occurred while creating the product.");
            }

        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product request)
        {
            if (id <= 0)
                return BadRequest("Id must be a positive integer.");

            // Validation for 'request' is still automatic due to [ApiController] + data annotations
            try
            {
                var product = _product.FirstOrDefault(p => p.Id == id);
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

        public ActionResult<Product> Delete(int id)

        {
            if (id <= 0)
                return BadRequest("Id must be a positive integer.");

            var product = _product.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"No product found with Id = {id}.");

            _product.Remove(product);
            return StatusCode(200, $"Successfully deleted product with Id = {id}.");
        }
    }


}