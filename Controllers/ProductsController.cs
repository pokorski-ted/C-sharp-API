using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;

using System.Collections.Generic;

namespace MyFirstApi.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class ProductsController : ControllerBase

    {

        private static readonly List<Product> _products = new()
        {
            new Product {Id = 1, Name = "Apple"},
            new Product {Id = 2, Name = "Banana"},
            new Product {Id = 3, Name = "Orange"}
        };

        [HttpGet]

        public ActionResult<List<Product>> Get()

        {

            return _products;

        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product request)
        {
            request.Id = _products.Max(p => p.Id) + 1;  // auto ID
            _products.Add(request);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);

        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product request)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            product.Name = request.Name;

            return product;
        }

        [HttpDelete("{id}")]

        public ActionResult<Product> Delete(int id)

        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            _products.Remove(product);

            return NoContent();

        }
    }


}