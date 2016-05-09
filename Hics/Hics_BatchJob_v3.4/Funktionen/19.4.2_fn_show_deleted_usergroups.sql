--19.4.2

USE itin18_akt;
GO

CREATE FUNCTION fn_show_deleted_usergroups(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE(id INT, usergroup_name NVARCHAR(50))
AS
BEGIN
	DECLARE @result INT = NULL;

        SET @result = dbo.fn_check_admin_scalar(@username, @password);

	IF ( @result = 1)
	    
	BEGIN
		INSERT INTO @ret_Table(id,usergroup_name)
		SELECT	dug.id,
			ug.name	
		FROM Usergroup AS ug
		RIGHT JOIN Deletedusergroup AS dug
		ON ug.id = dug.id	

	END
	RETURN;
END;
GO

USE master;
GO