using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUsersByIdAsync(int id);

        Task<List<User>> GetAllUsersAsync();
    }
}