USE [itin18_akt]
GO


CREATE PROCEDURE sp_lamp_off
	@username NVARCHAR(50),
	@password VARBINARY(MAX),
	@lamp_id  INT
AS
BEGIN
	DECLARE @user_id INT = NULL;
	DECLARE @last_brightness TINYINT = 0;
	
	SET @user_id = dbo.fn_check_user_scalar(@username,@password);
	SET @last_brightness = dbo.fn_show_lamp_brightness_scalar(@username,@password,@lamp_id);
	
	IF (@last_brightness IS NULL)
	BEGIN
		SET @last_brightness = 254;
	END	

	IF (NOT @user_id IS NULL) AND (@user_id > 0) AND (NOT @lamp_id IS NULL) AND (@lamp_id > 0)
	BEGIN		

		 INSERT INTO Lampstatus(stat,lamp_id,userlifetime,bright )
		 VALUES (0,@lamp_id,@user_id,@last_brightness);

	END;
END;

