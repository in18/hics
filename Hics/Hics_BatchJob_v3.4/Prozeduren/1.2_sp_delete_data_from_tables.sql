USE itin18_akt;
GO

CREATE PROCEDURE sp_delete_data_from_tables
AS
BEGIN
	DELETE
	FROM dbo.Userusergroupallocate;

	DELETE
	FROM itin18_akt.dbo.Lamproomgroupallocate;

	DELETE
	FROM itin18_akt.dbo.Deleteduser;

	DELETE
	FROM dbo.[User];

	DELETE
	FROM itin18_akt.dbo.Deletedlamp;
	
	DELETE
	FROM itin18_akt.dbo.Deletedusergroup;

	DELETE
	FROM itin18_akt.dbo.Deletedroomgroup;

	DELETE
	FROM itin18_akt.dbo.Usergroup;

	DELETE
	FROM itin18_akt.dbo.Roomgroup;



	DELETE
	FROM itin18_akt.dbo.Lampstatus;

	DELETE
	FROM itin18_akt.dbo.Lamp;


END;
GO 

USE master;
GO
