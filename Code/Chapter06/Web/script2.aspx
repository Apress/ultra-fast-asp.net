<%@ Page EnableTheming="false" StylesheetTheme="" EnableViewState="false"
    Language="C#" CodeFile="script2.aspx.cs" Inherits="script2" %>
<%@ OutputCache Duration="86400" VaryByParam="None" VaryByCustom="ieMozilla" %>
 var Event = {
<asp:MultiView runat="server" ActiveViewIndex="0" ie:ActiveViewIndex="1">
<asp:View runat="server">
    Add: function(obj, evt, func, capture) {
        obj.addEventListener(evt, func, capture);
    },
    Remove: function(obj, evt, func, capture) {
        obj.removeEventListener(evt, func, capture);
    }
</asp:View>
<asp:View runat="server">
    Add: function(obj, evt, func, capture) {
        obj.attachEvent('on' + evt, func);
    },
    Remove: function(obj, evt, func, capture) {
        obj.detachEvent('on' + evt, func);
    }
</asp:View>
</asp:MultiView>
}
