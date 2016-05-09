USE itin18_akt;
GO

CREATE UNIQUE INDEX ix_lamp_name
	ON lamp(address);
GO

CREATE UNIQUE INDEX ix_User_id_name
	ON [user](name);
GO


CREATE UNIQUE INDEX ix_Userusergroupallocate
	ON Userusergroupallocate([user_id] ,usergroup_id);
GO

CREATE UNIQUE INDEX ix_Lamproomgroupallocate
	ON Lamproomgroupallocate(roomgroup_id,lamp_id);
GO

USE master;
GO