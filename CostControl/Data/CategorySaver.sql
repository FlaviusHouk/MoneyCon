CREATE TRIGGER CategorySaver
ON Categories
AFTER DELETE
AS
DECLARE @CanRemove int
DECLARE @delId int
SELECT @delId = ID FROM deleted 
SELECT @CanRemove = COUNT(Costs.ID) FROM Costs WHERE Costs.Category = @delId
IF(@CanRemove > 0)
BEGIN
	RAISERROR('Витрати, які стосуються цієї категорії існують в базі.',16,1)
	ROLLBACK TRANSACTION
	RETURN
END 
