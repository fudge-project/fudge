<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/Default.master" Title="(*Fudge).The Social Programming Network" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="Stylesheet" href="/site/style/maindefault.css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <% if (FudgeUser == null) { %>
    <div class="mission">
        <div class="heading">
            <%=UserCount%>
            <b>geeks</b>
            <%=SchoolCount%>
            <b>schools</b>
            <%=CountryCount%>
            <b>countries</b>
        </div>
        <div style="border-bottom: 1px solid #CCC;">
            <p style="margin: 10px">
                <b style="font-size: 20px">Fudge</b> is a website that links <b><a href="/Users/Search.aspx">
                    programmers</a></b> in <b><a href="/Schools/">schools</a> across the world.</b>.
            </p>
        </div>
        <ul>
            <li><b>Make friends</b>
                <div class="description">
                    <b>Find friends on Fudge from your school and other schools around the world.</b>
                </div>
            </li>
            <li><b><a href="/Problems/Archive">Solve Problems</a></b>
                <div class="description">
                    <b>Fudge has
                        <%=ProblemCount%>
                        interesting problems for users to solve.</b>
                </div>
            </li>
            <li><b>Join Discussions</b>
                <div class="description">
                    <b>Need help with a problem? Got a job interview next week? Ask questions and join the
                        discussion.</b>
                </div>
            </li>
            <li><b>Join Teams</b>
                <div class="description">
                    <b>Create teams and join forces to solve problems, plan events and communicate with
                        friends.</b>
                </div>
            </li>
            <li><b>Compete in Contests</b>
                <div class="description">
                    <b>Compete in contests, boost your ranking!</b>
                </div>
            </li>
        </ul>
        <div class="links">
            <a href="Help/Faq.aspx#q12">Why can't I join Fudge :( ?</a> <a href="/Users/Register.aspx">
                Sign up now!</a>
        </div>
    </div>
    <% }
       else { %>    
        <fudge:NewsFeed runat="server" ID="newsFeed" />
    <% } %>
    <div id="sidebar">
        <ul>
            <li id="categories">
                <h2>
                    Find a User</h2>
                <ul>
                    <li>
                        <table cellpadding="3">
                            <tr>
                                <td>
                                    <asp:TextBox CssClass="search_box" Width="125" ID="userSearch" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton ID="searchUser" runat="server" OnClick="searchUser_Click">Go</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="/Users/Search">Advanced search &#187;</a>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </li>
            <li id="calendar">
                <h2>
                    Top 10</h2>
                <div>
                    <asp:ListView ID="toptenRankings" runat="server">
                        <LayoutTemplate>
                            <table cellpadding="3" style="margin-left: 30px; background-color: Transparent; border-style: none;">
                                <tr>
                                    <th style="text-align: left">
                                        #
                                    </th>
                                    <th style="text-align: left">
                                        User
                                    </th>
                                    <th style="text-align: left">
                                        Points
                                    </th>
                                </tr>
                                <tbody runat="server" id="itemPlaceholder">
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="border-style: none; background-color: Transparent; text-align: center">
                                    <%# Eval("GlobalRank") %>.
                                </td>
                                <td style="border-style: none; background-color: Transparent; width: 50%; text-align: left">
                                    <%#  Html.LinkToProfile((int)Eval("UserId")) %>
                                </td>
                                <td style="border-style: none; background-color: Transparent; text-align: left">
                                    <%# Eval("Points") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </li>
            <li>
                <h2>
                    <a name="invite" style="color: White; text-decoration: none">User Referral</a></h2>
                <div class="referral">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <b>Email:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="email" Width="125" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="referUser" runat="server" Text="Invite" OnClick="referUser_Click" />
                                    </td>
                                </tr>
                            </table>
                            <div class="error" runat="server" id="message" visible="false">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </li>
        </ul>
    </div>
    <!-- end sidebar -->
    </div>
</asp:Content>
