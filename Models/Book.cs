using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public Status Status { get; set; }

    }


    public enum Status
    {
        available,
        borrowed
    }
}
