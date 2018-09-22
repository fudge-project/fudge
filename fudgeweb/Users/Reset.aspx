<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Reset.aspx.cs"
    Inherits="Register_ResetPassword"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .reset_form table tr td
         {
         	padding:4px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div class="fudge_message" runat="server" id="resetText" visible="false">
        </div>
    </center>
    <asp:Panel ID="resetPanel" CssClass="reset_form" runat="server">
        <table>
            <tr>
            </tr>
            <tr>
                <td>
                    <b>New Password:</b>
                    <div class="description">
                        Enter your new password.
                    </div>
                </td>
                <td>
                    <asp:TextBox Width="200" ID="password" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Confirm Password:</b>
                </td>
                <td>
                    <asp:TextBox Width="200" ID="confirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match"
                        ControlToCompare="password" ControlToValidate="confirmPassword">                    
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="changePassword" runat="server" Text="Send" OnClick="OnPasswordChangedClick" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
