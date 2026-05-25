using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Service;
using SharedLib.DTOs;
using SharedLib.Model;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService _orderService) : ControllerBase
    {

        [HttpPost("addorder")]
        public async Task<ActionResult<ServiceResponse>> AddOrderAsync(Order order)
        {
            var response = await _orderService.AddOrderAsync(order);
            return response.flag ? Ok(response) : BadRequest();
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAsync()=> Ok(await _orderService.GetAllOrderAsync());
    }
}
