CREATE PROCEDURE AddCost (@PerformedDate date, @Description nvarchar(512), @Category int, @Price real, @ID int OUTPUT)
AS
BEGIN
	DECLARE @tab table(val int);

	INSERT INTO [MoneyCon].[dbo].[Costs] (PDate, Price, Description, Category) 
	OUTPUT inserted.ID into @tab
		VALUES (@PerformedDate, @Price, @Description, @Category) 

	SELECT @ID = val FROM @tab
END