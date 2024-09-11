-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Script to insert data in the table CFG_Country.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Insert initial data into CFG_Country
IF NOT EXISTS (SELECT * FROM CFG_Country WHERE CountryName = 'Mauritius')
BEGIN
    INSERT INTO CFG_Country (CountryName) VALUES 
    ('Mauritius')
    PRINT 'Initial data inserted into CFG_Country.';
END

IF NOT EXISTS (SELECT * FROM CFG_Country WHERE CountryName = 'France')
BEGIN
    INSERT INTO CFG_Country (CountryName) VALUES 
    ('France')
    PRINT 'Initial data inserted into CFG_Country.';
END

IF NOT EXISTS (SELECT * FROM CFG_Country WHERE CountryName = 'Germany')
BEGIN
    INSERT INTO CFG_Country (CountryName) VALUES 
    ('Germany')
    PRINT 'Initial data inserted into CFG_Country.';
END

GO