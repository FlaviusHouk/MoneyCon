CREATE PROCEDURE GetSumByDesc(@Cat int, @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = SUM(Price) From [dbo].Costs WHERE Category = @Cat
END