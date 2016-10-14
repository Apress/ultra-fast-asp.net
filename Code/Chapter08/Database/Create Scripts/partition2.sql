ALTER TABLE [Traffic].[PageViews]
	ADD CONSTRAINT [PageViewsPK]
	PRIMARY KEY CLUSTERED ([PvId], [PvDate])
	
ALTER TABLE [Traffic].[PageViews]
	SET (LOCK_ESCALATION = AUTO)
	
SELECT partition_number, rows
	FROM sys.partitions
	WHERE object_id = object_id('Traffic.PageViews')