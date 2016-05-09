USE itin18_akt;
GO

-- Tabellen
ALTER TABLE Lamp
ADD
CONSTRAINT pk_Lamp
PRIMARY KEY (id);
GO

ALTER TABLE Lampstatus
ADD
CONSTRAINT pk_Lampstatus
PRIMARY KEY (id);
GO

ALTER TABLE [User]
ADD
CONSTRAINT pk_User
PRIMARY KEY (id);
GO

-- Gruppen Tabellen
ALTER TABLE Usergroup
ADD
CONSTRAINT pk_Usergroup
PRIMARY KEY (id);
GO

ALTER TABLE Roomgroup
ADD
CONSTRAINT pk_Roomgroup
PRIMARY KEY (id);
GO

-- Delete Tabellen
ALTER TABLE Deletedlamp
ADD
CONSTRAINT pk_Deletedlamp
PRIMARY KEY (id);
GO

ALTER TABLE Deletedroomgroup
ADD
CONSTRAINT pk_Deletedroomgroup
PRIMARY KEY (id);
GO

ALTER TABLE Deleteduser
ADD
CONSTRAINT pk_Deleteduser
PRIMARY KEY (id);
GO

ALTER TABLE Deletedusergroup
ADD
CONSTRAINT pk_Deletedusergroup
PRIMARY KEY (id);
GO

-- ZwischenTabellen
ALTER TABLE Lamproomgroupallocate
ADD
CONSTRAINT pk_Lamproomgroupallocate
PRIMARY KEY (id);
GO

ALTER TABLE Userusergroupallocate
ADD
CONSTRAINT pk_Userusergroupallocate
PRIMARY KEY (id);
GO

USE master;
GO