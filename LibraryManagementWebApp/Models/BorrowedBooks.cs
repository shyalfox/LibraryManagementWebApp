using LibraryManagementWebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWebApp.Models
{
    public class BorrowedBook
    {
        [Key]
        public int BorrowId { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public bool Returned { get; set; }
        public decimal FineAmount { get; set; }

        // Navigation properties to Student and Book
        public virtual Student Student { get; set; }
        public virtual Book Book { get; set; }
    }
}
