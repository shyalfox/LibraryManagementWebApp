namespace LibraryManagementWebApp.Models
{
    public class AddBorrowViewModel

    {
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; } 
        public DateTime?  ReturnDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public bool Returned { get; set; }
        public decimal FineAmount { get; set; }
    }
}
