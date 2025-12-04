using Library.Models;
using Library.Models.ViewModels;

namespace Library.Repository
{
    public interface IBookRepo
    {
        Task<BookViewModel> GetAllBooks(int page,int categoryId, int status);
        Task<List<Book>> GetAllBooksForControl();
        Task<Book> GetBook(int bookId);
        Task<bool> DeleteBook(int bookId);
        Task<UpdateBookViewModel> GetUpdateBookViewModel(int bookId);
        Task<bool> UpdateBook(UpdateBookViewModel model);
        Task<bool> CreateBook(CreateBookViewModel model);
    }
}
