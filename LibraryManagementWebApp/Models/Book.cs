using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWebApp.Models.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool Available { get; set; }
    }
}
