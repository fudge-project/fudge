<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Environment.aspx.cs"
    Inherits="Contests_Environment" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .scoreboard_style table
        {
            text-align: center;
        }
        .scoreboard_style tr td
        {
            padding: 4px;
        }
        .selected_row td
        {
            background-color: #FFFFCC;
            border-top: 1px solid yellow;
            border-bottom: 1px solid yellow;
        }
        .normal_row td
        {
            border-top: 1px solid white;
            border-bottom: 1px solid white;
        }
    </style>

    <script type="text/javascript">                                
        var time  = null;
        function pad(d) {
            if(d < 10) {
                return "0" + d;
            }
            return d;
        }
        
        function GetCurrentContestTime() {            
            if(time.h <= 0 && time.m <= 0 && time.s <= 0) {
                $get('resultsTip').style.display = '';
                $get('resultsTip').innerHTML = '<%=ContestEnded %>'
            }
            else {
                setTimeout(GetCurrentContestTime, 1000);
            }
            
            
            if(time.h == 0 && time.m < 5) {                            
                $get('contestTime').style.color = '#FFF';
                $get('contestTime').style.backgroundColor = '#FF2400';                
            }
            
            $get('contestTime').innerHTML = pad(time.h) + ":" + pad(time.m) + ":" + pad(time.s);
            if(time.s > 0) {
                time.s--;
            }            
            if(time.s == 0) {                
                if(time.m > 0) {
                    time.s = 59;
                    time.m--;                                                                            
                }                             
            }
            if(time.m == 0){
                if(time.h > 0) {
                    time.m = 59;
                    time.h--;
                }                
            }
        }                          
    </script>

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
            var loadingProblem = $get('loading_problem');
            var compilingSubmission = $get('compiling');
                       
            if (elem.id != '<%= solveProblem.ClientID %>') {                
                compilingSubmission.style.display = 'none';
            }
            else if(elem.id.indexOf("problemChange") < 0) {
                loadingProblem.style.display = 'none';
            }
         }
         
         function EndRequestHandler(sender, args) {
            
         }    
    </script>

    <ajaxToolkit:AlwaysVisibleControlExtender runat="server" TargetControlID="timePanel"
        HorizontalOffset="30" VerticalOffset="40" VerticalSide="Top" HorizontalSide="Right">
    </ajaxToolkit:AlwaysVisibleControlExtender>
    <asp:Panel ID="timePanel" runat="server">
        <div id="contestTime" style="border: 1px solid #CCC; padding: 10px; font-size: 22px;
            background-color: White">
            00:00:00</div>
    </asp:Panel>
    <div class="heading">
        <%=Contest.Name %>
    </div>
    
    <div class="heading">
        Clarifications
    </div>
    
    <div class="heading" style="margin-top: 10px">
        Problem Set
    </div>
    <div class="heading_description">
        <b>Only the last submission for each problem will be judged.</b>
    </div>
    <div id="resultsTip" class="fudge_message" style="margin: 10px 0 10px 0; display: none">
    </div>
    <div class="tab_controlled" style="margin-top: 10px">
        <asp:LinqDataSource ID="contestProblemSource" runat="server" EnableUpdate="true"
            ContextTypeName="Fudge.Framework.Database.FudgeDataContext" TableName="ContestProblems"
            Where="Contest.UrlName == @UrlName">
            <WhereParameters>
                <asp:QueryStringParameter Name="UrlName" QueryStringField="name" Type="String" />
            </WhereParameters>
        </asp:LinqDataSource>
        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="300" AssociatedUpdatePanelID="UpdatePanel1"
            runat="server">
            <ProgressTemplate>
                <div id="loading_problem" class="fudge_message" style="margin-top: 10px; margin-bottom: 10px">
                    <img src="/site/images/ajax-loader.gif" />
                    Loading problem statement...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <table width="100%">
            <tr>
                <td style="width: 75%; vertical-align: top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListView ID="contestProblems" runat="server" DataSourceID="contestProblemSource"
                                OnItemCommand="contestProblems_ItemCommand">
                                <LayoutTemplate>
                                    <div id="header">
                                        <ul id="primary">
                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                        </ul>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li id="Li1" runat="server" visible='<%# (int)Eval("Problem.ProblemId") == SelectedProblemId %>'>
                                        <span>
                                            <%# Eval("Problem.Name") %></span></li>
                                    <li id="Li2" runat="server" visible='<%# (int)Eval("Problem.ProblemId") != SelectedProblemId %>'>
                                        <asp:LinkButton ID="problemChange" runat="server" CommandName="ChangeProblem" CommandArgument='<%# Eval("Problem.ProblemId") %>'><%# Eval("Problem.Name") %></asp:LinkButton></li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="main">
                        <div id="contents">
                            <asp:UpdatePanel ID="problemPanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <fudge:ProblemView ID="problemView" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </td>
                <td style="vertical-align: top; padding-left: 4px">
                    <div class="heading">
                        Scoreboard
                    </div>
                    <div class="heading_description">
                        <b>Updated every 5 minutes</b>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Timer ID="scoreBoardTimer" runat="server" Interval="300000" OnTick="scoreBoardTimer_Tick">
                            </asp:Timer>
                            <asp:LinqDataSource ID="scoreBoardSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                                OnSelecting="scoreBoardSource_Selecting" TableName="ContestUsers">
                            </asp:LinqDataSource>
                            <asp:ListView ID="scoreBoard" runat="server" DataSourceID="scoreBoardSource">
                                <LayoutTemplate>
                                    <table class="scoreboard_style" style="margin-top: 10px; border-bottom: 1px solid #CCC;
                                        margin-bottom: 5px">
                                        <tbody runat="server" id="itemPlaceholder">
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("Rank") %>.
                                        </td>
                                        <td>
                                            <%# Eval("Item.User.DisplayName") %>
                                        </td>
                                        <td>
                                            <%# Eval("Item.Submitted") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <fudge:Pager ID="Pager1" runat="server" PagerControlID="scoreBoard" PageSize="20" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div class="heading">
            Submission
        </div>
        <div class="heading_description" style="margin-bottom: 5px">
            <table style="border-collapse: collapse">
                <tr>
                    <td>
                        <b>Language: </b>
                    </td>
                    <td>
                        <fudge:LanguagesDropDown runat="server" ID="languages" SelectedLanguageId='<%# FudgeUser.LanguageId %>' />
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="sourcePanel" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="source" runat="server" TextMode="MultiLine" Width="100%" Height="300px"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="margin-top: 10px;">
            <asp:LinkButton ID="solveProblem" runat="server" Style="text-decoration: none;" OnClick="solveProblem_Click">
                                <img src="/site/images/page_white_go.png" />
                                    Submit it!
            </asp:LinkButton>
            <div class="heading" style="margin-top: 10px">
                Submission History
            </div>
            <asp:LinqDataSource ID="contestRunsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                TableName="Runs" OnSelecting="contestRunsSource_Selecting">
            </asp:LinqDataSource>
            <asp:UpdatePanel ID="runsPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="200" runat="server">
                        <ProgressTemplate>
                            <div id="compiling" class="fudge_message" style="margin-top: 10px; margin-bottom: 10px">
                                <img src="/site/images/ajax-loader.gif" />
                                Compiling submission...
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <fudge:Tooltip RenderAsError="true" ID="compilerErrorTip" Visible="false" runat="server" />
                    <asp:ListView ID="contestRuns" runat="server" DataSourceID="contestRunsSource" OnItemCommand="contestRuns_ItemCommand">
                        <LayoutTemplate>
                            <table cellpadding="5" style="border-collapse: collapse; width: 100%; text-align: center">
                                <tr>
                                    <th>
                                        Submitted
                                    </th>
                                    <th>
                                        Size
                                    </th>
                                    <th>
                                        Language
                                    </th>
                                </tr>
                                <tbody runat="server" id="itemPlaceholder">
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="border-left: 1px solid white">
                                    <%# FormatSubmissionTime((DateTime)Eval("TimeStamp"))%>
                                </td>
                                <td>
                                    <%# String.Format("{0:0.00} kB", (int)Eval("Size") / 1024.0)%>
                                </td>
                                <td>
                                    <%# Eval("Language.Name") %>
                                </td>
                                <td style="border-right: 1px solid white">
                                    <asp:LinkButton ID="viewSubmission" CommandName="Select" CommandArgument='<%# Eval("RunId") %>'
                                        runat="server">View                                                   
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <tr class="selected_row">
                                <td style="border-left: 1px solid yellow">
                                    <%# FormatSubmissionTime((DateTime)Eval("TimeStamp"))%>
                                </td>
                                <td>
                                    <%# String.Format("{0:0.00} kB", (int)Eval("Size") / 1024.0)%>
                                </td>
                                <td>
                                    <%# Eval("Language.Name") %>
                                </td>
                                <td style="border-right: 1px solid yellow">
                                    <asp:LinkButton ID="viewSubmission" CommandName="Select" CommandArgument='<%# Eval("RunId") %>'
                                        runat="server">View  
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="solveProblem" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="heading" style="margin-top: 10px">
            Submission Source
        </div>
        <div class="heading_description">
            View the source code of a submission here
        </div>
        <asp:UpdatePanel ID="sourceViewPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <fudge:SourceView runat="server" ID="sourceView" />
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
