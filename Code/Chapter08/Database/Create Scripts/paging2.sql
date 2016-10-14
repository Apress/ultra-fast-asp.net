ALTER PROC [Traffic].[PageViewRows]
	@startrow	INT,
	@pagesize	INT,
	@getcount	BIT,
	@count		INT OUT
AS
BEGIN
	SET NOCOUNT ON
	SET @count = -1;
	IF @getcount = 1
	BEGIN
		SELECT @count = count(*) FROM [Traffic].[PageViews]
	END
	;WITH ViewList ([row], [date], [user], [url]) AS (
		SELECT ROW_NUMBER() OVER (ORDER BY PvId) [row], PvDate, UserId, PvUrl
			FROM [Traffic].[PageViews]
		)
		SELECT [row], [date], [user], [url]
			FROM ViewList
			WHERE [row] BETWEEN @startrow AND @startrow + @pagesize - 1
END 