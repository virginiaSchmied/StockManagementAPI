using Abp.Authorization.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using StockManagementAPI.Services;
using StockManagementAPI.Models.User;

namespace StockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _userRoleService;

        // Constructor
        public UserRoleController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;

        }
        // USER ROLE CRUD //

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var userRoles = await _userRoleService.GetAllRolesAsync(pageNumber, pageSize);

                if (userRoles == null)
                {
                    return NotFound();
                }

                return Ok(userRoles);

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var _userRole = await _userRoleService.GetUserRoleAsync(id);
                if (_userRole == null)
                {
                    return NotFound();
                }
                return Ok(_userRole);
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
        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> Post(Models.User.UserRole userRole)
        {
            try
            {
                var _userRole = await _userRoleService.AddUserRoleAsync(userRole);
                if (_userRole == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("Get", new { id = userRole.Id }, userRole);
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
        public async Task<IActionResult> Put(Models.User.UserRole userRole, int id)
        {
            try
            {
                var _userRole = await _userRoleService.UpdateUserRoleAsync(id, userRole);
                if (_userRole == null)
                {
                    return NotFound();
                }
                return Ok();
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

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userRole = await _userRoleService.DeleteUserRoleAsync(id);

                if (userRole == null)
                {
                    return NotFound();
                }
                return Ok(userRole);
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
