using Microsoft.AspNetCore.Mvc;
using ProductAPI.Service;
using SharedLib.DTOs;
using SharedLib.Model;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpPost("addproduct")]
        public async Task<ActionResult<ServiceResponse>> AddProductAsync(Product product)
        {
            var response= await _productService.AddProductAsync(product);
            return response.flag? Ok(response):BadRequest(response);
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync() => Ok(await _productService.GetAllProductAsync()); 
    }
}
