using CustomerOnboardingApp.Business_Logic.Enums;
using CustomerOnboardingApp.Business_Logic.Models;

namespace CustomerOnboardingApp.Business_Logic.Services
{
    /// <summary>
    /// Interface for customer service
    /// </summary>
    public interface ICustomerService
    {
        List<DropdownModel> GetMainPurposesList();
        List<DropdownModel> GetTypeOfEntityList();
        List<DropdownModel> GetBusinessActivityList();
        List<DropdownModel> GetCountryList();
        int CreateCustomer(CustomerModel customerModel, List<DirectorShareholderModel> directorShareholderModelList);
        void SaveDocumentsData(int customerId, string documentName, EDocumentType eDocumentType);

    }
}
