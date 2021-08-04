using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCartItem>, IShoppingCartRepository
    {
        public ShoppingCartRepository(MyWashContext ctx) : base(ctx)
        {
        }

        public async Task AddShoppingCartItems(List<ShoppingCartItem> shoppingCartItems)
        {
            var user = shoppingCartItems.FirstOrDefault().User;
            var shoppingCart = _ctx.ShoppingCarts.FirstOrDefault(s => s.UserId == user.Id);

            if (shoppingCart == null)
            {
                _ctx.ShoppingCarts.Add(new ShoppingCart()
                {
                    UserId = user.Id,
                    ShoppingCartItems = shoppingCartItems
                });
            }
            else
            {
                foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
                {
                    var existingShoppingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == shoppingCartItem.Product.Id);

                    if (existingShoppingCartItem == null)
                    {
                        shoppingCart.ShoppingCartItems.Add(shoppingCartItem);
                    }
                    else
                    {
                        existingShoppingCartItem.Quantity++;
                    }
                }
            }

            await _ctx.SaveChangesAsync();
        }

        public async Task RemoveShoppingCartItems(List<ShoppingCartItem> shoppingCartItems)
        {
            var user = shoppingCartItems.FirstOrDefault().User;
            var shoppingCart = _ctx.ShoppingCarts.FirstOrDefault(s => s.UserId == user.Id);
            if (shoppingCart != null)
            {
                shoppingCartItems.ForEach(s => shoppingCart.ShoppingCartItems.Remove(shoppingCart.ShoppingCartItems.FirstOrDefault(s => s.Product == s.Product)));
            }

            if (shoppingCart.ShoppingCartItems.Count() == 0)
                _ctx.Remove(shoppingCart);

            await _ctx.SaveChangesAsync();
        }

        public async Task RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = _ctx.ShoppingCarts.FirstOrDefault(s => s.UserId == shoppingCartItem.User.Id);
            if (shoppingCart != null)
            {
                shoppingCart.ShoppingCartItems.Remove(shoppingCart.ShoppingCartItems.FirstOrDefault(s => s.Product == s.Product));
            }

            if (shoppingCart.ShoppingCartItems.Count() == 0)
                _ctx.Remove(shoppingCart);

            await _ctx.SaveChangesAsync();
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems(Guid userId)
        {
            var list = new List<ShoppingCartItem>();
            var shoppingCart = _ctx.ShoppingCarts.FirstOrDefault(s => s.UserId == userId);

            if (shoppingCart == null)
            {
                return list;
            }
            return shoppingCart.ShoppingCartItems;
        }
    }
}
