<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Community_Default" Title="Fudge Community" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="control_panel">
        <ul>
            <li><a href="/Community/Snippets/">
                <img src="/site/images/zip.gif" align="middle" />
                Code Snippets
                <div class="description">
                    <b>Share code snippets with the community. </b>
                </div>
            </a></li>
            <li><a href="/Community/Forum/">
                <img src="/site/images/forum_logo.png" align="middle" />
                Forums
                <div class="description">
                    <b>Discuss anything on Fudge. </b>
                </div>
            </a></li>
            <li><a href="/Schools">
                <img src="/site/images/book.png" align="middle" />
                Schools
                <div class="description">
                    <b>Take a look at the Schools on Fudge</b>
                </div>
            </a></li>
            <li><a href="/Community/Blogs/">Blogs
                <div class="description">
                    <b>Take a look at the Fudge Blogs </b>
                </div>
            </a></li>
        </ul>
    </div>
    <div style="clear: both">
    </div>
</asp:Content>
