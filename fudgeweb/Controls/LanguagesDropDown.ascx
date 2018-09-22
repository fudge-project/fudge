<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguagesDropDown.ascx.cs" Inherits="Controls_LanguagesDropdown" %>
<asp:LinqDataSource ID="languagesSource" runat="server" 
    ContextTypeName="Fudge.Framework.Database.FudgeDataContext" 
    TableName="Languages" Where="Visible == @Visible" OrderBy="SourceId">
    <WhereParameters>
        <asp:Parameter DefaultValue="true" Name="Visible" Type="Boolean" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:DropDownList ID="languages" runat="server" DataSourceID="languagesSource" 
    DataTextField="Name" DataValueField="LanguageId" 
    ondatabound="languages_DataBound">
</asp:DropDownList>
