USE itin18_akt;
GO

-- Funktion
--18.1
IF NOT OBJECT_ID('fn_show_lamp_status') IS NULL DROP FUNCTION fn_show_lamp_status;
GO

--18.2
IF NOT OBJECT_ID('fn_show_lampgroup_status') IS NULL DROP FUNCTION fn_show_lampgroup_status;
GO

--18.3.1
IF NOT OBJECT_ID('fn_show_lamps') IS NULL DROP FUNCTION fn_show_lamps;
GO

--18.3.2
IF NOT OBJECT_ID('fn_show_deleted_lamps') IS NULL DROP FUNCTION fn_show_deleted_lamps;
GO

--18.4.1
IF NOT OBJECT_ID('fn_show_lampgroups') IS NULL DROP FUNCTION fn_show_lampgroups;
GO

--18.4.2
IF NOT OBJECT_ID('fn_show_deleted_roomgroups') IS NULL DROP FUNCTION fn_show_deleted_roomgroups;
GO

--19.2
IF NOT OBJECT_ID('fn_check_user_table') IS NULL DROP FUNCTION fn_check_user_table;
GO

--19.3.1
IF NOT OBJECT_ID('fn_show_users') IS NULL DROP FUNCTION fn_show_users;
GO

--19.3.2
IF NOT OBJECT_ID('fn_show_deleted_users') IS NULL DROP FUNCTION fn_show_deleted_users;
GO

--19.4.1
IF NOT OBJECT_ID('fn_show_usergroups') IS NULL DROP FUNCTION fn_show_usergroups;
GO

--19.4.2
IF NOT OBJECT_ID('fn_show_deleted_usergroups') IS NULL DROP FUNCTION fn_show_deleted_usergroups;
GO

--19.5
IF NOT OBJECT_ID('fn_hash_password') IS NULL DROP FUNCTION fn_hash_password;
GO

--19.6
IF NOT OBJECT_ID('fn_check_admin_table') IS NULL DROP FUNCTION fn_check_admin_table;
GO

--19.7.1
IF NOT OBJECT_ID('fn_check_user_scalar') IS NULL DROP FUNCTION fn_check_user_scalar;
GO

--19.7.2
IF NOT OBJECT_ID('fn_check_admin_scalar') IS NULL DROP FUNCTION fn_check_admin_scalar;
GO

--19.8.1
IF NOT OBJECT_ID('fn_show_lamp_control') IS NULL DROP FUNCTION fn_show_lamp_control;
GO

--19.8.2
IF NOT OBJECT_ID('fn_show_lamp_control_history') IS NULL DROP FUNCTION fn_show_lamp_control_history;
GO

--19.8.3
IF NOT OBJECT_ID('fn_show_lampgroup_allocate') IS NULL DROP FUNCTION fn_show_lampgroup_allocate;
GO

--19.9
IF NOT OBJECT_ID('fn_show_lamp_status_scalar') IS NULL DROP FUNCTION fn_show_lamp_status_scalar;
GO

--19.10
IF NOT OBJECT_ID('fn_show_lamp_brightness_scalar') IS NULL DROP FUNCTION fn_show_lamp_brightness_scalar;
GO

--19.11
IF NOT OBJECT_ID('fn_check_lamp_delete_scalar') IS NULL DROP FUNCTION fn_check_lamp_delete_scalar;
GO


-- Prozeduren
--1.2
IF NOT OBJECT_ID('sp_delete_data_from_tables') IS NULL DROP PROCEDURE sp_delete_data_from_tables;
GO

--1.3
IF NOT OBJECT_ID('sp_insert_test_data') IS NULL DROP PROCEDURE sp_insert_test_data;
GO

--2.1
IF NOT OBJECT_ID('sp_add_lamp') IS NULL DROP PROCEDURE sp_add_lamp;
GO

--3.1
IF NOT OBJECT_ID('sp_delete_lamp') IS NULL DROP PROCEDURE sp_delete_lamp;
GO

--4.1
IF NOT OBJECT_ID('sp_add_lampgroup') IS NULL DROP PROCEDURE sp_add_lampgroup;
GO

--5.1
IF NOT OBJECT_ID('sp_add_lamp_to_lampgroup') IS NULL DROP PROCEDURE sp_add_lamp_to_lampgroup;
GO

--6.1
IF NOT OBJECT_ID('sp_delete_lamp_from_roomgroup') IS NULL DROP PROCEDURE sp_delete_lamp_from_roomgroup;
GO

--7.1
IF NOT OBJECT_ID('sp_delete_roomgroup') IS NULL DROP PROCEDURE sp_delete_roomgroup;
GO

--8.1
IF NOT OBJECT_ID('sp_add_usergroup') IS NULL DROP PROCEDURE sp_add_usergroup;
GO

--8.3
IF NOT OBJECT_ID('sp_add_user') IS NULL DROP PROCEDURE sp_add_user;
GO

--8.2
IF NOT OBJECT_ID('sp_delete_usergroup') IS NULL DROP PROCEDURE sp_delete_usergroup;
GO

--8.4
IF NOT OBJECT_ID('sp_delete_user') IS NULL DROP PROCEDURE sp_delete_user;
GO

--8.5
IF NOT OBJECT_ID('sp_add_user_to_usergroup') IS NULL DROP PROCEDURE sp_add_user_to_usergroup;
GO

--8.6
IF NOT OBJECT_ID('sp_delete_user_from_usergroup') IS NULL DROP PROCEDURE sp_delete_user_from_usergroup;
GO

--13.1
IF NOT OBJECT_ID('sp_lamp_on') IS NULL DROP PROCEDURE sp_lamp_on;
GO

--13.2
IF NOT OBJECT_ID('sp_set_lamp_stat') IS NULL DROP PROCEDURE sp_set_lamp_stat;
GO

--14.1
IF NOT OBJECT_ID('sp_lamp_off') IS NULL DROP PROCEDURE sp_lamp_off;
GO

--15.1
IF NOT OBJECT_ID('sp_lamp_dimm') IS NULL DROP PROCEDURE sp_lamp_dimm;
GO

--19.1.1
IF NOT OBJECT_ID('sp_change_password') IS NULL DROP PROCEDURE sp_change_password;
GO

--19.1.2
IF NOT OBJECT_ID('sp_change_password_by_admin') IS NULL DROP PROCEDURE sp_change_password_by_admin;
GO





--  TABLE

-- ZwischenTabellen
IF NOT OBJECT_ID('Lamproomgroupallocate') IS NULL DROP TABLE Lamproomgroupallocate;
GO

IF NOT OBJECT_ID('Userusergroupallocate') IS NULL DROP TABLE Userusergroupallocate;
GO

-- Delete Tabellen
IF NOT OBJECT_ID('Deletedlamp') IS NULL DROP TABLE Deletedlamp;
GO

IF NOT OBJECT_ID('Deletedroomgroup') IS NULL DROP TABLE Deletedroomgroup;
GO

IF NOT OBJECT_ID('Deleteduser') IS NULL DROP TABLE Deleteduser;
GO

IF NOT OBJECT_ID('Deletedusergroup') IS NULL DROP TABLE Deletedusergroup;
GO

-- Tabellen
IF NOT OBJECT_ID('Lampstatus') IS NULL DROP TABLE Lampstatus;
GO

IF NOT OBJECT_ID('Lamp') IS NULL DROP TABLE Lamp;
GO

IF NOT OBJECT_ID('[User]') IS NULL DROP TABLE [User];
GO

-- Gruppen Tabellen
IF NOT OBJECT_ID('Usergroup') IS NULL DROP TABLE Usergroup;
GO

IF NOT OBJECT_ID('Roomgroup') IS NULL DROP TABLE Roomgroup;
GO



USE master;
GO
