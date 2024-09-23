using Abp.Authorization.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using StockManagementAPI.Models.User;
using StockManagementAPI.Services;

namespace StockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;

        }

        // USER CRUD //

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);

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
                var _user = await _userService.GetUserAsync(id);
                if (_user == null)
                {
                    return NotFound();
                }
                return Ok(_user);
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
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                var _user = await _userService.AddUserAsync(user);
                if (_user == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("Get", new { id = user.Id }, user);
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
        public async Task<IActionResult> Put(int id, User user)
        {
            try
            {
                var _user = await _userService.UpdateUserAsync(id, user);
                if (_user == null)
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
                var user = await _userService.DeleteUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
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
