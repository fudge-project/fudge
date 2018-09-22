<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="TestRuns.aspx.cs"
    Inherits="Problems_TestRuns" Title="Fudge - View Test Cases" %>

<%@ Import Namespace="Fudge.Framework.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .test_runs
        {
            font-family: Consolas;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="testRunPanel" runat="server">
        <div id="testRuns" style="text-align: center">
            <asp:GridView ID="testRuns" BorderColor="WhiteSmoke" CellPadding="4" runat="server"
                AutoGenerateColumns="False" BorderStyle="Solid" CssClass="test_runs" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <pre><%# Eval("Input") %></pre>
                        </ItemTemplate>
                        <HeaderTemplate>
                            Test Case
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <pre><%# Eval("ExpectedOutput")%></pre>
                        </ItemTemplate>
                        <HeaderTemplate>
                            Expected Output
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <pre><%# Eval("Output") %></pre>
                        </ItemTemplate>
                        <HeaderTemplate>
                            Output
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <b>
                                <%# GetSolved((int)Eval("TestCaseId")) %></b>
                        </ItemTemplate>
                        <HeaderTemplate>
                            Judge Decision
                        </HeaderTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    <fudge:Tooltip RenderAsError="true" runat="server" Text="Test runs are not available for new problems."
        ID="tip" Visible="false" />
</asp:Content>
