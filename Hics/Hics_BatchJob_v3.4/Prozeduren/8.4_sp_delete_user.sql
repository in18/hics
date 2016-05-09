--8.4
USE itin18_akt;
GO

CREATE PROCEDURE sp_delete_user
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@del_id		  INT
	
AS
BEGIN
    DECLARE @admin_id INT = NULL;
	DECLARE @found_id INT = NULL;

    SET @admin_id = dbo.fn_check_user_scalar(@username,@password);
    IF ( NOT @admin_id IS NULL) AND (@admin_id > 0) AND (dbo.fn_check_admin_scalar(@username,@password) = 1) --Admin/User wird überprüft
	
	BEGIN
	   SELECT @found_id = u.id
	    FROM [User] AS u
            WHERE u.id = @del_id
			AND u.id > 1;
	
  	IF (NOT @found_id IS NULL) AND (@found_id > 0)      
		   BEGIN			
			  INSERT INTO Deleteduser(id,userlifetime)
			  VALUES(@found_id,@admin_id)
  	       END;
	END;
END;

GO

USE master;
GO
