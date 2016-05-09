--18.4.2

USE itin18_akt;
GO

CREATE FUNCTION fn_show_deleted_roomgroups(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE(id INT, roomgroup_name NVARCHAR(50))
AS
BEGIN
	DECLARE @result INT = NULL;

        SET @result = dbo.fn_check_admin_scalar(@username, @password);

	IF ( @result = 1)
	    
	BEGIN
		INSERT INTO @ret_Table(id,roomgroup_name )
		SELECT	dlg.id,
			rg.name	
		FROM Roomgroup AS rg
		RIGHT JOIN Deletedroomgroup AS dlg
		ON dlg.id = rg.id	

	END
	RETURN;
END;
GO

USE master;
GO