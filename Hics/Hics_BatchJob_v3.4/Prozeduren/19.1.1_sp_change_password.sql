USE itin18_akt;
GO

CREATE PROCEDURE sp_change_password
	@name NVARCHAR(50),
	@oldpassword VARBINARY(MAX),
	@newpassword VARBINARY(MAX)
AS
BEGIN
		UPDATE [user]
		SET [password] = dbo.fn_hash_password(@newpassword)
		WHERE name = @name
		AND [password] = dbo.fn_hash_password(@oldpassword)	
END
GO

USE master;
GO