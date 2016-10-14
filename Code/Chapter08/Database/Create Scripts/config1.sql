CREATE TABLE [dbo].[ConfigInfo] (
	[Key] VARCHAR(64) NOT NULL,
	[Strval] VARCHAR(256) NULL
)

ALTER TABLE [dbo].[ConfigInfo]
	ADD CONSTRAINT [ConfigInfoPK]
	PRIMARY KEY CLUSTERED ([Key])
	
INSERT INTO [dbo].[ConfigInfo]
	([Key], [Strval]) VALUES ('CookieName', 'CC')
INSERT INTO [dbo].[ConfigInfo]
	([Key], [Strval]) VALUES ('CookiePath', '/p/')
	
CREATE PROCEDURE [dbo].[GetConfigInfo]
AS
BEGIN
	SELECT [Key], [Strval] FROM [dbo].[ConfigInfo]
END