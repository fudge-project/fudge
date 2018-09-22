<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="Community_Blogs_View" Title="(*Fudge).Blogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" href="/site/style/blog.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <a href='/Community/Blogs/<%=Blog.UrlName %>' style="text-decoration: none">
            <%=Blog.Name %></a>
        <div class="description">
            <b>
                <%=Blog.Description %></b>
        </div>
    </div>
    <div class="blog">
        <div class="main">
            <asp:LinqDataSource ID="blogTopicsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                TableName="Topics" OnSelecting="blogTopicsSource_Selecting">
            </asp:LinqDataSource>
            <asp:ListView ID="blogTopics" DataSourceID="blogTopicsSource" runat="server">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="item">
                        <div class="heading">
                            <a href='/Community/Blogs/<%# Blog.UrlName %>/<%# Eval("TopicId") %>'>
                                <%# Eval("Title") %></a>
                        </div>
                        <div class="heading_description">
                            Posted by
                            <%# Html.LinkToProfile((int)Eval("UserId")) %>
                            on
                            <%# FormatHelper.FormatDateTimeNice((DateTime)Eval("Timestamp")) %>
                        </div>
                        <%# Eval("Post") %>
                        <div class="footer">
                            <%# GetComments((int)Eval("TopicId")) %>
                            Comments 
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="side_links">
            <div class="heading">
                Recent Posts
            </div>
            <asp:LinqDataSource ID="recentPostsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                OnSelecting="recentPostsSource_Selecting" TableName="Posts">
            </asp:LinqDataSource>
            <asp:ListView ID="recentPosts" runat="server" DataSourceID="recentPostsSource">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="recent_post">
                        <%# Eval("Link") %>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <div class="heading">
                Tags
            </div>
            <div class="heading">
                Feed
            </div>
            <div class="description">
                Subscribe to the Fudge Blog <a href="/Community/Blogs/<%= Blog.BlogId %>/Feed"><img src="/site/images/rss.png" align="middle"/></a>
            </div>
            <div class="heading">
                Archives
            </div>
            <asp:LinqDataSource ID="blogArchiveSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                OnSelecting="blogArchiveSource_Selecting" TableName="News">
            </asp:LinqDataSource>
            <asp:ListView ID="blogArchive" runat="server" DataSourceID="blogArchiveSource">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="archive_item">
                        <%# Eval("Year") %></div>
                    <asp:ListView ID="archiveLinks" runat="server" DataSource='<%# Eval("Links") %>'>
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="archive_item">
                                <a href='<%# Eval("Link") %>'>
                                    <%# Eval("Month") %></a> (<%# Eval("Count") %>)
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
