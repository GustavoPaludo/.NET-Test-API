using TestAPI.Models;

namespace TestAPI.Services.ProductServices
{
    public interface ProductService
    {
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public List<Product> GetProductsByMinAndMaxPrice(decimal? minPrice, decimal? maxPrice);
        public int CreateProduct(Product product);
        public bool UpdateProduct(int id, Product updatedProduct);
    }
}
