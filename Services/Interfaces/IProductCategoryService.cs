using StockManagementAPI.Models.Product;

namespace StockManagementAPI.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<Object> GetAllProductsCategoriesAsync(int pageNumber, int pageSize);
        Task<ProductCategory> GetProductCategoryAsync(int id);
        Task<ProductCategory> AddProductCategoryAsync(ProductCategory productCategory);
        Task<ProductCategory> UpdateProductCategoryAsync(int id, ProductCategory productCategory);
        Task<ProductCategory> DeleteProductCategoryAsync(int id);
    }
}
