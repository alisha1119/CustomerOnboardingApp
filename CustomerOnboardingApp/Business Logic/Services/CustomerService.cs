using CustomerOnboardingApp.Business_Logic.Enums;
using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Xml.Linq;

namespace CustomerOnboardingApp.Business_Logic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of the main purposes for the dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownModel> GetMainPurposesList()
        {
            var sqlQuery = @"SELECT 
                                MainPurposeId,  
                                PurposeName
                            FROM CFG_MainPurpose";

            return _context.CfgMainPurposes
                           .FromSqlRaw(sqlQuery)
                           .Select(p => new DropdownModel
                           {
                               DropdownId = p.MainPurposeId,
                               DropdownValue = p.PurposeName
                           }).ToList();
        }

        /// <summary>
        /// Get list of type of entities for the dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownModel> GetTypeOfEntityList()
        {
            var sqlQuery = @"SELECT 
                                TypeOfEntityId,  
                                EntityTypeName
                            FROM CFG_TypeOfEntity";

            return _context.CfgTypeOfEntities
                           .FromSqlRaw(sqlQuery)
                           .Select(e => new DropdownModel
                           {
                               DropdownId = e.TypeOfEntityId,
                               DropdownValue = e.EntityTypeName
                           }).ToList();
        }

        /// <summary>
        /// Get list of business activities for the dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownModel> GetBusinessActivityList()
        {
            var sqlQuery = @"SELECT 
                                BusinessActivityId,  
                                BusinessActivityName
                            FROM CFG_BusinessActivity";

            return _context.CfgBusinessActivities
                           .FromSqlRaw(sqlQuery)
                           .Select(b => new DropdownModel
                           {
                               DropdownId = b.BusinessActivityId,
                               DropdownValue = b.BusinessActivityName
                           }).ToList();
        }

        /// <summary>
        /// Get list of countries for the dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownModel> GetCountryList()
        {
            var sqlQuery = @"SELECT 
                                CountryId,  
                                CountryName
                            FROM CFG_Country";

            return _context.CfgCountries
                           .FromSqlRaw(sqlQuery)
                           .Select(c => new DropdownModel
                           {
                               DropdownId = c.CountryId,
                               DropdownValue = c.CountryName
                           }).ToList();
        }

        /// <summary>
        /// Create a new customer by saving it
        /// in the Tbl_Customer table.
        /// </summary>
        /// <param name="customerModel"></param>
        /// <param name="directorShareholderModelList"></param>
        /// <returns></returns>
        public int CreateCustomer(CustomerModel customerModel, List<DirectorShareholderModel> directorShareholderModelList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var customerEntity = new TblCustomer
                    {
                        MainPurposeId = customerModel.MainPurposeId,
                        CompanyName = customerModel.NameOfCompany,
                        TypeOfEntityId = customerModel.TypeOfEntityId,
                        BusinessActivityId = customerModel.BusinessActivityId,
                        LicenseRequirement = customerModel.LicenseRequirements,
                        CountryId = customerModel.CountryId,
                        RegistrationNumber = customerModel.RegistrationNumber,
                        DateOfIncorporation = customerModel.DateOfIncorporation,
                        DesignatedApplicantFirstName = customerModel.FirstNameDesignateApplicant,
                        DesignatedApplicantLastName = customerModel.LastNameDesignateApplicant,
                        DesignatedApplicantTitle = customerModel.TitleDesignateApplicant,
                        EmailAddress = customerModel.EmailAddress
                    };

                    _context.TblCustomers.Add(customerEntity);

                    // Save changes to the database
                    _context.SaveChanges();
                    if (directorShareholderModelList != null)
                    {
                        CreateDirectorShareholder(directorShareholderModelList, customerEntity.CustomerId);
                    }
                    
                    transaction.Commit();

                    return customerEntity.CustomerId; 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Create a new director/shareholder by saving it
        /// in the Tbl_DirectorShareholder table.
        /// </summary>
        /// <param name="directorShareholderModelList"></param>
        /// <param name="customerId"></param>
        private void CreateDirectorShareholder(List<DirectorShareholderModel> directorShareholderModelList, int customerId)
        {
            foreach (var item in directorShareholderModelList)
            {
                var directorShareholderEntity = new TblDirectorShareholder
                {
                    Title = item.Title,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PassportNumber = item.PassportNumber,
                    CustomerId = customerId,
                    Role = item.Role
                };
                _context.TblDirectorShareholders.Add(directorShareholderEntity);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Save the document data in Tbl_Document
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="documentName"></param>
        /// <param name="eDocumentType"></param>
        public void SaveDocumentsData(int customerId, string documentName, EDocumentType eDocumentType)
        {
            var documentEntity = new TblDocument
            {
                CustomerId = customerId,
                DocumentName = documentName,
                DocumentType = eDocumentType.ToString()
            };
            _context.TblDocuments.Add(documentEntity);
            _context.SaveChanges();
        }
    }
}
