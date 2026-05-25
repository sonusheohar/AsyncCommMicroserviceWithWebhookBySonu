using OrderAPI.Repository;
using SharedLib.DTOs;
using SharedLib.Model;

namespace OrderAPI.Service
{
    public class OrderService(IOrderRepository _orderRepository) : IOrderService
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
           return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllOrderAsync();
        }
    }
}
