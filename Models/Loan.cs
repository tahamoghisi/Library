using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Loan
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueTime { get; set; }   
        public DateTime? ReturnDate { get; set; }
        public float? FineAmount { get; set; }
    }
}
