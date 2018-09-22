<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tooltip.ascx.cs" Inherits="Controls_Tooltip" %>
<div runat="server" id="message" style="margin-top: 10px">
    <% if (!RenderAsError && IsClosable) { %>
    <table style="width: 100%; border-collapse: collapse; padding: 0px; margin: 0px;">
        <tr>
            <td style="padding: 0px">
                <%=Text%>
            </td>
            <td style="text-align: right; padding: 0px">
                <asp:ImageButton ID="closeMessage" runat="server" CausesValidation="false" OnClick="closeMessage_Click"
                    ImageUrl="~/site/images/cross.png" />
            </td>
        </tr>
    </table>
    <% }
       else { %>
    <% if (IsLoadingTip) { %>
    <img src="/site/images/ajax-loader.gif" />
    <% } %>
    <%=Text%>
    <% } %>
</div>
