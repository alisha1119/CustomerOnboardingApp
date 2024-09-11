using CustomerOnboardingApp.Business_Logic.Models;
using CustomerOnboardingApp.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Metrics;

namespace CustomerOnboardingApp.Business_Logic.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the user role from the user Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserRole(int userId)
        {
            var sqlQuery = @"SELECT 
	                           [Role]
                            FROM Cfg_User
                            WHERE Id = @userId";
            return _context.CfgUsers
                         .FromSqlRaw(sqlQuery, new SqlParameter("@userId", userId))
                         .Select(p => p.Role)
                         .First();
        }

        /// <summary>
        /// Get list of submitted customers for approval
        /// </summary>
        /// <returns></returns>
        public List<SubmittedCustomerModel> GetSubmittedCustomerList()
        {
            var submittedCustomers = new List<SubmittedCustomerModel>();
            var sqlQuery = @"SELECT
                                Cus.CustomerId AS CustomerId,
                                Cus.CompanyName AS CompanyName,
	                            Ba.BusinessActivityName AS BusinessActivity,
	                            Te.EntityTypeName AS TypeOfEntity,
	                            Co.CountryName AS Country,
	                            Cus.RegistrationNumber As RegistrationNumber
                            FROM Tbl_Customer Cus
                            INNER JOIN CFG_BusinessActivity Ba ON Cus.BusinessActivityId = Ba.BusinessActivityId
                            INNER JOIN CFG_TypeOfEntity Te ON Cus.TypeOfEntityId = Te.TypeOfEntityId
                            INNER JOIN CFG_Country Co ON Cus.CountryId = Co.CountryId
                            WHERE Cus.IsProcessed IS NULL";

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new SubmittedCustomerModel
                            {
                                CustomerId = reader.GetInt32(0),
                                CompanyName = reader.GetString(1),
                                BusinessActivity = reader.GetString(2),
                                TypeOfEntity = reader.GetString(3),
                                Country = reader.GetString(4),
                                RegistrationNumber = reader.GetString(5)
                            };

                            submittedCustomers.Add(customer);
                        }
                    }
                }
                connection.Close();
            }

            return submittedCustomers;
        }

        /// <summary>
        /// Get the customer model from the customer Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerModel GetCustomerModel(int customerId)
        {
            var sqlQuery = @"SELECT 

	                            Cus.CustomerId AS CustomerId,
	                            Cus.MainPurposeId AS MainPurposeId,
	                            Cus.CompanyName AS CompanyName,
	                            Cus.TypeOfEntityId AS TypeOfEntityId,
	                            Cus.BusinessActivityId AS BusinessActivityId,
	                            Cus.LicenseRequirement AS LicenseRequirement,
	                            Cus.CountryId AS CountryId,
	                            Cus.RegistrationNumber As RegistrationNumber,
	                            Cus.DateOfIncorporation AS DateOfIncorporation,
	                            Cus.DesignatedApplicantFirstName AS DesignatedApplicantFirstName,
	                            Cus.DesignatedApplicantLastName AS DesignatedApplicantLastName,
	                            Cus.DesignatedApplicantTitle AS DesignatedApplicantTitle,
	                            Cus.EmailAddress As EmailAddress

                            FROM Tbl_Customer Cus
                            WHERE Cus.CustomerId = @customerId";

            return _context.TblCustomers
                           .FromSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId))
                           .Select(p => new CustomerModel
                           {
                               CutomerId = p.CustomerId,
                               MainPurposeId = p.MainPurposeId,
                               NameOfCompany = p.CompanyName,
                               TypeOfEntityId = p.TypeOfEntityId,
                               BusinessActivityId = p.BusinessActivityId,
                               LicenseRequirements = p.LicenseRequirement,
                               CountryId = p.CountryId,
                               RegistrationNumber = p.RegistrationNumber,
                               DateOfIncorporation = p.DateOfIncorporation,
                               FirstNameDesignateApplicant = p.DesignatedApplicantFirstName,
                               LastNameDesignateApplicant = p.DesignatedApplicantLastName,
                               TitleDesignateApplicant = p.DesignatedApplicantTitle,
                               EmailAddress = p.EmailAddress
                           }).First();
        }

        /// <summary>
        /// Get a list of shareholder/directors for a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<DirectorShareholderModel> GetDirectorShareholderList(int customerId)
        {
            var sqlQuery = @"SELECT 
	                            DS.DirectorShareholderId AS DirectorShareholderId,
	                            DS.FirstName AS FirstName,
	                            DS.LastName AS LastName,
	                            DS.PassportNumber AS PassportNumber,
                                DS.Title AS Title,
                                DS.Role AS [Role]
                            FROM Tbl_DirectorShareholder DS
                            WHERE DS.CustomerId = @customerId";

            return _context.TblDirectorShareholders
                           .FromSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId))
                           .Select(p => new DirectorShareholderModel
                           {
                              Id = p.DirectorShareholderId,
                              FirstName = p.FirstName,
                              LastName = p.LastName,
                              PassportNumber = p.PassportNumber,
                              Title = p.Title,
                              Role = p.Role
                           }).ToList();
        }

        /// <summary>
        /// Process the customer
        /// </summary>
        /// <param name="customerId"></param>
        public void ProcessCustomer(int customerId)
        {
            var sqlQuery = "UPDATE Tbl_Customer SET IsProcessed = 1 WHERE CustomerId = @customerId";
            _context.Database
                           .ExecuteSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId));
        }

        /// <summary>
        /// Reject the customer by the processing team
        /// </summary>
        /// <param name="customerId"></param>
        public void RejectSubmittedCustomer(int customerId)
        {
            var sqlQuery = "UPDATE Tbl_Customer SET IsProcessed = 0 WHERE CustomerId = @customerId";
            _context.Database
                           .ExecuteSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId));
        }

        /// <summary>
        /// Get list of processed customers
        /// </summary>
        /// <returns></returns>
        public List<SubmittedCustomerModel> GetProcessedCustomerList()
        {
            var submittedCustomers = new List<SubmittedCustomerModel>();
            var sqlQuery = @"SELECT
                                Cus.CustomerId AS CustomerId,
                                Cus.CompanyName AS CompanyName,
	                            Ba.BusinessActivityName AS BusinessActivity,
	                            Te.EntityTypeName AS TypeOfEntity,
	                            Co.CountryName AS Country,
	                            Cus.RegistrationNumber As RegistrationNumber
                            FROM Tbl_Customer Cus
                            INNER JOIN CFG_BusinessActivity Ba ON Cus.BusinessActivityId = Ba.BusinessActivityId
                            INNER JOIN CFG_TypeOfEntity Te ON Cus.TypeOfEntityId = Te.TypeOfEntityId
                            INNER JOIN CFG_Country Co ON Cus.CountryId = Co.CountryId
                            WHERE Cus.IsProcessed = 1
                            AND Cus.IsApproved IS NULL";

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new SubmittedCustomerModel
                            {
                                CustomerId = reader.GetInt32(0),
                                CompanyName = reader.GetString(1),
                                BusinessActivity = reader.GetString(2),
                                TypeOfEntity = reader.GetString(3),
                                Country = reader.GetString(4),
                                RegistrationNumber = reader.GetString(5)
                            };

                            submittedCustomers.Add(customer);
                        }
                    }
                }
                connection.Close();
            }

            return submittedCustomers;
        }

        /// <summary>
        /// Approve a customer
        /// </summary>
        /// <param name="customerId"></param>
        public void ApproveCustomer(int customerId)
        {
            var sqlQuery = "UPDATE Tbl_Customer SET IsApproved = 1 WHERE CustomerId = @customerId";
            _context.Database
                           .ExecuteSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId));
        }

        /// <summary>
        /// Reject a customer by approver
        /// </summary>
        /// <param name="customerId"></param>
        public void RejectProcessedCustomer(int customerId)
        {
            var sqlQuery = "UPDATE Tbl_Customer SET IsApproved = 0 WHERE CustomerId = @customerId";
            _context.Database
                           .ExecuteSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId));
        }

        /// <summary>
        /// Get the document list from the customer Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<DocumentModel> GetDocumentList(int customerId)
        {
            var sqlQuery = @"SELECT 
	                            DO.DocumentId AS DocumentId,
	                            DO.DocumentName AS DocumentName,
	                            DO.DocumentType AS DocumentType

                            FROM Tbl_Document DO
                            WHERE DO.CustomerId = @customerId";

            return _context.TblDocuments
                           .FromSqlRaw(sqlQuery, new SqlParameter("@customerId", customerId))
                           .Select(p => new DocumentModel
                           {
                               DocumentId = p.DocumentId,
                               DocumentName = p.DocumentName,
                               DocumentType =  p.DocumentType
                           }).ToList();
        }
    }
}
