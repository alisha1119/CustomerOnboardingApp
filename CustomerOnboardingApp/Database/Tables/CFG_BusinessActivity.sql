-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Create the table CFG_BusinessActivity to store data for the 
--				different type of entities.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Create the CFG_BusinessActivity table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'CFG_BusinessActivity' AND xtype = 'U')
BEGIN
    CREATE TABLE CFG_BusinessActivity (
        BusinessActivityId INT PRIMARY KEY IDENTITY(1,1),  -- Primary key with auto-increment
        BusinessActivityName NVARCHAR(50) NOT NULL         -- Field to store business activity text
    );
    PRINT 'Table CFG_BusinessActivity created successfully.';
END
GO