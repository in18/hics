USE itin18_akt;
GO

CREATE PROCEDURE sp_delete_lamp
	@lamp_id  INT,
	@username NVARCHAR(50),
	@password VARBINARY(MAX)
AS
BEGIN
    DECLARE @user_id INT = NULL;
	DECLARE @found_nr INT = NULL;
	
	SET @user_id = dbo.fn_check_user_scalar(@username,@password);
	
	IF ( NOT @user_id IS NULL) AND (@user_id > 0)
		BEGIN
		SET @found_nr = dbo.fn_check_admin_scalar(@username,@password);
		END;
	
	IF (@found_nr = 1) AND ( NOT @user_id IS NULL) AND (@user_id > 0)
		BEGIN			
			INSERT INTO Deletedlamp(id, userlifetime)
			VALUES(@lamp_id, @user_id)
  		END;	
END;
GO

USE MASTER;
GO
