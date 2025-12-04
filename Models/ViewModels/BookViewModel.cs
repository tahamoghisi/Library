namespace Library.Models.ViewModels
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        public PaggingInformation PagingInformation { get; set; }
        public int CategoryId { get; set; }
    }
}
