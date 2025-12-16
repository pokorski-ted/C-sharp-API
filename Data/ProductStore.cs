using System.Collections.Generic;
using CRUD_API.Models;

namespace CRUD_API.Data
{
    public static class ProductStore
    {

        // Shared in-memory list used by BOTH the API and MVC CRUD controller
        public static List<Product> Products { get; } = new()
    {
            new Product {Id = 1, Name = "Apple"},
            new Product {Id = 2, Name = "Banana"},
            new Product {Id = 3, Name = "Orange"}
        };
    };
}
