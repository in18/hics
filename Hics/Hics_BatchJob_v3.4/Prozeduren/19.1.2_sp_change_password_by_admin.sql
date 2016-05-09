USE itin18_akt;
GO

CREATE PROCEDURE sp_change_password_by_admin
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@change_id INT,
	@newpassword VARBINARY(MAX)
AS
BEGIN
		
		DECLARE @check_admin INT = NULL;
		DECLARE @check_super_admin INT = NULL;
		DECLARE @check_change_id INT = NULL;


		--  prüfung ob admin rechte ( INT )
		SET @check_admin = dbo.fn_check_admin_scalar(@username, @password);
		SET @check_super_admin = dbo.fn_check_user_scalar(@username, @password);

		-- Prüfe ob change_id ist user/admin
		SELECT @check_change_id = ug.id
		FROM [User] AS u
			JOIN Userusergroupallocate AS uga
				ON u.id = uga.user_id
					JOIN Usergroup AS ug
						ON uga.usergroup_id = ug.id
		WHERE @change_id = u.id

		WHILE(1=1)
		BEGIN
			IF(@check_admin = 1 )
			BEGIN 

				IF ( @check_change_id > 1 )
				BEGIN
					UPDATE [user]
					SET [password] = dbo.fn_hash_password(@newpassword)
					WHERE id = @change_id;
					BREAK;
				END

				IF( @check_super_admin = 1)
				BEGIN
					UPDATE [user]
					SET [password] = dbo.fn_hash_password(@newpassword)
					WHERE id = @change_id;
					BREAK;
				END

			END
			BREAK;

		END

END
GO

USE master;
GO