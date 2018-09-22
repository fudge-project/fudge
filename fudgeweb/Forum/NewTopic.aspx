<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="NewTopic.aspx.cs"
    Inherits="Community_Forum_Topics_Create" Title="Create New Thread" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .newpost table
        {
            width: 100%;
        }
        .newpost table tr td
        {
            padding: 4px;
            position: relative;
        }
        .error
        {
            top: 9px;
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="postsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableInsert="True" TableName="Posts" Where="TopicId == @TopicId" OnInserted="postsSource_Inserted">
        <WhereParameters>
            <asp:QueryStringParameter Name="TopicId" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:Panel ID="newProblemPanel" runat="server">
        <div class="heading">
            New Topic
            <div class="description">
                <b>Allowed tags:</b> <i>annot abbr acronym blockquote b em i li ol p pre strike sub
                    sup strong u ul</i>
                <br />
                URLs will be auto-linked, and line-breaks will be preserved.
            </div>
        </div>
        <div class="heading_description">
            Start a new discussion!
        </div>
        <div class="newpost">
            <asp:FormView ID="newPost" runat="server" DataKeyNames="PostId" DataSourceID="postsSource"
                DefaultMode="Insert" OnItemInserting="newPost_ItemInserting">
                <InsertItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <b>Title:</b>
                                <div class="description">
                                    The title of your post
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="title" MaxLength="64" Width="250" Text='<%# Bind("Title") %>' runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="titleValidator" ControlToValidate="title" runat="server"
                                    CssClass="error" ErrorMessage="Title required"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="messageBody" Text='<%# Bind("Message") %>' TextMode="MultiLine" runat="server" Height="200px"
                                    Width="396px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="messageValidator" ControlToValidate="messageBody" runat="server"
                                    CssClass="error" ErrorMessage="Message body required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="insertPost" CommandName="Insert" runat="server" Text="Create" />
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </asp:Panel>
</asp:Content>
