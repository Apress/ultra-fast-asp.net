CREATE TYPE PageViewType AS TABLE (
	[UserId]	UNIQUEIDENTIFIER	NULL,
	[PvUrl]		VARCHAR(256)		NOT NULL
)
GO

CREATE PROCEDURE [Traffic].[AddPageViewTVP]
	@pvid	BIGINT OUT,
	@rows	PageViewType READONLY
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @trandate DATETIME
	SET @trandate = GETUTCDATE()
	INSERT INTO [Traffic].[PageViews]
		SELECT @trandate, UserId, PvUrl
			FROM @rows
	SET @pvid = SCOPE_IDENTITY()
END 