using System;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Register(User newUser);
        Task<bool> UserExists(string email);
        Task<User> GetUser(Guid id);
        Task<User> GetUser(string email);
    }
}