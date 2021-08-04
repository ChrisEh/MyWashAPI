using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWashApi.Data.Models;
using MyWashApi.Data.Repositories;

namespace MyWashApi.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public Task<User> GetUser(Guid id)
        {
            return _userRepository.GetUser(id);
        }

        public Task<User> GetUser(string email)
        {
            return _userRepository.GetUser(email);
        }

        public async Task<User> Register(User newUser)
        {
            return await _userRepository.Register(newUser);
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _userRepository.UserExists(email);
        }
    }
}