<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountriesDropDown.ascx.cs"
    Inherits="Controls_CountryDropDown" %>
<asp:LinqDataSource ID="countriesSource" runat="server" 
    ContextTypeName="Fudge.Framework.Database.FudgeDataContext" 
    TableName="Countries" OrderBy="Name">
</asp:LinqDataSource>
<asp:DropDownList ID="countries" DataTextField="Name" DataValueField="CountryId"
    runat="server" DataSourceID="countriesSource" 
    ondatabound="countries_DataBound">
</asp:DropDownList>
