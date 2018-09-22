<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Search.aspx.cs"
    Inherits="Problems_Search" Title="Fudge - Problem Search" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .search_result
        {
            border-top: 1px solid #CCC;
            margin-top: 10px;
            padding-top: 5px;
            margin-left: 15px;
        }
        .search_result .link a
        {
            font-size: 20px;
        }
        .search_result .desc
        {
            margin: 5px 0px 5px 0px;
        }
        .search_result .to a
        {
            color: #CCC;
            text-decoration: none;
        }
        
        .search_result .tags a
        {
            margin-top:5px;
            margin-right:5px;
        }
        
        .search_result .to:hover
        {
            color: Gray;
            text-decoration: none;
        }
        .ratio
        {
            font-weight: bold;
            font-size: 20px;
            width: 60px;
            min-height: 55px;
            padding: 5px;
            text-align: center;
            border: 1px solid #CCC;
            background-image: url(../site/images/ratio.png);
        }
        .search_panel
        {
            margin-left: 50px;
            margin-top: 10px;
        }
        .search_panel table
        {
            border-style: none;
            width: auto;
            margin-left: 150px;
        }
        .search_panel table td
        {
            text-align: left;
            border-style: none;
            padding: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        Problem Search
    </div>
    <div class="search_panel">
        <table>
            <tr>
                <td>
                    <b>Search:</b>
                </td>
                <td>
                    <asp:TextBox CssClass="search_box" ID="search" Width="300" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="buttonSearch" CssClass="search_button" runat="server" Text="Search"
                        OnClick="buttonSearch_Click" />
                </td>
            </tr>            
            <tr>
                <td colspan="3">
                    <div class="description">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:LinqDataSource ID="searchSource" runat="server" OnSelecting="searchSource_Selecting">
    </asp:LinqDataSource>
    <asp:ListView ID="problemView" runat="server" DataSourceID="searchSource">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <table style="width:100%">
                <tr>
                    <td style="vertical-align: top; padding-top: 15px;width:10%">
                        <div class="ratio">
                            <%# (int)Eval("Attempts") != 0 && (int)Eval("Solved") == 0 ? "0 %" : FormatHelper.FormatPercentage((double)Eval("Accuracy")) %>
                            <div class="description" style="text-align: center">
                                Accuracy
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="search_result">
                            <div class="link">
                                <%# Html.LinkToProblem((int)Eval("Problem.ProblemId")) %>
                            </div>
                            <div class="desc">                                
                            </div>
                            <div class="to">
                                <%# Html.LinkToProblem((int)Eval("Problem.ProblemId"), "http://fudge.fit.edu/Problems/Archive/" + Eval("Problem.UrlName")) %>
                            </div>
                            <div class="tags">
                                <%# FormatTags((int)Eval("Problem.ProblemId")) %>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <center>
            </center>
        </ItemTemplate>
    </asp:ListView>
    <fudge:Pager ID="Pager1" QueryStringField="page" PageSize="10" runat="server" PagerControlID="problemView" />
</asp:Content>
