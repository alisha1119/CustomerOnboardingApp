-- ===========================================================================
-- Author:	Alisha Ahamed
-- Create date: 10/09/2024
-- Description:	Create the table Tbl_Document to store customer documents.
-- ===========================================================================
USE CustomerOnboardingDB;
GO

-- Create the Tbl_Customer table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Tbl_Document' AND xtype = 'U')
BEGIN
    CREATE TABLE Tbl_Document (
        DocumentId INT PRIMARY KEY IDENTITY(1,1),					
        CustomerId INT NOT NULL,									
        [DocumentName] NVARCHAR(255) NULL,							 
        DocumentType VARCHAR(50) NULL,			
        CONSTRAINT FK_Customer_Document FOREIGN KEY (CustomerId) REFERENCES Tbl_Customer(CustomerId)
    );
    PRINT 'Table Tbl_Document created successfully.';
END
GO

