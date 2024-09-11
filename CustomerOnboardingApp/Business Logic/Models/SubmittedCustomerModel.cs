namespace CustomerOnboardingApp.Business_Logic.Models
{
    /// <summary>
    /// Model used to retrieve the customer data
    /// </summary>
    public class SubmittedCustomerModel
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string TypeOfEntity {  get; set; }
        public string BusinessActivity { get; set; }
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
