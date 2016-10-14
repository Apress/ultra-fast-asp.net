CREATE TABLE HtmlData
(
	id INT IDENTITY,
	Html VARCHAR(MAX)
)
GO

CREATE PROC GetHtml
(
	@fileId	INT	
)
AS
BEGIN
	SELECT hd.Html
		FROM HtmlData hd 
		WHERE id = @fileId
END
GO

INSERT INTO HtmlData
(
	Html
)
VALUES
(
	'<html><body>This is <b>record 1</b>.</body></html>'
)
INSERT INTO HtmlData
(
	Html
)
VALUES
(
	'<html><body>This is <i>record two</i>.</body></html>'
)