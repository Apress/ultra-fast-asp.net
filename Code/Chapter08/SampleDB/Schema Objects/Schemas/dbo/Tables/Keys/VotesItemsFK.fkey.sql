﻿ALTER TABLE [dbo].[Votes]
    ADD CONSTRAINT [VotesItemsFK] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
