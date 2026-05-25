using SharedLib.DTOs;
using SharedLib.Model;

namespace ProductAPI.Service
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductAsync();
    }
}
