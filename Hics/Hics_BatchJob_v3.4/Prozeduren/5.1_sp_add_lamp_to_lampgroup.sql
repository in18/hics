--5.1

USE [itin18_akt]
GO

CREATE PROCEDURE sp_add_lamp_to_lampgroup
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@roomgroup_id INT,
	@lamp_id INT
AS
BEGIN
		DECLARE @result INT = NULL;
		DECLARE @check INT = NULL;
		DECLARE @check2 INT = NULL;

		SET @result = dbo.fn_check_admin_scalar(@username,@password);
			
			SELECT @check = dl.id 
			FROM Deletedlamp AS dl WHERE @lamp_id = dl.id
			
			SELECT @check2 = drg.id 
			FROM Deletedroomgroup AS drg WHERE @roomgroup_id = drg.id

		IF(@check IS NULL) AND (@check2 IS NULL)
		BEGIN
			  IF (@result = 1)
			  BEGIN
				INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id)
				VALUES(@roomgroup_id,@lamp_id);
			  END
		END
END

GO
USE master;
GO
