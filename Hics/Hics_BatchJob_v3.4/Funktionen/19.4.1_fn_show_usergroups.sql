-- 19.4.1

USE itin18_akt;
GO

CREATE FUNCTION fn_show_usergroups(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @retTable TABLE( id INT, groupname NVARCHAR(50) )
AS
BEGIN
	DECLARE @user_id INT = NULL;
	SET @user_id = dbo.fn_check_user_scalar(@username, @password);
	IF (NOT @user_id IS NULL) AND (@user_id > 0)
	BEGIN
	
	INSERT INTO @retTable(id, groupname)
	SELECT	ug.id AS Usergroup_ID,
			ug.name AS Usergroup_Name
	FROM Usergroup AS ug
			
	WHERE NOT EXISTS
			(SELECT *
			FROM Deletedusergroup AS dug 
			WHERE ug.id = dug.id )
	END
	RETURN;
END;
GO

USE master;
GO
