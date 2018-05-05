CREATE PROCEDURE GetRecordsByDescription(@Desc nvarchar(512))
AS
BEGIN
	SELECT * FROM Costs WHERE CHARINDEX(Description, @Desc) != 0
END