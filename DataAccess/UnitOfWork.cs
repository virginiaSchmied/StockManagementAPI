using StockManagementAPI.Repositories;
using StockManagementAPI.Repositories.Interfaces;

namespace StockManagementAPI.DataAccess
{
    public class UnitOfWork
    {
        private readonly stockManagementDbContext _dbContext;

        // Entities
        private UserRepository _userRepository;
        private UserRoleRepository _roleRepository;
        private ProductRepository _productRepository;
        private ProductCategoryRepository _productCategoryRepository;

        //Constructor
        public UnitOfWork(stockManagementDbContext dbContext) { 
            _dbContext = dbContext; 
        }

        // User
        public UserRepository UserRepo
        {
            get
            {
                if (_userRepository == null)
                {
                    return _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        // UserRole
        public UserRoleRepository RoleRepo
        {
            get
            {
                if (_roleRepository == null)
                {
                    return _roleRepository = new UserRoleRepository(_dbContext);
                }

                return _roleRepository;
            }
        }

        // Product
        public ProductRepository ProductRepo
        {
            get
            {
                if (_productRepository == null)
                {
                    return _productRepository = new ProductRepository(_dbContext);
                }

                return _productRepository;
            }
        }

        // Product Category
        public ProductCategoryRepository ProductCategoryRepo
        {
            get
            {
                if (_productCategoryRepository == null)
                {
                    return _productCategoryRepository = new ProductCategoryRepository(_dbContext);
                }

                return _productCategoryRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
