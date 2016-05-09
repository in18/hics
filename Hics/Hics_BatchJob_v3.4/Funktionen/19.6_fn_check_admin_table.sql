USE itin18_akt;
GO

CREATE FUNCTION fn_check_admin_table( @username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @retTable TABLE(id INT NOT NULL)
AS
BEGIN
	DECLARE @result VARCHAR(50) = '';
	DECLARE @user_id INT = NULL;
	DECLARE @return INT = NULL;
	
	SELECT TOP 1 @user_id = id
	FROM dbo.fn_check_user_table(@username, @password)

	SELECT	@result = ug.name
	FROM [User] AS u
		JOIN Userusergroupallocate AS uga
			ON u.id = uga.user_id
				JOIN Usergroup AS ug
					ON uga.usergroup_id = ug.id
	WHERE u.id = @user_id
	
	IF(@result = 'admin')
	BEGIN
		INSERT INTO @retTable(id) VALUES (1);
	END
	IF(@result = '')
	BEGIN
		INSERT INTO @retTable(id) VALUES (0);
	END
	RETURN;
END;
GO

USE master;
GO