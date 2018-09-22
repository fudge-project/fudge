<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="NeedLogin.aspx.cs"
    Inherits="NeedLogin" Title="Required Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="error">
        You must login to gain access to this resource!
    </div>    
</asp:Content>
