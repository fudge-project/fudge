<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsList.ascx.cs" Inherits="Controls_News" %>
<asp:LinqDataSource ID="newsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
    TableName="News" OnSelecting="newsSource_Selecting">
</asp:LinqDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ListView ID="newsList" runat="server" DataSourceID="newsSource">
            <LayoutTemplate>
                <div id="content" style="width: 100%">
                    <div class="heading">
                        News
                    </div>
                    <div class="pager" style="margin-top: 5px">
                        <asp:DataPager ID="Pager" PageSize="5" runat="server" PagedControlID="newsList">
                            <Fields>
                                <asp:NumericPagerField CurrentPageLabelCssClass="active" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                    <br />
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="post">
                    <div class="title">
                        <h2>
                            <a href="<%# Eval("NewsLink") %>">
                                <%# Eval("Title") %></a></h2>
                        <p>
                            <small>Posted on
                                <%# FormatHelper.FormatDateTimeNice((DateTime)Eval("TimeStamp")) %>
                                by <a href="#">Admin</a></small></p>
                    </div>
                    <table>
                        <tr>
                            <td style="vertical-align: top; padding-right: 10px">
                                <%# Eval("PictureId") == null ? String.Empty : @"<img src=""/Images/" + Eval("PictureId") + @" width=""64"" height=""64"" align=""bottom"" />" %>
                            </td>
                            <td style="vertical-align: top;">
                                <p>
                                    <%# Eval("Text") %>
                                </p>
                            </td>
                        </tr>
                    </table>
                    <p class="links">
                        <a href="/News/<%# Eval("UrlName") %>" class="more">Read More</a> &nbsp;&nbsp;&nbsp;
                    </p>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
