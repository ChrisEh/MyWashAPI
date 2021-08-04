using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Service.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        Task<User> GetUser(Guid id);
        Task<User> GetUser(string email);
        Task<User> Register(User newUser);
        Task<bool> UserExists(string email);
        Task<User> Update(User user);
    }
}