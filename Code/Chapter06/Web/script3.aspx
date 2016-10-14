<%@ Page MasterPageFile="~/master/Main.master" Language="C#" AutoEventWireup="true"
    CodeFile="script3.aspx.cs" Inherits="script3" %>
<asp:Content runat="server" ID="NW" ContentPlaceHolderID="LG">
<asp:Label runat="server" ID="myInfo" Text="Initial text" />
<script type="text/javascript">
    function RegObj(clientId, anId) {
        eval('window.' + clientId + ' = document.getElementById(anId)');
    }
    RegObj('mytext', '<%= myInfo.ClientID %>');
    mytext.innerHTML = 'Reset text';
</script>
</asp:Content>
