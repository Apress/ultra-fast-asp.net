--For testing, run the following script from two SSMS
--instances at the same time, with one of them logged
--into the "vip" user created earlier.

DECLARE @count BIGINT
SET @count = 0
DECLARE @start DATETIME
SET @start = GETDATE()
WHILE DATEDIFF(second, @start, GETDATE()) < 30
BEGIN
	SET @count = @count + 1
END
SELECT @count 