<%@ Page Language="C#" AutoEventWireup="true" CodeFile="image1.aspx.cs" Inherits="image1" %>
<%@ Register Src="~/Controls/imagesub.ascx" TagPrefix="ctl" TagName="imagesub" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<ctl:imagesub runat="server" src="~/CSG.png" height="343" width="56" alt="Test Image" />
</body>
</html>
