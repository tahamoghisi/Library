using Library.Models;
using Library.Models.ViewModels;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;
        public AccountController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userRepo.CheckUser(model);
            if (user == null)
            {

                TempData["Message"] = "ورود ناموفق";
                return View();

            }
            HttpContext.Session.SetInt32("RoleId",user.RoleId);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.UserName);
            return RedirectToAction("ShowBookList", "Book");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["Message"] = "ثبت نام ناموفق";
            //    return View(model);
            //}
            var newuser = await _userRepo.CreateUser(model);
            if (newuser == null)
            {
                TempData["Message"] = "ثبت نام ناموفق";
                return View();
            }
            HttpContext.Session.SetInt32("UserId", newuser.Id);
            HttpContext.Session.SetInt32("RoleId", newuser.RoleId);
            HttpContext.Session.SetString("UserName", newuser.UserName);
            return RedirectToAction("ShowBookList", "Book");
        }
        public async Task<IActionResult> LogOut(int userId)
        {
            return View();
        }
    }
}
