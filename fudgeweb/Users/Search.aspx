<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Search.aspx.cs"
    Inherits="Users_Search" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="/site/style/usersearch.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        User Search
    </div>
    <div class="description">
        Search for user checking which options you with to be included in the result.
    </div>
    <div class="search_panel">
        <table>
            <tr>
                <td>
                    <b>Name:</b>
                </td>
                <td>
                    <asp:TextBox CssClass="search_box" ID="name" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>School:</b>
                </td>
                <td>
                    <asp:TextBox CssClass="search_box" ID="school" runat="server" Width="200"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="autoSchool" runat="server" TargetControlID="school"
                                    ServiceMethod="GetCompletionList" ServicePath="~/Services/Schools.asmx">
                                </ajaxToolkit:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Country:</b>
                </td>
                <td>
                    <fudge:CountriesDropDown ID="country" runat="server" AddDefaultCase="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Language Preference:</b>
                </td>
                <td>
                    <fudge:LanguagesDropDown ID="language" runat="server" AddDefaultCase="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="findAll" runat="server" Text="Find All" OnClick="findAll_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="heading">
        Search Results
    </div>
    <fudge:Pager runat="server" ID="resultPager" PagerControlID="searchResults" />
    <asp:LinqDataSource ID="userSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Users" OnSelecting="userSource_Selecting">
    </asp:LinqDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ListView ID="searchResults" runat="server" DataSourceID="userSource">
                <LayoutTemplate>
                    <center>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </center>
                </LayoutTemplate>
                <ItemTemplate>
                    <fudge:MiniProfile ID="friendProfile" runat="server" UserId='<%# Eval("UserId") %>'
                        ShowLinks="true" />
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="error">
                        No Results Found!</div>
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
