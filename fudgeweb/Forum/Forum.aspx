<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Forum.aspx.cs"
    Inherits="Community_Forum_Topic_Default" Title="Fudge Forums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .forums table
        {
            width: 100%;
            border: 1px solid #CCC;
            border-collapse: collapse;
            margin-top: 10px;
        }
        .forums table tr td
        {
            padding: 10px;
            border-bottom: 1px solid #CCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <%=ForumTitle %>
        <div class="description">
            Want updates for something specific? customize your rss feed <a href="#" style="text-decoration: none;
                margin-left: 5px">
                <img src="/site/images/rss.gif" align="middle" />
            </a>
        </div>
    </div>
    <div class="heading_description">
        <%=Category.Description %>
    </div>
    <asp:LinqDataSource ID="forumDataSource" runat="server" 
        ContextTypeName="Fudge.Framework.Database.FudgeDataContext" 
        onselecting="forumDataSource_Selecting" TableName="Forums">        
    </asp:LinqDataSource>
    <asp:ListView ID="Forum" runat="server" DataSourceID="forumDataSource">
        <LayoutTemplate>
            <div class="forums">
                <table>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </table>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Html.Link("/Community/Forum/Topic/" + Eval("ForumId"), (string)Eval("Title")) %>
                    <br />
                    
                        Created
                        <%# FormatHelper.FormatDateTimeNice((DateTime)Eval("Date")) %> <br />
                        by Fudge Admin
                    
                </td>
                <td>
                    <%# Eval("TopicCount")%>
                    <%# (int)Eval("TopicCount") == 1 ? "Topic" : "Topics"%>
                </td>
                <td>
                    <%# Eval("PostCount") %>
                    <%# (int)Eval("PostCount") == 1 ? "Post" : "Posts"%>
                </td>
                <td style="text-align:center">
                    <%# Eval("LastPost") == null ? "-" : String.Format("{1} <br /> Last post by {0}", Html.LinkToProfile((int)Eval("LastPost.UserId")), FormatHelper.FormatDateTimeNice((DateTime)Eval("LastPost.Timestamp")))%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <br />
    <fudge:Pager runat="server" ID="forumPager" PagerControlID="Forum" />
</asp:Content>
