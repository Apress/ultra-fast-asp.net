
CREATE SCHEMA [Traffic] AUTHORIZATION [dbo]
GO

CREATE TABLE [Traffic].[PageViews]
(
	[PvId]    BIGINT IDENTITY NOT NULL,
	[PvDate]  DATETIME NOT NULL,
	[UserId]  UNIQUEIDENTIFIER NULL,
	[PvUrl]   VARCHAR(256) NOT NULL
)
GO

ALTER TABLE [Traffic].[PageViews]
	ADD CONSTRAINT [PageViewIdPK]
	PRIMARY KEY CLUSTERED([PvId] ASC)
GO

CREATE PROCEDURE [Traffic].[AddPageView]
	@pvid BIGINT OUT,
	@userid UNIQUEIDENTIFIER,
	@pvurl VARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @trandate DATETIME
	SET @trandate = GETUTCDATE()
	INSERT INTO [Traffic].[PageViews]
	  (
	    PvDate,
	    UserId,
	    PvUrl
	  )
	VALUES
	  (
	    @trandate,
	    @userid,
	    @pvurl
	  )
	SET @pvid = SCOPE_IDENTITY()
END