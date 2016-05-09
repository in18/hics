--18.2
USE itin18_akt;
GO

CREATE FUNCTION fn_show_lampgroup_status(@username NVARCHAR(50), @password VARBINARY(MAX),@group_id INT)
	RETURNS @ret_Table TABLE(id INT,name NVARCHAR(50),[address] NVARCHAR(50),[status] bit, brightness tinyint) 
AS
BEGIN
	DECLARE @user_id INT = NULL;
	DECLARE @lamp_count INT = NULL;
	DECLARE @loop INT = 0;

	SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	IF (NOT @user_id IS NULL) AND (@user_id > 0) AND (NOT @group_id IS NULL) AND (@group_id > 0)
		BEGIN
			
		INSERT INTO @ret_Table(id, name, [address], [status], brightness)
		SELECT	l.id, 
				l.name,
				l.[address],
				dbo.fn_show_lamp_status_scalar(@username,@password,l.id) AS [status],
				dbo.fn_show_lamp_brightness_scalar(@username,@password,l.id) AS brightness
		FROM fn_show_lamps(@username,@password) AS l
			JOIN Lamproomgroupallocate AS lgr
				ON l.id = lgr.lamp_id
					JOIN Roomgroup AS rg 
						ON lgr.roomgroup_id = rg.id
		WHERE rg.id = @group_id
	END
RETURN;
END;
GO

USE master;
GO


