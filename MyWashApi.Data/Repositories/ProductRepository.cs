using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWashApi.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyWashContext ctx) : base(ctx)
        {
        }

        public List<Product> GetProducts()
        {
            return _ctx.Products.ToList();
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            return await _ctx.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}
