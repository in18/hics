-- 19.9

USE itin18_akt;
GO

CREATE FUNCTION fn_show_lamp_status_scalar(@username NVARCHAR(50), @password VARBINARY(MAX),@lamp_id INT)
	RETURNS BIT
AS
BEGIN
	DECLARE @stat BIT = 0;
	DECLARE @user_id INT = NULL;
	
	SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	IF (NOT @user_id IS NULL) AND (@user_id > 0) AND (NOT @lamp_id IS NULL) AND (@lamp_id > 0)
	BEGIN
	
		SELECT TOP 1 @stat = ls.stat
		FROM Lampstatus AS ls
		WHERE ls.lamp_id = @lamp_id
		ORDER BY ls.id DESC;
	END;
		 	
	RETURN @stat;
END;
GO

USE master;
GO
