CREATE PROCEDURE GetAvgByDesc(@Desc nvarchar(512), @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = AVG(Price) From [dbo].Costs WHERE CHARINDEX(Description, @Desc) != 0
END