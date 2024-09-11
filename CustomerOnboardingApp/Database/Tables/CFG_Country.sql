-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Create the table CFG_Country to store data for the 
--				different countries.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Create the CFG_Country table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'CFG_Country' AND xtype = 'U')
BEGIN
    CREATE TABLE CFG_Country (
        CountryId INT PRIMARY KEY IDENTITY(1,1),  -- Primary key with auto-increment
        CountryName NVARCHAR(100) NOT NULL        -- Field to store country names
    );
    PRINT 'Table CFG_Country created successfully.';
END
GO