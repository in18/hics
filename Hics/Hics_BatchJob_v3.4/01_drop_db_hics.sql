USE master;
GO

IF NOT DB_ID('itin18_akt') IS NULL ALTER DATABASE itin18_akt SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

IF NOT DB_ID('itin18_akt') IS NULL DROP DATABASE itin18_akt;
GO
