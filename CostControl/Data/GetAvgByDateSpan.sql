CREATE PROCEDURE GetAvgByDateSpan(@bDate date, @eDate date, @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = AVG(Price) From [dbo].Costs WHERE PDate < @eDate And PDate > @bDate
END