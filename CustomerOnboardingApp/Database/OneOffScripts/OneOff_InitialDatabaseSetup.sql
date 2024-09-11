-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 07/09/2024
-- Description:	Get the database CustomerOnboardingDB
-- =================================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CustomerOnboardingDB')
BEGIN
    -- Create the database
    CREATE DATABASE CustomerOnboardingDB;
    PRINT 'Database CustomerOnboardingDB created successfully.';
END