using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace MyFirstApi.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class ProductsController : ControllerBase

    {

        private static readonly List<string> _products = new()
        {
            "Apple",
            "Banana",
            "Orange"
        };


        [HttpGet]

        public ActionResult<List<string>> Get()

        {

            return _products;

        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] string newProduct)

        {

            _products.Add(newProduct);
            return $"Added: {newProduct} at index {_products.Count - 1}";

        }

        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string updatedProduct)
        {
            if (id < 0 || id >= _products.Count)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _products[id] = updatedProduct;
            return $"Updated product with ID {id} to: {updatedProduct}";
        }

        [HttpDelete("{id}")]

        public ActionResult<string> Delete(int id)

        {
            _products.RemoveAt(id);
            return $"Deleted product with ID: {id}";

        }
    }


}