<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Schools_Schools" Title="Schools on Fudge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .school_group
        {
            float: left;
            width: 100%;
        }
        .school
        {
            margin-top: 5px;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:LinqDataSource ID="schoolsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Schools" OnSelecting="schoolsSource_Selecting">
    </asp:LinqDataSource>
    <div class="heading">
        Schools on Fudge
        <div class="description">
            Click a letter to see the schools registered with fudge
        </div>
    </div>
    <div class="heading_description">
        <div class="pager" style="text-align: center">
            <%=AlphabetLinks%>
        </div>
    </div>
    <asp:ListView ID="schools" runat="server">
        <LayoutTemplate>
            <div class="school_group">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </div>
            <div class="heading" style="clear: both; margin-bottom: 5px; margin-top: 10px">
            </div>
            <fudge:Pager ID="allSchools" runat="server" PageSize="10" PagerControlID="schools" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="school">
                <div class="heading" style="margin-bottom:10px">
                    <%# Eval("Letter") %>
                </div>
                <asp:DataList ID="Schools" DataSource='<%# Eval("Schools") %>' runat="server">
                    <ItemTemplate>
                        <%# Html.LinkToSchoolProfile((int)Eval("SchoolId")) %>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <asp:ListView ID="schoolsByLetter" runat="server">
        <LayoutTemplate>
            <div class="school_group">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </div>
            <div class="heading" style="clear: both; margin-bottom: 10px;">
            </div>
            <fudge:Pager ID="schoolsByLetterPager" runat="server" PageSize="10" PagerControlID="schoolsByLetter" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="school">
                <%# Html.LinkToSchoolProfile((int)Eval("SchoolId")) %>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
