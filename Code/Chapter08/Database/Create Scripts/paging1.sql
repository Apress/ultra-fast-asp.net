CREATE PROC [Traffic].[PageViewRows]
	@startrow	INT,
	@pagesize	INT
AS
BEGIN
	SET NOCOUNT ON
	;WITH ViewList ([row], [date], [user], [url]) AS (
		SELECT ROW_NUMBER() OVER (ORDER BY PvId) [row], PvDate, UserId, PvUrl
			FROM [Traffic].[PageViews]
	)
	SELECT [row], [date], [user], [url]
		FROM ViewList
		WHERE [row] BETWEEN @startrow AND @startrow + @pagesize - 1
END 