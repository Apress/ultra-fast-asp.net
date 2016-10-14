create procedure [dbo].[GetConfigInfo]
as
begin
	select [Key], [Strval] from [dbo].[ConfigInfo]
end