--19.8.3
USE itin18_akt;
GO

CREATE FUNCTION fn_show_lampgroup_allocate(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE( lampen_id INT, lampen_name NVARCHAR(50),gruppen_id INT,gruppen_name NVARCHAR(50))
AS
BEGIN
	DECLARE @user_id INT = NULL;
	
	SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	IF (NOT @user_id IS NULL) AND (@user_id > 0)
		BEGIN
			INSERT INTO @ret_Table( lampen_id, lampen_name ,gruppen_id,gruppen_name)
			SELECT	la.lamp_id			AS Lampen_id,
					l.name				AS Lampen_name,
					la.roomgroup_id		AS Gruppen_id,
					r.name				AS Gruppen_name
			FROM Lamproomgroupallocate AS la
			JOIN Lamp AS l
				ON l.id = la.lamp_id
					JOIN Roomgroup AS r
						ON la.roomgroup_id = r.id
			WHERE   NOT EXISTS
						( SELECT * FROM Deletedlamp AS dl 
							WHERE l.id = dl.id) 
			AND NOT EXISTS
						(SELECT * FROM Deletedroomgroup AS drg 
							WHERE r.id = drg.id )
	END
RETURN;
END;
GO

USE master;
GO
