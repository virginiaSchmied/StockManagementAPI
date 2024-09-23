using StockManagementAPI.Models.User;
using System.Data;

namespace StockManagementAPI.Services.Interfaces
{
    public interface IUserRoleService 
    {
        Task<Object> GetAll(int pageNumber, int pageSize, string userId);
        Task<UserRole> GetById(int id, string userId);
        Task<UserRole> Insert(UserRole role, string userId);
        Task<UserRole> Update(int id, UserRole role);
        Task<bool> Delete(int id);
    }
}
