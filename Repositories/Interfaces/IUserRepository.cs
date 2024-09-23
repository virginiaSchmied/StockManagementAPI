using StockManagementAPI.Models.User;

namespace StockManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task Insert(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
