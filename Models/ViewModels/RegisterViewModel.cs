using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
