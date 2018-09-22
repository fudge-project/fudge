<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="Controls_Rating" %>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div class="rating">
            <ajaxToolkit:Rating ID="rating" runat="server" CurrentRating="2"
                MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" OnChanged="Rating_Changed"
                AutoPostBack="true" />
            <br />
            <div class="description" style="font-weight: bold;margin-left:15px" runat="server" id="descMessage">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
