using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetCustomerByIdAsync(int id);

        Task<List<User>> GetAllCustomersAsync();
    }
}