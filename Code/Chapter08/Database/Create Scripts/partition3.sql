CREATE TABLE [Traffic].[PageViewsArchive] (
	[PvId]		BIGINT				IDENTITY,
	[PvDate]	DATETIME			NOT NULL,
	[UserId]	UNIQUEIDENTIFIER	NULL,
	[PvUrl]		VARCHAR(256)		NOT NULL
) ON ByMonth ([PvDate])

ALTER TABLE [Traffic].[PageViewsArchive]
	ADD CONSTRAINT [PageViewArchivePK]
	PRIMARY KEY CLUSTERED ([PvId], [PvDate])
	
SELECT COUNT(*)
	FROM [Traffic].[PageViews]

SELECT COUNT(*)
	FROM [Traffic].[PageViewsArchive]
	
ALTER TABLE [Traffic].[PageViews]
	SWITCH PARTITION 1
	TO [Traffic].[PageViewsArchive] PARTITION 1
	
SELECT COUNT(*)
	FROM [Traffic].[PageViews]

SELECT COUNT(*)
	FROM [Traffic].[PageViewsArchive]
	
TRUNCATE TABLE [Traffic].[PageViewsArchive]