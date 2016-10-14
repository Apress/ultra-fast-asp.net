/*
Deployment script for SampleDB
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar Path1 "E:\sql\"
:setvar DatabaseName "SampleDB"
:setvar DefaultDataPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [Sample], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', MAXSIZE = UNLIMITED, FILEGROWTH = 1024 KB)
    LOG ON (NAME = [Sample_log], FILENAME = '$(Path1)$(DatabaseName)_log.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings for DB_CHAINING or TRUSTWORTHY cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET HONOR_BROKER_PRIORITY OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO

GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO

GO
PRINT N'Creating NT AUTHORITY\NETWORK SERVICE...';


GO
CREATE USER [NT AUTHORITY\NETWORK SERVICE];


GO
PRINT N'Creating <unnamed>...';


GO
EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'NT AUTHORITY\NETWORK SERVICE';


GO
PRINT N'Creating Traffic...';


GO
CREATE SCHEMA [Traffic]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating SearchCatalog...';


GO
CREATE FULLTEXT CATALOG [SearchCatalog]
    WITH ACCENT_SENSITIVITY = ON
    AS DEFAULT
    AUTHORIZATION [dbo];


GO
PRINT N'Creating ByMonth...';


GO
CREATE PARTITION FUNCTION [ByMonth](DATETIME)
    AS RANGE RIGHT
    FOR VALUES ('01/01/2009 00:00:00', '02/01/2009 00:00:00', '03/01/2009 00:00:00', '04/01/2009 00:00:00', '05/01/2009 00:00:00', '06/01/2009 00:00:00', '07/01/2009 00:00:00', '08/01/2009 00:00:00', '09/01/2009 00:00:00');


GO
PRINT N'Creating ByMonthPF...';


GO
CREATE PARTITION FUNCTION [ByMonthPF](DATETIME)
    AS RANGE RIGHT
    FOR VALUES ('01/01/2009 00:00:00', '02/01/2009 00:00:00', '03/01/2009 00:00:00', '04/01/2009 00:00:00', '05/01/2009 00:00:00', '06/01/2009 00:00:00', '07/01/2009 00:00:00', '08/01/2009 00:00:00', '09/01/2009 00:00:00');


GO
PRINT N'Creating ByMonth...';


GO
CREATE PARTITION SCHEME [ByMonth]
    AS PARTITION [ByMonth]
    TO ([PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY]);


GO
PRINT N'Creating ByMonthPS...';


GO
CREATE PARTITION SCHEME [ByMonthPS]
    AS PARTITION [ByMonthPF]
    TO ([PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY]);


GO
PRINT N'Creating dbo.ConfigInfo...';


GO
CREATE TABLE [dbo].[ConfigInfo] (
    [Key]    VARCHAR (64)  NOT NULL,
    [Strval] VARCHAR (256) NULL
);


GO
PRINT N'Creating dbo.Items...';


GO
CREATE TABLE [dbo].[Items] (
    [ItemId]          INT          IDENTITY (1, 1) NOT NULL,
    [ItemName]        VARCHAR (64) NULL,
    [ItemCategory]    VARCHAR (32) NULL,
    [ItemSubcategory] VARCHAR (32) NULL
);


GO
PRINT N'Creating dbo.TextInfo...';


GO
CREATE TABLE [dbo].[TextInfo] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Email] NVARCHAR (256)  NULL,
    [Quote] NVARCHAR (1024) NULL,
    [Info]  VARCHAR (2048)  NULL
);


GO
PRINT N'Creating dbo.Users...';


GO
CREATE TABLE [dbo].[Users] (
    [UserId]   INT          IDENTITY (1, 1) NOT NULL,
    [UserName] VARCHAR (64) NULL
);


GO
PRINT N'Creating dbo.Votes...';


GO
CREATE TABLE [dbo].[Votes] (
    [VoteId]    INT      IDENTITY (1, 1) NOT NULL,
    [UserId]    INT      NULL,
    [ItemId]    INT      NULL,
    [VoteValue] INT      NULL,
    [VoteTime]  DATETIME NULL
);


GO
PRINT N'Creating Traffic.PageViews...';


GO
CREATE TABLE [Traffic].[PageViews] (
    [PvId]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [PvDate] DATETIME         NOT NULL,
    [UserId] UNIQUEIDENTIFIER NULL,
    [PvUrl]  VARCHAR (256)    NOT NULL
) ON [ByMonthPS] ([PvDate]);


GO
PRINT N'Creating Traffic.PageViewsArchive...';


GO
CREATE TABLE [Traffic].[PageViewsArchive] (
    [PvId]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [PvDate] DATETIME         NOT NULL,
    [UserId] UNIQUEIDENTIFIER NULL,
    [PvUrl]  VARCHAR (256)    NOT NULL
) ON [ByMonth] ([PvDate]);


GO
PRINT N'Creating dbo.TextInfo.TextInfoIX...';


GO
CREATE UNIQUE CLUSTERED INDEX [TextInfoIX]
    ON [dbo].[TextInfo]([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating dbo.ConfigInfoPK...';


GO
ALTER TABLE [dbo].[ConfigInfo]
    ADD CONSTRAINT [ConfigInfoPK] PRIMARY KEY CLUSTERED ([Key] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating dbo.ItemsPK...';


GO
ALTER TABLE [dbo].[Items]
    ADD CONSTRAINT [ItemsPK] PRIMARY KEY CLUSTERED ([ItemId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating dbo.UsersPK...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [UsersPK] PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating dbo.VotesPK...';


GO
ALTER TABLE [dbo].[Votes]
    ADD CONSTRAINT [VotesPK] PRIMARY KEY CLUSTERED ([VoteId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating Traffic.PageViewArchivePK...';


GO
ALTER TABLE [Traffic].[PageViewsArchive]
    ADD CONSTRAINT [PageViewArchivePK] PRIMARY KEY CLUSTERED ([PvId] ASC, [PvDate] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [ByMonth] ([PvDate]);


GO
PRINT N'Creating Traffic.PageViewsPK...';


GO
ALTER TABLE [Traffic].[PageViews]
    ADD CONSTRAINT [PageViewsPK] PRIMARY KEY CLUSTERED ([PvId] ASC, [PvDate] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [ByMonthPS] ([PvDate]);


GO
PRINT N'Creating Full Text...';


GO
CREATE FULLTEXT INDEX ON [dbo].[TextInfo]
    ([Email] LANGUAGE 1033, [Quote] LANGUAGE 1033)
    KEY INDEX [TextInfoIX]
    ON [SearchCatalog];


GO
PRINT N'Creating dbo.VotesItemsFK...';


GO
ALTER TABLE [dbo].[Votes]
    ADD CONSTRAINT [VotesItemsFK] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating dbo.VotesUsersFK...';


GO
ALTER TABLE [dbo].[Votes]
    ADD CONSTRAINT [VotesUsersFK] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating dbo.GetConfigInfo...';


GO
CREATE PROCEDURE [dbo].[GetConfigInfo]

AS
begin
	select [Key], [Strval] from [dbo].[ConfigInfo]
end


GO
PRINT N'Creating Traffic.AddPageView...';


GO
CREATE PROCEDURE [Traffic].[AddPageView]
@pvid BIGINT OUTPUT, @userid UNIQUEIDENTIFIER, @pvurl VARCHAR (256)
AS
begin
    set nocount on
    declare @trandate datetime
    set @trandate = getutcdate()
    insert into [Traffic].[PageViews]
        (PvDate, UserId, PvUrl)
        values
        (@trandate, @userid, @pvurl)
    set @pvid = scope_identity()
end


GO
PRINT N'Creating AutoCreatedLocal...';


GO
CREATE ROUTE [AutoCreatedLocal]
    AUTHORIZATION [dbo]
    WITH ADDRESS = N'LOCAL';


GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO

GO

GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
