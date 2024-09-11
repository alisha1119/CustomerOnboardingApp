using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Data;
using CustomerOnboardingApp.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CustomerOnboardingApp.Business_Logic.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a user's details from the user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel? GetUserDetails(int userId)
        {
            var sqlQuery = @"SELECT 
	                            Id,
	                            FirstName,
	                            LastName,[Role]
                            FROM Cfg_User
                            WHERE Id = @userId";

            return _context.CfgUsers
                           .FromSqlRaw(sqlQuery, new SqlParameter("@userId", userId))
                           .Select(p => new UserModel
                           {
                               Id = p.Id,
                               FirstName = p.FirstName,
                               LastName = p.LastName,
                               Role = p.Role
                           }).FirstOrDefault();
        }

        /// <summary>
        /// Authenticate the user from its credentials
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public int? AuthenticateUser(LoginModel loginModel)
        {
            var sqlQuery = @"SELECT 
	                            Id
                            FROM Cfg_User
                            WHERE Email = @email AND [Password] = @password";

            return _context.CfgUsers
                          .FromSqlRaw(sqlQuery, new SqlParameter("@email", loginModel.Email),
                                                new SqlParameter("@password", loginModel.Password))
                          .Select(p => p.Id)
                          .FirstOrDefault();
        }
    }
}
