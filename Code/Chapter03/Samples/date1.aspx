<%@ Page Language="C#" AutoEventWireup="true" CodeFile="date1.aspx.cs" Inherits="date1" %>
<%@ Register Src="~/Controls/Date.ascx" TagPrefix="ct" TagName="Date" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<ct:Date ID="Date1" runat="server" />
<br/>Page time: <%= DateTime.Now.ToString() %>
</body>
</html>
