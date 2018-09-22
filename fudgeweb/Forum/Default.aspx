<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Forum_Default" Title="Fudge Forums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .categories table
        {
            width: 100%;
            border: 1px solid #CCC;
            border-collapse: collapse;
            margin-top: 10px;
        }
        .categories table tr td
        {
            padding: 10px;
            border-bottom: 1px solid #CCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        Fudge Forum Categories
        <div class="description">
            Want updates for something specific? customize your rss feed <a href="#" style="text-decoration: none;
                margin-left: 5px">
                <img src="/site/images/rss.gif" align="middle" />
            </a>
        </div>
    </div>
    <div class="heading_description">
        Join the community discussion!
    </div>
    <asp:ListView ID="Categories" runat="server">
        <LayoutTemplate>
            <div class="categories">
                <table>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </table>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Html.Link("/Community/Forum/"+ Eval("CategoryId"), (string)Eval("Title")) %>
                    <div class="description">
                        <%# Eval("Description") %>
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    
    <asp:ListView ID="latestPosts" runat="server">
        <LayoutTemplate>
            <div class="heading" style="margin-top:15px">
                Latest Post
            </div>
            <div class="heading_description">
                <b>See who's posting on fudge</b>
            </div>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <div style="margin-top:10px">            
            <%# Html.LinkToPost((int)Eval("PostId")) %> on <%# FormatHelper.FormatDateNice((DateTime)Eval("Timestamp")) %> by <%# Html.LinkToProfile((int)Eval("User.UserId")) %>            
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
