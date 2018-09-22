<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Community_Blogs_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .blog
        {
            margin-top: 10px;
        }
        .blog a img
        {
        	margin-left:10px;        
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        Blogs on Fudge!
    </div>
    <div class="heading_description">
        <b>Take a look at some of the Blogs on fudge</b>
    </div>
    <asp:LinqDataSource ID="blogsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Blogs">
    </asp:LinqDataSource>
    <asp:ListView ID="blogs" runat="server" DataSourceID="blogsSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="blog">
                <%# Html.LinkToBlog((int)Eval("BlogId")) %> 
                <a href="/Community/Blogs/<%# Eval("BlogId") %>/Feed"><img src="/site/images/rss.png"/></a>
                <div class="description">
                    <b>
                        <%# Eval("Description") %></b>
                    <br />
                    by
                    <%# Html.LinkToProfile((int)Eval("UserId")) %>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
