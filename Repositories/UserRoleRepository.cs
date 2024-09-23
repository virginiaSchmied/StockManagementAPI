using Microsoft.EntityFrameworkCore;
using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.User;
using StockManagementAPI.Repositories.Interfaces;
using System.Data;

namespace StockManagementAPI.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly stockManagementDbContext _dbContext;

        public UserRoleRepository(stockManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            return await _dbContext.UserRole
                .ToListAsync();
        }

        public async Task<UserRole> GetById(int id)
        {
            return await _dbContext.UserRole.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Insert(UserRole role)
        {
            _dbContext.UserRole.Add(role);
        }

        public async Task Update(UserRole role)
        {
            _dbContext.UserRole.Update(role);
        }

        public async Task Delete(int id)
        {
            var role = _dbContext.UserRole.FirstOrDefault(a => a.Id == id);
            if (role != null)
            {
                _dbContext.UserRole.Remove(role);
            }
        }
    }
}
