using StockManagementAPI.Models.Product;

namespace StockManagementAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Insert(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
