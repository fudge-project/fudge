<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Problems_Default" Title="Fudge - Problems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="control_panel">
        <ul>
            <li><a href="/Problems/Archive">
                <img src="/site/images/archive.gif" align="middle" />
                View the problem archive
                <div class="description">
                    <b>Start solving problems from the Fudge Archive!</b>
                </div>
            </a></li>
            <li><a href="/Problems/Search">
                <img src="/site/images/icon_search.gif" align="middle" />
                Advanced problem search
                <div class="description">
                    <b>Search for problems in specific categories or by name.</b>
                </div>
            </a></li>
            <li><a href="/Problems/Submissions">
                <img src="/site/images/stats.gif" align="middle" />
                View Submissions
                <div class="description">
                    <b>See the live view of submissions.</b>
                </div>
            </a></li>
            <li><a href="#">                
                Problem Submissions
                <div class="description">
                    <b>Coming soon...</b>
                </div>
            </a></li>
        </ul>
    </div>
    <div style="clear:both"></div>
</asp:Content>
