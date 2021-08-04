using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWashApi.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProducts();
        Task<Product> GetProduct(Guid productId);
    }
}
