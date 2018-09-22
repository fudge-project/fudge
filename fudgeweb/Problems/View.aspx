<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="Problems_View" %>

<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <link rel="Stylesheet" href="/site/style/problemview.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         function BeginRequestHandler(sender, args) {            
            var elem = args.get_postBackElement();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm._scrollPosition = null;
            // First set associatedUpdatePanelId to non existant control
            // this will force the updateprogress not to try and show itself.
            var o = $find('<%= UpdateProgress1.ClientID %>');
            o.set_associatedUpdatePanelId('Non_Existant_Control_Id');
            // Then based on the control ID, show the updateprogress
            if (elem.id == '<%= shareProblem.ClientID %>' || elem.id == '<%=tagProblem.ClientID %>') {
                var updateProgress1 = $get('<%= UpdateProgress1.ClientID %>');
                updateProgress1.style.display = '';
            }        
         }
         
         function EndRequestHandler(sender, args) {
            var updateProgress1 = $get('<%= UpdateProgress1.ClientID %>');
            updateProgress1.style.display = (updateProgress1.style.display == '') ? 'none' : '';
         }    
    </script>

    <asp:LinqDataSource ID="LinqDataSource1" runat="server">
    </asp:LinqDataSource>
    <center>
        <div class="heading" style="color: Black; font-size: 25px">
            <%=CurrentProblem.Name %>
            <div style="text-align: center; font-size: 13px;color:#CCCCCC">
                <b>ID: <%=CurrentProblem.ProblemId + 1000 %></b>
            </div>
        </div>
    </center>
    <fudge:ProblemView runat="server" ID="ProblemXml" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="problem_footer" style="margin-top: 10px">
                <div class="heading_description">
                    <table style="border-collapse: collapse; width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <b>Source:
                                    <%=CurrentProblem.Source.Name %></b>
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <a href="/Community/Forum/Topic/<%=CurrentProblem.ForumId %>">
                                <img src="/site/images/comments.png" />
                                Discuss it!</a>
                            <% if (FudgeUser != null) { %>
                            <a href="/Problems/Submit/<%=CurrentProblem.UrlName %>">
                                <img src="/site/images/page_white_go.png" />
                                Solve it!</a>
                            <% } %>
                            <a href="/Problems/Runs.aspx?pid=<%=CurrentProblem.ProblemId %>">
                                <img src="/site/images/page_white_stack.png" />
                                View Submissions</a>
                            <asp:LinkButton ID="shareProblem" runat="server" OnClick="ShowSharePanel">
                    <img src="/site/images/arrow_turn_right.png" />
                            Share It!</asp:LinkButton>
                            <asp:LinkButton ID="tagProblem" runat="server" OnClick="ShowTagProblemPanel">
                    <img src="/site/images/tag_blue_add.png" />
                            Tag It!</asp:LinkButton>
                        </td>
                        <td style="text-align: right; vertical-align: top; padding-top: 4px;">
                            <fudge:Rating runat="server" ID="problemRating" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1000"
                runat="server">
                <ProgressTemplate>
                    <fudge:Tooltip ID="Tooltip1" runat="server" IsLoadingTip="true" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <fudge:Tooltip runat="server" Text="Problem tagged successfully" ID="tagTip" Visible="false" />
            <asp:Panel ID="tagProblemPanel" runat="server" Visible="false">
                <div class="heading" style="margin-top: 15px">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                Tag It!
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/site/images/cross.png" runat="server"
                                    OnClick="CloseTagging" />
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="margin-top: 15px; border-collapse: collapse; width: 100%" cellpadding="5">
                    <tr>
                        <td style="width: 12%">
                            <b>Select Category:</b>
                        </td>
                        <td>
                            <asp:LinqDataSource ID="tagsKeywordSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                                TableName="Tags">
                            </asp:LinqDataSource>
                            <asp:DropDownList ID="problemCategories" DataTextField="Keyword" DataValueField="TagId"
                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="problemCategories_SelectedIndexChanged"
                                DataSourceID="tagsKeywordSource">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="description">
                                Click on the category to remove it
                            </div>
                            <asp:Panel ID="categories" runat="server" CssClass="tags">
                            </asp:Panel>
                            <fudge:Tooltip runat="server" Text="There was an error tagging the problem" RenderAsError="true"
                                ID="errorTagging" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top: 10px">
                            <asp:LinkButton ID="addTags" runat="server" OnClick="TagProblem">
                            <img src="/site/images/tag_blue_add.png" /> Tag Problem
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="shareProblemPanel" runat="server" Visible="false">
                <div class="heading" style="margin-top: 15px">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                Share It!
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="closeSharing" ImageUrl="~/site/images/cross.png" runat="server"
                                    OnClick="closeSharing_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="margin-top: 10px" cellpadding="4">
                    <tr>
                        <td style="vertical-align: top">
                            <b>Names:</b>
                            <div class="description">
                                Select who you want to share this problem with.
                            </div>
                        </td>
                        <td>
                            <asp:LinqDataSource ContextTypeName="Fudge.Framework.Database.FudgeDataContext" ID="userFriendsSource"
                                runat="server" TableName="Users" EnableUpdate="True" OnSelecting="userFriendsSource_Selecting">
                            </asp:LinqDataSource>
                            <div style="overflow: auto; height: 200px; width: 250px; border: 1px solid #CCC">
                                <asp:CheckBoxList DataSourceID="userFriendsSource" DataTextField="DisplayName" ID="friendList"
                                    runat="server" DataValueField="UserId">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="share" runat="server" Text="Share!" OnClick="ShareIt" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <fudge:Tooltip runat="server" Text="Problem shared successfully!" ID="problemSharedTip"
                Visible="false" />
            <div class="heading" style="margin-top: 15px">
                Recent Posts
            </div>
            <div class="heading_description">
                <b>Recent posts about this problem..</b>
            </div>
            <asp:DataList ID="RecentTopics" CellSpacing="5" runat="server">
                <ItemTemplate>
                    <%# Html.LinkToPosts((int)Eval("TopicId"))%>
                    on
                    <%# FormatHelper.FormatDateNice((DateTime)Eval("Timestamp"))%>
                    by
                    <%# Html.LinkToProfile((int)Eval("UserId"))%>
                </ItemTemplate>
            </asp:DataList>
            <% if (CurrentProblem.ProblemTags.Any()) { %>
            <div class="heading" style="margin-top: 15px; margin-bottom: 5px">
                Tags
            </div>
            <asp:LinqDataSource ID="tagSource" runat="server" OnSelecting="tagSource_Selecting">
            </asp:LinqDataSource>
            <fudge:Cloud ID="tagCloud" runat="server" DataHrefField="Url" DataTextField="Keyword"
                DataWeightField="Count" DataSourceID="tagSource">
            </fudge:Cloud>
            <% } %>
            <div class="heading" style="margin-top: 15px">
                Statistics
            </div>
            <div class="heading_description">
                <b>The Tops</b>
            </div>
            <asp:LinqDataSource ID="theTopsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                OnSelecting="OntheTopsSourceSelecting" TableName="Runs">
            </asp:LinqDataSource>
            <asp:ListView ID="topStats" runat="server" DataSourceID="theTopsSource">
                <LayoutTemplate>
                    <table cellpadding="5">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <b>
                                <%# Eval("Title") %></b>
                        </td>
                        <td style="text-align: right">
                            <%# Html.LinkToProfile((int)Eval("UserId")) %>
                        </td>
                        <td style="text-align: right">
                            <%# Eval("Value") %>
                        </td>
                        <td style="text-align: right">
                            <%# Eval("Language") %>
                        </td>
                        <td style="text-align: right">
                            <%# Html.Link("/Problems/SourceView/" + Eval("RunId"), "view") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="fudge_message" style="margin-top: 10px">
                        No Submissions
                    </div>
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
