USE [itin18_akt]
GO

CREATE PROCEDURE sp_delete_user_from_usergroup
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@user_id INT,
	@usergroup_id INT
AS
BEGIN
		DECLARE @result INT = NULL;
		DECLARE @result2 INT = NULL;
		DECLARE @check INT = NULL;
		DECLARE @check2 INT = NULL;

		SET @result2 = dbo.fn_check_user_scalar(@username,@password);
		SET @result = dbo.fn_check_admin_scalar(@username,@password);
			
			SELECT @check = du.id 
			FROM Deleteduser AS du WHERE @user_id = du.id
			
			SELECT @check2 = dug.id 
			FROM Deletedusergroup AS dug WHERE @usergroup_id = dug.id

		IF(@check IS NULL) AND (@check2 IS NULL) AND (@result2 != @user_id) 
		BEGIN
			  IF (@result = 1)
			  BEGIN
					DELETE FROM Userusergroupallocate
					WHERE @user_id = [user_id]
					AND @usergroup_id = usergroup_id; 
			  END
		END
END
GO

USE master;
GO