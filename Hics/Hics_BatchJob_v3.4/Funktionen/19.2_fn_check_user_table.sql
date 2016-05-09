USE itin18_akt;
GO

CREATE FUNCTION fn_check_user_table( @username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @retTable TABLE(id INT NOT NULL)
AS
BEGIN
	INSERT INTO @retTable(id)
	SELECT TOP 1 u.id
	FROM [User] AS u
	WHERE u.name = @username
	AND u.password = dbo.fn_hash_password(@password);
	
	RETURN;
END;
GO

USE master;
GO