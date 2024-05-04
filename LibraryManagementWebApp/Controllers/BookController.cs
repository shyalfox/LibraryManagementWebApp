using LibraryManagementWebApp.Data;
using LibraryManagementWebApp.Models.Entities;
using LibraryManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementWebApp.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult AddBooks()
        {
            return View();
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> AddBooks(AddBooksViewModel viewModel)
        {
            var book = new Book
            {
                BookTitle = viewModel.BookTitle,
                Author = viewModel.Author,
                Genre = viewModel.Genre,
                Available = viewModel.Available,
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Book added successfully.";
            return RedirectToAction("ListBooks", "Book");
        }
        [HttpGet]
        public async Task<IActionResult> ListBooks()
        {
            var books = await dbContext.Books.ToListAsync();

            return View(books);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> EditBooks(int id)
        {
            var books = await dbContext.Books.FindAsync(id);
            return View(books);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> EditBooks(Book viewModel)
        {
            var book = await dbContext.Books.FindAsync(viewModel.BookId);
            if (book is not null)
            {
                book.BookTitle = viewModel.BookTitle;
                book.Author = viewModel.Author;
                book.Genre = viewModel.Genre;
                book.Available = viewModel.Available;
                

                await dbContext.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Book Edited successfully.";
            return RedirectToAction("ListBooks", "Book");

        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> DeleteBooks(int bookId) 
        {
            var book = await dbContext.Books
                .FirstOrDefaultAsync(x => x.BookId == bookId);

            if (book != null)
            {
                dbContext.Books.Remove(book); 

                await dbContext.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Book Deleted successfully.";

            return RedirectToAction("ListBooks", "Book");
        }



    }
}
