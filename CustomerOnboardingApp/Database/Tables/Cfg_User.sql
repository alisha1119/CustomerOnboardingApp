-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 08/09/2024
-- Description:	Create the table Cfg_User to store data of the users.
-- =================================================================
USE CustomerOnboardingDB;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Cfg_User' AND xtype = 'U')
	BEGIN
	CREATE TABLE Cfg_User (
		Id INT IDENTITY(1,1) PRIMARY KEY,  -- Unique identifier for the user
		FirstName NVARCHAR(50) NOT NULL,   -- User's first name
		LastName NVARCHAR(50) NOT NULL,    -- User's last name
		Email NVARCHAR(100) NOT NULL UNIQUE,  -- User's email address
		[Password] NVARCHAR(255) NOT NULL,   -- User's password (hashed)
		[Role] NVARCHAR(50) NOT NULL         -- User's role (e.g., ProcessingTeam, Approver)
	);
		PRINT 'Table Cfg_User created successfully.';
	END
GO