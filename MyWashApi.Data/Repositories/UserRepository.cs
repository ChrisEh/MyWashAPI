using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyWashContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }

        public async Task<User> GetUsersByIdAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await GetAll().ToListAsync();
        }
    }
}
