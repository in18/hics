USE itin18_akt;
GO

CREATE PROCEDURE sp_add_user
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@new_username NVARCHAR(50),
	@new_password VARBINARY(MAX)
AS
BEGIN
	DECLARE @result INT = NULL;
	DECLARE @user_id INT = NULL;
	DECLARE @user_units INT = NULL;

	SELECT @user_units = COUNT(u.id) 
	FROM [User] AS u
	
	SET @user_id = dbo.fn_check_user_scalar(@username,@password);
	SET @result = dbo.fn_check_admin_scalar(@username,@password);

	IF(@user_units < 2) AND (@result IS NULL) AND (@user_id IS NULL) AND (@new_username = 'admin')
	BEGIN
		SET @result = 1;
	END;
	IF(@result = 1)
	BEGIN
		INSERT INTO [User](name, password)
		VALUES(@new_username , dbo.fn_hash_password(@new_password) )
	END;

END
GO

USE master;
GO