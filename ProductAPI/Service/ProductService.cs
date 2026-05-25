using Microsoft.EntityFrameworkCore;
using ProductAPI.Repository;
using SharedLib.DTOs;
using SharedLib.Model;

namespace ProductAPI.Service
{
    public class ProductService(IProductRepository _repository): IProductService
    {
        public async Task<ServiceResponse> AddProductAsync(Product product)
        {
           
            return await _repository.AddProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {

            return await _repository.GetAllProductAsync(); ;
        }
    }
}
