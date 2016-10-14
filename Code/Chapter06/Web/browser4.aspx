<%@ Page Language="C#" AutoEventWireup="true" CodeFile="browser4.aspx.cs" Inherits="browser4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<asp:MultiView runat="server" ie:ActiveViewIndex="0" ActiveViewIndex="1">
  <asp:View runat="server">
    This is <a href="#">for IE</a>
  </asp:View>
  <asp:View runat="server">
    And this is <a href="#">for others</a>
  </asp:View>
</asp:MultiView>
</body>
</html>
