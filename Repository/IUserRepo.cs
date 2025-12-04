using Library.Models;
using Library.Models.ViewModels;

namespace Library.Repository
{
    public interface IUserRepo
    {
        Task<User> CreateUser(RegisterViewModel model); 
        Task<User?> CheckUser(LoginViewModel model);
        Task<List<User>> GetUsers();
        Task<UpdateUserViewModel> GetUpdateUser(int userId);
        Task<bool> UpdateUser(UpdateUserViewModel model);
    }
}
