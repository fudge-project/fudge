<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="About.aspx.cs"
    Inherits="About" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        David Fowler
        <div class="description">
            <b>Profile:
                <%=Html.LinkToProfile(15) %></b>
        </div>
    </div>
    <div class="description">
        Fudge Website Developer
    </div>
    <b>Quotes:</b>
    <blockquote>
        <span>Fudge is gonna be the facebook of online judges. </span>
    </blockquote>
    David Fowler is an undergraduate senior in Computer Science at Florida Institute
    of Technology.
    <div class="heading" style="margin-top: 10px">
        Eugenio Panero
        <div class="description">
            <b>Profile:
                <%=Html.LinkToProfile(2) %></b>
        </div>
    </div>
    <div class="description">
        Fudge Framework Developer
    </div>
    <b>Quotes:</b>
    <blockquote>
        <span>We shouldn't go public yet. </span>
    </blockquote>
    Eugenio Panero is an undergraduate senior in Computer Science at Florida Institute
    of Technology.
</asp:Content>
