<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Post.aspx.cs"
    Inherits="Community_Blogs_Post" Title="Untitled Page" %>

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
            <div class="item">
                <div class="heading" style="font-size: 20px">
                    <%=Topic.Title %>
                </div>
                <%=Topic.Posts[0].Message %>
                <div class="footer">
                    Posted by
                    <%= Html.LinkToProfile(Blog.UserId) %>
                    on
                    <%=FormatHelper.FormatDateTimeNice(Topic.Timestamp) %>
                    |
                    <%=(Topic.Posts.Count-1) %>
                    Comments |
                    <div style="float: right; margin-top: -17px;">

                        <script src="http://digg.com/tools/diggthis.js" type="text/javascript"></script>

                    </div>
                </div>
                <div class="heading" style="margin-top: 20px">
                    Comments
                </div>
                <div class="heading_description">
                    <img src="/site/images/comments.png" align="middle" />
                    <b>Check out the comments for this on this post!</b>
                </div>
                <fudge:Comments ID="comments" OnPosted="CommentPosted" StartFrom="1" ShowEmpty="false" runat="server" CanModifyPost="false"
                    CanPost="true" />
            </div>
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
            <asp:LinqDataSource ID="tagSource" runat="server" OnSelecting="tagSource_Selecting">
            </asp:LinqDataSource>
            <fudge:Cloud ID="tagCloud" runat="server" DataHrefField="Url" DataTextField="Keyword"
                DataWeightField="Count" DataSourceID="tagSource">
            </fudge:Cloud>
            <div class="heading">
                Feed
            </div>
            <div class="description">
                Subscribe to the Fudge Blog <a href="/Community/Blogs/<%= Blog.BlogId %>/Feed">
                    <img src="/site/images/rss.png" align="middle" /></a>
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
