using PustokMvcApp.Models;

namespace PustokMvcApp.ViewModels
{
    public class BookVm
    {
        public Book Book { get; set; } = null!;
        public List<Book> RelatedBooks { get; set; } 
    }
}
