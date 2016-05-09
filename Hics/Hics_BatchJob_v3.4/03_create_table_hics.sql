USE itin18_akt;
GO

-- Tabellen
CREATE TABLE Lamp(
	id INT IDENTITY(1,1) NOT NULL,
	[address] VARCHAR(50) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE Lampstatus(
	id INT IDENTITY(1,1) NOT NULL,
	lamp_id INT NOT NULL,
	bright TINYINT NOT NULL, -- defaultvalue 255 
	stat BIT,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE [User](
	id INT IDENTITY(1,1) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	[password] VARBINARY(max) NOT NULL,
 	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- Gruppen Tabellen
CREATE TABLE Usergroup(
	id INT IDENTITY(1,1) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE Roomgroup(
	id INT IDENTITY(1,1) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- Delete Tabellen
CREATE TABLE Deletedlamp(
	id INT NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE Deletedroomgroup(
	id INT NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE Deleteduser(
	id INT NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE Deletedusergroup(
	id INT NOT NULL,
	userlifetime INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ZwischenTabellen
CREATE TABLE Lamproomgroupallocate(
	id INT IDENTITY(1,1) NOT NULL,
	roomgroup_id INT NOT NULL,
	lamp_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE Userusergroupallocate(
	id INT IDENTITY(1,1) NOT NULL,
	user_id INT NOT NULL,
	usergroup_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE() NOT NULL
);
GO

USE master;
GO