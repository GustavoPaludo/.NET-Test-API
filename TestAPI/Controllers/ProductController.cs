using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Services.ProductServices;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        //Simple get example
        [HttpGet("get-product")]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        //Example with route parameter
        [HttpGet("get-product/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productService.GetProductById(id);

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
            var result = _productService.GetProductsByMinAndMaxPrice(minPrice, maxPrice);
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

            int newProductId = _productService.CreateProduct(product);

            return CreatedAtAction(nameof(GetProduct), new { id = newProductId }, product);
        }


        //Example put request with queryparameter and payload
        [HttpPut("update-product")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Invalid product data.");
            }

            var updated = _productService.UpdateProduct(id, updatedProduct);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}