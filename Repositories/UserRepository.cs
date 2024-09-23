using Microsoft.EntityFrameworkCore;
using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.User;
using StockManagementAPI.Repositories.Interfaces;

namespace StockManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly stockManagementDbContext _dbContext;

        public UserRepository(stockManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users
                .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Insert(User user)
        {
            _dbContext.Users.Add(user);
        }

        public async Task Update(User user)
        {
            _dbContext.Users.Update(user);
        }

        public async Task Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
            }
        }
    }
}

