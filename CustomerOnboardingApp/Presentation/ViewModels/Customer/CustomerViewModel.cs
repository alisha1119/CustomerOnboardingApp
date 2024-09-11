using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Presentation.ViewModels.Dashboard;

namespace CustomerOnboardingApp.Presentation.ViewModels.Customer
{
    /// <summary>
    /// Customer view model to display the creation form
    /// </summary>
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public List<DropdownModel> MainPurposeList { get; set; }

        public int MainPurposeId { get; set; }

        public string NameOfCompany { get; set; }
        public List<DropdownModel> TypeOfEntityList { get; set; }

        public int TypeOfEntityId { get; set; }
        public List<DropdownModel> BusinessActivityList { get; set; }

        public int BusinessActivityId { get; set; }
        public string LicenseRequirements { get; set; } // Displayed if business activity is "Banking"
        public List<DropdownModel> CountryList { get; set; }

        public int CountryId { get; set; }

        public string RegistrationNumber { get; set; }

        public DateOnly DateOfIncorporation { get; set; }
        public List<DirectorShareholderViewModel> DirectorShareholderList { get; set; }

        public string FirstNameDesignateApplicant { get; set; }

        public string LastNameDesignateApplicant { get; set; }

        public string TitleDesignateApplicant { get; set; }

        public string EmailAddress { get; set; }
        public bool IsReadOnly { get; set; } = false;
        public List<DocumentViewModel> DocumentList { get; set; }

    }
}
