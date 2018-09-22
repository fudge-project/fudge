<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="Controls_Pager" %>
<div class="pager">
    <asp:DataPager ID="Pager" PageSize="10" runat="server">
        <Fields>
            <asp:NextPreviousPagerField FirstPageText="«" PreviousPageText="‹"
                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                ShowLastPageButton="false" ShowNextPageButton="false" />
            <asp:NumericPagerField ButtonCount="5"  CurrentPageLabelCssClass="active"/>
            <asp:NextPreviousPagerField LastPageText="»" NextPageText="›"
                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                ShowLastPageButton="true" ShowNextPageButton="true" />
        </Fields>
    </asp:DataPager>
</div>
