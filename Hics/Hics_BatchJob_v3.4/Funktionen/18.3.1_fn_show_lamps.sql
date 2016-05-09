--18.3.1
USE itin18_akt;
GO

CREATE FUNCTION fn_show_lamps(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE(id INT,name NVARCHAR(50),[address] VARCHAR(50)) 
AS
BEGIN
	DECLARE @user_id INT = NULL;

	SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	IF (NOT @user_id IS NULL) AND (@user_id > 0)
	BEGIN

	INSERT INTO @ret_Table(id,name,[address])
		SELECT  l.id AS ID,
				l.name AS Name,
				l.[address] AS [Address]
		FROM Lamp AS l 	 
		WHERE   NOT EXISTS
			( SELECT * FROM Deletedlamp AS dl WHERE l.id = dl.id)
	END	
	RETURN;
END;
GO

USE master;
GO






