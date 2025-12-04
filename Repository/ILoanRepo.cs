using Library.Models;

namespace Library.Repository
{
    public interface ILoanRepo
    {
        Task<List<Loan>> GetAllLoans();
    }
}
