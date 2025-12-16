using Microsoft.AspNetCore.Mvc;
using CRUD_API.Data;
using CRUD_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using CRUD_API.Services;

namespace CRUD_API.Controllers

{

    [ApiController]

    //Ignore the controller name.
    //[Route("api/[controller]")]
    //ALWAYS use /api/products as the route
    [Route("api/products")]

    public class ProductsApiController : ControllerBase

    {

        private readonly IProductService _products;

        public ProductsApiController(IProductService products)
        {
            _products = products;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var items = await _products.GetAllAsync();
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _products.GetByIdAsync(id);
            if (product == null)
                return NotFound($"No product found with Id = {id}.");

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _products.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _products.UpdateAsync(id, request);
            if (updated == null)
                return NotFound($"No product found with Id = {id}.");

            return updated;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _products.DeleteAsync(id);
            if (!success)
                return NotFound($"No product found with Id = {id}.");

            return Ok($"Successfully deleted product with Id = {id}.");
        }
    }


}