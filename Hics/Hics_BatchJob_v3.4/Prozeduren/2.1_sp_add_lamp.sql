USE itin18_akt;
GO

CREATE PROCEDURE sp_add_lamp
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@address VARCHAR(50),
	@name NVARCHAR(50)
AS
BEGIN
		DECLARE @user_id INT = NULL;
		DECLARE @result INT = NULL;

		SET @user_id = dbo.fn_check_user_scalar(@username,@password);
		SET @result = dbo.fn_check_admin_scalar(@username,@password);

		IF(@result = 1)
	    BEGIN
			INSERT INTO Lamp([address], name, userlifetime)
			VALUES(@address, @name, @user_id);
		END
END
GO

USE MASTER;
GO


