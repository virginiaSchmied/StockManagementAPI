using Microsoft.EntityFrameworkCore;
using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Repositories.Interfaces;

namespace StockManagementAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly stockManagementDbContext _dbContext;

        public ProductRepository(stockManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Insert(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public async Task Update(Product product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(a => a.Id == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
            }
        }
    }
}
