namespace CustomerOnboardingApp.Business_Logic.Models
{
    /// <summary>
    /// Model used for the director/shareholder
    /// </summary>
    public class DirectorShareholderModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string PassportNumber { get; set; }
        public string? Role {  get; set; }
    }
}
