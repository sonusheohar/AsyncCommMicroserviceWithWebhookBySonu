using SharedLib.DTOs;
using SharedLib.Model;

namespace OrderAPI.Service
{
    public interface IOrderService
    {
        Task<ServiceResponse> AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrderAsync();
    }
}
