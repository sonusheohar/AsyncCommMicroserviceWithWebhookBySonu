using SharedLib.DTOs;
using SharedLib.Model;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<ServiceResponse> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductAsync();
    }
}
