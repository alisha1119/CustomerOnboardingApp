using CustomerOnboardingApp.Business_Logic.Models;

namespace CustomerOnboardingApp.Business_Logic.Services
{
    /// <summary>
    /// Interface for login service
    /// </summary>
    public interface ILoginService
    {
        public UserModel? GetUserDetails(int userId);
        public int? AuthenticateUser(LoginModel loginModel);
    }
}
