using Library.Models;
using Library.Models.ViewModels;

namespace Library.Repository
{
    public interface ICategoryRepo
    {
        Task<List<CategoryViewModel>> GetCategories();
        Task<bool> CreateCategory(CreateCategoryViewModel model);
        Task<UpdateCategoryViewModel> GetUpdateCategory(int categoryId);
        Task<bool> UpdateCategory(UpdateCategoryViewModel model);
        Task<bool> DeleteCategory(int categoryId);
    }
}
