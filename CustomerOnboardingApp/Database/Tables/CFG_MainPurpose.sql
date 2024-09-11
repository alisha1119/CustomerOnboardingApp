-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Create the table CFG_MainPurpose to store data for the 
--				different main purposes.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Create the CFG_MainPurpose table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'CFG_MainPurpose' AND xtype = 'U')
BEGIN
    CREATE TABLE CFG_MainPurpose (
        MainPurposeId  INT PRIMARY KEY IDENTITY(1,1),  -- Primary key with auto-increment
        PurposeName NVARCHAR(255) NOT NULL -- Free text field
    );
    PRINT 'Table CFG_MainPurpose created successfully.';
END
GO