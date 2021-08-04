using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;
using AuthenticationPlugin;
using System;

namespace MyWashApi.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyWashContext ctx) : base(ctx)
        {
        }

        public async Task<User> Register(User newUser)
        {
            newUser.Role = "User";
            newUser.Password = SecurePasswordHasherHelper.Hash(newUser.Password);
            _ctx.Users.Add(newUser);
            await _ctx.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _ctx.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
