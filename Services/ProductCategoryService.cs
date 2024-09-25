using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Services.Interfaces;

namespace StockManagementAPI.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor
        public ProductCategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ProductCategory business logic

        public async Task<ProductCategory> AddProductCategoryAsync(ProductCategory productCategory)
        {
            var _productCategory = new ProductCategory
            {
                ProductName = productCategory.ProductName,
                
            };
            await _unitOfWork.ProductCategoryRepo.Insert(_productCategory);
            await _unitOfWork.SaveChangesAsync();
            return _productCategory;
        }

        public async Task<ProductCategory> DeleteProductCategoryAsync(int id)
        {
            var _productCategory = await _unitOfWork.ProductCategoryRepo.GetById(id);
            if (_productCategory == null)
            {
                return null;
            }
            await _unitOfWork.ProductCategoryRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return _productCategory;
        }

        public async Task<Object> GetAllProductsCategoriesAsync(int pageNumber, int pageSize)
        {
            var productsCategories = await _unitOfWork.ProductCategoryRepo.GetAll();
            var totalProducts = productsCategories.Count();
            var pagesProductsCategories = productsCategories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagesProductsCategories.Count() == 0)
            {
                return null;
            }

            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            var nextPage = pageNumber < (int)Math.Ceiling((double)productsCategories.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                ProductsCategories = pagesProductsCategories,
                PrevPage = prevPage,
                NextPage = nextPage,
                TotalPages = totalPages
            };

            return result;
        }

        public async Task<ProductCategory> GetProductCategoryAsync(int id)
        {
            var _productCategory = await _unitOfWork.ProductCategoryRepo.GetById(id);
            if (_productCategory == null)
            {
                return null;
            }
            return _productCategory;
        }

        public async Task<ProductCategory> UpdateProductCategoryAsync(int id, ProductCategory productCategory)
        {
            var _productCategory = await _unitOfWork.ProductCategoryRepo.GetById(id);
            if (_productCategory == null)
            {
                return null;
            }

            _productCategory.ProductName = productCategory.ProductName;

            await _unitOfWork.ProductCategoryRepo.Update(_productCategory);
            await _unitOfWork.SaveChangesAsync();

            return _productCategory;
        }

    }
}
