using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWebApp.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminHomePageController : Controller
    {
        public IActionResult AdminHomePage()
        {
            return View();
        }
    }
}
