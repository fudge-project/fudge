<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Schedule.aspx.cs"
    Inherits="Contests_Schedule" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .heading 
        {        	
        	color:Black;
        }
        .calendar .selected_day
        {
            border: 1px solid #CCC;
            background-color: #F5F5F5;
        }
        .calendar .day_header
        {
        }
        .calendar a
        {
            color: Black;
            text-decoration: none;
            display: block;
            width: 100%;
        }
        .calendar .title
        {
        }
        .calendar .other_month
        {
            border: 1px solid #CCC;
        }
        .calendar .other_month .daytext
        {
            background-color: #F5F5F5;
        }
        .calendar .other_month a
        {
            color: #CCC;
        }
        .calendar .next_prev
        {
        }
        .calendar .today
        {
            border: 1px solid #CCC;
            background-color: #FFFFCC;
        }
        .calendar .event
        {
            background-color: green;
        }
        .calendar .day
        {
            border: 1px solid #CCC;
        }
        .calendar
        {
            width: 100%;
            height: 500px;
            border-collapse: collapse;
        }
        .calendar .weekend
        {
            border: 1px solid #CCC;
        }
        .calendar .daytext
        {
            background-color: #F5F5F5;
            padding-right: 2px;
            padding-bottom: 2px;
        }
        .contests
        {
        }
        .contests li
        {
        }
        .box
        {
            float: left;
            margin-left: 10px;
            width: 15px;
            height: 15px;
        }
        .box_desc
        {
            font-weight: bold;
            float: left;
            margin-left: 10px;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading" style="margin-bottom: 10px">
        Key
    </div>
    <div class="box" style="background-color: Green;">
    </div>
    <div class="box_desc">
        Contest on this day
    </div>
    <div class="box" style="background-color: #FFFFCC">
    </div>
    <div class="box_desc">
        Today
    </div>
    <div class="box" style="background-color: #CCCCCC">
    </div>
    <div class="box_desc">
        Selected day
    </div>
    <div style="clear:both"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Calendar ID="contestSchedule" runat="server" WeekendDayStyle-CssClass="weekend"            
                DayStyle-VerticalAlign="Top" DayStyle-HorizontalAlign="Right" BorderColor="#CCCCCC"
                CssClass="calendar" OnDayRender="contestSchedule_DayRender" SelectedDayStyle-CssClass="selected_day"
                TodayDayStyle-CssClass="today" OtherMonthDayStyle-CssClass="other_month" NextPrevStyle-CssClass="next_prev"
                TitleStyle-CssClass="heading" DayHeaderStyle-CssClass="day_header" DayStyle-CssClass="day"
                BorderStyle="Solid" BorderWidth="1" CellPadding="0" BackColor="White" OnSelectionChanged="contestSchedule_SelectionChanged" TitleStyle-BackColor="WhiteSmoke">
            </asp:Calendar>
            <div class="heading" style="margin-top: 10px">
                Contests
            </div>
            <div class="heading_description">
                Contests on
                <%=contestSchedule.SelectedDate.ToString("dddd, MMMM dd")%>
            </div>
            <asp:LinqDataSource ID="contests" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
                OnSelecting="contests_Selecting" TableName="Contests">
            </asp:LinqDataSource>
            <asp:ListView ID="selectedContests" runat="server" DataSourceID="contests">
                <LayoutTemplate>
                    <div class="contests">
                        <ul>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </ul>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <li><a href="/Contests/<%# Eval("UrlName") %>">
                        <%# Eval("Name") %></a> </li>
                </ItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
