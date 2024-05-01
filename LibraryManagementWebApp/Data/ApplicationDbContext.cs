
using LibraryManagementWebApp.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementWebApp.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        
    }
}