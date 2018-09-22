<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="SourceView.aspx.cs"
    Inherits="Problems_SourceView" Title="Fudge - Problem Solution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading" style="border-style:none">
        <asp:Label ID="codeheader" runat="server"></asp:Label>
    </div>
    <fudge:SourceView ID="codeView" runat="server" EnableScrolling="true" />
    <fudge:Tooltip ID="tip" RenderAsError="true" runat="server" Text="Source code is not available for new problems."
        Visible="false" />
    <asp:Panel runat="server" ID="ratingPanel">
        <fudge:Rating runat="server" ID="problemRating" />
    </asp:Panel>
    <asp:Panel runat="server" ID="commentPanel" Visible="false">
        <div class="heading" style="margin-top: 10px">
            Source Stack
        </div>
        <div class="description">
            See what others think about this solution.
        </div>
        <fudge:Comments runat="server" ID="sourceComments" OnPosted="CommentPosted" ShowEmpty="false" />
    </asp:Panel>
    <asp:Label ID="popularity" CssClass="description" runat="server"></asp:Label>
</asp:Content>
