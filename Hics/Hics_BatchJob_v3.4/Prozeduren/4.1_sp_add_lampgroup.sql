--4.1
USE itin18_akt;
GO

CREATE PROCEDURE sp_add_lampgroup
	@name NVARCHAR(50),
	@password VARBINARY(MAX),
	@group_name NVARCHAR(50)
AS
	DECLARE @admin_check INT = NULL;
	DECLARE @user_check INT = NULL;
	DECLARE @group_check NVARCHAR(50) = NULL;

	SET @admin_check = dbo.fn_check_admin_scalar(@name,@password);
	SET @user_check = dbo.fn_check_user_scalar(@name,@password);
		

	 SELECT @group_check = rg.name
	 FROM Roomgroup AS rg
	
	IF(@admin_check = 1) AND (@group_check != @group_name)
	BEGIN
		INSERT INTO Roomgroup(name,userlifetime)
		 VALUES(@group_name, @user_check);
	END
GO

USE master;
GO
