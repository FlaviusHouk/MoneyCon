CREATE PROCEDURE GetSumByDesc(@Desc nvarchar(512), @sum real OUTPUT)
AS
BEGIN
	SELECT @sum = SUM(Price) From [dbo].Costs WHERE CHARINDEX(Description, @Desc) != 0
END