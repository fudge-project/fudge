<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Add.aspx.cs"
    Inherits="Teams_Add" Title="Fudge - Create Team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .team table tr td
        {
            padding: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="teamSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableInsert="True" TableName="Teams" OnInserted="teamSource_Inserted">
    </asp:LinqDataSource>
    <div class="heading">
        Add Team
    </div>
    <div class="heading_description">
        Create a new team on Fudge meet people, solve problems, go crazy.
    </div>
    <center>
        <div class="fudge_message" style="margin-top: 10px; margin-bottom: 10px" id="message"
            runat="server" visible="false">
        </div>
    </center>
    <asp:Panel ID="addTeamPanel" runat="server">
        <asp:FormView ID="newTeamForm" DefaultMode="Insert" runat="server" DataKeyNames="TeamId"
            DataSourceID="teamSource" OnItemInserting="newTeamForm_ItemInserting">
            <InsertItemTemplate>
                <div class="team">
                    <table>
                        <tr>
                            <td>
                                <b>Team Name:</b>
                                <div class="description">
                                    Give your team a name.
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="teamName" Width="200" MaxLength="128" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="requireSchoolName" ControlToValidate="teamName"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <tr>
                                <td>
                                    <b>Privacy:</b>
                                    <div class="description">
                                        Set the privacy settings for your team.
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="privacy" runat="server">
                                        <asp:ListItem Text="Open"></asp:ListItem>
                                        <asp:ListItem Text="Invitation"></asp:ListItem>
                                        <asp:ListItem Text="Private"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Scope:</b>
                                    <div class="description">
                                        Select a group of users that can join this team.
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="scope" runat="server">
                                        <asp:ListItem Text="Global"></asp:ListItem>                                        
                                        <asp:ListItem Text="Country"></asp:ListItem>
                                        <asp:ListItem Text="School"></asp:ListItem>
                                        <asp:ListItem Text="Friends"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Avatar:</b>
                                    <div class="description">
                                        <b>Supported files types:</b> jpeg, gif, bmp.
                                        <br />
                                        <b>Pictures will be resized to 96 x 96.</b>
                                    </div>
                                </td>
                                <td>
                                    <fudge:ImageUpload ID="avatar" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Description:</b>
                                    <div class="description">
                                        Whats this group about?
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="description" Text='<%# Bind("Description") %>' Height="100px" Width="300" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="createTeam" CommandName="Insert" runat="server" Text="Create Team" />
                                </td>
                            </tr>
                        </tr>
                    </table>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
    </asp:Panel>
</asp:Content>
