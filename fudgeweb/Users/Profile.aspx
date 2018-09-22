<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs"
    Inherits="Users_Profile" %>
<%@ Import Namespace="Fudge.Framework.Database" %>
<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="/site/style/profile.css" />
    <%=Html.Rss("Users/{0}/Feed", Request.QueryString["id"])%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="userData" runat="server" EnableUpdate="true" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Users" Where="UserId == @UserId">
        <WhereParameters>
            <asp:QueryStringParameter Name="UserId" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:FormView ID="userProfile" DataSourceID="userData" Width="100%" runat="server"
        DataKeyNames="UserId" OnItemCommand="userProfile_ItemCommand" OnItemUpdating="userProfile_ItemUpdating"
        OnDataBound="userProfile_DataBound" OnItemUpdated="userProfile_ItemUpdated">
        <EditItemTemplate>
            <div class="heading">
                Profile
                <div class="description">
                </div>
            </div>
            <div class="heading_description">
                <a href="/Users/Profile">
                    <img src="/site/images/user_go.png" />
                    Back to profile </a>
            </div>
            <fudge:Tooltip Text="Your preferences have been saved." runat="server" Closable="false"
                ID="editTip" Visible="false" />
            <div class="userform" runat="server" id="defaultSettings">
                <table>
                    <tr>
                        <td>
                            <strong>Name:</strong>
                            <div class="description">
                            </div>
                        </td>
                        <td>
                            <%# Eval("FullName")%>
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <strong>Email:</strong>
                            <div class="description">
                                By default, emails are sent to your login email.
                                <br />
                                If you have another email you wish to<br />
                                receive messages to, set it here.
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="email" Text='<%# Bind("SecondaryEmail")%>' runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email"
                                ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Country:</strong>
                            <div class="description">
                                Select the country you wish to represent.
                            </div>
                        </td>
                        <td>
                            <fudge:CountriesDropDown runat="server" ID="country" SelectedCountryId='<%# Bind("CountryId") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Timezone:</strong>
                            <div class="description">
                                All dates on the website will be in your specified timezone.
                            </div>
                        </td>
                        <td>
                            <fudge:TimezonesDropDown runat="server" ID="timezone" SelectedTimezone='<%# Bind("Timezone") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Avatar:</strong>
                            <div class="description">
                                Supported files types: jpeg, gif, bmp
                            </div>
                        </td>
                        <td>
                            <asp:FileUpload ID="avatarupload" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Language Preference:</strong>
                        </td>
                        <td>
                            <fudge:LanguagesDropDown runat="server" ID="language" SelectedLanguageId='<%# Bind("LanguageId") %>' />
                        </td>
                    </tr>
                </table>
                <div class="heading" style="margin-top: 10px">
                    Privacy
                </div>
                <div class="heading_description">
                    <img src="/site/images/lock_go.png" />
                    <strong>Configure your privacy options</strong>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <strong>Hide last name:</strong>
                                </td>
                                <td>
                                    <fudge:PreferenceButton runat="server" Option="HideLastName" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Opt out of Fudge emails:</strong>
                                </td>
                                <td>
                                    <fudge:PreferenceButton runat="server" Option="NoEmailNotifications" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Subscribe to my topics:</strong>
                                </td>
                                <td>
                                    <fudge:PreferenceButton runat="server" Option="AutomaticallySubscribeToMyTopics" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Subscribe to topics I reply to:</strong>
                                </td>
                                <td>
                                    <fudge:PreferenceButton runat="server" Option="AutomaticallySubscribeToTopicsIReplyTo" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button Style="margin-top: 10px" ID="UpdateSettings" CommandName="Update" runat="server"
                    Text="Save Preferences"></asp:Button>
            </div>
        </EditItemTemplate>
        <ItemTemplate>
            <table cellpadding="4">
                <tr>
                    <td>
                        <img src='<%# UserImage %>' />
                    </td>
                    <td>
                        <%# FormatHelper.FormatOnlineStatus((int)Eval("UserId"))%>
                        <span style="font-size: 22px">
                            <%# CurrentUser.FullName %>
                        </span>
                        <div class="description">
                            <b>currently
                                <%=UserExtensions.OnlineUsers.Contains(CurrentUser.UserId) ? "online" : "offline"%>
                            </b>
                            <br />
                            <br />
                            Joined
                            <%# FormatHelper.FormatDateNice((DateTime)Eval("Timestamp"))%>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="heading">
            </div>
            <div class="heading_description">
                <table style="border-collapse: collapse; width: 100%">
                    <tr>
                        <td>
                            <% if (!CurrentUser.IsLoggedOn) { %>
                            <% if (CurrentUser.IsPendingApproval(FudgeUser.UserId)) { %>
                            <asp:LinkButton ID="approveFriend" runat="server" CommandName="Approve" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                Approve
                            </asp:LinkButton>
                            <asp:LinkButton ID="rejectFriend" runat="server" CommandName="Reject" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                Reject
                            </asp:LinkButton>
                            <%  }
                               else if (FudgeUser.IsPendingApproval(CurrentUser.UserId)) { %>
                            <span class="disabled">Pending Approval</span>
                            <% }
                               else if (CurrentUser.IsFriend(FudgeUser.UserId)) { %>
                            <asp:LinkButton ID="removeFriend" runat="server" CommandName="RemoveFriend" CommandArgument='<%# Eval("UserId") %>'>
                                <img src="/site/images/user_delete.png" style="vertical-align:middle;padding-right:2px" />
                                    Remove Friend
                            </asp:LinkButton>
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="removeFriend"
                                DisplayModalPopupID="ModalPopupExtender1">
                            </ajaxToolkit:ConfirmButtonExtender>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="removeFriend"
                                PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="PNL" runat="server" Style="display: none; width: 200px; background-color: White;
                                border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                Are you sure you want to delete
                                <%# Eval("DisplayName")%>
                                from your friends?
                                <br />
                                <br />
                                <div style="text-align: right;">
                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
                            <% }
                               else if (!CurrentUser.IsRejected(FudgeUser.UserId)) { %>
                            <asp:LinkButton ID="addFriend" runat="server" CommandName="AddFriend" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                <img src="/site/images/user_add.png" style="vertical-align:middle;padding-right:2px" />
                                    Add To Friends
                            </asp:LinkButton>
                            <% } %>
                            <a href="#">Head to Head</a>
                            <% } %>
                            <% else { %>
                            <a href="/Users/Profile/Edit">
                                <img src="/site/images/user_edit.png" />
                                Edit Profile </a>
                            <% } %>
                        </td>
                        <td style="text-align: right">
                            <a href="/Users/<%=CurrentUser.UserId %>/Feed" style="text-decoration: none; margin-left: 5px">
                                <img src="/site/images/rss.gif" align="middle" />
                            </a>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="container">
                <div class="title">
                    School:
                </div>
                <div class="content">
                    <%# Html.LinkToSchoolProfile((int)Eval("School.SchoolId"))%></div>
                <div class="title">
                    Country:
                </div>
                <div class="content">
                    <%# Eval("Country.PictureId") == null ? String.Empty :  Html.LinkToPicture((int)Eval("Country.PictureId"))%>
                    <%# Eval("Country.Name")%></div>
                <div class="title">
                    Display Name:
                </div>
                <div class="content">
                    <%# Eval("DisplayName")%></div>
                <div class="title">
                    default language:
                </div>
                <div class="content">
                    <%# Eval("Language.Name")%></div>
                <div class="title">
                    Timezone:
                </div>
                <div class="content">
                    <%# Eval("Timezone")%></div>
                <div class="title">
                    Fudge Points:
                </div>
                <div class="content">
                    <%# Eval("Points")%>
                </div>
                <div class="title">
                    Posts:
                </div>
                <div class="content">
                    <%=CurrentUser.Posts.Count(p => p.Topic.Visible) %>
                </div>
                <div class="title">
                    Rank:
                </div>
                <div class="content">
                    <%# Eval("GlobalRank")%>
                </div>
                <div class="title">
                    School Rank:
                </div>
                <div class="content">
                    <%# Eval("SchoolRank")%>
                </div>
                <div class="title">
                    Country Rank:
                </div>
                <div class="content">
                    <%# Eval("CountryRank")%>
                </div>
            </div>
            <div class="heading">
                Statistics
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="heading_description">
                        <table style="border-collapse: collapse; width: 100%">
                            <tr>
                                <th style="text-align: left">
                                    <b>Problems Solved</b>(<%=CurrentUser.SolvedProblems.Count()%>)
                                </th>
                                <th style="text-align: right">
                                    <fudge:Pager runat="server" ID="problemStatPager" PagerControlID="problemView" />
                                </th>
                            </tr>
                        </table>
                    </div>
                    <asp:LinqDataSource ID="solvedProblemsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                        OnSelecting="solvedProblemSource_Selecting" TableName="Problems">
                    </asp:LinqDataSource>
                    <asp:ListView ID="problemView" runat="server" DataSourceID="solvedProblemsSource">
                        <LayoutTemplate>
                            <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <fudge:Tooltip runat="server" IsLoadingTip="true" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div class="problem_view">
                                <table>
                                    <tr>
                                        <th style="width: 25px">
                                        </th>
                                        <th style="width: 30%">
                                            Name
                                        </th>
                                        <th style="width: 80px">
                                            Solved
                                        </th>
                                        <th style="width: 80px">
                                            Attempts
                                        </th>
                                        <th style="width: 80px">
                                            Languages
                                        </th>
                                        <th style="width: 100px">
                                            Compile Errors
                                        </th>
                                        <th style="width: 100px">
                                            Wrong Answers
                                        </th>
                                        <th style="width: auto">
                                            Stats
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# FormatHelper.NewProblemSolved(CurrentUser.SolvedWhenNew((int)Eval("Problem.ProblemId")))%>
                                </td>
                                <td>
                                    <%# Html.LinkToRuns(CurrentUser.UserId, (int?)Eval("Problem.ProblemId"), null, (string)Eval("Problem.Name"))%>
                                </td>
                                <td>
                                    <%# Eval("Solved")%>
                                </td>
                                <td>
                                    <%# Eval("Attempts")%>
                                </td>
                                <td>
                                    <%# Eval("Languages")%>
                                </td>
                                <td>
                                    <%# Eval("CompileError")%>
                                </td>
                                <td>
                                    <%# Eval("WrongAnswer")%>
                                </td>
                                <td>
                                    <fudge:VisualProgress runat="server" ID="progress" MaxValue="150" BackgroundColor="white"
                                        BorderColor="#CCCCCC" ProgressIndicatorColor="#CCFFCC" TextColor="gray" Numerator='<%# Eval("Solved") %>'
                                        Denominator='<%# Eval("Attempts") %>' Text='<%# Eval("Solved").ToString() + " \\ " + (string)Eval("Attempts").ToString() %>'>
                                    </fudge:VisualProgress>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="fudge_message" style="margin-top: 10px; margin-bottom: 10px">
                                <%=CurrentUser.FirstName%>
                                has not solved any problems
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="heading" style="margin-top: 10px;">
                Standings
            </div>
            <div class="heading_description" style="margin-bottom: 5px">
                Showing your fudge point progress over time.
            </div>
            <center>
                <graph:ChartControl runat="server" ID="standings" Width="700" ChartPadding="30" GridLines="Both"
                    HasChartLegend="False" Padding="12" ShowTitlesOnBackground="true" YCustomEnd="0"
                    YCustomStart="0" YValuesInterval="0">
                    <Border Color="LightGray" />
                    <YAxisFont Font="Consolas, 12px, style=Bold" ForeColor="Black" StringFormat="Far,Near,Character,LineLimit" />
                    <XTitle ForeColor="White" StringFormat="Center,Far,Character,LineLimit" />
                    <PlotBackground Color="White" />
                    <XAxisFont Font="Consolas, 12px, style=Bold" ForeColor="Black" StringFormat="Center,Near,Character,LineLimit" />
                    <Background Color="White" />
                    <ChartTitle Font="Tahoma, 12pt, style=Bold" ForeColor="Black" StringFormat="Center,Near,Character,LineLimit" />
                    <YTitle ForeColor="White" StringFormat="Near,Near,Character,DirectionVertical" />
                    <Charts>
                        <graph:LineChart DataXValueField="Key" DataYValueField="Points">
                            <DataLabels ShowXTitle="true">
                            </DataLabels>
                        </graph:LineChart>
                    </Charts>
                </graph:ChartControl>
            </center>
            <div class="heading">
                Contests
            </div>
            <div class="heading_description">
                <strong>Contests <%=CurrentUser.IsLoggedOn ? "you" : CurrentUser.FirstName %> have participated in</strong>
            </div>
            
            <div class="heading" style="margin-top: 10px;">
                Friends
            </div>
            <div class="heading_description">
                <% if (CurrentUser.ApprovedFriends.Count() > 0) { %>
                <a href="/Users/Friends<%# CurrentUser.IsLoggedOn ? "" : "/" + Eval("UserId") %>">
                    <img src="/site/images/group.png" />
                    <%=GetFriendDescription(CurrentUser.ApprovedFriends.Count())%>
                </a>
                <% }
                   else { %>
                <%=GetFriendDescription(CurrentUser.ApprovedFriends.Count())%>
                <%} %>
            </div>
            <% if (CurrentUser.IsLoggedOn && CurrentUser.PendingFriends.Any()) { %>
            <div class="fudge_message" style="margin-top: 10px; margin-bottom: 10px">
                <b>You have
                    <%=CurrentUser.PendingFriends.Count()%>
                    friend
                    <%=CurrentUser.PendingFriends.Count() == 1 ? "request" : "requests"%>. Click <a href="/Users/Friends/Pending"
                        style="margin-right: 0px">here</a> to see
                    <%=CurrentUser.PendingFriends.Count() == 1 ? "it" : "them"%>
                </b>
            </div>
            <% } %>
            <fudge:FriendsList runat="server" MaxFriends="4" UserId='<%# Eval("UserId") %>' ID="friends"
                Filter="Accepted" />
            <div class="heading" style="margin-top: 10px">
                Community
            </div>
            <div class="heading_description">
                Recent Posts
            </div>
            <asp:LinqDataSource ID="recentPostsSource" runat="server" 
                ContextTypeName="Fudge.Framework.Database.FudgeDataContext" 
                onselecting="recentPostsSource_Selecting" TableName="Posts">
            </asp:LinqDataSource>   
            <asp:ListView ID="recentPosts" runat="server" DataSourceID="recentPostsSource">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                <div style="margin-bottom:10px">
                    <%# Html.LinkToPost((int)Eval("PostId"))%>
                    on
                    <%# FormatHelper.FormatDateNice((DateTime)Eval("Timestamp"))%>
                    </div>
                </ItemTemplate>
            </asp:ListView>                   
            <% if (CurrentUser.IsLoggedOn) { %>
            <div class="heading">
                Notifications
            </div>
            <div class="heading_description">
                <img src="/site/images/note.png" />
                <% if (CurrentUser.Notifications.Count == 0) { %>
                You have no notifications
                <% }
                   else { %>
                <a href="/Users/Notifications">You have
                    <%=CurrentUser.Notifications.Count%>
                    <% if (CurrentUser.Notifications.Count == 1) { %>
                    notification
                    <% }
                       else { %>
                    notifications
                    <% } %>
                </a>
                <% } %>
            </div>
            <asp:LinqDataSource ID="notificationsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                EnableUpdate="True" EnableDelete="true" OnSelecting="notificationsSource_Selecting"
                TableName="Notifications">
            </asp:LinqDataSource>
            <asp:DataList ID="notifications" runat="server" DataSourceID="notificationsSource">
                <ItemTemplate>
                    <div style="margin-top: 10px">
                        <%# FormatHelper.GetNotificationImage((int)Eval("Type"))%>
                        <%# Eval("Text")%>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <% } %>
            <div class="heading" style="margin-top: 10px">
                Teams
            </div>
            <div class="heading_description">
                Member of
                <%=CurrentUser.TeamUsers.Count(t => t.Status == TeamUserStatus.Member || t.Status == TeamUserStatus.Admin)%>
                <%=CurrentUser.TeamUsers.Count(t => t.Status == TeamUserStatus.Member || t.Status == TeamUserStatus.Admin) == 1 ? "Team" : "Teams"%>
                <% if (CurrentUser.TeamUsers.Count(t => t.Status == TeamUserStatus.Member || t.Status == TeamUserStatus.Admin) == 0 && CurrentUser.IsLoggedOn) { %>
                <a href="/Teams/Default.aspx">Join a Team!</a>
                <% } %>
            </div>
            <asp:LinqDataSource ID="teamsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                EnableUpdate="True" OnSelecting="teamsSource_Selecting" TableName="Teams">
            </asp:LinqDataSource>
            <asp:ListView ID="myTeams" runat="server" DataSourceID="teamsSource">
                <LayoutTemplate>
                    <div class="teams">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div style="margin-right: 10px; float: left">
                        <img src='/Images/<%# Eval("Team.PictureId") %>' height="32" width="32" align="middle" />
                        <%# Html.LinkToTeamProfile((int)Eval("TeamId"))%>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <div class="heading" style="margin-top: 10px">
                The Stack
            </div>
            <fudge:Comments ShowHeader="true" CanSubscribe="false" PostButtonText="Push Comment" OnPosted="OnCommentPosted"
                runat="server" CanModifyPost='<%# CurrentUser.IsLoggedOn  %>' ShowEmpty="false"
                CanPost='<%# CurrentUser.IsLoggedOn || FudgeUser.IsFriend(CurrentUser.UserId) %>'
                DeleteButtonText="Pop off the stack" MaxPosts="10" TopicId='<%# Eval("TopicId") %>' />
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
