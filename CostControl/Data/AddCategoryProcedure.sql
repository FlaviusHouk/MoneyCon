CREATE PROCEDURE AddCategory (@Name nvarchar(256), @ID int OUTPUT)
AS
BEGIN
	DECLARE @tab table(val int);

	INSERT INTO [MoneyCon].[dbo].[Categories] (CatName) 
	OUTPUT inserted.ID into @tab
		VALUES (@Name) 

	SELECT @ID = val FROM @tab
END