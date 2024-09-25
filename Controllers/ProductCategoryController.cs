using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Services;

namespace StockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryService _productCategoryService;

        // Constructor
        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;

        }


        // PRODUCT CATEGORY CRUD //

        [HttpGet]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var productsCategories = await _productCategoryService.GetAllProductsCategoriesAsync(pageNumber, pageSize);

                if (productsCategories == null)
                {
                    return NotFound();
                }

                return Ok(productsCategories);

            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var productCategory = await _productCategoryService.GetProductCategoryAsync(id);
                if (productCategory == null)
                {
                    return NotFound();
                }
                return Ok(productCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ProductCategory productCategory)
        {
            try
            {
                var _productCategory = await _productCategoryService.AddProductCategoryAsync(productCategory);
                if (_productCategory == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("Get", new { id = productCategory.Id }, productCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductCategory productCategory)
        {
            try
            {
                // Validar si el producto existe
                var existingProductCategory = await _productCategoryService.GetProductCategoryAsync(id);
                if (existingProductCategory == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Product not found."
                    });
                }

                var updatedProductCategory = await _productCategoryService.UpdateProductCategoryAsync(id, productCategory);
                if (updatedProductCategory == null)
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Failed to update product."
                    });
                }

                return Ok(updatedProductCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }



        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var productCategory = await _productCategoryService.DeleteProductCategoryAsync(id);

                if (productCategory == null)
                {
                    return NotFound();
                }
                return Ok(productCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
    }
}
