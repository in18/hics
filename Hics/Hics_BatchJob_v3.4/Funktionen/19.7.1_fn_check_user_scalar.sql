USE itin18_akt;
GO

CREATE FUNCTION fn_check_user_scalar( @username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS INT
AS
BEGIN
	DECLARE @result INT = NULL;
	SELECT TOP 1 @result = u.id
	FROM [User] AS u
	WHERE u.name = @username
	AND u.password = dbo.fn_hash_password(@password);
	
	RETURN @result;
END;
GO

USE master;
GO