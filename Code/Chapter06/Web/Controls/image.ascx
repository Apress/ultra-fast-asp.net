<%@ Control Language="C#" AutoEventWireup="true" CodeFile="image.ascx.cs"
    Inherits="Controls_image" %>
<%@ OutputCache Duration="86400" VaryByControl="src" Shared="true" %>
<img src="<%= src %>" height="<%= height %>" width="<%= width %>" alt="<%= alt %>" />