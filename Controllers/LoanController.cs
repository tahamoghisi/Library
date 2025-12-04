using Library.Context;
using Library.Models;
using Library.Models.ViewModels;
using Library.Repository;
using Library.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class LoanController : Controller
    {
        private readonly IBookRepo bookRepo;
        private readonly LibraryDBContext context;
        private readonly ILoanRepo loanRepo;
        public LoanController(IBookRepo bookRepo, LibraryDBContext context, ILoanRepo loanRepo)
        {
            this.bookRepo = bookRepo;
            this.context = context;
            this.loanRepo = loanRepo;
        }
        #region AddAndRemoveFromBasket
        public async Task<IActionResult> RemoveFromBasket(int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var basket = HttpContext.Session.GetObject<List<LoanViewModel>>($"Basket{userId}");
            var index = basket.FindIndex(b => b.BookId == bookId);
            if (index != -1)
            {
                basket.RemoveAt(index);
            }
            HttpContext.Session.SetObject($"Basket{userId}", basket);
            return RedirectToAction("Basket");
        }
        public async Task<IActionResult> AddToBasket(int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var basket = HttpContext.Session.GetObject<List<LoanViewModel>>($"Basket{userId}");
            if (basket == null)
            {
                basket = new List<LoanViewModel>();
            }
            var book = await bookRepo.GetBook(bookId);
            basket.Add(new LoanViewModel
            {
                BookId = bookId,
                BookName = book.Name,
                UserId = userId.Value
            });
            HttpContext.Session.SetObject($"Basket{userId}", basket);
            return RedirectToAction("basket");

        }
        public async Task<IActionResult> Basket()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var basket = HttpContext.Session.GetObject<List<LoanViewModel>>($"Basket{userId}");
            if (basket == null)
            {
                basket = new List<LoanViewModel>();
            }
            return View(basket);
        }
        #endregion
        #region SaveLoan
        public async Task<IActionResult> SaveBasketLoan()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var basket = HttpContext.Session.GetObject<List<LoanViewModel>>($"Basket{userId}");
            if (basket == null || basket.Count == 0)
            {
                TempData["Message"] = "کتابی برای ثبت وجود ندارد";
                return RedirectToAction("AddToBasket");
            }
            foreach (var item in basket)
            {
                var loan = new Loan
                {
                    BookId = item.BookId,
                    UserId = userId.Value,
                    LoanDate = DateTime.Now,
                    DueTime = DateTime.Now.AddDays(3)
                };
                var book = await bookRepo.GetBook(item.BookId);
                if (book != null)
                {
                    book.Status = Status.borrowed;
                }
                await context.Loans.AddAsync(loan);


            }
            await context.SaveChangesAsync();
            HttpContext.Session.Remove($"Basket{userId}");
            return RedirectToAction("LoanSuccess");
        }
        public async Task<IActionResult> LoanSuccess()
        {
            return View();
        }
        #endregion
        #region UserLoan
        public async Task<IActionResult> ShowUserLoan()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var loans = await context.Loans.Where(l => l.UserId == userId)
                .Include(c => c.Book)
                .ToListAsync();
            if (loans == null)
            {
                return RedirectToAction("EmptyLoan");
            }
            return View(loans);
        }
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var loan = await context.Loans.FirstOrDefaultAsync(l => l.BookId == bookId && l.UserId == userId);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now;
            }
            var book = await bookRepo.GetBook(bookId);
            if (book != null)
            {
                book.Status = Status.available;
            }
            await context.SaveChangesAsync();
            return RedirectToAction("ShowUserLoan");
        }
        public async Task<IActionResult> EmptyLoan()
        {
            return View();
        }
        #endregion
        #region Dashbord
        public async Task<IActionResult> Index()
        {
            var loan = await loanRepo.GetAllLoans();
            List<DashbordLoanViewModel> dashbordLoanViewModels = new List<DashbordLoanViewModel>();
            foreach (var item  in loan)
            {
                var book = await bookRepo.GetBook(item.BookId);
                var dashbordLoan = new DashbordLoanViewModel
                {
                    UserName = item.User.UserName,
                    BookName = book.Name,
                    LoanDate = item.LoanDate,
                    DueTime = item.DueTime,
                    ReturnDate = item.ReturnDate,
                    FineAmount = item.FineAmount
                };
                dashbordLoanViewModels.Add(dashbordLoan);
            }
            return View(dashbordLoanViewModels);
        }
        #endregion
    }
}
