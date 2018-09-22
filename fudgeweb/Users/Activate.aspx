<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Activate.aspx.cs"
    Inherits="Register_Activation" Title="Fudge Activation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div class="fudge_message" style="margin-top: 10px; margin-bottom: 10px" runat="server"
            id="activationText">
            <%=Resources.Resource.ActivationSuccess%>
        </div>
    </center>
    <div class="heading">
        Who's on Fudge?
    </div>
    <div class="heading_description">
        <b>Which of your friends are already on Fudge? Click <a href="/Users/Search">here</a>to search.</b>
    </div>
</asp:Content>
