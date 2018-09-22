<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs"
    Inherits="Teams_Settings" Title="Fudge - Edit Team" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         function BeginRequestHandler(sender, args) {            
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm._scrollPosition = null;            
         }
         
         function EndRequestHandler(sender, args) {
            
         }    
    </script>

    <asp:LinqDataSource ID="teamSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableInsert="True" EnableUpdate="True" TableName="Teams" Where="TeamId == @TeamId">
        <WhereParameters>
            <asp:QueryStringParameter Name="TeamId" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <div class="heading">
        Team Settings
    </div>
    <div class="heading_description">
        Back to <a href="/Teams/<%=Team.TeamId %>">
            <%= Team.Name %></a>
    </div>
    <fudge:Tooltip runat="server" Text="Group preferences have been saved." ID="message"
        Visible="false" />
    <asp:FormView ID="newTeamForm" DefaultMode="Edit" runat="server" DataKeyNames="TeamId"
        DataSourceID="teamSource" Width="100%" OnItemUpdated="newTeamForm_ItemUpdated"
        OnItemUpdating="newTeamForm_ItemUpdating">
        <EditItemTemplate>
            <div class="team">
                <table>
                    <tr>
                        <td>
                            <b>Team Name:</b>
                            <div class="description">
                                Give your team a name.
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="teamName" Width="200" runat="server" Text='<%# Bind("Name") %>' />
                        </td>
                        <tr>
                            <td>
                                <b>Privacy:</b>
                                <div class="description">
                                    Set the privacy settings for your team.
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="privacy" runat="server">
                                    <asp:ListItem Text="Open"></asp:ListItem>
                                    <asp:ListItem Text="Invitation"></asp:ListItem>
                                    <asp:ListItem Text="Private"></asp:ListItem>
                                    <asp:ListItem Text="Closed"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Scope:</b>
                                <div class="description">
                                    Who gets updates?
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="scope" runat="server">
                                    <asp:ListItem Text="Global"></asp:ListItem>
                                    <asp:ListItem Text="Region"></asp:ListItem>
                                    <asp:ListItem Text="Country"></asp:ListItem>
                                    <asp:ListItem Text="School"></asp:ListItem>
                                    <asp:ListItem Text="Friends"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Avatar:</b>
                                <div class="description">
                                    <b>Supported files types:</b> jpeg, gif, bmp.
                                    <br />
                                    <b>Pictures will be resized to 96 x 96.</b>
                                </div>
                            </td>
                            <td>
                                <asp:FileUpload ID="avatarupload" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Description:</b>
                                <div class="description">
                                    Whats this group about?
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="description" Text='<%# Bind("Description") %>' Height="100px" Width="300"
                                    TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="saveSettings" CommandName="Update" runat="server" Text="Save Settings" />
                            </td>
                        </tr>
                    </tr>
                </table>
            </div>
        </EditItemTemplate>
    </asp:FormView>
    <div class="heading" style="margin-top: 10px">
        Members
    </div>
    <div class="heading_description">
        <table style="margin-left: 5px">
            <tr>
                <td>
                    <b>Show only: </b>
                </td>
                <td>
                    <asp:DropDownList ID="memberFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="memberFilter_SelectedIndexChanged">
                        <asp:ListItem Text="All Members"></asp:ListItem>
                        <asp:ListItem Text="Admins"></asp:ListItem>
                        <asp:ListItem Text="Accepted Members"></asp:ListItem>
                        <asp:ListItem Text="Pending Members"></asp:ListItem>
                        <asp:ListItem Text="Banned Members"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%; border-left: 1px solid #CCC; border-right: 1px solid #CCC;
        border-top: 1px solid #CCC">
        <tr>
            <td style="vertical-align: top; width: 60%; border-right: 1px solid #CCC; padding-right: 5px">
                <asp:UpdatePanel ID="teamConsolePanel" runat="server">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <fudge:Tooltip IsLoadingTip="true" runat="server" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:LinqDataSource ID="memberSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                            EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="TeamUsers"
                            Where="TeamId == @TeamId" OnSelecting="memberSource_Selecting">
                            <WhereParameters>
                                <asp:QueryStringParameter Name="TeamId" QueryStringField="id" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>                        
                        <asp:ListView ID="members" runat="server" DataSourceID="memberSource" DataKeyNames="TeamId,UserId"
                            OnItemDeleted="members_ItemDeleted" OnItemCommand="members_ItemCommand">
                            <LayoutTemplate>
                                <table cellpadding="3" style="width: 100%; border-collapse: collapse;margin-bottom:10px;">
                                    <tr>
                                        <th style="text-align: left">
                                            Name
                                        </th>
                                        <th style="text-align: left">
                                            Display Name
                                        </th>
                                        <th style="text-align: left">
                                            Status
                                        </th>
                                    </tr>
                                    <tbody id="itemPlaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style="background-color: <%# GetRowColor((TeamUserStatus)Eval("Status")) %>">
                                    <td style="width: 30%; border-bottom: 1px solid #CCC">
                                        <%# Eval("User.FullName") %>
                                    </td>
                                    <td style="width: 25%; border-bottom: 1px solid #CCC">
                                        <%# Eval("User.DisplayName") %>
                                    </td>
                                    <td style="border-bottom: 1px solid #CCC; width: 70px">
                                        <b>
                                            <%# GetStatus(Container.DataItem as TeamUser) %></b>
                                    </td>
                                    <td style="border-bottom: 1px solid #CCC">
                                        <% if (FudgeUser.IsAdmin(Team.TeamId)) { %>
                                        <asp:LinkButton Visible='<%# (int)Eval("UserId") != FudgeUser.UserId && IsMember((int)Eval("UserId")) 
                                        && (int)Eval("UserId") != Team.UserId %>' ID="RemoveMember" CommandName="Delete"
                                            runat="server">Remove</asp:LinkButton>
                                        <asp:LinkButton ID="PromoteMember" Style="margin-left: 5px" Visible='<%# (int)Eval("UserId") != FudgeUser.UserId
                                        && (int)Eval("UserId") != Team.UserId %>' CommandArgument='<%# Eval("User.UserId") %>'
                                            CommandName='<%# GetPromotionCommand((TeamUserStatus)Eval("Status")) %>' runat="server"><%# GetPromotionCommand((TeamUserStatus)Eval("Status"))%></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton1" Style="margin-left: 5px" Visible='<%# (int)Eval("UserId") != FudgeUser.UserId 
                                        && (int)Eval("UserId") != Team.UserId %>' CommandArgument='<%# Eval("User.UserId") %>'
                                            CommandName='<%# GetBannedCommand((TeamUserStatus)Eval("Status")) %>' runat="server"><%# GetBannedCommand((TeamUserStatus)Eval("Status"))%></asp:LinkButton>
                                        <%} %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="error" style="margin-top: 10px">
                                    No users found.
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <fudge:Pager ID="Pager1" runat="server" PageSize="15" PagerControlID="members" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="memberFilter" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td style="vertical-align: top; padding: 5px">
                <div class="heading">
                    Invite Members
                </div>
                <div class="heading_description">
                    Invite people on Fudge to join your team!
                </div>
                <div style="margin-top: 10px">
                    <asp:UpdatePanel ID="inviteUpdatePanel" runat="server">
                        <ContentTemplate>
                            <asp:LinqDataSource ContextTypeName="Fudge.Framework.Database.FudgeDataContext" ID="userFriendsSource"
                                runat="server" TableName="Users" EnableUpdate="True" OnSelecting="userFriends_Selecting">
                            </asp:LinqDataSource>
                            <div style="overflow: auto; height: 300px; width: 250px; border: 1px solid #CCC">
                                <asp:CheckBoxList DataSourceID="userFriendsSource" DataTextField="DisplayName" ID="friendList"
                                    runat="server" DataValueField="UserId">
                                </asp:CheckBoxList>
                            </div>
                            <br />
                            <asp:Button ID="sendInvite" runat="server" Text="Send Invitation" OnClick="SendInvites">
                            </asp:Button>
                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="inviteUpdatePanel"
                                runat="server">
                                <ProgressTemplate>
                                    <fudge:Tooltip runat="server" Text="Sending invitations.." IsLoadingTip="true" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <fudge:Tooltip runat="server" Visible="false" ID="inviteMessage" IsClosable="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
