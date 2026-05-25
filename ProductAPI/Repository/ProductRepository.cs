using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using SharedLib.DTOs;
using SharedLib.Model;

namespace ProductAPI.Repository
{
    public class ProductRepository(ProductDbContext _context) : IProductRepository
    {
        public async Task<ServiceResponse> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ServiceResponse(true,"Product added successfully.");
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            var products= await _context.Products.ToListAsync();
            return products;
        }
    }
}
