CREATE PROCEDURE GetAvgByCat(@Cat int, @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = AVG(Price) From [dbo].Costs WHERE Category = @Cat
END