-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Script to insert data in the table CFG_BusinessActivity.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Insert initial data into CFG_BusinessActivity
IF NOT EXISTS (SELECT * FROM CFG_BusinessActivity WHERE BusinessActivityName = 'Banking')
BEGIN
    INSERT INTO CFG_BusinessActivity (BusinessActivityName) VALUES 
    ('Banking')
    PRINT 'Initial data inserted into CFG_BusinessActivity.';
END

IF NOT EXISTS (SELECT * FROM CFG_BusinessActivity WHERE BusinessActivityName = 'Fishing')
BEGIN
    INSERT INTO CFG_BusinessActivity (BusinessActivityName) VALUES 
    ('Fishing')
    PRINT 'Initial data inserted into CFG_BusinessActivity.';
END

IF NOT EXISTS (SELECT * FROM CFG_BusinessActivity WHERE BusinessActivityName = 'Manufacturing')
BEGIN
    INSERT INTO CFG_BusinessActivity (BusinessActivityName) VALUES 
    ('Manufacturing')
    PRINT 'Initial data inserted into CFG_BusinessActivity.';
END

GO