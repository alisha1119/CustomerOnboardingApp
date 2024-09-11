using System.ComponentModel.DataAnnotations;

namespace CustomerOnboardingApp.Presentation.ViewModels.Customer
{
    /// <summary>
    /// Director/shareholder view model for the creation form
    /// </summary>
    public class DirectorShareholderViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string PassportNumber { get; set; }

        public string? Role { get; set; }
    }
}
