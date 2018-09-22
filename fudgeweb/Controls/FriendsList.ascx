<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FriendsList.ascx.cs" Inherits="Controls_FriendsList" %>
<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:DataList ID="friends" CssClass="friends_list" runat="server" RepeatDirection="Horizontal"
    RepeatLayout="Table" RepeatColumns="2" CellSpacing="4">
    <ItemTemplate>
        <table class="table">
            <tr>
                <th colspan="2">
                    <%# Html.LinkToProfile((int)Eval("UserId"), (string)Eval("FullName")) %>
                    <span style="font-weight: normal">(<%# Eval("DisplayName") %>)</span>
                </th>
            </tr>            
            <tr>
                <td style="width: 100px; height: 100px; vertical-align: middle; text-align: center">
                    <a href="/Users/Profile/<%# Eval("UserId") %>">
                        <img src="/Images/<%# Eval("PictureId") %>" />
                    </a>
                </td>
                <td>
                    <div class="container">
                        <div class="content">
                            <%# Html.LinkToSchoolProfile((int)Eval("SchoolId")) %>
                        </div>
                        <div class="content">
                            <%# Eval("Country.Name")%>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        </div>
    </ItemTemplate>
</asp:DataList>