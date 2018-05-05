CREATE PROCEDURE DeleteCategory (@ID int)
AS
BEGIN
	DELETE FROM [dbo].Categories WHERE ID = @ID
END