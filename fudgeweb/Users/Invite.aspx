<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Invite.aspx.cs"
    Inherits="Users_Invite" Title="Invite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .invite_form
        {
            margin-left: 25%;
        }
        .invite_form table
        {
            border-collapse: collapse;
        }
        .invite_form table td
        {
            padding: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        Invite Your Friends
    </div>
    <div class="heading_description">
        Enter a list of emails and invite some friends to join Fudge!
    </div>
    <div class="invite_form">
        <table>
            <tr>
                <td style="text-align: right">
                    <b>From:</b>
                </td>
                <td>
                    <b>
                        <%=FudgeUser.FullName %>
                        &lt;
                        <%=FudgeUser.DisplayName %>
                        &gt;</b>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: right">
                    <b>To:</b>
                    <div class="description">
                        (use commas to
                        <br />
                        separate emails)
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="emails" TextMode="MultiLine" runat="server" Height="140px" Width="287px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="Invite" runat="server" Text="Invite" OnClick="Invite_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="error" style="margin-top: 10px" visible="false" id="message" runat="server">
    </div>
</asp:Content>
