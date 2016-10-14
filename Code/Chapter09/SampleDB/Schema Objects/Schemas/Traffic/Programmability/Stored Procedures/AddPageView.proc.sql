
create procedure [Traffic].[AddPageView]
    @pvid    bigint out,
    @userid  uniqueidentifier,
    @pvurl   varchar (256)
as
begin
    set nocount on
    declare @trandate datetime
    set @trandate = getutcdate()
    insert into [Traffic].[PageViews]
        (PvDate, UserId, PvUrl)
        values
        (@trandate, @userid, @pvurl)
    set @pvid = scope_identity()
end
