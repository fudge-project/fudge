<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="HeadToHead.aspx.cs"
    Inherits="Users_HeadToHead" Title="Head To Head" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table
        {
            width: 100%;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <table border="1">
        <tr>
            <td style="width: 40%; text-align: right">
                <img src="image.aspx?id=327" />
            </td>
            <td style="width: 10%">
                
            </td>            
            <td style="width: 40%; text-align: left">
                <img src="image.aspx?id=327" />
                
            </td>
        </tr>
        <tr>
            <td style="text-align: right;padding:3px">
                1
            </td>
            <td style="text-align:center">
                Ranking
            </td>
            <td style="padding:3px">
                2
            </td>
        </tr>        
    </table>
</asp:Content>
