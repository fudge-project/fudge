<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="New.aspx.cs"
    Inherits="Community_Snippets_New" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        New Code Snippet
    </div>
    <asp:LinqDataSource ID="snippetSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="CodeSnippets">
    </asp:LinqDataSource>
    <fudge:Tooltip ID="snippetTip" runat="server" IsClosable="false" Visible="false" />
    <asp:FormView ID="snippetForm" runat="server" Width="100%" DataKeyNames="SnippetId" 
        DataSourceID="snippetSource" DefaultMode="Insert" 
        oniteminserted="snippetForm_ItemInserted" 
        oniteminserting="snippetForm_ItemInserting">
        <InsertItemTemplate>
            <table cellpadding="4" style="border-collapse: collapse">
                <tr>
                    <td>
                        <b>Name: </b>
                        <div class="description">
                            Name your snippet so that people can search for it.<br />
                            e.g. gcd
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="Name" Width="200" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="Name" ID="RequiredFieldValidator1" runat="server" CssClass="error"
                         ErrorMessage="Name required!"></asp:RequiredFieldValidator>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <b>Language: </b>
                        <div class="description">
                            What language is your snippet in?
                        </div>
                    </td>
                    <td>
                        <fudge:LanguagesDropDown ID="LanguagesDropDown1" runat="server" SelectedLanguageId='<%# Bind("LanguageId") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Snippet: </b>
                        <div class="description">
                            Paste the code snippet here!
                        </div>
                    </td>
                    <td>                                            
                        <asp:TextBox ID="snippet" runat="server"  TextMode="MultiLine" Height="214px"
                            Width="416px" Text='<%# Bind("Snippet") %>'></asp:TextBox>                                                                                    
                    </td>                                        
                </tr>
                <tr>
                    <td></td>
                    <td style="padding:10px">
                    <asp:RequiredFieldValidator ControlToValidate="snippet" ID="RequiredFieldValidator2" runat="server" CssClass="error"
                         ErrorMessage="Snippet required!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button CommandName="Insert" ID="Button1" runat="server" Text="Submit My Snippet!" />
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
