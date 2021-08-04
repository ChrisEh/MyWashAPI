using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartItem>
    {
        Task AddShoppingCartItems(List<ShoppingCartItem> shoppingCartItems);
        Task RemoveShoppingCartItems(List<ShoppingCartItem> shoppingCartItems);
        Task RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem);
        List<ShoppingCartItem> GetAllShoppingCartItems(Guid userId);
    }
}
