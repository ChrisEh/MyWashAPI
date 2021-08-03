﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private MyWashContext _dbContext;
        public OrdersController(MyWashContext dbContext)
        {
            _dbContext = dbContext;
        }

        // For Admin
        // GET: api/Orders/PendingOrders
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult PendingOrders()
        {
            var orders = _dbContext.Orders.Where(order => order.IsOrderCompleted == false);

            return Ok(orders);
        }

        // GET: api/Orders/CompletedOrders
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult CompletedOrders()
        {
            var orders = _dbContext.Orders.Where(order => order.IsOrderCompleted == true);

            return Ok(orders);
        }

        // GET: api/Orders/OrderDetails/5
        [HttpGet("[action]/{orderId}")]
        public IActionResult OrderDetails(string orderId)
        {

            var orders = _dbContext.Orders.Where(order => order.Id == new Guid(orderId))
                   .Include(order => order.OrderDetails)
                   .ThenInclude(product => product.Product);

            return Ok(orders);
        }


        // GET: api/Orders/OrdersCount
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult OrdersCount()
        {
            var orders = (from order in _dbContext.Orders
                          where order.IsOrderCompleted == false
                          select order.IsOrderCompleted).Count();

            return Ok(new { PendingOrders = orders });
        }


        // GET: api/Orders/OrdersByUser/5
        [HttpGet("[action]/{userId}")]
        public IActionResult OrdersByUser(int userId)
        {
            var orders = _dbContext.Orders.Where(order => order.UserId == userId).OrderByDescending(o => o.OrderPlaced);

            return Ok(orders);
        }

        // POST: api/Orders
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            order.IsOrderCompleted = false;
            order.OrderPlaced = DateTime.Now;
            _dbContext.Orders.Add(order);
            _dbContext.Orders.Add(order);

            var shoppingCartItems = _dbContext.ShoppingCartItems.Where(cart => cart.CustomerId == order.UserId);
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Price = item.Price,
                    TotalAmount = item.TotalAmount,
                    Qty = item.Quantity,
                    ProductId = item.ProductId,
                    OrderId = order.Id,
                };
                _dbContext.OrderDetails.Add(orderDetail);
            }

            _dbContext.ShoppingCartItems.RemoveRange(shoppingCartItems);
            _dbContext.SaveChanges();

            return Ok(new { OrderId = order.Id });
        }

        // PUT: api/Orders/MarkOrderComplete/5
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{orderId}")]
        public IActionResult MarkOrderComplete(int orderId, [FromBody] Order order)
        {
            var entity = _dbContext.Orders.Find(orderId);
            if (entity == null)
            {
                return NotFound($"No order found for id '{orderId}'.");
            }
            else
            {
                entity.IsOrderCompleted = order.IsOrderCompleted;
                _dbContext.SaveChanges();

                return Ok("Order completed");
            }
        }
    }
}
