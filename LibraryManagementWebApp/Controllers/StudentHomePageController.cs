using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWebApp.Controllers
{
    public class StudentHomePageController : Controller
    {
        public IActionResult StudentHomePage()
        {
            return View();
        }
    }
}
