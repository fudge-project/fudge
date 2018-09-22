<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="Teams_View" ValidateRequest="false" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        table tr td
        {
            padding: 3px;
        }
        .user_list .user
        {
            display: inline;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:LinqDataSource ID="teamSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableUpdate="True" TableName="Teams" Where="TeamId == @TeamId">
        <WhereParameters>
            <asp:QueryStringParameter Name="TeamId" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:FormView ID="teamView" Width="100%" runat="server" DataSourceID="teamSource">
        <ItemTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="padding-right: 5px; width: 60%">
                        <table cellpadding="4">
                            <tr>
                                <td>
                                    <img src="/Images/<%=Team.PictureId %>" style="border: 1px soild #CCC" />
                                </td>
                                <td>
                                    <span style="font-size: 22px">
                                        <%# Eval("Name") %></span>
                                    <div class="description">
                                        <b>
                                            <%# Eval("Description") %></b><br />
                                        <%=StatusDescription %>
                                        <br />
                                        <br />
                                        Created
                                        <%# FormatHelper.FormatDateNice((DateTime)Eval("Timestamp")) %>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: left; vertical-align: top; padding-left: 20px; border-left: 1px solid #CCC">
                        <div class="heading">
                            Admins
                        </div>
                        <asp:LinqDataSource ID="adminSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                            OnSelecting="adminSource_Selecting" TableName="TeamUsers">
                        </asp:LinqDataSource>
                        <asp:DataList ID="adminList" RepeatDirection="Horizontal" RepeatColumns="3" runat="server"
                            DataSourceID="adminSource">
                            <ItemTemplate>
                                <div style="margin-top: 5px; margin-right: 5px">
                                    <%# Html.LinkToProfile((int)Eval("UserId")) %>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
            <div class="heading">
            </div>
            <div class="heading_description">
                <% if (FudgeUser.IsAdmin(Team.TeamId)) { %>
                <a href="Settings/<%# Eval("TeamId") %>">
                    <img src="/site/images/group_edit.png" />
                    Group Settings</a>
                <%}
                   else if (FudgeUser.IsInvitedTo(Team.TeamId)) { %>
                <asp:LinkButton ID="joinTeam" runat="server" OnClick="joinTeam_Click">
                <img src="/site/images/bullet_add.png" />
                        I&#39;m in!</asp:LinkButton>
                <asp:LinkButton ID="rejectTeam" runat="server" OnClick="rejectTeam_Click">
                <img src="/site/images/bullet_delete.png" />
                        Thanks but no thanks!</asp:LinkButton>
                <% }
                   else if (!Team.TeamUsers.Any(u => u.UserId == FudgeUser.UserId)) {%>
                <asp:LinkButton ID="requestTeam" runat="server" OnClick="requestTeam_Click">                
                        Join this team!</asp:LinkButton>
                <% }
                   else if (Team.TeamUsers.Any(u => u.UserId == FudgeUser.UserId && u.Status == TeamUserStatus.Requested)) { %>
                Pending Approval
                <% }
                   else if (FudgeUser.IsMemberOf(Team.TeamId)) { %>
                <asp:LinkButton ID="leaveTeam" runat="server" OnClick="leaveTeam_Click">                
                        Leave Team</asp:LinkButton>
                <% } %>
            </div>
            <fudge:Tooltip runat="server" ID="message" Visible="false" />
            <div class="heading" style="margin-top: 10px">
                Statistics
            </div>
            <div class="heading_description">
            </div>
            <div class="heading" style="margin-top: 10px">
                Events
            </div>
            <div class="heading_description">
                <% if (FudgeUser.IsAdmin(Team.TeamId)) { %>
                <a href="#">
                    <img src="/site/images/calendar_add.png" />
                    Create an Event</a>
                <%} %>
            </div>
            <div class="heading" style="margin-top: 10px">
                The Message Pump
            </div>
            <div class="heading_description">
                Join in the team discussion!
            </div>
            <fudge:Comments OnPosted="OnCommentPosted" MaxPosts="10" CanModifyPost='<%# FudgeUser.IsAdmin((int)Eval("TeamId")) %>'
                CanPost='<%# FudgeUser.IsMemberOf((int)Eval("TeamId")) %>' ID="Comments1" runat="server"
                TopicId='<%# Eval("TopicId") %>' />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="heading" style="margin-top: 10px">
                        Users
                    </div>
                    <table style="border-collapse: collapse; width: 100%">
                        <tr>
                            <td>
                            </td>
                            <td style="text-align: right">
                                <fudge:Pager runat="server" ID="userPager" PagerControlID="teamUsers" />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                        <ProgressTemplate>
                            <fudge:Tooltip ID="progress" runat="server" IsLoadingTip="true" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:LinqDataSource ID="usersSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                        TableName="Users" OnSelecting="usersSource_Selecting">
                    </asp:LinqDataSource>
                    <asp:ListView ID="teamUsers" runat="server" DataSourceID="usersSource" OnItemDataBound="teamUsers_ItemDataBound">
                        <LayoutTemplate>
                            <div class="user_list">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="user">
                                <fudge:MiniProfile ShowLinks="false" runat="server" ID="user" UserId='<%# Eval("UserId") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
