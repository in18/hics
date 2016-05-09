USE itin18_akt;
GO

CREATE FUNCTION fn_check_admin_scalar( @username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS INT
AS
BEGIN
	DECLARE @result NVARCHAR(50) = NULL;
	DECLARE @user_id INT = NULL;
	DECLARE @return INT = NULL;

	SET @user_id = dbo.fn_check_user_scalar( @username, @password);
		
	SELECT	@result = ug.name
	FROM [User] AS u
		JOIN Userusergroupallocate AS uga
			ON u.id = uga.user_id
				JOIN Usergroup AS ug
					ON uga.usergroup_id = ug.id
	WHERE u.id = @user_id
		
	IF(@result = 'admin')
	BEGIN
		SET @return = 1;
	END

	RETURN @return;
END
GO

USE master;
GO