<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Topic.aspx.cs"
    Inherits="Community_Forum_Topic" Title="Forum Topics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .topics table
        {
            width: 100%;
            border: 1px solid #CCC;
            border-collapse: collapse;
            margin-top: 10px;
        }
        .topics table tr td
        {
            padding: 10px;
            border-bottom: 1px solid #CCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <%=ForumTitle %>
    </div>
    <div class="heading_description">
        <% if (FudgeUser != null) { %>
        <a href="/Community/Forum/NewTopic/<%=Request.QueryString["id"] %>">Create Topic</a>        
        <% } %>
    </div>
    <% if (Problem != null && Problem.IsNew) { %>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fudge:Tooltip runat="server" Text="This problem is still new. Please don't give too many hints."
                ID="tip" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <% } %>
    <asp:LinqDataSource ID="topicsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        OnSelecting="topicsSource_Selecting" TableName="Topics">
    </asp:LinqDataSource>
    <asp:ListView ID="Topics" runat="server" DataSourceID="topicsSource">
        <LayoutTemplate>
            <div class="topics">
                <table>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </table>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Html.Link("/Community/Forum/Posts/" + Eval("TopicId"), (string)Eval("Title")) %>
                    <br />
                    Created
                    <%# FormatHelper.FormatDateTimeNice((DateTime)Eval("Date")) %>
                    <br />
                    by
                    <%# Html.LinkToProfile((int)Eval("User.UserId")) %>
                </td>
                <td>
                    <%# Eval("Replies")%>
                    <%# (int)Eval("Replies") == 1 ? "Reply" : "Replies"%>
                </td>
                <td style="text-align: center">
                    <%# Eval("LastPost") == null ? "-" : String.Format("{1} <br /> Last post by {0}", Html.LinkToProfile((int)Eval("LastPost.UserId")), FormatHelper.FormatDateTimeNice((DateTime)Eval("LastPost.Timestamp")))%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <br />
    <fudge:Pager runat="server" ID="topicPager" PagerControlID="Topics" />
</asp:Content>
