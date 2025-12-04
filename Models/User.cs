using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        public string Password {  get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Loan> Loans { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
