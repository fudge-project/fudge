<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Community_Snippets_View" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    Code Snippets
                    <div class="description">
                        <b>Share interesting code snippets with the community.</b>
                    </div>
                </td>
                <td style="text-align: right;font-size: 12px">
                    <table>
                        <tr>
                            <td>
                                <fudge:LanguagesDropDown ID="language" runat="server" AddDefaultCase="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="snippetName" Width="200" CssClass="search_box" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Search" CssClass="search_button" runat="server" Text="Search" OnClick="Search_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="heading_description">
        <% if (FudgeUser != null) { %>
        <a href="/Community/Snippets/New">New Snippet!</a>
        <% } %>
    </div>
    <asp:LinqDataSource ID="snippetSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="CodeSnippets"
        OnSelecting="snippetSource_Selecting">
    </asp:LinqDataSource>
    <fudge:Pager ID="Pager1" runat="server" PageSize="10" PagerControlID="codeSnippetView" />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <fudge:Tooltip IsLoadingTip="true" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ListView ID="codeSnippetView" runat="server" DataSourceID="snippetSource" DataKeyNames="SnippetId">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>                
                <ItemTemplate>
                    <div class="heading_description" style="border-top: 1px solid #CCC">
                        <table style="width: 100%; border-collapse: collapse">
                            <tr>
                                <td>
                                    <b>
                                        <%# Html.Link("/Community/Snippets/" + Eval("SnippetId"), (string)Eval("Name"), 
                                            new System.Xml.Linq.XAttribute("style", "margin-right:0px")) %>
                                        in
                                        <%# Eval("Language.SourceId") %>
                                    </b>
                                </td>
                                <td style="text-align: right;padding-right:5px;">                                    
                                    <asp:ImageButton  Visible='<%# FudgeUser != null && FudgeUser.UserId == (int)Eval("User.UserId") %>' ImageAlign="Top"
                                        ImageUrl="~/site/images/cross.png" ID="deleteSnippet" CommandName="Delete" runat="server" />
                                    <fudge:Popup runat="server" Width="300" ID="snippetPopup" TargetControlID="deleteSnippet">
                                        <popuptemplate>                                    
                                            Are you sure you want to delete this snippet?
                                        </popuptemplate>
                                    </fudge:Popup>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <fudge:SourceView SnippetId='<%# Eval("SnippetId") %>' runat="server" />
                    <div class="description">
                        Submitted by
                        <%# Html.LinkToProfile((int)Eval("User.UserId")) %>
                        on
                        <%# FormatHelper.FormatDateNice((DateTime)Eval("Timestamp")) %>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <fudge:Tooltip runat="server" Text="No results found." IsClosable="false" />
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
