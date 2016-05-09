--18.3.2

USE itin18_akt;
GO

	CREATE FUNCTION fn_show_deleted_lamps(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE(id INT, lamp_name NVARCHAR(50))
AS
BEGIN
	DECLARE @result INT = NULL;

        SET @result = dbo.fn_check_admin_scalar(@username, @password);

	IF ( @result = 1)
	    
	BEGIN
		INSERT INTO @ret_Table(id,lamp_name)
		SELECT	dl.id,
			l.name	
		FROM Lamp AS l
		RIGHT JOIN Deletedlamp AS dl
		ON l.id = dl.id	

	END
	RETURN;
END;
GO

USE master;
GO