namespace Library.Models.ViewModels
{
    public class DashbordLoanViewModel
    {
        public string UserName { get; set; }
        public string BookName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime? ReturnDate { get; set; }
        public float? FineAmount { get; set; }
    }
}
