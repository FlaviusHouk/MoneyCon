CREATE PROCEDURE GetSumByDateSpan(@Date date, @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = SUM(Price) From [dbo].Costs WHERE PDate = @Date
END