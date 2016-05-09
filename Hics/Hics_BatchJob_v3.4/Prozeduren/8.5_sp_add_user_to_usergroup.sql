USE [itin18_akt]
GO

CREATE PROCEDURE sp_add_user_to_usergroup
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@user_id INT,
	@usergroup_id INT
AS
BEGIN
		DECLARE @result INT = NULL;
		DECLARE @check INT = NULL;
		DECLARE @check2 INT = NULL;

		SET @result = dbo.fn_check_admin_scalar(@username,@password);
			
			SELECT @check = du.id 
			FROM Deleteduser AS du WHERE @user_id = du.id
			
			SELECT @check2 = dug.id 
			FROM Deletedusergroup AS dug WHERE @usergroup_id = dug.id

		IF(@check IS NULL) AND (@check2 IS NULL)
		BEGIN
			  IF (@result = 1)
			  BEGIN
				INSERT INTO Userusergroupallocate([user_id],usergroup_id)
				VALUES(@user_id,@usergroup_id);
			  END
		END
END


