using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWebApp.Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }

        // Navigation property to User
        public virtual User User { get; set; }
    }

}

