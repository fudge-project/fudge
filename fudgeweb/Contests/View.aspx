<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="Contests_View" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="contestSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Contests" Where="UrlName == @UrlName">
        <WhereParameters>
            <asp:QueryStringParameter Name="UrlName" QueryStringField="name" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="heading">
                <%=Contest.Name %>
            </div>
            <div class="heading_description">
                <% if (FudgeUser != null && !FudgeUser.IsRegisteredFor(Contest.ContestId) && !Contest.HasStarted && !Contest.HasEnded) { %>
                <strong><asp:LinkButton ID="SignUp" runat="server" OnClick="SignUp_Click">Sign up!</asp:LinkButton></strong>
                <% }
                   else if (Contest.HasStarted || Contest.HasEnded) { %>
                <strong><a href="/Contests/Scoreboard/<%=Contest.UrlName %>">View Live Scoreboard</a></strong>
                <% }
                   else if (!Contest.HasEnded) { %>
                <strong>Registered</strong>
                <%  }
                   if (Contest.HasEnded) { %>                
                   <strong><a href="/Contests/Stats/<%=Contest.UrlName %>">Statistics</a></strong>
                   <% } %>
            </div>
            <fudge:Tooltip runat="server" ID="contestTip" Visible="false" />
            <asp:FormView ID="contestView" runat="server" DataKeyNames="ContestId" DataSourceID="contestSource"
                Width="100%">
                <ItemTemplate>
                    <table cellpadding="5">
                        <tr>
                            <td>
                                <b>Date:</b>
                                <div class="description">
                                    The date of the contest
                                    <br />
                                    in your timezone.
                                </div>
                            </td>
                            <td>
                                <b>
                                    <%# FormatHelper.FormatDateNice((DateTime)Eval("StartTime")) %>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Starts:</b>
                                <div class="description">
                                    You can register anytime
                                    <br />
                                    before the contest starts.
                                </div>
                            </td>
                            <td>
                                <b>
                                    <%# FormatHelper.FormatTime((DateTime)Eval("StartTime")) %></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Duration:</b>
                            </td>
                            <td>
                                <b>
                                    <%=FormatHelper.FormatTime(Contest.Duration, true) %>
                                    <%=Contest.Duration.Hours == 0 ? "minutes" : "hours"%>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Participants Per Team:</b>
                                <div class="description">
                                    <%# (int)Eval("TeamSize") == 1 ? "Individual contest" : "Team contest" %>
                                </div>
                            </td>
                            <td>
                                <b>
                                    <%# Eval("TeamSize") %></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Scoring:</b>
                            </td>
                            <td>
                                <b>
                                    <%# GetScoring(Eval("Scoring")) %>
                                </b>-
                                <%# GetScoringDescription(Eval("Scoring")) %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <b>This contest is open to anyone.</b>
                            </td>
                        </tr>
                    </table>
                    Participants will be expected to solve problems in any of the supported language
                    on Fudge.
                </ItemTemplate>
            </asp:FormView>
            <div class="heading" style="margin-top: 10px">
                <%=Contest.ContestUsers.Count %> Participants
            </div>
            <div class="heading_description">
                The following users have signed up for the contest.
            </div>
            <fudge:Pager runat="server" PagerControlID="contestUsers" PageSize="20" />
            <asp:LinqDataSource ID="contestUsersSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                OnSelecting="contestUsers_Selecting" TableName="ContestUsers">
            </asp:LinqDataSource>
            <asp:ListView ID="contestUsers" runat="server" DataSourceID="contestUsersSource">
                <LayoutTemplate>
                    <table cellpadding="4" style="margin-top: 10px">
                        <tbody runat="server" id="itemPlaceholder">
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Html.LinkToProfile((int)Eval("UserId")) %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
