CREATE PROCEDURE GetRecordsByCategory(@Category nchar(256))
AS
BEGIN
	SELECT * FROM Costs
	INNER JOIN Categories ON Categories.ID = Costs.Category
	WHERE Categories.CatName = @Category
END