--19.8.2
USE itin18_akt;
GO

CREATE FUNCTION fn_show_lamp_control_history(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @ret_Table TABLE( lamp_id INT, lamp_name NVARCHAR(50),userlifetime INT,[user_name] NVARCHAR(50), [date] DATETIME, [address] VARCHAR(50),[status] BIT, brightness TINYINT, deleted INT)
AS
BEGIN
	DECLARE @user_id INT = NULL;
	
	SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	IF (NOT @user_id IS NULL) AND (@user_id > 0)
		BEGIN
		INSERT INTO @ret_Table(lamp_id, lamp_name, userlifetime, [user_name], [date], [address], [status], brightness,deleted)
		SELECT	l.id, 
				l.name,
				ls.userlifetime,
				u.name,
				ls.createdate,
				l.[address],
				ls.stat AS [status],
				ls.bright AS brightness,
				dbo.fn_check_lamp_delete_scalar(@username,@password,l.id) AS deleted

		FROM Lampstatus AS ls
			JOIN Lamp AS l
				ON l.id = ls.lamp_id
					JOIN [User] AS u
						ON ls.userlifetime = u.id
	END
RETURN;
END;
GO

USE master;
GO
