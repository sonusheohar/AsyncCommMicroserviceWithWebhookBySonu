using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using SharedLib.DTOs;
using SharedLib.Model;

namespace OrderAPI.Repository
{
    public class OrderRepository(OrderDbContext _context) : IOrderRepository
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return new ServiceResponse(true,"Order placed successfully.");
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
           return await _context.Orders.ToListAsync();
        }
    }
}
