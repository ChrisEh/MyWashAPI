using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWashApi.Data.Models;
using MyWashApi.Service.Services;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController( IOrderService orderService)
        {
            _orderService = orderService;
        }

        // For Admin
        // GET: api/Orders/PendingOrders
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult PendingOrders()
        {
            return Ok(_orderService.GetPendingOrders());
        }

        // GET: api/Orders/CompletedOrders
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult CompletedOrders()
        {
            return Ok(_orderService.GetCompletedOrders());
        }

        // GET: api/Orders/OrderDetails/5
        [HttpGet("[action]/{orderId}")]
        public IActionResult OrderDetails(string orderId)
        {
            return Ok(_orderService.GetCompletedOrders());
        }

        // GET: api/Orders/UncompletedOrdersCount
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult UncompletedOrdersCount()
        {
            return Ok(new { PendingOrders = _orderService.GetUncompletedOrdersCount() });
        }

        // GET: api/Orders/OrdersByUser/5
        [HttpGet("[action]/{userId}")]
        public IActionResult OrdersByUser(string userId)
        {
            return Ok(_orderService.GetOrdersOfUser(new Guid(userId)));
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Order order)
        {
            var newOrder = await _orderService.CreateOrder(order);

            return Ok(new { OrderId = newOrder.Id });
        }

        // PUT: api/Orders/CompleteOrder/5
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{orderId}")]
        public async Task<IActionResult> CompleteOrderAsync(string orderId)
        {
            var order = await _orderService.GetOrderById(new Guid(orderId));

            if (order == null)
            {
                return NotFound($"No order found for id '{orderId}'.");
            }
            else
            {
                await _orderService.CompleteOrder(new Guid(orderId));

                return Ok("Order completed");
            }
        }
    }
}