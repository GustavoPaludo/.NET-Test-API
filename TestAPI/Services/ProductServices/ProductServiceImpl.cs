using System.Reflection.Metadata.Ecma335;
using TestAPI.Models;

namespace TestAPI.Services.ProductServices
{
    public class ProductServiceImpl : ProductService
    {
        public Product GetProductById(int id)
        {
            List<Product> productList = new List<Product>();
            productList = this.GetProducts();

            if (productList == null || !productList.Any())
            {
                return null;
            }

            return productList.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product { Id = 1, Name = "Product 1", Price = 9.99M });
            Products.Add(new Product { Id = 2, Name = "Product 2", Price = 19.99M });

            return Products;
        }

        public List<Product> GetProductsByMinAndMaxPrice(decimal? minPrice, decimal? maxPrice)
        {
            List<Product> producList = this.GetProducts();

            return producList.Where(p => (!minPrice.HasValue || p.Price >= minPrice) && (!maxPrice.HasValue || p.Price <= maxPrice)).ToList();
        }

        public int CreateProduct(Product product)
        {
            var products = GetProducts();

            int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            product.Id = newId;

            products.Add(product);

            return newId;
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        {
            List<Product> productList = new List<Product>();
            productList = this.GetProducts();

            var existingProduct = productList.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return true;
        }
    }
}
