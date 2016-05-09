--19.3.2

USE itin18_akt;
GO

CREATE FUNCTION fn_show_deleted_users(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE(id INT, user_name NVARCHAR(50))
AS
BEGIN
	DECLARE @result INT = NULL;

        SET @result = dbo.fn_check_admin_scalar(@username, @password);

	IF ( @result = 1)
	    
	BEGIN
		INSERT INTO @ret_Table(id,user_name)
		SELECT	du.id,
				u.name	
		FROM [User] AS u
		RIGHT JOIN Deleteduser AS du
		ON u.id = du.id	

	END
	RETURN;
END;
GO

USE master;
GO