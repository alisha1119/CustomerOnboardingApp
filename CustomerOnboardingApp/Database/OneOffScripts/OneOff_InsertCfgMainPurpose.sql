-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Script to insert data in the table CFG_MainPurpose.
-- =================================================================
USE CustomerOnboardingDB;
GO

-- Insert initial data into CFG_MainPurpose
IF NOT EXISTS (SELECT * FROM CFG_MainPurpose WHERE PurposeName = 'Investment portfolio')
BEGIN
    INSERT INTO CFG_MainPurpose (PurposeName) VALUES 
    ('Investment portfolio')
    PRINT 'Initial data inserted into CFG_MainPurpose.';
END

IF NOT EXISTS (SELECT * FROM CFG_MainPurpose WHERE PurposeName = 'Account to operate locally')
BEGIN
    INSERT INTO CFG_MainPurpose (PurposeName) VALUES 
    ('Account to operate locally')
    PRINT 'Initial data inserted into CFG_MainPurpose.';
END

IF NOT EXISTS (SELECT * FROM CFG_MainPurpose WHERE PurposeName = 'Account to operate overseas')
BEGIN
    INSERT INTO CFG_MainPurpose (PurposeName) VALUES 
    ('Account to operate overseas')
    PRINT 'Initial data inserted into CFG_MainPurpose.';
END

IF NOT EXISTS (SELECT * FROM CFG_MainPurpose WHERE PurposeName = 'Energy & commodities financing')
BEGIN
    INSERT INTO CFG_MainPurpose (PurposeName) VALUES 
    ('Energy & commodities financing')
    PRINT 'Initial data inserted into CFG_MainPurpose.';
END
GO