using LibraryManagementWebApp.Models;
using LibraryManagementWebApp.Models.Entities;
using LibraryManagementWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementWebApp.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserAdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UserAdminController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]

        public IActionResult AddUserAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUserAdmin(AddUserAdminViewModel viewModel)
        {
            var user = new User
            {
               
               Role=viewModel.Role,
               /*PasswordHash=HashPassword(viewModel.PasswordHash),*/
               PasswordHash=viewModel.PasswordHash,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            if(user.Role == "Admin" ) {
                var admin = new Admin
                {
                    AdminName = viewModel.FullName,
                    AdminEmail = viewModel.Email,
                    UserId = user.UserId,

                };

                await dbContext.Admins.AddAsync(admin);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                var student = new Student
                {
                    StudentName = viewModel.FullName,
                    StudentEmail = viewModel.Email,
                    UserId = user.UserId,

                };
                await dbContext.Students.AddAsync(student);
                await dbContext.SaveChangesAsync();

            }
           
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> ListStudent()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> ListAdmin()
        {
            var admins = await dbContext.Admins.ToListAsync();

            return View(admins);
        }
        [HttpGet]
        public async Task<IActionResult> EditStudents(int id)
        {
            var student =await dbContext.Students.FindAsync(id);
            return  View(student);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudents(Student viewModel) {
            var student=await dbContext.Students.FindAsync(viewModel.StudentId);
            if(student is not null)
            {
                student.StudentName=viewModel.StudentName;
                student.StudentEmail = viewModel.StudentEmail;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListStudent", "UserAdmin");

        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudents(Student viewModel)
        {
            var student = await dbContext.Students.
                AsNoTracking()
                .FirstOrDefaultAsync (x => x.StudentId == viewModel.StudentId);
            if(student is not null)
            {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListStudent", "UserAdmin");

        }
        [HttpGet]
        public async Task<IActionResult> EditAdmins(int id)
        {
            var student = await dbContext.Admins.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmins(Admin viewModel)
        {
            var admin = await dbContext.Admins.FindAsync(viewModel.AdminId);
            if (admin is not null)
            {
                admin.AdminName = viewModel.AdminName;
                admin.AdminEmail = viewModel.AdminEmail;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "UserAdmin");

        }
        [HttpPost]
        public async Task<IActionResult> DeleteAdmin(Admin viewModel)
        {
            var admin = await dbContext.Admins.
                AsNoTracking()
                .FirstOrDefaultAsync(x => x.AdminId == viewModel.AdminId);
            if (admin is not null)
            {
                dbContext.Admins.Remove(viewModel);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ListAdmin", "UserAdmin");

        }


       /* private string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }*/


    }

}
