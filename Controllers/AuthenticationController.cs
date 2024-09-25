﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockManagementAPI.DataAccess;
using StockManagementAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly stockManagementDbContext _context;

        // Constructor
        public AuthenticationController(IConfiguration config, stockManagementDbContext context)
        {
            _secretKey = config.GetSection("settings:secretkey").Value;
            _context = context;
        }

        // POST method to validate user credentials
        [HttpPost]
        [Route("userAuthentication")]
        public async Task<IActionResult> validateUser ([FromBody] Authentication request)
        {

            // Check if the user exists in the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            // If user is not found, return Unauthorized status
            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "inexistente o inhabilitado" });
            }

            if (encryptPass.GetSHA256(request.Password) == user.Password && request.Email == user.Email)
            {
                var keyBytes = Encoding.UTF8.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();

                // Claims for the JWT token
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Password.ToString()));
                claims.AddClaim(new Claim("Id", user.Id.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Role, user.Role_Id == 1 ? "Admin" : "Regular"));

                // Create token with claims
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                // Create JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string newToken = tokenHandler.WriteToken(tokenConfig);

                //Returns OK status
                return StatusCode(StatusCodes.Status200OK, new { token = newToken, roleId = user.Role_Id });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "inexistente o inhabilitado" });
            }
        }
    }
}
