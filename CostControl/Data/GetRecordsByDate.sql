CREATE PROCEDURE [dbo].GetRecordsByDate (@Date date)
AS
BEGIN
	SELECT * FROM Costs WHERE PDate = @Date
END