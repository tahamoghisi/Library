using Library.Context;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class LoanRepo : ILoanRepo
    {
        private readonly LibraryDBContext context;
        public LoanRepo(LibraryDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Loan>> GetAllLoans()
        {
            return await context.Loans.Include(l => l.User).ToListAsync();
        }
    }
}
