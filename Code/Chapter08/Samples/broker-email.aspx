<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="false" Async="true" CodeFile="broker-email.aspx.cs" Inherits="broker_email" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Email: <asp:TextBox ID="Email" runat="server" /><br />
        Subject: <asp:TextBox ID="Subject" runat="server" /><br />
        Body: <asp:TextBox ID="Body" runat="server" Width="500" /><br />
        <asp:Button ID="Submit" runat="server" Text="Submit" /><br />
        <asp:Label ID="Status" runat="server" ForeColor="Red" />
    </div>
    </form>
</body>
</html>
