using Microsoft.EntityFrameworkCore;
using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Repositories.Interfaces;

namespace StockManagementAPI.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly stockManagementDbContext _dbContext;

        // Constructor
        public ProductCategoryRepository(stockManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Product Category CRUD - works on ProductCategories table
        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _dbContext.ProductsCategories
                .ToListAsync();
        }

        public async Task<ProductCategory> GetById(int id)
        {
            return await _dbContext.ProductsCategories.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Insert(ProductCategory productCategory)
        {
            _dbContext.ProductsCategories.Add(productCategory);
        }

        public async Task Update(ProductCategory productCategory)
        {
            _dbContext.ProductsCategories.Update(productCategory);
        }

        public async Task Delete(int id)
        {
            var productCategory = _dbContext.ProductsCategories.FirstOrDefault(a => a.Id == id);
            if (productCategory != null)
            {
                _dbContext.ProductsCategories.Remove(productCategory);
            }
        }
    }
}
