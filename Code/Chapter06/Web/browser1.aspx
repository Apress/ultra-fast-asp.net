<%@ Page Language="C#" AutoEventWireup="true" CodeFile="browser1.aspx.cs" Inherits="browser1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
User-Agent: <%= Request.UserAgent %><br /><br />
ID: <%= Request.Browser.Id %><br />
Browser: <%= Request.Browser.Browser %><br />
Type: <%= Request.Browser.Capabilities["type"] %>
</body>
</html>
