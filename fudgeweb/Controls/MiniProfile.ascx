<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniProfile.ascx.cs" Inherits="Controls_MiniProfile" %>
<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:FormView ID="userView" runat="server" OnItemCommand="userView_ItemCommand">
    <ItemTemplate>
        <div class="mini_profile">
            <table class="<%=Class %>">
                <tr>
                    <th class="top_header" colspan="<%=ShowLinks ? "3" : "2"%>">
                        <%# Eval("FullName") %>
                    </th>
                </tr>
                <tr>
                    <td class="picture">
                        <a href="/Users/Profile/<%# Eval("UserId") %>">
                            <img src="/Images/<%# Eval("PictureId") %>" />
                        </a>
                    </td>
                    <td class="infomation_panel">
                        <div class="container">
                            <div class="title">
                                School:
                            </div>
                            <div class="content">
                                <%# Html.LinkToSchoolProfile((int)Eval("SchoolId")) %>
                            </div>
                            <div class="title">
                                Country:
                            </div>
                            <div class="content">
                                <%# Eval("Country.Name")%>
                            </div>
                            <div class="title">
                                Language Preference:
                            </div>
                            <div class="content">
                                <%# Eval("Language.Name")%>
                            </div>
                            <div class="title">
                                Display Name:
                            </div>
                            <div class="content">
                                <%# Eval("DisplayName")%>
                            </div>
                        </div>
                    </td>
                    <% if (ShowLinks) { %>
                    <td>
                        <div class="links">
                            <%# Html.LinkToProfile((int)Eval("UserId"), "View Profile") %>
                            <% if (!CurrentUser.IsLoggedOn) { %>
                            <% if (CurrentUser.IsPendingApproval(User.LoggedInUser.UserId)) { %>
                            <asp:LinkButton ID="approveFriend" runat="server" CommandName="Approve" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                Approve
                            </asp:LinkButton>
                            <asp:LinkButton ID="rejectFriend" runat="server" CommandName="Reject" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                Reject
                            </asp:LinkButton>
                            <%  }
                               else if (User.LoggedInUser.IsPendingApproval(CurrentUser.UserId)) { %>
                            <span class="span">Pending Approval</span>
                            <% }
                               else if (CurrentUser.IsFriend(User.LoggedInUser.UserId)) { %>
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="removeFriend"
                                DisplayModalPopupID="ModalPopupExtender1">
                            </ajaxToolkit:ConfirmButtonExtender>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="removeFriend"
                                PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="PNL" runat="server" CssClass="popup_style">
                                Are you sure you want to delete
                                <%# Eval("DisplayName") %>
                                from your friends?
                                <br />
                                <br />
                                <div style="text-align: right;">
                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
                            <asp:LinkButton ID="removeFriend" runat="server" CommandName="RemoveFriend" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                Remove Friend
                            </asp:LinkButton>
                            <% }
                               else if (!User.LoggedInUser.IsRejected(CurrentUser.UserId)) { %>
                            <asp:LinkButton ID="addFriend" runat="server" CommandName="AddFriend" CommandArgument='<%# Eval("UserId") %>'>                                                                 
                                Add Friend
                            </asp:LinkButton>
                            <% } %>
                            <%# Html.Link("#", "Head to Head") %>
                            <% } %>
                        </div>
                    </td>
                    <% } %>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:FormView>
