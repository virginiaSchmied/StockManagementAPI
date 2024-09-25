using StockManagementAPI.DataAccess;
using StockManagementAPI.Models.User;
using StockManagementAPI.Services.Interfaces;
using static Microsoft.VisualStudio.Services.Notifications.VssNotificationEvent;

namespace StockManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor
        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // User business logic
        public async Task<User> AddUserAsync(User user)
        {
            var _user = new User
            {
                Email = user.Email,
                Password = encryptPass.GetSHA256(user.Password),
                Role_Id = 2
            };
            await _unitOfWork.UserRepo.Insert(_user);
            await _unitOfWork.SaveChangesAsync();
            return _user;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var _user = await _unitOfWork.UserRepo.GetById(id);
            if (_user == null)
            {
                return null;
            }
            await _unitOfWork.UserRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return _user;
        }

        public async Task<Object> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _unitOfWork.UserRepo.GetAll();
            var totalProducts = users.Count();
            var pagesUsers = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (pagesUsers.Count() == 0)
            {
                return null;
            }

            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            var nextPage = pageNumber < (int)Math.Ceiling((double)users.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                Users = pagesUsers,
                PrevPage = prevPage,
                NextPage = nextPage,
                TotalPages = totalPages
            };

            return result;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var _user = await _unitOfWork.UserRepo.GetById(id);
            if (_user == null)
            {
                return null;
            }
            return _user;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var _user = await _unitOfWork.UserRepo.GetById(id);
            if (_user == null)
            {
                return null;
            }

            _user.Email = user.Email;
            _user.Password = encryptPass.GetSHA256(user.Password);

            await _unitOfWork.UserRepo.Update(_user);
            await _unitOfWork.SaveChangesAsync();

            return _user;
        }
    }
}

