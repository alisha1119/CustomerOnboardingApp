using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Business_Logic.Services;
using CustomerOnboardingApp.Presentation.ViewModels.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CustomerOnboardingApp.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        
        /// <summary>
        /// Display the login page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Login into the application
        /// Check authentication
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = MapLoginModel(viewModel);
                var userId = _loginService.AuthenticateUser(userModel);    

                if (userId != null)
                {
                    var user = _loginService.GetUserDetails((int)userId);
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);

                    return RedirectToAction("Index", "Dashboard", new { userId = user.Id });

                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(viewModel);
        }

        /// <summary>
        /// Hash the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Map the login model from the view model
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private LoginModel MapLoginModel(LoginViewModel viewModel)
        {
            var loginModel = new LoginModel
            {
                Email = viewModel.Email,
                Password = HashPassword(viewModel.Password)
            };
            return loginModel;
        }
    }
}
