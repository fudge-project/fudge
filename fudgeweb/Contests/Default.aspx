<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Contests_Default" Title="Fudge - Contests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="control_panel">
        <ul>
            <li><a href="/Contests/Active">Active Contests
                <div class="description">
                    <b>Take a look at the contests in progress. </b>
                </div>
            </a></li>
            <li><a href="/Contests/Schedule">
             <img src="/site/images/calendar.gif" align="middle" />
            Contest Schedule
                <div class="description">
                    <b>Event calendar showing all contest dates. </b>
                </div>
            </a></li>
            <li><a href="/Contests/Past">Past Contests
                <div class="description">
                    <b>View statistics, problems and results for previous contests. </b>
                </div>
            </a></li>
            <li><a href="/Contests/Upcoming">Upcoming Contests
                <div class="description">
                    <b>Sign up for upcoming contests! </b>
                </div>
            </a></li>
        </ul>
    </div><div style="clear:both"></div>
</asp:Content>
