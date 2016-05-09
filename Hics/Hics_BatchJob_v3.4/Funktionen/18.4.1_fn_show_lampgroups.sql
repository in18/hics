-- 18.4.1

USE itin18_akt;
GO

CREATE FUNCTION fn_show_lampgroups(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @retTable TABLE( id INT, roomgroupname NVARCHAR(50) )
AS
BEGIN
	DECLARE @user_id INT = NULL;

	SET @user_id = dbo.fn_check_user_scalar(@username, @password);
 	
	IF (NOT @user_id IS NULL) AND (@user_id > 0)
	BEGIN
	INSERT INTO @retTable(id, roomgroupname)
	SELECT	rg.id AS Roomroup_ID,
			rg.name AS Roomgroup_Name
	FROM Roomgroup AS rg
			
	WHERE NOT EXISTS
			(SELECT *
			FROM Deletedroomgroup AS drg 
			WHERE rg.id = drg.id )
	END
	RETURN;
END;
GO

USE master;
GO
