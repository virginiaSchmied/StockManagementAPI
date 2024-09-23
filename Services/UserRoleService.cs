using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.User;
using StockManagementAPI.Services.Interfaces;
using System.Data;

namespace StockManagementAPI.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserRoleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Object> GetAll(int pageNumber, int pageSize, string userId)
        {
            var roles = await _unitOfWork.RoleRepo.GetAll();

            var pagedAccounts = roles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagedAccounts.Count() == 0)
            {
                return null;
            }

            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;

            var nextPage = pageNumber < (int)Math.Ceiling((double)pagedAccounts.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                Roles = pagedAccounts,
                prevPage = prevPage,
                nextPage = nextPage
            };
            return result;
        }

        public async Task<UserRole> GetById(int id, string userId)
        {
            var role = await _unitOfWork.RoleRepo.GetById(id);

            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<UserRole> Insert(UserRole roleDto, string userId)
        {

            var role = new UserRole
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                Description = roleDto.Description
            };

            await _unitOfWork.RoleRepo.Insert(role);
            await _unitOfWork.SaveChangesAsync();
            return role;
        }

        public async Task<UserRole> Update(int id, UserRole roleDto)
        {
            var role = await _unitOfWork.RoleRepo.GetById(id);

            if (role == null)
            {
                return null;
            }

            role.Name = roleDto.Name;
            role.Description = roleDto.Description;

            await _unitOfWork.RoleRepo.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return role;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _unitOfWork.RoleRepo.GetById(id);

            if (role == null)
            {
                return false;
            }

            await _unitOfWork.RoleRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
