<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Posts.aspx.cs"
    Inherits="Community_Forum_Posts" Title="Fudge Posts" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .posts
        {
            width: 100%;
        }
        .posts table
        {
            width: 100%;
            border-collapse: collapse;
        }
        .posts table tr th
        {
            border-bottom: 1px solid #CCC;
            background-color: #F5F5F5;
            padding: 3px;
            text-align: left;
        }
        .posts table tr td
        {
            padding: 4px;
        }
        .posts table tr td img
        {
            border: 1px solid #CCC;
        }
        .posts .error
        {
            margin: 10px 0px 10px 0px;
            width: 95%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         function BeginRequestHandler(sender, args) {            
            var elem = args.get_postBackElement();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm._scrollPosition = null;            
            
            var deleting = $get('deleting')
            var inserting = $get('inserting');            
            var loading = $get('loading');
            var updating = $get('updating');
            
            // Then based on the control ID, show the updateprogress
            if (elem.id.indexOf("addPost") >= 0) {
                deleting.style.display = 'none'; 
                loading.style.display = 'none';
                updating.style.display = 'none';
                inserting.style.display = '';                                               
            }            
            else if(elem.id.indexOf("DeletePost") >= 0) {
                inserting.style.display = 'none';                
                loading.style.display = 'none';
                updating.style.display = 'none';
                deleting.style.display = '';
            }
            else if(elem.id.indexOf("EditPost") >= 0 || elem.id.indexOf("Cancel") >= 0) {
                inserting.style.display = 'none';                
                deleting.style.display = 'none';
                loading.style.display = 'none';
                updating.style.display = 'none';
            }            
            else if(elem.id.indexOf('Update') >= 0) {
            inserting.style.display = 'none';                
                deleting.style.display = 'none';
                loading.style.display = 'none';
                updating.style.display = '';                
            }
            else {
                inserting.style.display = 'none';                
                deleting.style.display = 'none';
                updating.style.display = 'none';   
                loading.style.display = '';
            }
         }
         
         function EndRequestHandler(sender, args) {            
         }    
    </script>

    <asp:LinqDataSource EnableInsert="True" ID="postsSource" EnableDelete="True" EnableUpdate="True"
        runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext" TableName="Posts"
        Where="TopicId == @TopicId">
        <WhereParameters>
            <asp:QueryStringParameter Name="TopicId" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <div class="heading_description" style="border-top: 1px solid #CCC; margin-bottom: 10px">
        <%=PostPath %>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:ListView ID="Posts" runat="server" DataKeyNames="PostId" InsertItemPosition="LastItem"
                DataSourceID="postsSource" OnItemDataBound="Posts_ItemDataBound" OnItemInserting="Posts_ItemInserting"
                OnItemUpdating="Posts_ItemUpdating">
                <LayoutTemplate>
                    <div class="posts">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <fudge:Popup runat="server" Width="200" ID="deletePopup" TargetControlID="DeletePost">
                        <popuptemplate>                                    
                                Are you sure you want to delete this post?
                         </popuptemplate>
                    </fudge:Popup>
                    <table style="border: 1px solid #CCC;">
                        <tr>
                            <th colspan="2">
                                <b>
                                    <%# GetPostTitle(Container.DataItem as Fudge.Framework.Database.Post) %></b>
                            </th>
                            <th style="width: 50%; text-align: right; padding-right: 5px" class="description">
                                <asp:LinkButton ID="EditPost" Visible='<%# FudgeUser != null && (int)Eval("UserId") == FudgeUser.UserId %>'
                                    CommandName="Edit" runat="server" Style="text-decoration: none">
                                        <img src="/site/images/page_edit.png" />
                                    Edit
                                </asp:LinkButton>
                                <asp:LinkButton ID="DeletePost" Visible='<%# FudgeUser != null && (int)Eval("UserId") == FudgeUser.UserId && (int)Eval("PostId") != FirstPostId %>'
                                    CommandName="Delete" runat="server" CausesValidation="false" Style="text-decoration: none">
                                        <img src="/site/images/cross.png" />
                                    Delete
                                </asp:LinkButton>
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 21%; vertical-align: top">
                                <a href="/Users/Profile/<%# Eval("UserId") %>">
                                    <img src="/Images/<%# Eval("User.PictureId") %>" />
                                </a>
                                <br />
                                <%# Html.LinkToProfile((int)Eval("UserId")) %>
                                <br />
                                <div class="description">
                                    <a href="#">
                                        <%# GetUserPosts(Container.DataItem as Fudge.Framework.Database.Post) %>
                                    </a>
                                    <br />
                                    Joined on
                                    <%# FormatHelper.FormatDateNice((DateTime)Eval("User.Timestamp")) %>
                                </div>
                            </td>
                            <td colspan="2" style="width: auto; padding-left: 10px; vertical-align: top">
                                <%# FormatHelper.FormatPost((string)Eval("Message")) %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border-top: 1px solid #CCC">
                                <table>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Panel runat="server" ID="ratingPanel" Visible='<%# FudgeUser != null && (int)Eval("UserId") != FudgeUser.UserId %>'>
                                                <fudge:Rating RatingId='<%# Eval("RatingId") %>' runat="server" ID="postRating" />
                                            </asp:Panel>
                                        </td>
                                        <td style="text-align: right; vertical-align: top">
                                            <%# FormatHelper.FormatDateTimeNice((DateTime)Eval("Timestamp")) %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="heading_description" style="border-top: 1px solid #CCC">
                        <b>
                            <%="Re: " + Topic.Title%></b>
                    </div>
                    <table>
                        <tr>
                            <td style="width: 20%; text-align: center">
                                <a href="/Users/Profile/<%# Eval("UserId") %>">
                                    <img src="/Images/<%# Eval("User.PictureId") %>" />
                                </a>
                                <br />
                                <%# Html.LinkToProfile((int)Eval("UserId"))%>
                            </td>
                            <td style="width: auto; padding-left: 10px; vertical-align: top">
                                <asp:TextBox ID="postBody" Height="150px" runat="server" TextMode="MultiLine" Width="95%"
                                    Style="border: 1px solid #CCC" Text='<%# Bind("Message") %>'>
                                </asp:TextBox>
                                <div class="error" runat="server" id="editError" visible="false">
                                    Message required!
                                </div>
                                <div class="description">
                                    <b>Allowed tags:</b> <i>annot abbr acronym blockquote b em i li ol p pre strike sub
                                        sup strong u ul</i>
                                    <br />
                                    URLs will be auto-linked, and line-breaks will be preserved.
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border-top: 1px solid #CCC; text-align: right" class="description">
                                <asp:LinkButton ID="Update" CommandName="Update" runat="server" Style="text-decoration: none">
                                    <img src="/site/images/page_go.png"  style="border-style:none"/>
                                    Update
                                </asp:LinkButton>
                                <asp:LinkButton ID="Cancel" CausesValidation="false" CommandName="Cancel" runat="server"
                                    Style="text-decoration: none">
                                    <img src="/site/images/cross.png" style="border-style:none" />
                                    Cancel
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <ItemSeparatorTemplate>
                    <br />
                </ItemSeparatorTemplate>
                <InsertItemTemplate>
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="50" AssociatedUpdatePanelID="UpdatePanel1"
                        runat="server">
                        <ProgressTemplate>
                            <div id="inserting" class="fudge_message" style="margin-top: 10px;margin-bottom:10px">
                                <img src="/site/images/ajax-loader.gif" />
                                Posting...
                            </div>
                            <div id="deleting" class="fudge_message" style="margin-top: 10px;margin-bottom:10px">
                                <img src="/site/images/ajax-loader.gif" />
                                Removing Post...
                            </div>
                            <div id="loading" class="fudge_message" style="margin-top: 10px;margin-bottom:10px">
                                <img src="/site/images/ajax-loader.gif" />
                                Loading...
                            </div>                            
                            <div id="updating" class="fudge_message" style="margin-top: 10px;margin-bottom:10px">
                                <img src="/site/images/ajax-loader.gif" />
                                Updating...
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <% if (FudgeUser != null) { %>
                    <div class="heading_description" style="border-top: 1px solid #CCC">
                        <b>
                            <%="Re: " + Topic.Title%></b>
                    </div>
                    <table>
                        <tr>
                            <td style="width: 20%; text-align: center">
                                <a href="/Users/Profile/<%=FudgeUser.UserId %>">
                                    <img src="/Images/<%=FudgeUser.PictureId %>" />
                                </a>
                                <br />
                                <%=Html.LinkToProfile(FudgeUser.UserId)%>
                            </td>
                            <td style="width: auto; padding-left: 10px; vertical-align: top">
                                <asp:TextBox ID="postBody" Height="90px" runat="server" Text='<%# Bind("Message") %>'
                                    TextMode="MultiLine" Width="95%" Style="border: 1px solid #CCC">
                                </asp:TextBox>
                                <div class="error" runat="server" id="insertError" visible="false">
                                    Message required!
                                </div>
                                <ajaxToolkit:TextBoxWatermarkExtender TargetControlID="postBody" WatermarkCssClass="watermarkcss"
                                    WatermarkText="Post a reply..." ID="TextBoxWatermarkExtender2" runat="server">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                <div class="description">
                                    <b>Allowed tags:</b> <i>annot abbr acronym blockquote b em i li ol p pre strike sub
                                        sup strong u ul</i>
                                    <br />
                                    URLs will be auto-linked, and line-breaks will be preserved.
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border-top: 1px solid #CCC; text-align: right">
                                <asp:Button ID="addPost" CommandName="Insert" runat="server" Text="Post" />
                            </td>
                        </tr>
                    </table>
                    <% } %>
                </InsertItemTemplate>
            </asp:ListView>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <fudge:Pager ID="postsPager" runat="server" PagerControlID="Posts" PageSize="5" />
</asp:Content>
