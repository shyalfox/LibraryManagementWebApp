using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWebApp.Controllers
{
    public class RegisterBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
