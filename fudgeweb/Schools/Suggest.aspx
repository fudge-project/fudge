<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Suggest.aspx.cs"
    Inherits="Register_AddSchool" Title="Fudge Request Schoool" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="head">
    <style type="text/css">
        .school_suggest table tr td
        {
            padding: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="suggestSchoolSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableInsert="True" TableName="SuggestedSchools" OnInserted="suggestSchoolSourceInserted">
    </asp:LinqDataSource>
    <div class="heading">
        School Request
    </div>
    <div class="heading_description">
        Didn&#39;t find your school during registration? Tell us about your school and we&#39;ll 
        have it in no time.
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <center>
                <div class="fudge_message" style="margin-top: 10px; margin-bottom: 10px" id="message"
                    runat="server" visible="false">
                </div>
            </center>
            <asp:Panel ID="addSchoolPanel" runat="server">
                <asp:FormView ID="schoolRequestForm" DefaultMode="Insert" runat="server" DataKeyNames="SuggestedSchoolId"
                    DataSourceID="suggestSchoolSource">
                    <InsertItemTemplate>
                        <div class="school_suggest">
                            <table>
                                <tr>
                                    <td>
                                        <b>Name of School:</b>
                                        <div class="description">
                                            The name of the school you are requesting.
                                            <br />
                                            Do not translate the name of the school into english.
                                        </div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="schoolName" Width="200" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="requireSchoolName" ControlToValidate="schoolName"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <tr>
                                        <td>
                                            <b>School Website:</b>
                                            <div class="description">
                                                School website helps us determine if the school is legit.<br />
                                                ( e.g. http://fit.edu )
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="schoolWebsite" Width="200" runat="server" Text='<%# Bind("Domain") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="schoolWebsite"
                                                runat="server" ErrorMessage="*">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Your Email:</b>
                                            <div class="description">
                                                Specify an email address
                                                so we can tell you when<br /> the school has been added.
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="contactEmail" Width="200" runat="server" Text='<%# Bind("NotifyEmail") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="contactEmail"
                                                runat="server" ErrorMessage="*">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator CssClass="error" ControlToValidate="contactEmail"
                                                ID="emailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="email you specified is not valid"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Miscellaneous:</b>
                                            <div class="description">
                                                Any addition information you wish to specify.
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="messageBody" Height="100px" Width="300" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="checkSchool" runat="server" 
                                                Text="Submit Request" onclick="checkSchool_Click" />
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </div>
                        <div id="possibleSchools" class="fudge_message" runat="server" visible="false">
                            Does your school already exist on Fudge? Here are the list of matches we found.
                            <asp:DataList ID="schools" runat="server">
                                <ItemTemplate>
                                    <%# Eval("Name") %>
                                    =&gt; <a href="http://<%# Eval("Domain") %>">
                                        <%# Eval("Domain") %></a>
                                </ItemTemplate>
                            </asp:DataList>
                            <br />
                            <asp:Button ID="insertAnyways" CommandName="Insert" runat="server" Text="Submit Anways" />
                        </div>
                        <div id="schoolRequested" class="error" runat="server" visible="false">
                            Your school has been requested. Please please be patient while we add your school
                            <asp:DataList ID="DataList1" runat="server">
                                <ItemTemplate>
                                    <%# Eval("Name") %>
                                    =&gt; <a href="http://<%# Eval("Domain") %>">
                                        <%# Eval("Domain") %></a>
                                </ItemTemplate>
                            </asp:DataList>                            
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
