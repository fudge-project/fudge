<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreferenceRadioButton.ascx.cs"
    Inherits="Controls_PreferenceRadioButton" %>
<asp:RadioButtonList RepeatDirection="Horizontal" style="padding:0;" AutoPostBack="true" 
    ID="preference" runat="server" 
    onselectedindexchanged="preference_SelectedIndexChanged">
    <asp:ListItem Text="yes"></asp:ListItem>
    <asp:ListItem Text="no"></asp:ListItem>    
</asp:RadioButtonList>
