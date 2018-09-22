<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsFeed.ascx.cs" Inherits="Controls_NewsFeed" %>
<asp:LinqDataSource ID="feedSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
    EnableDelete="True" EnableUpdate="True" TableName="NewsFeeds" OrderBy="Timestamp desc"
    OnSelecting="feedSource_Selecting">
</asp:LinqDataSource>
<asp:ListView ID="feed" runat="server" DataSourceID="feedSource" OnItemCommand="feed_ItemCommand">
    <LayoutTemplate>
        <div style="width: 500px; float: left;">
            <div class="heading">
                Fudge Live Feed
            </div>
            <div class="heading_description" style="width: 500px">
                <table style="width: 100%; border-collapse: collapse">
                    <tr>
                        <td>
                            <b>See whats happening on Fudge!</b>
                        </td>
                        <td style="text-align:right">
                            <a href="/NewsFeed/Feed">
                                <img src="/site/images/rss.gif" align="middle" />
                            </a>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div>
            <table style="border-collapse: collapse; width: 100%">
                <tr>
                    <td style="width: 5%; vertical-align: top; padding-top: 5px">
                        <asp:ImageButton ID="feedtype" runat="server" CommandName="Filter" CommandArgument='<%# (int)Eval("Type") %>'
                            ImageUrl='<%# GetImage(Eval("Type")) %>' />
                    </td>
                    <td style="width: 90%; border-bottom: 1px solid #CCC; margin-top: 5px; padding: 10px">
                        <%# ParseDates((string)Eval("Text")) %>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:ListView>
