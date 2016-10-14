CREATE TABLE [dbo].[Votes] (
    [VoteId]    INT      IDENTITY (1, 1) NOT NULL,
    [UserId]    INT      NULL,
    [ItemId]    INT      NULL,
    [VoteValue] INT      NULL,
    [VoteTime]  DATETIME NULL
);

