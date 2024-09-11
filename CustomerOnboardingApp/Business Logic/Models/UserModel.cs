namespace CustomerOnboardingApp.Business_Logic.Models
{
    /// <summary>
    /// Model used for the user
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
