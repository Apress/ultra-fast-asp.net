CREATE TABLE [dbo].[Products] (
    [Id]   INT                                IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (128)                      NULL,
    [Info] XML(CONTENT [dbo].[ProductSchema]) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

