using Microsoft.EntityFrameworkCore;
using SharedLib.Model;

namespace ProductAPI.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options) { }
        
        public DbSet<Product> Products { get; set; }
    }
}
