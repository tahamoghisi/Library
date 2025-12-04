using Library.Context;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly LibraryDBContext context;
        public CategoryRepo(LibraryDBContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateCategory(CreateCategoryViewModel model)
        {
            var categories = await context.Categories.ToListAsync();
            foreach (var item in categories)
            {
                if (model.Name == item.Name) return false;
            }
            var category = new Category
            {
                Name = model.Name
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category.Books.Any() && category.Books != null) return false;
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            var result = from category in context.Categories
                         join book in context.Books on
                         category.Id equals book.CategoryId
                         into groupItem
                         orderby groupItem.Count() descending, category.Name descending
                         select new CategoryViewModel
                         {
                             CategoryId = category.Id,
                             BookCount = groupItem.Count(),
                             CategoryName = category.Name
                         };
            return await result.ToListAsync();
        }

        public async Task<UpdateCategoryViewModel> GetUpdateCategory(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            var updateCategory = new UpdateCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
            return updateCategory;
        }

        public async Task<bool> UpdateCategory(UpdateCategoryViewModel model)
        {
            if (model.Name == null) return false;
            var category = await context.Categories.FindAsync(model.Id);
            category.Name = model.Name;
            await context.SaveChangesAsync();
            return true;
        }
    }

}
