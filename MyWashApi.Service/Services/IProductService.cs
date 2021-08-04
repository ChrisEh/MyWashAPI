using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Service.Services
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product newProduct);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Guid productId);
        List<Product> GetProducts();
        Task<Product> GetProduct(Guid id);
    }
}
