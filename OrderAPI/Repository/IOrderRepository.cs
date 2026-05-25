using SharedLib.DTOs;
using SharedLib.Model;

namespace OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<ServiceResponse> AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<OrderSummary> GetOrderSummaryAsync();
    }
}
