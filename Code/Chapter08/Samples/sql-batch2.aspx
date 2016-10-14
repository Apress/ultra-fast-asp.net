<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sql-batch2.aspx.cs" Inherits="sql_batch2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
<div>
Record count: <asp:TextBox runat="server" ID="cnt" /><br />
Batch size: <asp:TextBox runat="server" ID="sz" /><br />
<asp:Button ID="Button1" runat="server" Text="Submit" /><br />
<asp:Literal runat="server" ID="info" />
</div>
</form>
</body>
</html>
