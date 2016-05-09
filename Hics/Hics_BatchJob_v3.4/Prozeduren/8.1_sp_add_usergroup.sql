USE itin18_akt;
GO

CREATE PROCEDURE sp_add_usergroup
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@gourpname NVARCHAR(50)
AS
BEGIN
		DECLARE @user_id INT = NULL;
		DECLARE @result INT = NULL;

		SET @user_id = dbo.fn_check_user_scalar(@username,@password);
		SET @result = dbo.fn_check_admin_scalar(@username,@password);

		IF(@result = 1)
		BEGIN
			INSERT INTO Usergroup(name, userlifetime)
	            VALUES(@gourpname, @user_id);
		END
END
GO

USE MASTER;
GO