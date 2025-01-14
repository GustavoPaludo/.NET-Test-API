using TestAPI.Models;
using TestAPI.Handlers;

namespace TestAPI.Services.ProductServices
{
    public class ProductServiceImpl : ProductService
    {
        private readonly ProductQueryHandler _queryHandler;

        public ProductServiceImpl(ProductQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public Product GetProductById(int id)
        {
            var product = _queryHandler.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            return product;
        }

        public List<Product> GetProducts()
        {
            return _queryHandler.GetAllProducts();
        }

        public List<Product> GetProductsByMinAndMaxPrice(decimal? minPrice, decimal? maxPrice)
        {
            return _queryHandler.GetProductsByPriceRange(minPrice, maxPrice);
        }

        public int CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            _queryHandler.AddProduct(product);
            return product.Id;
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = _queryHandler.GetProductById(id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            _queryHandler.UpdateProduct(existingProduct);
            return true;
        }
    }
}
