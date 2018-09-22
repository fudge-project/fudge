<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="View.aspx.cs"
    Inherits="Schools_View" Title="School Profile" ValidateRequest="false" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<%@ Import Namespace="System.Linq" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        table tr td
        {
            padding: 3px;
        }
        .user_list .user
        {
            display: inline;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:LinqDataSource ID="schoolSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        EnableUpdate="True" TableName="Schools" Where="SchoolId == @SchoolId">
        <WhereParameters>
            <asp:QueryStringParameter Name="SchoolId" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:FormView ID="schoolView" Width="100%" runat="server" DataSourceID="schoolSource">
        <ItemTemplate>
            <div class="heading">
                <%# Eval("Name") %>
            </div>
            <div class="heading_description">
            </div>
            <table>
                <tr>
                    <td>
                        <b>Country:</b>
                    </td>
                    <td>
                        <%# Eval("Country.Name") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Fudge Points:</b>
                    </td>
                    <td>
                        <%# Eval("Points") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Rank:</b>
                    </td>
                    <td>
                        <%# Eval("GlobalRank") %>
                    </td>
                </tr>
            </table>
            <div class="heading" style="margin-top: 10px">
                Statistics
            </div>
            <div class="heading_description" style="margin-bottom: 10px">
            </div>
            <graph:ChartControl GridLines="None" runat="server" ID="schoolStandings" Width="450px"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                <YAxisFont StringFormat="Far,Near,Character,LineLimit" />
                <XTitle StringFormat="Center,Near,Character,LineLimit" />
                <XAxisFont StringFormat="Center,Near,Character,LineLimit" />
                <Background Color="WhiteSmoke" LinearGradientMode="Vertical" />
                <ChartTitle StringFormat="Center,Near,Character,LineLimit" Font="Trebuchet MS, 8.25pt" />
                <Charts>
                    <graph:PieChart DataXValueField="Language" DataYValueField="Count">
                        <DataLabels ShowXTitle="True">
                            <Border Color="Transparent" />
                            <Background Color="Transparent" />
                        </DataLabels>
                    </graph:PieChart>
                </Charts>
                <YTitle StringFormat="Center,Near,Character,LineLimit" />
            </graph:ChartControl>
            <div class="heading" style="margin-top: 10px">
                Events
            </div>
            <div class="heading_description">
                <a href="#">
                    <img src="/site/images/calendar_add.png" />
                    Create an Event</a>
            </div>
            <div class="heading" style="margin-top: 10px">
                Stack
            </div>
            <div class="heading_description">
            </div>
            <fudge:Comments MaxPosts="10" CanPost='<%# FudgeUser != null && School.SchoolId == FudgeUser.SchoolId %>'
                ID="Comments1" runat="server" ShowEmpty="false" TopicId='<%# Eval("TopicId") %>' />
            <div class="heading" style="margin-top: 10px">
                Users
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="border-collapse: collapse; width: 100%">
                        <tr>
                            <td>
                                <%=UserDisplay %>
                            </td>
                            <td style="text-align: right">
                                <fudge:Pager runat="server" ID="userPager" PagerControlID="schoolUsers" />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <fudge:Tooltip ID="progress" runat="server" IsLoadingTip="true" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:LinqDataSource ID="usersSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                        OnSelecting="usersSource_Selecting" TableName="Users">
                    </asp:LinqDataSource>
                    <asp:ListView ID="schoolUsers" runat="server" DataSourceID="usersSource" OnItemDataBound="SchoolUsers_ItemDataBound">
                        <LayoutTemplate>
                            <div class="user_list">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="user">
                                <fudge:MiniProfile ShowLinks="false" runat="server" ID="user" UserId='<%# Eval("UserId") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
