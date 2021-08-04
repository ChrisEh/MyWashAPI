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

        public async Task AddProducts(List<Product> products, Guid userId)
        {
            await _shoppingCartRepository.AddProducts(products, userId);
        }

        public async Task Delete(Guid userId)
        {
            await _shoppingCartRepository.Delete(userId);
        }

        public async Task<List<Product>> GetAllShoppingCartProducts(Guid userId)
        {
            return await _shoppingCartRepository.GetAllShoppingCartProducts(userId);
        }
    }
}
