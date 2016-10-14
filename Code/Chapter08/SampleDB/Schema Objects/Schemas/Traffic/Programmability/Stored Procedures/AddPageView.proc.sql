
CREATE PROCEDURE [Traffic].[AddPageView]
	@pvid BIGINT OUT,
	@userid UNIQUEIDENTIFIER,
	@pvurl VARCHAR (256)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @trandate DATETIME
	SET @trandate = GETUTCDATE()
	INSERT INTO [Traffic].[PageViews]
		(PvDate, UserId, PvUrl)
		VALUES
		(@trandate, @userid, @pvurl)
	SET @pvid = SCOPE_IDENTITY()
END