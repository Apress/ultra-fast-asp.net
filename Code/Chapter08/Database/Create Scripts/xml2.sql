 SELECT * FROM Products
 GO
 
SELECT [Id], [Name], [Info].query('/info/props')
	FROM [Products]
	WHERE [Info].exist('/info/props[@width]') = 1
GO

SELECT [Id], [Name], [Info].value('(/info/props/@width)[1]', 'REAL') [Width]
	FROM [Products]
	WHERE [Info].value('(/info/color[@part = "top"])[1]', 'VARCHAR(16)') = 'black'
GO

DECLARE @part VARCHAR(16)
SET @part = 'legs'
SELECT [Id], [Name], [Info].value('(/info/color)[1]', 'VARCHAR(16)') [First Color]
	FROM [Products]
	WHERE [Info].value('(/info/color[@part = sql:variable("@part")])[1]',
		'VARCHAR(16)') = 'chrome'
GO
