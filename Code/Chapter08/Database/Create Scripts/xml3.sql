-- The "text()" part of this query on page 308 is a typo
update [Products]
    set [Info].modify('replace value of
        (/info/color[@part = "legs"])[1]
        with "silver"')
    where [Name] = 'Desk Chair'
GO

UPDATE [Products]
	SET [Info].modify('insert
		<color part="arms">white</color>
		as first
		into (/info)[1]')
	WHERE [Name] = 'Desk Chair'
GO

UPDATE [Products]
	SET [Info].modify('delete
		(/info/color)[1]')
	WHERE [Name] = 'Desk Chair'
GO