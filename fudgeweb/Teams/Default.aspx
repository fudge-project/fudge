<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Teams_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .team_list
        {
            margin-top: 10px;
        }
        .team_list .team
        {
            margin-top: 15px;
            margin-right: 10px;
            display: inline;
            float: left;
            width: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    Teams
                    <div class="description">
                        Create a new team and meet people on Fudge.
                    </div>
                </td>
                <td style="text-align: right;font-size:12px">
                    <asp:TextBox ID="teamSearch" Width="200" CssClass="search_box" runat="server"></asp:TextBox>
                    <asp:Button ID="Search" CssClass="search_button" runat="server" Text="Search" OnClick="Search_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="heading_description">
        <% if (FudgeUser != null) { %>
        <a href="/Teams/Add">
            <img src="/site/images/group_add.png" />
            Create Team</a>
        <% } %>
    </div>
    <asp:LinqDataSource ID="teamsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableUpdate="True" TableName="Teams" OnSelecting="teamsSource_Selecting">
    </asp:LinqDataSource>
    <asp:ListView ID="teams" runat="server" DataSourceID="teamsSource">
        <LayoutTemplate>
            <div class="team_list">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <table class="team" style="border-collapse: collapse;">
                <tr>
                    <td style="height: 96px; width: 96px; text-align: center">
                        <a href="/Teams/<%# Eval("TeamId") %>">
                            <img src="/Images/<%# Eval("PictureId") %>" style="border: 1px soild #CCC" />
                        </a>
                    </td>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td>
                                    <%# Html.LinkToTeamProfile((int)Eval("TeamId")) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Members :</b>
                                    <%# MemberCount(Eval("Members")) %>
                                </td>
                            </tr>
                            <tr>
                                <td class="description" style="font-weight: bold">
                                    Created
                                    <%# FormatTeamDate((DateTime)Eval("Timestamp"))%>
                                    <br />
                                    by
                                    <%# Html.LinkToProfile((int)Eval("UserId")) %>
                                    <br />
                                    <b>
                                        <%# GetDescription((Fudge.Framework.Database.TeamStatus)Eval("Status")) %></b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
