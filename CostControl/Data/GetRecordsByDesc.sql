CREATE PROCEDURE GetRecordsByDescription(@Desc nchar(512))
AS
BEGIN
	SELECT * FROM Costs WHERE CHARINDEX(Description, @Desc) != 0
END