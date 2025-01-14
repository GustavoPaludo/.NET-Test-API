using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Handlers
{
    public class ProductQueryHandler
    {
        private readonly ProductDbContext _dbContext;

        public ProductQueryHandler(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.AsNoTracking().ToList();
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductsByPriceRange(decimal? minPrice, decimal? maxPrice)
        {
            return _dbContext.Products.AsNoTracking()
                .Where(p => (!minPrice.HasValue || p.Price >= minPrice) &&
                            (!maxPrice.HasValue || p.Price <= maxPrice))
                .ToList();
        }

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}