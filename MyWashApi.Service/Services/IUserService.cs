using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Service.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User newUser);
    }
}