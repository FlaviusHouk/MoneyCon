CREATE PROCEDURE UpdateCost(@ID int, @PerformedDate date, @Description nvarchar(512), 
							@Category int, @Price real)
AS
BEGIN
	UPDATE [dbo].Costs SET PDate = @PerformedDate, Price = @Price, Description = @Description,
						   Category = @Category WHERE ID=@ID
END