-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Create the table Tbl_DirectorShareholder to store 
--				directors/shareholders info.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Create the Tbl_DirectorShareholder table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Tbl_DirectorShareholder' AND xtype = 'U')
BEGIN
    CREATE TABLE Tbl_DirectorShareholder (
        DirectorShareholderId INT PRIMARY KEY IDENTITY(1,1),     -- Primary key with auto-increment
        CustomerId INT NOT NULL,                                  -- Foreign key to Tbl_Customer
        Title NVARCHAR(10) NOT NULL,                                    -- Foreign key to CFG_Title
        FirstName NVARCHAR(100) NOT NULL,                        -- First name
        LastName NVARCHAR(100) NOT NULL,                         -- Last name
        PassportNumber NVARCHAR(50) NOT NULL,                    -- Passport number
        [Role] NVARCHAR(50),                                       -- Role (Director/Shareholder)
        CONSTRAINT FK_Customer_DirectorShareholder
            FOREIGN KEY (CustomerId) REFERENCES Tbl_Customer(CustomerId)
    );
    PRINT 'Table Tbl_DirectorShareholder created successfully.';
END
GO