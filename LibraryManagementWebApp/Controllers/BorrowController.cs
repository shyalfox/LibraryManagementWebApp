using LibraryManagementWebApp.Data;
using LibraryManagementWebApp.Models.Entities;
using LibraryManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryManagementWebApp.Controllers
{
    [Authorize]
    public class BorrowController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BorrowController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult AddBorrow()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBorrow(AddBorrowViewModel viewModel)
        {
            DateTime currentDate = DateTime.Now;

            // Calculate the Expected Return Date (15 days from the current date)
            DateTime expectedReturnDate = currentDate.AddDays(15);
           
            {

            }
            var borrow = new BorrowedBook
            {
                BookId = viewModel.BookId,
                StudentId = viewModel.StudentId,
                BorrowDate = viewModel.BorrowDate,
                ExpectedReturnDate = expectedReturnDate,
                Returned=false,
                ReturnDate=null,
                FineAmount=0,
              


            };
            await dbContext.BorrowedBooks.AddAsync(borrow);
            var book = dbContext.Books.FirstOrDefault(b => b.BookId == viewModel.BookId);
            if (book != null)
            {
                book.Available = false; // Set Available to false
            }
            await dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Record added successfully.";
            return RedirectToAction("ListBorrow", "Borrow");
        }
        [HttpGet]
        public async Task<IActionResult> ListBorrow(int searchTerm= 0)
        {
            // Get the current user's role from claims
            var roleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var userRole = roleClaim?.Value; // This will contain the user's role, or null if not found
           
         

            // Check if the user is an admin
            if (userRole == "Admin")
            {
                if (searchTerm == 0)
                {
                    var borrowedbooks = await dbContext.BorrowedBooks.ToListAsync();
                    return View(borrowedbooks);
                }
                else
                {
                    var borrowedBooksQuery = dbContext.BorrowedBooks.Where(b => b.StudentId == searchTerm);

                    // Apply search filter if search term is provided
                  
                    
                       borrowedBooksQuery = borrowedBooksQuery.Where(b => b.StudentId == searchTerm);
                    

                    var borrowedBooks = await borrowedBooksQuery.ToListAsync();

                    return View(borrowedBooks);
                }
            }
            else
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
              
                var userId=int.Parse(userIdClaim?.Value ?? "0");
                Console.WriteLine("userId: " + userId);

                var studentId = await dbContext.Students
                    .Where(s => s.UserId == userId)
                    .Select(s => s.StudentId)
                    .FirstOrDefaultAsync();

                // Retrieve borrowed books associated with the current user
                var borrowedBooks = await dbContext.BorrowedBooks.Where(b => b.StudentId == studentId).ToListAsync();

                return View(borrowedBooks);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> EditBorrow(int id)
        {
            var borrowedbooks = await dbContext.BorrowedBooks.FindAsync(id);
            return View(borrowedbooks);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> EditBorrow(BorrowedBook viewModel)
        {
            var borrowedbook = await dbContext.BorrowedBooks.FindAsync(viewModel.BorrowId);
            if (borrowedbook is not null)
            {
             
                borrowedbook.Returned = viewModel.Returned;
                if (borrowedbook.Returned)
                {  DateTime returnDate = DateTime.Now;
                    borrowedbook.ReturnDate = returnDate;
                    if (borrowedbook.ReturnDate > borrowedbook.ExpectedReturnDate)
                    {
                        borrowedbook.FineAmount = 50; // Set the fine amount to 20
                    }
                    else
                    {
                        borrowedbook.FineAmount = 0; // No fine
                }
                    var book = dbContext.Books.FirstOrDefault(b => b.BookId == viewModel.BookId);
                    if (book != null)
                    {
                        book.Available = true; // Set Available to true
                    }
                }
                if(borrowedbook.FineAmount !=  0)
                {
                    TempData["SuccessMessage"] = "Record  Updated successfully. The Student has been fined for Late Return";
                }
                else {
                    TempData["SuccessMessage"] = "Record  Updated successfully.";
                }
               
              
          


                await dbContext.SaveChangesAsync();
            }
           
            return RedirectToAction("ListBorrow", "Borrow");

        }

    }
}
