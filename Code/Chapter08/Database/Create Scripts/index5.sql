-- This code might generate duplicate values for v2 due to the random numbers.
-- If so, it will report errors when the script runs, but the errors won't
-- materially impact the final results.
DECLARE @i INT
SET @i = 0
WHILE (@i < 5000)
BEGIN
	INSERT INTO ind
		(v2, v3)
		VALUES
		((CONVERT(INT, RAND() * 500000) * 2) + 1, 'test')
	SET @i = @i + 1
END 
GO

DBCC SHOWCONTIG (ind) WITH ALL_INDEXES
GO