ALTER DATABASE [Sample] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE
 
CREATE MESSAGE TYPE [//12titans.net/TaskRequest]
	AUTHORIZATION [dbo]
	VALIDATION = NONE
	
CREATE CONTRACT [//12titans.net/TaskContract/v1.0]
	AUTHORIZATION [dbo]
	([//12titans.net/TaskRequest] SENT BY INITIATOR)
	
CREATE QUEUE [dbo].[TaskRequestQueue]

CREATE SERVICE [//12titans.net/TaskService]
	AUTHORIZATION [dbo]
	ON QUEUE [dbo].[TaskRequestQueue] ([//12titans.net/TaskContract/v1.0])
	
CREATE PROC [dbo].[SendTaskRequest]
@msg VARBINARY(MAX)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @handle UNIQUEIDENTIFIER
	BEGIN TRANSACTION
		BEGIN DIALOG @handle FROM SERVICE [//12titans.net/TaskService]
			TO SERVICE '//12titans.net/TaskService'
			ON CONTRACT [//12titans.net/TaskContract/v1.0]
			WITH ENCRYPTION = OFF
		;SEND ON CONVERSATION @handle
			MESSAGE TYPE [//12titans.net/TaskRequest] (@msg)
		END CONVERSATION @handle
	COMMIT TRANSACTION
END

CREATE PROC [dbo].[ReceiveTaskRequest]
@msg VARBINARY(MAX) OUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @handle UNIQUEIDENTIFIER
	DECLARE @msgtable TABLE (
		handle		UNIQUEIDENTIFIER,
		[message]	VARBINARY(MAX),
		msgtype		VARCHAR(256)
	);
	SET @handle = NULL
	WAITFOR (
		RECEIVE [conversation_handle], message_body, message_type_name
		FROM [dbo].[TaskRequestQueue]
		INTO @msgtable
	), TIMEOUT 60000
	SELECT @handle = handle
		FROM @msgtable
		WHERE msgtype = 'http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog'
	IF @handle IS NOT NULL
	BEGIN
		END CONVERSATION @handle
	END
	SELECT @msg = [message]
		FROM @msgtable
		WHERE msgtype = '//12titans.net/TaskRequest'
END

DECLARE @msg VARBINARY(MAX)
EXEC dbo.ReceiveTaskRequest @msg OUT
SELECT CONVERT(VARCHAR(MAX), @msg)

DECLARE @msg VARBINARY(MAX)
SET @msg = CONVERT(VARBINARY(MAX), 'abc')
EXEC dbo.SendTaskRequest @msg