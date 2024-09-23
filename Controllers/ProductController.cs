using Abp.Authorization.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Models.User;
using StockManagementAPI.Services;
using StockManagementAPI.Services.Interfaces;

namespace StockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;

        }


        // PRODUCT CRUD //

        [HttpGet]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(pageNumber, pageSize);

                if (products == null)
                {
                    return NotFound();
                }

                return Ok(products);

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
                var product = await _productService.GetProductAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
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
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            try
            {
                var _product = await _productService.AddProductAsync(product);
                if (_product == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("Get", new { id = product.Id }, product);
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
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            try
            {
                // Validar si el producto existe
                var existingProduct = await _productService.GetProductAsync(id);
                if (existingProduct == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Product not found."
                    });
                }

                // Actualizar el producto
                var updatedProduct = await _productService.UpdateProductAsync(id, product);
                if (updatedProduct == null)
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Failed to update product."
                    });
                }

                // Retornar el producto actualizado
                return Ok(updatedProduct);
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
                var product = await _productService.DeleteProductAsync(id);

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
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
