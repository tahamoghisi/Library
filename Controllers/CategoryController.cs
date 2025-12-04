using Library.Component;
using Library.Models;
using Library.Models.ViewModels;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepo.GetCategories();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View(new CreateCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            var category = await _categoryRepo.CreateCategory(model);
            if (category)
            {
                return RedirectToAction("Index");
            }
            TempData["Message"] = "دسته بندی ای با همین نام وجود دارد";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            var category = await _categoryRepo.GetUpdateCategory(categoryId);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel model)
        {
            var category = await _categoryRepo.UpdateCategory(model);
            if (category)
            {
                return RedirectToAction("Index");
            }
            TempData["Message"] = "لطفا مقادیر را پر کنید";
            return View(model);
        }
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepo.DeleteCategory(categoryId);
            if (category)
            {
                return RedirectToAction("Index");
            }
            TempData["Message"] = "این دسته بندی را نمیتوان حذف کرد";
            return RedirectToAction("Index");
        }
    }
}
