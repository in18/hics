USE itin18_akt;
GO

CREATE PROCEDURE sp_insert_test_data
AS
BEGIN


EXEC sp_delete_data_from_tables;


-- Main Daten Folgen
-- Super_admin anlegen
SET IDENTITY_INSERT [User] ON;
INSERT INTO [User](id, name, password) VALUES (1,'super_admin',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
SET IDENTITY_INSERT [User] OFF;

-- Usergruppen anlegen
SET IDENTITY_INSERT Usergroup ON;
INSERT INTO Usergroup(id,name,userlifetime) VALUES (1, 'admin',1);
INSERT INTO Usergroup(id,name,userlifetime) VALUES (2, 'user' , 1);
SET IDENTITY_INSERT Usergroup OFF;

-- In der Zwischentabelle zuweisen
SET IDENTITY_INSERT Userusergroupallocate ON;
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (1,1,1);
SET IDENTITY_INSERT Userusergroupallocate OFF;


-- TestDaten Folgen

-- User anlegen
SET IDENTITY_INSERT [User] ON;
INSERT INTO [User](id, name, password) VALUES (2,'admin',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
INSERT INTO [User](id, name, password) VALUES (3,'Sepp',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
INSERT INTO [User](id, name, password) VALUES (4,'Karl',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
INSERT INTO [User](id, name, password) VALUES (5,'Fritz',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
INSERT INTO [User](id, name, password) VALUES (6,'Blub',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
INSERT INTO [User](id, name, password) VALUES (7,'Lisi',0xE3D2608341B3C00604519271F425E3DDA76949B0C819D05064683117BB1168612F3C955AF9EBC328F24B27C0EC2D83B79684B2DF47F4E95E8FDD1F838283E024);
SET IDENTITY_INSERT [User] OFF;

-- Usergruppen anlegen
SET IDENTITY_INSERT Usergroup ON;
INSERT INTO Usergroup(id,name,userlifetime) VALUES (3, 'admintest',1);
INSERT INTO Usergroup(id,name,userlifetime) VALUES (4, 'usertest' , 1);
SET IDENTITY_INSERT Usergroup OFF;

-- In der Zwischentabelle zuweisen
SET IDENTITY_INSERT Userusergroupallocate ON;
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (2,2,1);
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (3,3,1);
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (4,4,2);
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (5,5,2);
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (6,6,2);
INSERT INTO Userusergroupallocate(id,user_id,usergroup_id) VALUES (7,7,2);
SET IDENTITY_INSERT Userusergroupallocate OFF;

-- Gelöschte User eintragen
INSERT INTO Deleteduser(id, userlifetime) VALUES (5,2);
INSERT INTO Deleteduser(id, userlifetime) VALUES (6,2);

-- Gelöschte Usergruppen eintragen
INSERT INTO Deletedusergroup(id, userlifetime) VALUES (3,2);
INSERT INTO Deletedusergroup(id, userlifetime) VALUES (4,2);

--Lampen hinzufügen
SET IDENTITY_INSERT Lamp ON;
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (1,'lampx0001','Hue color lamp 1',1);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (2,'lampx0002','Hue color lamp 2',1);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (3,'lampx0003','Hue color lamp 3',1);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (4,'lampx0004','Flur1',1);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (5,'lampx0005','Flur2',2);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (6,'lampx0006','Flur3',3);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (7,'lampx0007','Schlafzimmer1',4);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (8,'lampx0008','Schlafzimmer2',5);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (9,'lampx0009','Schlafzimmer3',6);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (10,'lampx0010','Schlafzimmer3',6);
INSERT INTO Lamp(id,[address],name,userlifetime) VALUES (11,'lampx0011','Schlafzimmer3',6);
SET IDENTITY_INSERT Lamp OFF;

-- RaumGruppen hinzufügen
SET IDENTITY_INSERT Roomgroup ON;
INSERT INTO Roomgroup(id, name, userlifetime) VALUES (1,'Wohnzimmer Gruppe1',1);
INSERT INTO Roomgroup(id, name, userlifetime) VALUES (2,'Wohnzimmer Gruppe2',2);
INSERT INTO Roomgroup(id, name, userlifetime) VALUES (3,'Flur Gruppe1',3);
INSERT INTO Roomgroup(id, name, userlifetime) VALUES (4,'Flur Gruppe2',4);
INSERT INTO Roomgroup(id, name, userlifetime) VALUES (5,'Schlafzimmer Gruppe1',5);
INSERT INTO Roomgroup(id, name, userlifetime) VALUES (6,'WC',6);
SET IDENTITY_INSERT Roomgroup OFF;

-- In der Zwischentabelle zuweisen
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (1,1);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (1,2);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (1,3);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (2,1);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (3,4);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (3,5);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (3,6);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (4,4);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (5,7);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (5,8);
INSERT INTO Lamproomgroupallocate(roomgroup_id, lamp_id) VALUES (5,9);

-- Gelöschte Lampen eintragen
INSERT INTO Deletedlamp(id,userlifetime) VALUES (10,1);
INSERT INTO Deletedlamp(id,userlifetime) VALUES (11,2);

-- Gelöschte Raumgruppen eintragen
INSERT INTO Deletedroomgroup(id, userlifetime) VALUES (6,1);
INSERT INTO Deletedroomgroup(id, userlifetime) VALUES (4,2);

-- Lampen status eintragen
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (1,255,1,2);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (2,255,1,2);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (3,255,1,2);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (4,128,1,4);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (4,128,0,4);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (5,255,0,6);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (5,0,0,1);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (8,255,1,3);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (8,255,0,4);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (9,100,1,1);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (9,100,0,1);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (6,90,1,4);
INSERT INTO Lampstatus(lamp_id,bright,stat,userlifetime) VALUES (6,255,0,4);

END;
GO 

USE master;
GO