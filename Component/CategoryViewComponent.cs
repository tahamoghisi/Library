using Library.Models.ViewModels;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Library.Component
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryViewComponent(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.CurrentCategory = RouteData?.Values["categoryId"];
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            categories = await _categoryRepo.GetCategories();
            return View(categories);
        }
    }
}
