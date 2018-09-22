<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Submit.aspx.cs"
    Inherits="Problems_Submit" Title="Fudge - Submit Problem Solution" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table
        {
            margin-top: 10px;
        }
        table tr td
        {
            padding: 4px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div class="heading">
        Problem Submission
    </div>
    <div class="heading_description">
        Having trouble submitting? A tutorial on problem submission can be found <a href="/Help/Faq.aspx#q11">
            here.</a>
    </div>
    <% if (FudgeUser.Language.LanguageId == 4) { %>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <fudge:Tooltip runat="server" ID="javaTip" Text="For java submissions, the class must be called Main." />            
        </ContentTemplate>
    </asp:UpdatePanel>
    <% } %>
    <% if (!FudgeUser.Language.Visible) {%>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <fudge:Tooltip runat="server" ID="Tooltip1" Text="Your default language is no longer available. Please change your preferences." />            
        </ContentTemplate>
    </asp:UpdatePanel>
    <%  } %>
    <div style="margin-left: 35%">
        <table>
            <tr>
                <td>
                    Languages:
                    <div class="description">
                        Pick your poison!
                    </div>
                </td>
                <td>
                    <fudge:LanguagesDropDown runat="server" ID="language" />
                </td>
            </tr>
            <tr>
                <td>
                    Problem:
                </td>
                <td>
                    <div id="probref" runat="server">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="description" style="text-align: center">
        Submit solution by file
    </div>
    <div style="margin-left: 37%">
        <asp:FileUpload Width="250" ID="submittedFile" runat="server" />
    </div>
    <br />
    <div style="font-size: 20px; text-align: center">
        OR</div>
    <div class="description" style="text-align: center">
        Paste the code for submission here.
    </div>
    <asp:TextBox Style="margin-top: 5px;" TextMode="MultiLine" ID="codeBox" runat="server"
        Height="600px" Width="100%"></asp:TextBox>
    <br />
    <table style="margin-left: auto; margin-right: auto">
        <tr>
            <td style="text-align: center">
                <asp:Button AccessKey="s" ID="submitProblem" runat="server" OnClick="OnProblemSubmitted"
                    Text="Submit Solution" />
            </td>
        </tr>
    </table>
</asp:Content>
