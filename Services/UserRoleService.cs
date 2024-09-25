using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.User;
using StockManagementAPI.Services.Interfaces;
using System.Data;

namespace StockManagementAPI.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor
        public UserRoleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // UserRole business logic
        public async Task<Object> GetAllRolesAsync(int pageNumber, int pageSize)
        {
            var roles = await _unitOfWork.RoleRepo.GetAll();
            var totalProducts = roles.Count();
            var pagedAccounts = roles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagedAccounts.Count() == 0)
            {
                return null;
            }

            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            var nextPage = pageNumber < (int)Math.Ceiling((double)pagedAccounts.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                Roles = pagedAccounts,
                prevPage = prevPage,
                nextPage = nextPage,
                TotalPages = totalPages
            };
            return result;
        }

        public async Task<UserRole> GetUserRoleAsync(int id)
        {
            var role = await _unitOfWork.RoleRepo.GetById(id);

            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<UserRole> AddUserRoleAsync(UserRole userRole)
        {

            var _userRole = new UserRole
            {
                Name = userRole.Name,
                Description = userRole.Description
            };

            await _unitOfWork.RoleRepo.Insert(_userRole);
            await _unitOfWork.SaveChangesAsync();
            return _userRole;
        }

        public async Task<UserRole> UpdateUserRoleAsync(int id, UserRole userRole)
        {
            var _userRole = await _unitOfWork.RoleRepo.GetById(id);

            if (_userRole == null)
            {
                return null;
            }

            _userRole.Name = userRole.Name;
            _userRole.Description = userRole.Description;

            await _unitOfWork.RoleRepo.Update(_userRole);
            await _unitOfWork.SaveChangesAsync();
            return _userRole;
        }

        public async Task<bool> DeleteUserRoleAsync(int id)
        {
            var userRole = await _unitOfWork.RoleRepo.GetById(id);

            if (userRole == null)
            {
                return false;
            }

            await _unitOfWork.RoleRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
