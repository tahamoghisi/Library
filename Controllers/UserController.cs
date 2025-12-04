using Library.Models.ViewModels;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo userRepo;
        public UserController(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userRepo.GetUsers();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int userId)
        {
            var user = await userRepo.GetUpdateUser(userId);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            var user = await userRepo.UpdateUser(model);
            if (user)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
