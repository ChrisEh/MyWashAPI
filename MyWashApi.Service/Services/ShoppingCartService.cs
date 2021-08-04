using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;
using MyWashApi.Data.Repositories;

namespace MyWashApi.Service.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartyRepository)
        {
            _shoppingCartRepository = shoppingCartyRepository;
        }

        public async Task AddShoppingCartItems(List<ShoppingCartItem> shoppingCartItems)
        {
            await _shoppingCartRepository.AddShoppingCartItems(shoppingCartItems);
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems(Guid userId)
        {
            return _shoppingCartRepository.GetAllShoppingCartItems(userId);
        }

        public async Task RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            await _shoppingCartRepository.RemoveShoppingCartItem(shoppingCartItem);
        }

        public async Task RemoveShoppingCartItems(List<ShoppingCartItem> shoppingCartItems)
        {
            await _shoppingCartRepository.RemoveShoppingCartItems(shoppingCartItems);
        }
    }
}
