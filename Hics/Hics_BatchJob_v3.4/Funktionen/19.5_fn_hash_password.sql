
USE itin18_akt;
GO

CREATE FUNCTION fn_hash_password(@text_to_hash VARBINARY(MAX) )
	RETURNS VARBINARY(max)
AS
BEGIN
	RETURN HASHBYTES('sha2_512',@text_to_hash); 
END;
GO

USE master;
GO