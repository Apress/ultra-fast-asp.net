CREATE XML SCHEMA COLLECTION ProductSchema AS
'<?xml version="1.0"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
	<xs:element name="info">
		<xs:complexType>
			<xs:sequence>
				<xs:element name="props" minOccurs="0">
					<xs:complexType>
						<xs:attribute name="width" type="xs:decimal" />
						<xs:attribute name="depth" type="xs:decimal" />
					</xs:complexType>
				</xs:element>
				<xs:element name="color" minOccurs="0" maxOccurs="unbounded">
					<xs:complexType>
						<xs:simpleContent>
							<xs:extension base="xs:string">
								<xs:attribute name="part" type="xs:string"
									use="required" />
							</xs:extension>
						</xs:simpleContent>
					</xs:complexType>
				</xs:element>
			</xs:sequence>
		</xs:complexType>
	</xs:element>
</xs:schema>' 
GO

CREATE TABLE [Products] (
	[Id]	INT IDENTITY PRIMARY KEY,
	[Name]	VARCHAR(128),
	[Info]	XML (ProductSchema)
)
GO

INSERT INTO [Products]
	([Name], [Info])
	VALUES
	('Big Table',
	'<info>
		<props width="1.0" depth="3.0" />
		<color part="top">red</color>
		<color part="legs">chrome</color>
	</info>')
INSERT INTO [Products]
	([Name], [Info])
	VALUES
	('Small Table',
	'<info>
		<props width="0.5" depth="1.5" />
		<color part="top">black</color>
		<color part="legs">chrome</color>
	</info>')
INSERT INTO [Products]
	([Name], [Info])
	VALUES
	('Desk Chair',
	'<info>
		<color part="top">black</color>
		<color part="legs">chrome</color>
	</info>')