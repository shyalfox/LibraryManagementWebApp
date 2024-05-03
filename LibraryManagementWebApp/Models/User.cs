using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWebApp.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
   
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    
        // Navigation property to Student
        public virtual Student Student { get; set; }

        // Navigation property to Admin
        public virtual Admin Admin { get; set; }
    }
}
