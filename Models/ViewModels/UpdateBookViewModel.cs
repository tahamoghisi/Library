using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public string AuthorName { get; set; }      
        public int PageCount { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } 
        public Status Status { get; set; }
    }
}
