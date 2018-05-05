CREATE PROCEDURE GetSumByDateSpan(@bDate date, @eDate date, @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = SUM(Price) From [dbo].Costs WHERE PDate < @eDate And PDate > @bDate
END