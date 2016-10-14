
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
