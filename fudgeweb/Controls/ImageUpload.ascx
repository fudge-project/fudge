<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageUpload.ascx.cs" Inherits="Controls_ImageUpload" %>
<table>
    <tr>
        <td>
            <asp:FileUpload ID="imageUpload" runat="server" />
        </td>
        <td>
            <asp:CustomValidator ID="imageValidator" runat="server" CssClass="error" ControlToValidate="imageUpload"
                ErrorMessage="Unsupported image type"></asp:CustomValidator>
        </td>
    </tr>
</table>
