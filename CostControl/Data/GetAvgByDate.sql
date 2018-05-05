CREATE PROCEDURE GetAvgByDate(@Date date, @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = AVG(Price) From [dbo].Costs WHERE PDate = @Date
END