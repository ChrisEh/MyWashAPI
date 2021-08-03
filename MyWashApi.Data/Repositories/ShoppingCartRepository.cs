using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(MyWashContext ctx) : base(ctx)
        {
        }

        public async Task AddProducts(List<Guid> productIds, Guid userId)
        {
            var shoppingCart = _ctx.ShoppingCarts.FirstOrDefault(s => s.User.Id == userId);

            if (shoppingCart == null)
            {
                _ctx.ShoppingCarts.Add(shoppingCart);
            }
            else
            {
                productIds.ForEach(p => shoppingCart.Products.Add(p));
            }

            await _ctx.SaveChangesAsync();
        }

        public Task Delete(Guid userId)
        {

        }

        public Task<List<Product>> GetAllShoppingCartProducts(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
