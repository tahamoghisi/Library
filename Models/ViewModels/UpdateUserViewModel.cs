using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
