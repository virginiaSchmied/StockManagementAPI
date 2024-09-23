using StockManagementAPI.Models.User;
using System.Data;

namespace StockManagementAPI.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAll();
        Task<UserRole> GetById(int id);
        Task Insert(UserRole role);
        Task Update(UserRole role);
        Task Delete(int id);
    }
}
