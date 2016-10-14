CREATE TABLE TextInfo (
	Id		INT				IDENTITY,
	Email	NVARCHAR(256),
	Quote	NVARCHAR(1024)
)

CREATE UNIQUE CLUSTERED INDEX TextInfoIX ON TextInfo (Id)

INSERT INTO TextInfo (Email, Quote)
	VALUES (N'joe@gmail.com', N'The less you talk, the more you''re listened to.')
INSERT INTO TextInfo (Email, Quote)
	VALUES (N'bob@yahoo.com', N'Nature cannot be fooled.')
INSERT INTO TextInfo (Email, Quote)
	VALUES (N'mary@gmail.com', N'The truth is not for all men.')
INSERT INTO TextInfo (Email, Quote)
	VALUES (N'alice@12titans.net', N'Delay is preferable to error.')
	
CREATE FULLTEXT CATALOG [SearchCatalog] AS DEFAULT

CREATE FULLTEXT INDEX
	ON TextInfo (Email, Quote)
	KEY INDEX TextInfoIX
	
SELECT FULLTEXTCATALOGPROPERTY('SearchCatalog', 'PopulateStatus')

SELECT * FROM TextInfo t WHERE t.Email LIKE '%12titans%'

SELECT * FROM TextInfo WHERE CONTAINS(Email, N'12titans')

SELECT * FROM TextInfo WHERE contains(Quote, N'"nat*"')

SELECT * FROM TextInfo WHERE contains((Email, Quote), N'truth OR bob')

SELECT * FROM TextInfo WHERE freetext(Quote, N'man')

SELECT ftt.[RANK], t.Id, t.Quote
	FROM TextInfo AS t
	INNER JOIN CONTAINSTABLE([TextInfo], [Quote], 'delay ~ error') ftt
		ON ftt.[KEY] = t.Id
	ORDER BY ftt.[RANK]