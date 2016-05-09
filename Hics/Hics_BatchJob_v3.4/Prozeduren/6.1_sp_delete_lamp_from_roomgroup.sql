-- 6.1 sp_delete_lamp_from_roomgroup
USE itin18_akt;
GO

CREATE PROCEDURE sp_delete_lamp_from_roomgroup
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@roomgroup_id INT,
	@lamp_id INT
AS
BEGIN
	DECLARE @found_id INT = NULL;
	DECLARE @result INT = NULL;

    SET @result = dbo.fn_check_admin_scalar(@username,@password);
	
	IF (@result = 1) --Admin/User wird überprüft
	BEGIN

		SELECT @found_id = drg.id
		FROM Deletedroomgroup	AS drg
		WHERE @roomgroup_id = drg.id	

			IF(@found_id IS NULL)
			BEGIN
				SELECT @found_id = dl.id
				FROM Deletedlamp AS dl
				WHERE @lamp_id = dl.id

					IF(@found_id IS NULL)
					BEGIN
						DELETE FROM Lamproomgroupallocate
						WHERE roomgroup_id = @roomgroup_id
						AND lamp_id = @lamp_id; 
					END
			END
	END
END;
GO

USE master;
GO