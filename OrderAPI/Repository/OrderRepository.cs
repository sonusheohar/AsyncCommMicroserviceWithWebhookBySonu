using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using SharedLib.DTOs;
using SharedLib.Model;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderAPI.Repository
{
    public class OrderRepository(OrderDbContext _context, IPublishEndpoint _publishEndpoint) : IOrderRepository
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var orderSummary = await GetOrderSummaryAsync();
            var content = BuildOrderEmailBody(orderSummary.Id, orderSummary.ProductName,
                orderSummary.ProductPrice, orderSummary.Quantity, orderSummary.TotalAmount, orderSummary.Date);
            await _publishEndpoint.Publish(new EmailDTO("Order Information", content));
            await ClearOrderTableAsync();
            return new ServiceResponse(true, "Order placed successfully.");
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<OrderSummary> GetOrderSummaryAsync()
        {
            var order = await _context.Orders.FirstOrDefaultAsync();
            var products = await _context.Products.ToListAsync();
            var productInfo = products.Find(x => x.Id == order!.ProductId);
            return new OrderSummary(
                        order!.Id,
                        order.ProductId,
                        productInfo!.Name!,
                        productInfo.Price,
                        order.Quantity,
                        order.Quantity * productInfo.Price,
                        order.Date
                );
        }

        private  string BuildOrderEmailBody(int orderId, string productName, decimal price, int orderQuantity, decimal totalAmount, DateTime date)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<body style='font-family: Arial, sans-serif; color: #333;'>");
            sb.AppendLine("<h2 style='color: #2c3e50;'>Order Confirmation</h2>");
            sb.AppendLine("<p>Dear Customer,</p>");
            sb.AppendLine($"<p>Thank you for your order <strong>#{orderId}</strong>. Below are your order details:</p>");

            sb.AppendLine("<table style='border-collapse: collapse; width: 100%;'>");
            sb.AppendLine("<tr style='background-color: #f2f2f2;'>");
            sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left;'>Product</th>");
            sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left;'>Price</th>");
            sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left;'>Quantity</th>");
            sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left;'>Total</th>");
            sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: left;'>Date Ordered</th>");
            sb.AppendLine("</tr>");

            sb.AppendLine("<tr>");
            sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{productName}</td>");
            sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>₹{price}</td>");
            sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{orderQuantity}</td>");
            sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>₹{totalAmount}</td>");
            sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px;'>{date}</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("<p>We will notify you once your order has been shipped.</p>");
            sb.AppendLine("<p>Best regards,<br/>E‑Commerce Team</p>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        private async Task ClearOrderTableAsync()
        {
            _context.Orders.Remove(await _context.Orders.FirstOrDefaultAsync());
            await _context.SaveChangesAsync();
        }
    }
}
