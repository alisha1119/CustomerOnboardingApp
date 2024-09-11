using CustomerOnboardingApp.Presentation.ViewModels.Customer;

namespace CustomerOnboardingApp.Business_Logic.Models
{
    /// <summary>
    /// Model used for the customer model
    /// </summary>
    public class CustomerModel
    {
        public int CutomerId { get; set; }
        public int MainPurposeId { get; set; }
        public string NameOfCompany { get; set; }
        public int TypeOfEntityId { get; set; }
        public int BusinessActivityId { get; set; }
        public string? LicenseRequirements { get; set; } 
        public int CountryId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateOnly DateOfIncorporation { get; set; }
        public string FirstNameDesignateApplicant { get; set; }
        public string LastNameDesignateApplicant { get; set; }
        public string TitleDesignateApplicant { get; set; }
        public string EmailAddress { get; set; }
    }
}
