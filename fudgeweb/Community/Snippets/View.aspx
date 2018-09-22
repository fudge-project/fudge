<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="Community_Snippets_View" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <%=Snippet.Name %>
    </div>
    <div class="heading_description">
        <b>Language : <%=Snippet.Language.SourceId %> </b>
    </div>
    <fudge:SourceView runat="server" ID="source" />
    <fudge:Rating ID="snippetRating"  runat="server" />
    <% if (FudgeUser != null) { %>
    <div class="heading">
        Comments
    </div>
    <div class="heading_description">
        <b>Was this snippet useful?</b>
    </div>
    <% } %>
    <fudge:Comments runat="server" ShowEmpty="false" ID="snippetComments" />    
</asp:Content>
