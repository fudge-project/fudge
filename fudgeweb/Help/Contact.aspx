<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs"
    Inherits="Help_Contact" Title="Contact The Fudge Team" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .contact_form table
        {
            width: 100%;
        }
        .contact_form table tr td
        {
            padding: 7px;
            position: relative;
        }
        .contact_form table tr td .error
        {
            top: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="newProblemPanel" runat="server">
                <div class="heading">
                    Contact the Fudge Team
                    <div class="description">
                        <b>Allowed tags:</b> <i>annot abbr acronym blockquote b br em i li ol p pre strike sub
                            sup strong u ul</i>
                        <br />
                        URLs will be auto-linked, and line-breaks will be preserved.
                    </div>
                </div>
                <div class="heading_description">
                    Send us feedback!
                </div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <fudge:Tooltip ID="Tooltip1" runat="server" Text="Sending email..." IsClosable="false" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <fudge:Tooltip runat="server" Visible="false" ID="tip" />
                </div>
                <div class="contact_form">
                    <table>
                        <tr>
                            <td>
                                <b>From:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="email" Width="200" runat="server"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="email"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;
                                <asp:RegularExpressionValidator ControlToValidate="email" ID="RegularExpressionValidator1"
                                    CssClass="error" runat="server" ErrorMessage="Not a valid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Subject:</b>
                                <div class="description">
                                    The title of your post
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="subject" runat="server">
                                    <asp:ListItem Text="Bug"></asp:ListItem>
                                    <asp:ListItem Text="Complaint"></asp:ListItem>
                                    <asp:ListItem Text="Suggestion"></asp:ListItem>
                                    <asp:ListItem Text="Question"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Body:</b>
                                <div class="description">
                                    Enter the body of your message
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="messageBody" TextMode="MultiLine" runat="server" Height="200px"
                                    Width="396px"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="messageBody"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="sendEmail" runat="server" Text="Send Feedback" OnClick="sendEmail_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
