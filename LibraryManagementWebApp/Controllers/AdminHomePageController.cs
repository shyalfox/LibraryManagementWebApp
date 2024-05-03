using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWebApp.Controllers
{
    public class AdminHomePageController : Controller
    {
        public IActionResult AdminHomePage()
        {
            return View();
        }
    }
}
