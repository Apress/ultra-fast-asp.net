CREATE TABLE [Traffic].[PageViews] (
    [PvId]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [PvDate] DATETIME         NOT NULL,
    [UserId] UNIQUEIDENTIFIER NULL,
    [PvUrl]  VARCHAR (256)    NOT NULL
) ON [ByMonthPS] ([PvDate]);

