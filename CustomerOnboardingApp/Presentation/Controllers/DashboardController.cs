using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Business_Logic.Services;
using CustomerOnboardingApp.Presentation.ViewModels.Customer;
using CustomerOnboardingApp.Presentation.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;



namespace CustomerOnboardingApp.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly ICustomerService _customerService;

        public DashboardController(IDashboardService dashboardService, ICustomerService customerService)
        {
            _dashboardService = dashboardService;
            _customerService = customerService;
        }
        /// <summary>
        /// Display the dashboard once user is connected
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult Index(int userId)
        {
            var submittedCustomerList = new List<SubmittedCustomerModel>();
            var userRole = _dashboardService.GetUserRole(userId);
            if (userRole == "ProcessingTeam")
            {
                submittedCustomerList = _dashboardService.GetSubmittedCustomerList();
            }
            else if(userRole == "Approver")
            {
                submittedCustomerList = _dashboardService.GetProcessedCustomerList();
            }
            var viewModel = CreateDashboardViewModel(userId, submittedCustomerList);
            viewModel.UserRole = userRole;
            return View(viewModel);
        }

        /// <summary>
        /// Create the dashboard view model
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="submittedCustomerList"></param>
        /// <returns></returns>
        private DasboardViewModel CreateDashboardViewModel(int userId, List<SubmittedCustomerModel> submittedCustomerList)
        {
            var viewModel = new DasboardViewModel()
            {
                UserId = userId,
                SubmittedCustomerList = submittedCustomerList
            };
            return viewModel;

        }

        /// <summary>
        /// Display the edit customer page
        /// for user to process or reject the customer application
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IActionResult EditCustomer(int customerId, bool isReadOnly)
        {
            var customerModel = _dashboardService.GetCustomerModel(customerId);
            var customerViewModel = MapSubmittedCustomersViewModel(customerModel);
            customerViewModel.MainPurposeList = _customerService.GetMainPurposesList();
            customerViewModel.TypeOfEntityList = _customerService.GetTypeOfEntityList();
            customerViewModel.BusinessActivityList = _customerService.GetBusinessActivityList();
            customerViewModel.CountryList = _customerService.GetCountryList();
            var directorShareholderList = _dashboardService.GetDirectorShareholderList(customerId); 
            MapDirectorShareHolderViewModel(customerViewModel, directorShareholderList);
            var documentList = _dashboardService.GetDocumentList(customerId);
            MapDocumentViewModel(customerViewModel, documentList);
            customerViewModel.IsReadOnly = isReadOnly;
            return View(customerViewModel);
        }

        /// <summary>
        /// Map the submitted customer view model from the model
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        private CustomerViewModel MapSubmittedCustomersViewModel(CustomerModel customerModel)
        {
            var customerViewModel = new CustomerViewModel()
            {
                CustomerId = customerModel.CutomerId,
                MainPurposeId = customerModel.MainPurposeId,
                NameOfCompany = customerModel.NameOfCompany,
                TypeOfEntityId = customerModel.TypeOfEntityId,
                BusinessActivityId = customerModel.BusinessActivityId,
                LicenseRequirements = customerModel.LicenseRequirements,
                CountryId = customerModel.CountryId,
                RegistrationNumber = customerModel.RegistrationNumber,
                DateOfIncorporation = customerModel.DateOfIncorporation,
                FirstNameDesignateApplicant = customerModel.FirstNameDesignateApplicant,
                LastNameDesignateApplicant = customerModel.LastNameDesignateApplicant,
                TitleDesignateApplicant = customerModel.TitleDesignateApplicant,
                EmailAddress = customerModel.EmailAddress,
            };
            return customerViewModel;
        }

        /// <summary>
        /// Map the director/shareholder view model
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <param name="directorShareholderList"></param>
        private void MapDirectorShareHolderViewModel(CustomerViewModel customerViewModel, List<DirectorShareholderModel> directorShareholderList)
        {
            var directorShareholderViewModelList = new List<DirectorShareholderViewModel>();
            foreach (var item in directorShareholderList)
            {
                var directorShareholderViewModel = new DirectorShareholderViewModel()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PassportNumber = item.PassportNumber,
                    Title = item.Title,
                    Role = item.Role
                };
                directorShareholderViewModelList.Add(directorShareholderViewModel);
            }
            customerViewModel.DirectorShareholderList = directorShareholderViewModelList;
        }

        /// <summary>
        /// Process the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IActionResult ProcessCustomer(int customerId) {
            _dashboardService.ProcessCustomer(customerId);
            return View("ProcessCustomer"); 
        }

        /// <summary>
        /// Reject the customer
        /// by the processing team
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IActionResult RejectSubmittedCustomer(int customerId)
        {
            _dashboardService.RejectSubmittedCustomer(customerId);
            return View("RejectSubmittedCustomer");
        }

        /// <summary>
        /// Approve the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IActionResult ApproveCustomer(int customerId)
        {
            _dashboardService.ApproveCustomer(customerId);
            return View("ApproveCustomer");
        }

        /// <summary>
        /// Reject the customer by the approver
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IActionResult RejectProcessedCustomer(int customerId)
        {
            _dashboardService.RejectProcessedCustomer(customerId);
            return View("RejectProcessedCustomer");
        }

        /// <summary>
        /// Map the document view model
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="model"></param>
        private void MapDocumentViewModel(CustomerViewModel customerViewModel, List<DocumentModel> documentList)
        {
            var documentViewModelList = new List<DocumentViewModel>();
            foreach (var item in documentList)
            {
                var documentViewModel = new DocumentViewModel()
                {
                    DocumentId = item.DocumentId,
                    DocumentName = item.DocumentName,
                    DocumentType = item.DocumentType
                };
                documentViewModelList.Add(documentViewModel);
            }
            customerViewModel.DocumentList = documentViewModelList;
        }

        /// <summary>
        /// Get the document for viewing
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewDocument(string fileName, int customerId)
        {
            string customerFolder = "upload_" + customerId.ToString();
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", customerFolder);
            string filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var mimeType = GetMimeType(fileName); 
                //To view the document in the browser
                Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}");
                return File(fileBytes, mimeType);
            }
            else
            {
                // If the file is not found, return a 404
                return NotFound(); 
            }
        }

        public string GetMimeType(string fileName)
        {
            var mimeTypes = new Dictionary<string, string>
            {
                { ".pdf", "application/pdf" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" }
            };

            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return mimeTypes.ContainsKey(ext) ? mimeTypes[ext] : "application/octet-stream";
        }
    }
}
