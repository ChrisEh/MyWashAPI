using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<List<Product>> GetAllShoppingCartProducts(Guid userId);
        Task AddProducts(List<Product> products, Guid userId);
        Task RemoveProducts(List<Product> products, Guid userId);
        Task RemoveProduct(Product product, Guid userId);
        Task Delete(Guid userId);
    }
}
