-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Script to insert data in the table CFG_TypeOfEntity.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Insert initial data into CFG_TypeOfEntity
IF NOT EXISTS (SELECT * FROM CFG_TypeOfEntity WHERE EntityTypeName = 'Trust')
BEGIN
    INSERT INTO CFG_TypeOfEntity (EntityTypeName) VALUES 
    ('Trust')
    PRINT 'Initial data inserted into CFG_TypeOfEntity.';
END

IF NOT EXISTS (SELECT * FROM CFG_TypeOfEntity WHERE EntityTypeName = 'Association')
BEGIN
    INSERT INTO CFG_TypeOfEntity (EntityTypeName) VALUES 
    ('Association')
    PRINT 'Initial data inserted into CFG_TypeOfEntity.';
END

IF NOT EXISTS (SELECT * FROM CFG_TypeOfEntity WHERE EntityTypeName = 'Private Company')
BEGIN
    INSERT INTO CFG_TypeOfEntity (EntityTypeName) VALUES 
    ('Private Company')
    PRINT 'Initial data inserted into CFG_TypeOfEntity.';
END

GO