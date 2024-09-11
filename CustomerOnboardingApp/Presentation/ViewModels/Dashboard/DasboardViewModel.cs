using CustomerOnboardingApp.Business_Logic.Models;

namespace CustomerOnboardingApp.Presentation.ViewModels.Dashboard
{
    /// <summary>
    /// View model for the dashboard
    /// </summary>
    public class DasboardViewModel
    {
        public int UserId { get; set; }
        public string UserRole { get; set; }
        public List<SubmittedCustomerModel> SubmittedCustomerList { get; set; }
       
}
}
