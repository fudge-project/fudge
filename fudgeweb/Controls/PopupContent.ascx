<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupContent.ascx.cs"
    Inherits="Controls_PopupContent" %>
<ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="ModalPopupExtender1">
</ajaxToolkit:ConfirmButtonExtender>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="PopupPanel"
    BackgroundCssClass="modalBackground" OkControlID="ButtonOk" CancelControlID="ButtonCanel" />
<asp:Panel ID="PopupPanel" runat="server" Style="display: none; background-color: White;
    border-width: 2px; border-color: Black; border-style: solid; padding: 10px;">
    <table width="100%">
        <tr>
            <% if (ShowCloseButton) { %>
            <td class="description" style="text-align: right">
                <asp:LinkButton ID="CloseButton" runat="server">
                <img src="/site/images/cross.png" />
                Close</asp:LinkButton>
            </td>
            <% } %>
        </tr>
        <tr>
            <td style="text-align:left">
                <asp:PlaceHolder runat="server" ID="popupContent" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Button ID="ButtonOk" runat="server" Text="Ok" />
                <% if (!ShowCloseButton) { %>
                    <asp:Button ID="ButtonCanel" runat="server" Text="Cancel" />
                <% } %>
            </td>
        </tr>
    </table>
</asp:Panel>
