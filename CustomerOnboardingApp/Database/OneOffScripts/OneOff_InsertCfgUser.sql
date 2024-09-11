-- =================================================================
-- Author:	Alisha Ahamed
-- Create date: 08/09/2024
-- Description:	Script to insert data in the table Cfg_Users.
-- =================================================================
USE CustomerOnboardingDB;
GO

INSERT INTO Cfg_User (FirstName, LastName, Email, [Password], [Role])
VALUES
    ('Alice', 'Smith', 'alice.smith@example.com', 'K9gGyX8OAK8aH8Myj6djqSaXI8jbj6xPk69x2xhtbpA=', 'ProcessingTeam'),
    ('Bob', 'Johnson', 'bob.johnson@example.com', 'gbY32PzSxtpjWeaWMROhFw3nleS3JbhNHgtM/Z7FjOk=', 'Approver');