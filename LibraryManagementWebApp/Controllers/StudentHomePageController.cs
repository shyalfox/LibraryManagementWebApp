using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWebApp.Controllers
{
    [Authorize(Policy = "StudentOnly")]
    public class StudentHomePageController : Controller
    {
        public IActionResult StudentHomePage()
        {
            return View();
        }
    }
}
