<%@ Page Language="C#" EnableViewState="false" Async="true" AutoEventWireup="false" CodeFile="paging.aspx.cs" Inherits="paging" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="pvgrid" runat="server" AllowPaging="true"
            OnPageIndexChanging="PageIndexChanging"
            PageSize="5" DataSourceID="PageViewSource">
            <PagerSettings FirstPageText="First" LastPageText="Last"
                Mode="NumericFirstLast" />
        </asp:GridView>
        <asp:ObjectDataSource ID="PageViewSource" runat="server" 
            EnablePaging="True" TypeName="Samples.PageViews"
            SelectMethod="GetRows" SelectCountMethod="GetCount">
        </asp:ObjectDataSource>    
    </div>
    </form>
</body>
</html>
