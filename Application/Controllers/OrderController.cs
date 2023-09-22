using Application.Model;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderModel orderModel)
        {
            try
            {
                await _orderService.CreateOrder(orderModel);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
