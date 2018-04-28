CREATE PROCEDURE [dbo].GetRecordsByDateSpan(@bDate date, @eDate date)
AS
BEGIN
	SELECT * FROM Costs WHERE PDate > @bDate AND PDate < @eDate
END