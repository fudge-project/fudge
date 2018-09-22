<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Friends.aspx.cs"
    Inherits="Users_Friends" Title="Friends" %>

<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .friends_list 
        {
        	width:100%;
        }
        
        .friends_list .friend
        {
            display: inline;
            float: left;
            width: 45%;            
        }
        .heading
        {
            margin-top: 10px;
        }
        .heading_menu
        {
            border-collapse: collapse;
            width: 100%;
        }
        .heading_menu .pager_menu
        {
            text-align: right;
        }
        .heading_menu .friend_count_pager
        {
            text-align: right;
            height: 25px;
        }
        .clear_bottom
        {
            clear: both;
            padding-top: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="friendsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        OnSelecting="friendsSource_Selecting" TableName="Users">
    </asp:LinqDataSource>
    <div class="heading">
        <%=CurrentUser.IsLoggedOn ? "Your" : CurrentUser.FirstName + "'s" %>
        friends
        <% if (CurrentUser.IsLoggedOn) { %>
        <div class="description">
            Can't find your friends on fudge? Invite them <a href="/Users/Invite">here.</a>
        </div>
        <% } %>
    </div>
    <% if (Pending) { %>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <fudge:Tooltip runat="server" IsLoadingTip="true" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="heading_description">
                <table class="heading_menu">
                    <tr>
                        <td>
                            <%=Heading %>
                        </td>
                        <td class="pager_menu">
                            <fudge:Pager ID="pagerPendingFriends" runat="server" PagerControlID="pendingFriends" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="fudge_message" runat="server" id="message" visible="false">
            </div>
            <asp:ListView ID="pendingFriends" runat="server" DataSourceID="friendsSource" OnItemDataBound="Friends_ItemDataBound">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </LayoutTemplate>
                <ItemTemplate>
                    <fudge:MiniProfile OnFriendAddedRemoved="OnFriendStatusChanged" ShowLinks="true"
                        runat="server" ID="friend" UserId='<%# Eval("UserId") %>' />
                </ItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <% }
       else { %>
    <table class="heading_menu">
        <tr>
            <td>
                <%=Friends.Count() == 1 ? "1 friend" : Friends.Count() + " friends"%>
            </td>
            <td class="friend_count_pager">
                <fudge:Pager ID="pagerFriends" runat="server" QueryStringField="page" PagerControlID="friends" />
            </td>
        </tr>
    </table>
    <asp:ListView ID="friends" runat="server" DataSourceID="friendsSource" OnItemDataBound="Friends_ItemDataBound">
        <LayoutTemplate>
            <div class="friends_list">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="friend">
                <fudge:MiniProfile ShowLinks="false" runat="server" ID="friend" UserId='<%# Eval("UserId") %>' />
            </div>
        </ItemTemplate>
    </asp:ListView>
    <% } %>
    <div class="clear_bottom">
    </div>
</asp:Content>
