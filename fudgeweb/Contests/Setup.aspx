<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Setup.aspx.cs"
    Inherits="Contests_Setup" Title="Fudge - Configure Contest" %>

<%@ Import Namespace="Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table
        {
            margin-top: 10px;
            margin-bottom: 10px;
        }
        table tr td
        {
            padding: 4px;
        }
    </style>

    <script type="text/javascript">
        function showDescription(value) {
            var desc = $get('desc');
            switch(value) {
                case 0:
                    desc.innerHTML = "<%=Resource.FudgeRushDescription %>"
                    break;
                case 1:
                    desc.innerHTML = "<%=Resource.ICPCDescription %>"
                    break;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        Contest Setup
    </div>
    <table>
        <tr>
            <td>
                Name:
                <div class="description">
                    Name of the contest that will be shown
                </div>
            </td>
            <td>
                <asp:TextBox ID="contestName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Type:
                <div class="description">
                    Select a type of contest
                </div>
            </td>
            <td>
                <asp:DropDownList ID="contestType" runat="server">
                    <asp:ListItem Text="Fudge Rush"></asp:ListItem>
                    <asp:ListItem Text="ICPC Style"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div id="desc" class="description">
                    <%=Resource.FudgeRushDescription %>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Minimum Rank:
                <div class="description">
                    Only users who are minimum rank will<br />
                    be allowed to participate in the contest.
                </div>
            </td>
            <td>
                <asp:DropDownList ID="rankings" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Schedule:
                <div class="description">
                    Select a start date and time for the contest
                </div>
            </td>
            <td>
                <ajaxToolkit:CalendarExtender ID="cld" runat="server" Animated="false" TargetControlID="dateStart">
                </ajaxToolkit:CalendarExtender>
                <asp:TextBox ID="dateStart" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="dateEnd" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div class="heading">
        Privacy
    </div>
    <table>
        <tr>
            <td>
                Password:
                <div class="description">
                    Enter a password if you wish for the contest<br />
                    to be secure. Otherwise leave it blank.
                </div>
            </td>
            <td>
                <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Confirm Password:
            </td>
            <td>
                <asp:TextBox ID="confirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="heading">
        Problems
    </div>
    <table runat="server" id="problems">
    </table>
</asp:Content>
