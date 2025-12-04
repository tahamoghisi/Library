using Library.Context;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly LibraryDBContext context;
        public UserRepo(LibraryDBContext context)
        {
            this.context = context;
        }

        public async Task<User> CheckUser(LoginViewModel model)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);
            if (user == null) return null;
            return user;
        }

        public async Task<User> CreateUser(RegisterViewModel model)
        {
            var newuser = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address,
                RoleId = 2
            };
            await context.AddAsync(newuser);
            await context.SaveChangesAsync();
            return newuser;
        }

        public async Task<UpdateUserViewModel> GetUpdateUser(int userId)
        {
            var user = await context.Users.FindAsync(userId);
            var updateUser = new UpdateUserViewModel
            {
                Id = userId,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone
            };
            return updateUser;
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<bool> UpdateUser(UpdateUserViewModel model)
        {
            var user = await context.Users.FindAsync(model.Id);
            if (user == null) return false;
            user.UserName = model.UserName;
            user.Address = model.Address;
            user.Phone = model.Phone;
            user.Email = model.Email;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
