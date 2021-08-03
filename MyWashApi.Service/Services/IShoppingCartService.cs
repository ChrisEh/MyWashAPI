using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWashApi.Service.Services
{
    public interface IShoppingCartService
    {
        Task<List<Product>> GetAllShoppingCartProducts(Guid userId);
        Task AddProducts(ICollection<Guid> productIds, Guid userId);
        Task Delete(Guid userId);
    }
}
