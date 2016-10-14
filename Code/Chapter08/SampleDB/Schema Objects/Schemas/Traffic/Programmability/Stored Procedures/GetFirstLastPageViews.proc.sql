CREATE PROCEDURE [Traffic].[GetFirstLastPageViews]
	@count	INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT TOP (@count) PvId, PvDate, UserId, PvUrl
		FROM [Traffic].[PageViews]
		ORDER BY Pvid ASC
	SELECT TOP (@count) PvId, PvDate, UserId, PvUrl
		FROM [Traffic].[PageViews]
		ORDER BY Pvid DESC
END 