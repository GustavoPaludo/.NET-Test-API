using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products = new List<Product> {
            new Product { Id = 1, Name = "Product 1", Price = 9.99M },
            new Product { Id = 2, Name = "Product 2", Price = 19.99M },
        };

        //Simple get example
        [HttpGet("get-product")]
        public IEnumerable<Product> GetProducts()
        {
            return Products;
        }

        //Example with route parameter
        [HttpGet("get-product/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //Example with queryparameters
        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> GetProductsByPrice(decimal? minPrice, decimal? maxPrice)
        {
            var result = Products.Where(p => (!minPrice.HasValue || p.Price >= minPrice) && (!maxPrice.HasValue || p.Price <= maxPrice));
            return result.ToList();
        }

        //Example post with payload
        [HttpPost("create-product")]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }

            product.Id = Products.Max(p => p.Id) + 1;
            Products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        //Example put request with queryparameter and payload
        [HttpPut("update-product")]
        public ActionResult<Product> UpdateProduct(int? id, [FromBody] Product updatedProduct)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return NoContent();
        }
    }
}