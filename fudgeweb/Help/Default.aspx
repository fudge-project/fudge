<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Help_Default" Title="Fudge - Help Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="control_panel">
        <ul>
            <li><a href="/Help/Contact">
                <img src="/site/images/contactus.gif" align="middle" />
                Contact Us
                <div class="description">
                    Send us any suggestions, bugs or anything at all.
                </div>
            </a></li>
            <li><a href="/Help/Faq">
                <img src="/site/images/question_icon.png" align="middle" />
                FAQ
                <div class="description">
                    Check the Fudge's Frequently Asked Questions!
                </div>
            </a></li>
            <li><a href="/Help/Resources">
                <img src="/site/images/resources.gif" align="middle" />
                Resources
                <div class="description">
                    Looking for some tools? Check out our resources.
                </div>
            </a></li>
        </ul>
    </div>
</asp:Content>
