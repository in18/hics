-- 7.1_sp_delete_roomgroup
USE itin18_akt;
GO

CREATE PROCEDURE sp_delete_roomgroup
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@roomgroup_id INT	
AS
BEGIN
	-- anlegen und deklarieren von Variablen
	DECLARE @user_id INT = NULL;
	DECLARE @result INT = NULL;
	DECLARE @check INT = NULL;

    SET @user_id = dbo.fn_check_user_scalar(@username,@password);		
    SET @result = dbo.fn_check_admin_scalar(@username,@password);
	
	IF (@result = 1) --Admin/User wird überprüft
	BEGIN
			SELECT @check = rg.id
			FROM Roomgroup AS rg
			WHERE @roomgroup_id = rg.id
	
  		   IF (NOT @check IS NULL)    
		   BEGIN			
			  INSERT INTO Deletedroomgroup(id,userlifetime)
			  VALUES(@roomgroup_id,@user_id)
  	       END;
	END;
END;
GO

USE master;
GO