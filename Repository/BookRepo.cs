using Library.Context;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Library.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly LibraryDBContext context;
        private int pageSize = 5;
        public BookRepo(LibraryDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateBook(CreateBookViewModel model)
        {
            var books = await context.Books.ToListAsync();
            foreach(var item in books)
            {
                if (item.Name == model.Name && item.AuthorName == model.AuthorName)
                {
                    return false;
                }
            }
            var book = new Book
            {
                Name = model.Name,
                AuthorName = model.AuthorName,
                Description = model.Description,
                PageCount = model.PageCount,
                CategoryId = model.CategoryId,
                Status = model.Status
            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteBook(int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book.Status == Status.borrowed) return false;
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<UpdateBookViewModel> GetUpdateBookViewModel(int bookId)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            var bookViewModel = new UpdateBookViewModel
            {
                Id = bookId,
                Name = book.Name,
                AuthorName = book.AuthorName,
                CategoryId = book.CategoryId,
                Description = book.Description,
                PageCount = book.PageCount,
                Status = book.Status,
                Categories = await context.Categories.ToListAsync()
            };
            return bookViewModel;
        }
        public async Task<bool> UpdateBook(UpdateBookViewModel model)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (book == null) return false;
            book.Name = model.Name;
            book.AuthorName = model.AuthorName;
            book.Description = model.Description;
            book.PageCount = model.PageCount;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<BookViewModel> GetAllBooks(int page, int categoryId, int status)
        {
            int total;
            if (status != null && status == 0)
            {
                total = await context.Books
               .Where(c => (categoryId == 0 ||
               c.CategoryId == categoryId) &&
               c.Status == Status.available)
               .CountAsync();

                return new BookViewModel
                {
                    Books = await context.Books
                .Where(c => (categoryId == 0 || c.CategoryId == categoryId)
                && c.Status == Status.available)
                .OrderBy(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync(),
                    PagingInformation = new PaggingInformation
                    {
                        PageSize = pageSize,
                        CurrentPage = page,
                        TotalRecord = total

                    },
                    CategoryId = categoryId
                };
            }



            else if (status != null && status == 1)
            {
                total = await context.Books
                .Where(c => (categoryId == 0 ||
                c.CategoryId == categoryId) &&
                c.Status == Status.borrowed)
                .CountAsync();
                return new BookViewModel
                {
                    Books = await context.Books
                     .Where(c => (categoryId == 0 || c.CategoryId == categoryId)
                     && c.Status == Status.borrowed)
                     .OrderBy(b => b.Id)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize).ToListAsync(),
                    PagingInformation = new PaggingInformation
                    {
                        PageSize = pageSize,
                        CurrentPage = page,
                        TotalRecord = total

                    },
                    CategoryId = categoryId
                };
            }



            else
            {
                total = await context.Books
                .Where(c => categoryId == 0 ||
                c.CategoryId == categoryId)
                .CountAsync();
                return new BookViewModel
                {
                    Books = await context.Books
                    .Where(c => categoryId == 0 || c.CategoryId == categoryId)
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToListAsync(),
                    PagingInformation = new PaggingInformation
                    {
                        PageSize = pageSize,
                        CurrentPage = page,
                        TotalRecord = total

                    },
                    CategoryId = categoryId
                };
            }
        }
        public async Task<List<Book>> GetAllBooksForControl()
        {
            var books = await context.Books
                .Include(b => b.Category)
                .OrderBy(b => b.Id)
                .ToListAsync();
            return books;
        }
        public async Task<Book> GetBook(int bookId)
        {
            var book = await context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == bookId);
            return book;
        }
    }
}
