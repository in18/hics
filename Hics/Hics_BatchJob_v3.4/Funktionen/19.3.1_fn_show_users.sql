-- 19.3.1

USE itin18_akt;
GO

CREATE FUNCTION fn_show_users(@username NVARCHAR(50), @password VARBINARY(MAX))
	RETURNS @retTable TABLE( id INT, name NVARCHAR(50), [group] NVARCHAR(50) )
AS
BEGIN
	DECLARE @user_id INT = NULL;

        SET @user_id = dbo.fn_check_user_scalar(@username, @password);

	IF (NOT @user_id IS NULL) AND (@user_id > 0)
	BEGIN

		INSERT INTO @retTable(id, name, [group])
		SELECT	u.id	AS ID,
				u.name	AS [User-Name],
				ug.name AS [User-Group]
		FROM [User] AS u
			JOIN Userusergroupallocate AS uga
				ON u.id = uga.user_id
					JOIN Usergroup AS ug
						ON ug.id = uga.usergroup_id
		WHERE u.id > 1 AND NOT EXISTS
			(SELECT *
				FROM Deleteduser AS du WHERE u.id = du.id )
	END

	RETURN;
END;
GO

USE master;
GO

