CREATE TABLE [Traffic].[Machines] (
	MachineId		UNIQUEIDENTIFIER,
	CreationDate	DATETIME
)
GO

CREATE PROCEDURE [Traffic].[AddMachine]
	@id		UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @trandate DATETIME
	SET @trandate = GETUTCDATE()
	INSERT INTO [Traffic].[Machines]
		([MachineId], [CreationDate])
	VALUES
		(@id, @trandate)
END 