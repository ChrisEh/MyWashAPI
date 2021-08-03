using System.Collections.Generic;
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUsersByIdAsync(id);
        }

        public async Task<User> AddUserAsync(User newCustomer)
        {
            return await _userRepository.AddAsync(newCustomer);
        }
    }
}