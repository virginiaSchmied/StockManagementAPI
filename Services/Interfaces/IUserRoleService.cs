using StockManagementAPI.Models.User;
using System.Data;

namespace StockManagementAPI.Services.Interfaces
{
    public interface IUserRoleService 
    {
        Task<Object> GetAllRolesAsync(int pageNumber, int pageSize);
        Task<UserRole> GetUserRoleAsync(int id);
        Task<UserRole> AddUserRoleAsync(UserRole role);
        Task<UserRole> UpdateUserRoleAsync(int id, UserRole role);
        Task<bool> DeleteUserRoleAsync(int id);
    }
}
