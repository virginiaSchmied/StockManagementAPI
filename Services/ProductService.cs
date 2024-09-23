using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Services.Interfaces;

namespace StockManagementAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;
        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var _product = new Product
            {
                Price = product.Price,
                UploadDate = DateTime.Now,
                IdProductCategory_Id = product.IdProductCategory_Id
            };
            await _unitOfWork.ProductRepo.Insert(_product);
            await _unitOfWork.SaveChangesAsync();
            return _product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var _product = await _unitOfWork.ProductRepo.GetById(id);
            if (_product == null)
            {
                return null;
            }
            await _unitOfWork.ProductRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return _product;
        }

        public async Task<Object> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.ProductRepo.GetAll();
            var pagesProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagesProducts.Count() == 0)
            {
                return null;
            }

            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;

            var nextPage = pageNumber < (int)Math.Ceiling((double)products.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                Products = pagesProducts,
                PrevPage = prevPage,
                NextPage = nextPage
            };

            return result;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var _product = await _unitOfWork.ProductRepo.GetById(id);
            if (_product == null)
            {
                return null;
            }
            return _product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var _product = await _unitOfWork.ProductRepo.GetById(id);
            if (_product == null)
            {
                return null;
            }

            _product.Price = product.Price;
            _product.UploadDate = DateTime.Now;
            _product.IdProductCategory_Id = product.IdProductCategory_Id;

            await _unitOfWork.ProductRepo.Update(_product);
            await _unitOfWork.SaveChangesAsync();

            return _product;
        }
    }
}
