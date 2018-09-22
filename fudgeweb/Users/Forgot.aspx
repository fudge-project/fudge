<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Forgot.aspx.cs"
    Inherits="Register_ForgotPassword" Title="Fudge Forgot Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .resetPwdPanel table tr td
        {
            padding: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div class="fudge_message" runat="server" id="message" visible="false">
        </div>
    </center>
    <asp:Panel ID="resetPanel" CssClass="resetPwdPanel" runat="server">
        <table>
            <tr>
                <td>
                    <b>Email Address:</b>
                    <div class="description">
                        A email will be sent to the address you specify<br />
                        with instructions on how to change your password.
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="email" Width="200" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="validateEmail" ControlToValidate="email"
                        ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="sendPassword" runat="server" Text="Send" OnClick="OnSendPasswordClick" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:CustomValidator CssClass="error" ForeColor="Black" ID="emailValidator" runat="server"
        ControlToValidate="email"></asp:CustomValidator>
</asp:Content>
