using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
