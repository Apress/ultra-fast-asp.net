CREATE FULLTEXT INDEX ON [dbo].[TextInfo]
    ([Email] LANGUAGE 1033, [Quote] LANGUAGE 1033)
    KEY INDEX [TextInfoIX]
    ON [SearchCatalog];

