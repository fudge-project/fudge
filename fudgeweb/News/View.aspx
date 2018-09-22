<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="News_View" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="post">
        <div class="title">
            <h2>
                <a href='<%=News.NewsLink %>'>
                    <%=News.Title %></a></h2>
            <p>
                <small>Posted on
                    <%=FormatHelper.FormatDateNice(News.Timestamp) %>
                    by <a href="#">Admin</a></small></p>
        </div>
        <table style="margin-top:10px">
            <tr>
                <% if (News.PictureId.HasValue) { %>
                <td style="vertical-align: top; padding-right: 10px">
                    <img src="/Images/<%=News.PictureId %>" width="64" height="64" align="bottom" />
                </td>
                <% } %>
                <td style="vertical-align: top;">
                    <p>
                        <%=News.Text %>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
