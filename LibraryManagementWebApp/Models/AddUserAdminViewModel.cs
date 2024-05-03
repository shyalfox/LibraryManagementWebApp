namespace LibraryManagementWebApp.Models
{
    public class AddUserAdminViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public int AdminId { get; set; }
        public int StudentId { get; set; }
      

        public string FullName { get; set; }
     
        public string Email { get; set; }



    }
}
