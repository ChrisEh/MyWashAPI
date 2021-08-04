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
using System.Threading.Tasks;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        // GET: api/ShoppingCartItems
        [HttpGet("{userId}")]
        public IActionResult GetAllShoppingCartItemsAsync(Guid userId)
        {
            var shoppingCartItems = _shoppingCartService.GetAllShoppingCartItems(userId);

            if (shoppingCartItems.Count() == 0)
                return NotFound();

            return Ok(shoppingCartItems);
        }

        // POST: api/ShoppingCartItems
        [HttpPost]
        public IActionResult Post([FromBody] List<ShoppingCartItemDto> shoppingCartItemDtos)
        {
            var shoppingCartItems = shoppingCartItemDtos.Select(s => _mapper.Map<ShoppingCartItem>(s)).ToList();
            _shoppingCartService.AddShoppingCartItems(shoppingCartItems);

            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(string userId)
        {
            var shoppingCartItems = _shoppingCartService.GetAllShoppingCartItems(new Guid(userId));
            _shoppingCartService.RemoveShoppingCartItems(shoppingCartItems);

            return Ok();
        }
    }
}