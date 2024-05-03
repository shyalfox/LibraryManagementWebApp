
using LibraryManagementWebApp.Models;
using LibraryManagementWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books{ get; set; }

        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
      

    }
}