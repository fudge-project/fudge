<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs"
    Inherits="Users_Notifications" Title="Fudge Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="notificationsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableUpdate="True" EnableDelete="True" TableName="Notifications" Where="UserId == @UserId">
        <WhereParameters>
            <asp:Parameter Name="UserId" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <div class="heading">
        My Notifications
    </div>
    <div class="heading_description">
        <a href="/Users/Profile">
            <img src="/site/images/user_go.png" />
            Back to profile </a>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ListView ID="ListView1" runat="server" DataSourceID="notificationsSource" DataKeyNames="NotificationId">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div style="margin-top: 10px">
                        <%# FormatHelper.GetNotificationImage((int)Eval("Type")) %>
                        <%# Eval("Text") %>
                        <span class="description" style="margin-left: 10px">
                            <%# Html.Link((string)Eval("Link"), "See it!") %>                            
                            <asp:LinkButton ID="deleteNotificaton" CommandName="Delete" Style="padding-left: 5px"
                                runat="server">Hide it!</asp:LinkButton>
                        </span>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="fudge_message" style="margin-top: 10px">
                        You have no notifications
                    </div>
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
