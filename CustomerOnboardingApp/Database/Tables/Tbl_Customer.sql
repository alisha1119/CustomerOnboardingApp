-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Create the table Tbl_Customer to store customer data.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Create the Tbl_Customer table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Tbl_Customer' AND xtype = 'U')
BEGIN
    CREATE TABLE Tbl_Customer (
        CustomerId INT PRIMARY KEY IDENTITY(1,1),					-- Primary key with auto-increment
        MainPurposeId INT NOT NULL,									-- Foreign key to CFG_MainPurpose
        CompanyName NVARCHAR(255) NOT NULL,							 -- Free text field for company name
        TypeOfEntityId INT NOT NULL,								 -- Foreign key to CFG_TypeOfEntity
        BusinessActivityId INT NOT NULL,							 -- Foreign key to CFG_BusinessActivity
        LicenseRequirement NVARCHAR(255),							-- Text field for license requirements
        CountryId INT NOT NULL,										-- Foreign key to CFG_Country
        RegistrationNumber NVARCHAR(50) NOT NULL,					-- Registration number
        DateOfIncorporation DATE NOT NULL,                          -- Date of incorporation
        DesignatedApplicantTitle NVARCHAR(10) NOT NULL,              -- Title of designated applicant
        DesignatedApplicantFirstName NVARCHAR(100) NOT NULL,		-- First name of designated applicant
        DesignatedApplicantLastName NVARCHAR(100) NOT NULL,			 -- Last name of designated applicant
        EmailAddress NVARCHAR(255) NOT NULL,						 -- Email address 
		IsProcessed BIT NULL,
		IsApproved BIT NULL
        CONSTRAINT FK_MainPurpose FOREIGN KEY (MainPurposeId) REFERENCES CFG_MainPurpose(MainPurposeId),
        CONSTRAINT FK_TypeOfEntity FOREIGN KEY (TypeOfEntityId) REFERENCES CFG_TypeOfEntity(TypeOfEntityId),
        CONSTRAINT FK_BusinessActivity FOREIGN KEY (BusinessActivityId) REFERENCES CFG_BusinessActivity(BusinessActivityId),
        CONSTRAINT FK_Country FOREIGN KEY (CountryId) REFERENCES CFG_Country(CountryId)
    );
    PRINT 'Table Tbl_Customer created successfully.';
END
GO