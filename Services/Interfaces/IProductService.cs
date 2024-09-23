using StockManagementAPI.Models.Product;

namespace StockManagementAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<Object> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<Product> GetProductAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<Product> DeleteProductAsync(int id);
    }
}
