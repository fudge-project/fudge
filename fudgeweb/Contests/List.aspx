<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="Contests_List" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .contest
        {
            margin-top: 10px;
        }
        .contest .heading a
        {
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="contestSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        OnSelecting="contestSource_Selecting" TableName="Contests">
    </asp:LinqDataSource>
    <asp:ListView ID="contests" runat="server" DataSourceID="contestSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="contest">
                <div class="heading">
                    <%# Html.LinkToContest((int)Eval("ContestId")) %>
                    <div class="description">
                    </div>
                </div>
                <div class="heading_description">
                    <strong><a href="/Contests/Scoreboard/<%# Eval("UrlName") %>">Scoreboard</a></strong>
                    <strong><a href="/Contests/Stats/<%# Eval("UrlName") %>">Statistics</a></strong>
                </div>
                <table>
                    <tr>
                        <td>
                            <strong>
                                <%= IsPast || IsActive ? "Participants:" : "Signed up:"  %></strong>
                        </td>
                        <td>
                            <%# Eval("ContestUsers.Count") %>
                        </td>
                    </tr>
                    <% if (IsPast) { %>
                    <tr>
                        <td>
                            <strong>Winner :</strong>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <% } %>
                </table>
                <% if (IsPast) { %>
                <div class="heading">
                    Problem Set
                </div>
                <div class="heading_description">
                    <strong><%# Eval("ContestProblems.Count") %> problems</strong>
                </div>
                <asp:DataList CellPadding="4" RepeatDirection="Vertical" ID="problemSet" DataSource='<%# Eval("ContestProblems") %>'
                    runat="server">
                    <ItemTemplate>
                        <%# Eval("Problem.Name") %>
                    </ItemTemplate>
                </asp:DataList>
                <% } %>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
