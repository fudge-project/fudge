<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Archive.aspx.cs"
    Inherits="Problems_Archive" Title="Fudge - Problem Archive" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" href="/site/style/problemarchive.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="problemsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Problems" OnSelecting="problemsSource_Selecting">
    </asp:LinqDataSource>
    <div class="heading">
        Problem Archive
    </div>
    <div class="heading_description">
        <table style="border-collapse: collapse; width: 100%">
            <td style="width: 15%">
                <b>Problems Per Page :</b>
            </td>
            <td style="width: 5%">
                <asp:DropDownList ID="pageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="pageSize_SelectedIndexChanged">
                    <asp:ListItem Text="25"></asp:ListItem>
                    <asp:ListItem Text="50" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="75"></asp:ListItem>
                    <asp:ListItem Text="100"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="padding-left: 10px">
                <table style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <b>Group By: </b>
                        </td>
                        <td>
                            <asp:DropDownList ID="groupBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="groupBy_SelectedIndexChanged">
                                <asp:ListItem Text="None"></asp:ListItem>
                                <asp:ListItem Text="Source"></asp:ListItem>
                                <asp:ListItem Text="Archived/New"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right">
                <a href="/Problems/Feed" style="text-decoration: none; margin-left: 5px">
                    <img src="/site/images/rss.gif" align="top" />
                </a>
            </td>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <fudge:Tooltip runat="server" Text="Fudge points are awarded for new problems. Problems stay new for 3 weeks. During this time, the test cases and source code are unavailable." />
        </ContentTemplate>
    </asp:UpdatePanel>
    <% if (groupBy.SelectedIndex > 0) { %>
    <asp:LinqDataSource ID="groupingSource" runat="server" OnSelecting="groupingSource_Selecting">
    </asp:LinqDataSource>
    <asp:ListView ID="groupView" runat="server" DataSourceID="groupingSource">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="heading" style="margin-top: 10px">
                <%# Eval("Key") %>
            </div>
            <center>
                <asp:ListView ID="problemSet" runat="server" DataSource='<%# Eval("Problems") %>'>
                    <LayoutTemplate>
                        <div class="problemView">
                            <table>
                                <tr>
                                    <th style="width: 50px">
                                        Solved
                                    </th>
                                    <th style="width: 35%">
                                        Name
                                    </th>
                                    <th style="width: 150px">
                                        Date
                                    </th>
                                    <th style="width: 50px">
                                        Accuracy
                                    </th>
                                    <th style="width: auto">
                                        Stats
                                    </th>
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# FudgeUser == null ? String.Empty : FormatHelper.FormatSolved(FudgeUser.Solved((int)Eval("Problem.ProblemId")))%>
                            </td>
                            <td>
                                <%#FormatHelper.FormatNewProblem((int)Eval("Problem.ProblemId")) %>
                                &nbsp;
                                <%# Html.LinkToProblem((int)Eval("Problem.ProblemId"))%>
                            </td>
                            <td>
                                <%# FormatHelper.FormatDate((DateTime)Eval("Problem.Timestamp"))%>
                            </td>
                            <td>
                                <%# (int)Eval("Attempts") != 0 && (int)Eval("Solved") == 0 ? "0 %" : FormatHelper.FormatPercentage((double)Eval("Accuracy")) %>
                            </td>
                            <td>
                                <fudge:VisualProgress runat="server" ID="progress" MaxValue="300" BackgroundColor="white"
                                    BorderColor="#CCCCCC" ProgressIndicatorColor="#CCFFCC" Numerator='<%# Eval("Solved") %>'
                                    Denominator='<%# Eval("Attempts") %>' Text='<%# Eval("Solved").ToString() + " \\ " + (string)Eval("Attempts").ToString() %>'>
                                </fudge:VisualProgress>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </center>
        </ItemTemplate>
    </asp:ListView>
    <br />
    <fudge:Pager runat="server" ID="Pager1" PageSize="15" PagerControlID="groupView" />
    <% }
       else { %>
    <center>
        <asp:ListView ID="problemView" runat="server" DataSourceID="problemsSource">
            <LayoutTemplate>
                <div class="problemView">
                    <table>
                        <tr>
                            <th style="width: 50px">
                                Solved
                            </th>
                            <th style="width: 35%">
                                <asp:LinkButton ID="sortByName" CommandName="Sort" CommandArgument="Problem.Name" runat="server">Name</asp:LinkButton>
                            </th>
                            <th style="width: 150px">
                                <asp:LinkButton ID="sortByDate" CommandName="Sort" CommandArgument="Problem.Timestamp" runat="server">Date 
                                Added</asp:LinkButton>
                            </th>
                            <th style="width: 50px">
                                <asp:LinkButton ID="sortByAccuracy" CommandName="Sort" CommandArgument="Accuracy"
                                    runat="server">
                                Accuracy</asp:LinkButton>
                            </th>
                            <th style="width: auto">
                                Stats
                            </th>
                        </tr>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# FudgeUser == null ? String.Empty : FormatHelper.FormatSolved(FudgeUser.Solved((int)Eval("Problem.ProblemId")))%>
                    </td>
                    <td>
                        <%#FormatHelper.FormatNewProblem((int)Eval("Problem.ProblemId")) %>
                        &nbsp;
                        <%# Html.LinkToProblem((int)Eval("Problem.ProblemId"))%>
                    </td>
                    <td>
                        <%# FormatHelper.FormatDate((DateTime)Eval("Problem.Timestamp"))%>
                    </td>
                    <td>
                        <%# (int)Eval("Attempts") != 0 && (int)Eval("Solved") == 0 ? "0 %" : FormatHelper.FormatPercentage((double)Eval("Accuracy")) %>
                    </td>
                    <td>
                        <fudge:VisualProgress runat="server" ID="progress" MaxValue="300" BackgroundColor="white"
                            BorderColor="#CCCCCC" ProgressIndicatorColor="#CCFFCC" Numerator='<%# Eval("Solved") %>'
                            Denominator='<%# Eval("Attempts") %>' Text='<%# Eval("Solved").ToString() + " \\ " + (string)Eval("Attempts").ToString() %>'>
                        </fudge:VisualProgress>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <br />
        <fudge:Pager runat="server" ID="problemPager" PageSize="50" PagerControlID="problemView" />
    </center>
    <% } %>
</asp:Content>
