using CustomerOnboardingApp.Business_Logic.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerOnboardingApp.Presentation.ViewModels.Customer
{
    /// <summary>
    /// View model for when the form is submitted for a customer
    /// </summary>
    public class SubmitCustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Main purpose is required.")]
        public int MainPurposeId { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string NameOfCompany { get; set; }
        [Required(ErrorMessage = "Type of entity is required.")]
        public int TypeOfEntityId { get; set; }
        [Required(ErrorMessage = "Business activity is required.")]
        public int BusinessActivityId { get; set; }
        public string? LicenseRequirements { get; set; } // Displayed if business activity is banking

        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Registration number is required.")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Date of incorporation is required.")]
        public DateOnly DateOfIncorporation { get; set; }
        public List<DirectorShareholderViewModel>? DirectorShareholderList { get; set; }

        [Required(ErrorMessage = "Designated applicant first name is required.")]
        public string FirstNameDesignateApplicant { get; set; }

        [Required(ErrorMessage = "Designated applicant last name is required.")]
        public string LastNameDesignateApplicant { get; set; }

        [Required(ErrorMessage = "Designated applicant title is required.")]
        public string TitleDesignateApplicant { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }

    }
}
