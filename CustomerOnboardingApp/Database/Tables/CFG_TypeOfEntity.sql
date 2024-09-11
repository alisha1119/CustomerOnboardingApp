-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Create the table CFG_TypeOfEntity to store data for the 
--				different type of entities.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Create the CFG_TypeOfEntity table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'CFG_TypeOfEntity' AND xtype = 'U')
BEGIN
    CREATE TABLE CFG_TypeOfEntity (
        TypeOfEntityId INT PRIMARY KEY IDENTITY(1,1),  -- Primary key with auto-increment
        EntityTypeName NVARCHAR(150) NOT NULL          -- Field to store entity type text
    );
    PRINT 'Table CFG_TypeOfEntity created successfully.';
END
GO