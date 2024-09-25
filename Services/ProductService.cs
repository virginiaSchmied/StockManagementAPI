using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Services.Interfaces;

namespace StockManagementAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor
        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Product business logic
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
            var totalProducts = products.Count();
            var pagesProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagesProducts.Count() == 0)
            {
                return null;
            }

            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            var nextPage = pageNumber < totalPages ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                Products = pagesProducts,
                PrevPage = prevPage,
                NextPage = nextPage,
                TotalPages = totalPages
            };

            return result;
        }


        public async Task<List<Product>> GetProductListAsync(double amount)
        {
            List<Product> productList = new List<Product>();

            var productsGroupedByCategory = await _unitOfWork.ProductRepo.GetProductsGroupedByCategory();

            if (productsGroupedByCategory.Count() == 0)
            {
                return null;
            }

            for (int i = 0; i < productsGroupedByCategory.Count; i++)
            {
                var list_i = productsGroupedByCategory[i];

                for (int j = i + 1; j < productsGroupedByCategory.Count; j++)
                {
                    var list_j = productsGroupedByCategory[j];

                    for (int x = 0; x < list_i.Count; x++)
                    {
                        for (int y = 0; y < list_j.Count; y++)
                        {
                            double value = list_i[x].Price + list_j[y].Price;
                            if (value <= amount)
                            {
                                productList.Add(list_i[x]);
                                productList.Add(list_j[y]);

                                return productList;
                            }
                        }
                    }
                }
            }

            if (productList.Count() == 0)
            {
                return null;
            }

            return productList;
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
