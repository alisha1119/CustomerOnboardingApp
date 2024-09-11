using Microsoft.AspNetCore.Mvc;

namespace CustomerOnboardingApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Display the home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
