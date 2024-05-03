using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWebApp.Models.Entities
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }

        // Navigation property to User
        public virtual User User { get; set; }
    }
}
