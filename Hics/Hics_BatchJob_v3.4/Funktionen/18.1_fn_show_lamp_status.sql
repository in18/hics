--18.1
USE itin18_akt;
GO

CREATE FUNCTION fn_show_lamp_status(@username NVARCHAR(50), @password VARBINARY(MAX),@lamp_id INT)
	RETURNS @ret_Table TABLE(id INT,name NVARCHAR(50),address VARCHAR(50),bright TINYINT, status BIT) 
AS
BEGIN
	DECLARE @user_id INT = NULL;
	
	SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	 IF (NOT @user_id IS NULL) AND (@user_id > 0) AND (NOT @lamp_id IS NULL) AND (@lamp_id > 0)

	INSERT INTO @ret_Table(id,name, address,bright,status)
		SELECT  TOP 1	l.id,
				l.name,
				l.address,
				ls.bright,
				ls.stat
		FROM Lamp AS l 
		JOIN Lampstatus AS ls
		ON l.id = ls.lamp_id
		
		WHERE   NOT EXISTS
			( SELECT * FROM Deletedlamp AS dl WHERE l.id = dl.id) AND  (l.id = @lamp_id)
		ORDER BY ls.id DESC 	
	RETURN;
END;

GO

USE master;
GO


