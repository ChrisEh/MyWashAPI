using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using MyWashApi.Models;
using MyWashApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWashApi.Data.Models;
using System;
using MyWashApi.Dtos;
using AutoMapper;
using MyWashApi.Service.Services;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private IMapper _mapper;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        // GET: api/ShoppingCartItems
        [HttpGet("{userId}")]
        public IActionResult GetAllShoppingCartProducts(string userId)
        {
            if (shoppingCartItems.Count == 0)
            {
                return NotFound();
            }

            return Ok(shoppingCartItems);
        }

        // POST: api/ShoppingCartItems
        [HttpPost]
        public IActionResult Post([FromBody] ShoppingCartItemsDto shoppingCartItemsDto)
        {
            _shoppingCartService.AddProducts(shoppingCartItemsDto.ProductIds, shoppingCartItemsDto.UserId);

            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(string userId)
        {
            var shoppingCart = _ctx.ShoppingCarts.FirstOrDefault(s => s.User.Id == new Guid(userId));

            if (shoppingCart != null)
            {
                _ctx.ShoppingCarts.Remove(shoppingCart);
            }
            
            _ctx.SaveChanges();

            return Ok();
        }
    }
}
