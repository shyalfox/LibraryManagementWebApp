using Microsoft.AspNetCore.Mvc;
using LibraryManagementWebApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace LibraryManagementWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /Account/Login
        public IActionResult LoginPage()
        {
            // Check if the user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to the home page or any other page
                return RedirectToAction("Index", "Home");
            }

            // If not authenticated, render the login page
            return View();
        }

        // POST: /Account/LoginPage
        [HttpPost]
        public IActionResult LoginPage(int userid, string password)
        {
            // Validate user credentials
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == userid && u.PasswordHash == password);
            if (user != null)
            {
                // Create claims for the authenticated user
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                    // Add more claims if needed
                };

                // Create authentication properties
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    // Configure properties like whether to persist the cookie
                    IsPersistent = true
                };

                // Sign in the user
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties).Wait();

                // Redirect based on user role
                if (user.Role == "Admin")
                {
                    return RedirectToAction("AdminHomePage", "AdminHomePage");
                }
                else
                {
                    return RedirectToAction("StudentDashboard", "Student");
                }
            }
            else
            {
                // Authentication failed, display error message
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            // Sign out the current user
            await HttpContext.SignOutAsync();

            // Redirect to the home page or any other desired page
            return RedirectToAction("LoginPage", "Account");
        }
    }
}
