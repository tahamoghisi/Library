using Library.Context;
using Library.Models.ViewModels;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepo _bookRepo;
        private readonly LibraryDBContext context;
        public BookController(IBookRepo bookRepo,LibraryDBContext context)
        {
            _bookRepo = bookRepo;
            this.context = context;
        }
        public async Task<IActionResult> ShowBookList(int categoryId, int status = 2, int page = 1)
        {
            var books = await _bookRepo.GetAllBooks(page, categoryId, status);
            return View(books);
        }
        public async Task<IActionResult> BookDetail(int bookId)
        {
            var book = await _bookRepo.GetBook(bookId);
            if (book == null) return NotFound();
            return View(book);
        }
        #region CRUD
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepo.GetAllBooksForControl();
            return View(books);
        }
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var book = await _bookRepo.DeleteBook(bookId);
            if (!book)
            {
                TempData["Message"] = "این کتاب امانت داده شده است و نمیتوان حذف کرد";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBook(int bookId)
        {
            var book = await _bookRepo.GetUpdateBookViewModel(bookId);
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(UpdateBookViewModel model)
        {
            var book = await _bookRepo.UpdateBook(model);
            if (!book)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            var model = new CreateBookViewModel
            {
                Categories = await context.Categories.ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookViewModel model)
        {
                var book = await _bookRepo.CreateBook(model);
                if (book)
                {
                    return RedirectToAction("Index");
                }
            var viewModel = new CreateBookViewModel
            {
                Categories = await context.Categories.ToListAsync()
            };
            TempData["Message"] = "این کتاب در کتابخانه موجود است";
            return View(viewModel);

        }
        #endregion
    }
}
