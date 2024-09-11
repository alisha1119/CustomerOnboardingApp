using CustomerOnboardingApp.Business_Logic.Enums;
using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Business_Logic.Services;
using CustomerOnboardingApp.Presentation.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace CustomerOnboardingApp.Presentation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// Return the customer form
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var viewModel = new CustomerViewModel();
            viewModel.MainPurposeList = _customerService.GetMainPurposesList();
            viewModel.TypeOfEntityList = _customerService.GetTypeOfEntityList();
            viewModel.BusinessActivityList = _customerService.GetBusinessActivityList();
            viewModel.CountryList = _customerService.GetCountryList();
            return View(viewModel);
        }

        /// <summary>
        /// create a new customer.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubmitCustomerViewModel viewModel, IFormFile documentIdentityCard, IFormFile documentCertificate)
        {
            if (ModelState.IsValid) { 
                var customerModel = MapCustomerModel(viewModel);
                var directorShareholderModelList = MapDirectorShareholderModel(viewModel.DirectorShareholderList);
                //The application ID would generally be a unique number generated from a counter instead of using the customer Id itself.
                var applicationId = _customerService.CreateCustomer(customerModel, directorShareholderModelList);
                SaveDocuments(applicationId, documentIdentityCard, documentCertificate);
                
                return View("Success", applicationId);
            }
            var customerViewModel = CreateCustomerViewModel(viewModel);
            
            return View(customerViewModel);
        }

        /// <summary>
        /// Save the uploaded documents
        /// </summary>
        private void SaveDocuments(int customerId, IFormFile documentIdentityCard, IFormFile documentCertificate)
        {
            var isValid = ValidateUploadedDocuments(documentIdentityCard, documentCertificate);
            if (isValid)
            {
                SaveDocumentsPhysically(customerId, documentIdentityCard, documentCertificate);
                SaveDocumentsData(customerId, documentIdentityCard.FileName, documentCertificate.FileName); 
            }
        }

        /// <summary>
        /// Validate the documents before creating a folder for saving
        /// </summary>
        /// <param name="documentIdentityCard"></param>
        /// <param name="documentCertificate"></param>
        /// <returns></returns>
        private bool ValidateUploadedDocuments(IFormFile documentIdentityCard, IFormFile documentCertificate)
        {
            var isValid = (documentIdentityCard != null && documentIdentityCard.Length > 0) 
                            || (documentIdentityCard != null && documentIdentityCard.Length > 0);
            return isValid;
        }

        /// <summary>
        /// Physically save the documents in the upload folders
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="documentIdentityCard"></param>
        /// <param name="documentCertificate"></param>
        private void SaveDocumentsPhysically(int customerId, IFormFile documentIdentityCard, IFormFile documentCertificate)
        {
            //Create customer upload folder per customer Id
            string cutomerUploadFolder = "upload_" + (customerId.ToString());
            var uploadDirectory = Path.Combine("wwwroot", cutomerUploadFolder);
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }
            var filePathIdentityCard = Path.Combine(uploadDirectory, documentIdentityCard.FileName);

            using (var stream = new FileStream(filePathIdentityCard, FileMode.Create))
            {
                documentIdentityCard.CopyToAsync(stream);
            }
            var filePathCertificate = Path.Combine(uploadDirectory, documentCertificate.FileName);

            using (var stream = new FileStream(filePathCertificate, FileMode.Create))
            {
                documentCertificate.CopyToAsync(stream);
            }
        }

        /// <summary>
        /// Save the document data
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="documentIdentityCard"></param>
        /// <param name="documentCertificate"></param>
        private void SaveDocumentsData(int customerId, string identityCardDocumentName, string certificateDocumentName)
        {
            _customerService.SaveDocumentsData(customerId, identityCardDocumentName, EDocumentType.IdentityCard);
            _customerService.SaveDocumentsData(customerId, certificateDocumentName, EDocumentType.Passport);
        }

        /// <summary>
        /// Create a new customer view model to reload the creation form.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private CustomerViewModel CreateCustomerViewModel(SubmitCustomerViewModel viewModel)
        {
            var customerViewModel = new CustomerViewModel()
            {
                MainPurposeList = _customerService.GetMainPurposesList(),
                TypeOfEntityList = _customerService.GetTypeOfEntityList(),
                BusinessActivityList = _customerService.GetBusinessActivityList(),
                CountryList = _customerService.GetCountryList(),
            };
            return customerViewModel;
        }

        /// <summary>
        /// Map the customer model from the submitted
        /// customer view model.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private CustomerModel MapCustomerModel(SubmitCustomerViewModel viewModel)
        {
            var customerModel = new CustomerModel()
            {
                MainPurposeId = viewModel.MainPurposeId,
                NameOfCompany = viewModel.NameOfCompany,
                TypeOfEntityId = viewModel.TypeOfEntityId,
                BusinessActivityId = viewModel.BusinessActivityId,
                LicenseRequirements = viewModel.LicenseRequirements,
                CountryId = viewModel.CountryId,
                RegistrationNumber = viewModel.RegistrationNumber,
                DateOfIncorporation = viewModel.DateOfIncorporation,
                FirstNameDesignateApplicant = viewModel.FirstNameDesignateApplicant,
                LastNameDesignateApplicant = viewModel.LastNameDesignateApplicant,
                TitleDesignateApplicant = viewModel.TitleDesignateApplicant,
                EmailAddress = viewModel.EmailAddress
            };

            return customerModel;
        }

        /// <summary>
        /// Map the director/shareholder model from the view model.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private List<DirectorShareholderModel> MapDirectorShareholderModel(List<DirectorShareholderViewModel> viewModel)
        {
            var directorShareholderList = new List<DirectorShareholderModel>();
            if (viewModel != null)
            {
                foreach (var item in viewModel)
                {
                    var directorShareholderModel = new DirectorShareholderModel()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Title = item.Title,
                        PassportNumber = item.PassportNumber,
                        Role = item.Role
                    };
                    directorShareholderList.Add(directorShareholderModel);
                }
            }
            return directorShareholderList;
        }
    }
}
