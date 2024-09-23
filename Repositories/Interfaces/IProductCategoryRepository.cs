using StockManagementAPI.Models.Product;

namespace StockManagementAPI.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAll();
        Task<ProductCategory> GetById(int id);
        Task Insert(ProductCategory productCategory);
        Task Update(ProductCategory productCategory);
        Task Delete(int id);
    }
}
