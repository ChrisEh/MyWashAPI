using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Service.Services
{
    public interface IShoppingCartService
    {
        Task AddShoppingCartItems(List<ShoppingCartItem> shoppingCartItems);
        Task RemoveShoppingCartItems(List<ShoppingCartItem> shoppingCartItems);
        Task RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem);
        List<ShoppingCartItem> GetAllShoppingCartItems(Guid userId);
    }
}
