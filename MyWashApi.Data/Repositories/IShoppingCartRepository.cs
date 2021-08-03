using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<List<Product>> GetAllShoppingCartProducts(Guid userId);
        Task AddProducts(List<Guid> products, Guid userId);
        Task Delete(Guid userId);
    }
}
