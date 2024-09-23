using StockManagementAPI.Models.User;

namespace StockManagementAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<Object> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<User> GetUserAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task<User> DeleteUserAsync(int id);
    }
}
