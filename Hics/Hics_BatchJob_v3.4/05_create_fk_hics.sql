USE itin18_akt;
GO

-- Lamp
ALTER TABLE Deletedlamp
ADD
CONSTRAINT fk_Deletedlamp_Lamp
FOREIGN KEY (id)
REFERENCES Lamp(id);
GO

ALTER TABLE Lampstatus
ADD
CONSTRAINT fk_Lampstatus_Lamp
FOREIGN KEY (lamp_id)
REFERENCES Lamp(id);
GO

ALTER TABLE Lamproomgroupallocate
ADD
CONSTRAINT fk_Lamproomgroupallocate_Lamp
FOREIGN KEY (lamp_id)
REFERENCES lamp(id);
GO

--RoomGroup
ALTER TABLE Deletedroomgroup
ADD
CONSTRAINT fk_Deletedroomgroup_Roomgroup
FOREIGN KEY (id)
REFERENCES Roomgroup(id);
GO

ALTER TABLE Lamproomgroupallocate
ADD
CONSTRAINT fk_Lamproomgroupallocate_Roomgroup
FOREIGN KEY (roomgroup_id)
REFERENCES Roomgroup(id);
GO

-- User
ALTER TABLE Deleteduser
ADD
CONSTRAINT fk_Deleteduser_User
FOREIGN KEY (id)
REFERENCES [User](id);
GO

ALTER TABLE Userusergroupallocate
ADD
CONSTRAINT fk_Userusergroupallocate_User
FOREIGN KEY (user_id)
REFERENCES [User](id);
GO

--UserGroup
ALTER TABLE Deletedusergroup
ADD
CONSTRAINT fk_Deletedusergroup_Usergroup
FOREIGN KEY (id)
REFERENCES Usergroup(id);
GO

ALTER TABLE Userusergroupallocate
ADD
CONSTRAINT fk_Userusergroupallocate_Usergroup
FOREIGN KEY (usergroup_id)
REFERENCES Usergroup(id);
GO

USE master;
GO












