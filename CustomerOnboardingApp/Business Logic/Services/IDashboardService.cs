using CustomerOnboardingApp.Business_Logic.Models;

namespace CustomerOnboardingApp.Business_Logic.Services
{
    /// <summary>
    /// Interface for dashboard service
    /// </summary>
    public interface IDashboardService
    {
        public string GetUserRole(int userId);
        public List<SubmittedCustomerModel> GetSubmittedCustomerList();
        public CustomerModel GetCustomerModel(int customerId);
        public List<DirectorShareholderModel> GetDirectorShareholderList(int customerId);
        public void ProcessCustomer(int customerId);
        public void RejectSubmittedCustomer(int customerId);
        public List<SubmittedCustomerModel> GetProcessedCustomerList();
        public void ApproveCustomer(int customerId);
        public void RejectProcessedCustomer(int customerId);
        public List<DocumentModel> GetDocumentList(int customerId);
    }
}
