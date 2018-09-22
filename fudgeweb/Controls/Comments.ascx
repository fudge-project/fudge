<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comments.ascx.cs" Inherits="Controls_Posts" %>

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
            
            // Then based on the control ID, show the updateprogress
            if (elem.id == '<%= insertPost.FindControl("addPost").ClientID %>') {
                deleting.style.display = 'none'; 
                loading.style.display = 'none';
                inserting.style.display = '';                                               
            }            
            else if(elem.id.indexOf("DeletePost") >= 0) {
                inserting.style.display = 'none';                
                loading.style.display = 'none';
                deleting.style.display = '';
            }
            else {
                inserting.style.display = 'none';                
                deleting.style.display = 'none';
                loading.style.display = '';
            }
         }
         
         function EndRequestHandler(sender, args) {            
         }    
</script>

<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:LinqDataSource ID="postsSource" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
            TableName="Posts" EnableDelete="True" EnableInsert="True" EnableUpdate="True"
            OnSelecting="postsSource_Selecting" OnInserted="postsSource_Inserted">
        </asp:LinqDataSource>
        <% if (CanPost) { %>
        <%if (ShowHeader) { %>
        <div class="heading_description">
            <%=Topic.Posts.Count %>
            <%=Topic.Posts.Count == 1 ? "Comment" : "Comments"%>
        </div>
        <% } %>
        <asp:FormView ID="insertPost" DataSourceID="postsSource" DefaultMode="Insert" runat="server"
            OnItemInserting="Posts_ItemInserting" OnItemInserted="Posts_ItemInserted" Width="100%">
            <InsertItemTemplate>
                <% if (Fudge.Framework.Database.User.LoggedInUser != null) { %>
                <table style="width: 100%">
                    <tr>
                        <td style="width: auto; padding-left: 10px; vertical-align: top">
                            <asp:TextBox ID="postBody" Height="90px" runat="server" Text='<%# Bind("Message") %>'
                                TextMode="MultiLine" Width="95%" Style="border: 1px solid #CCC">
                            </asp:TextBox>
                            <div class="error" style="margin: 10px 0px 0px 0px" runat="server" id="insertError"
                                visible="false">
                                Message body required!
                            </div>
                            <ajaxToolkit:TextBoxWatermarkExtender TargetControlID="postBody" WatermarkCssClass="watermarkcss"
                                WatermarkText="Post a comment.." ID="TextBoxWatermarkExtender2" runat="server">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <% if (CanPostTags) { %>
                            <div class="description">
                                <b>Allowed tags:</b> <i>annot abbr acronym blockquote b em i li ol p pre strike sub
                                    sup strong u ul</i>
                                <br />
                                URLs will be auto-linked, and line-breaks will be preserved.
                            </div>
                            <% }
                               else { %>
                            <br />
                            <br />
                            <% } %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="border-top: 1px solid #CCC; text-align: right; padding-top: 5px">
                            <asp:Button ID="addPost" CommandName="Insert" runat="server" Text="<%# PostButtonText %>" />
                        </td>
                    </tr>
                </table>
                <% } %>
            </InsertItemTemplate>
        </asp:FormView>
        <% } %>
        <table style="width:100%">
            <tr>
                <td style="text-align:left;height:30px">
                    <div class="pager">
                        <asp:DataPager ID="Pager" runat="server" PagedControlID="Posts">
                            <Fields>
                                <asp:NumericPagerField CurrentPageLabelCssClass="active" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </td>
                <% if (CanSubscribe && Topic.Posts.Count > 0) { %>
                <td style="text-align:right">                    
                    <asp:LinkButton ID="subscribe" runat="server" onclick="subscribe_Click">
                    <%=SubscribeImage %>  <%=SubscribeText  %></asp:LinkButton>
                </td>
                <% } %>
            </tr>
        </table>
        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="50" AssociatedUpdatePanelID="UpdatePanel1"
            runat="server">
            <ProgressTemplate>
                <div id="inserting" class="fudge_message" style="margin-top: 10px;">
                    <img src="/site/images/ajax-loader.gif" />
                    Posting...
                </div>
                <div id="deleting" class="fudge_message" style="margin-top: 10px">
                    <img src="/site/images/ajax-loader.gif" />
                    Removing Post...
                </div>
                <div id="loading" class="fudge_message" style="margin-top: 10px">
                    <img src="/site/images/ajax-loader.gif" />
                    Loading...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ListView ID="Posts" runat="server" DataKeyNames="PostId" DataSourceID="postsSource">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </LayoutTemplate>
            <ItemTemplate>
                <table style="border-top: 1px solid #CCC; margin-top: 10px; border-collapse: collapse;
                    width: 100%">
                    <tr>
                        <th colspan="3" style="background-color: #F5F5F5; text-align: left; font-weight: normal;
                            padding: 2px;">
                            <b>
                                <%# Html.LinkToProfile((int)Eval("UserId")) %>
                                wrote on
                                <%# FormatHelper.FormatDateTimeNice((DateTime)Eval("Timestamp"))%></b>
                        </th>
                    </tr>
                    <tr>
                        <td style="width: 15%; vertical-align: top">
                            <a href="/Users/Profile/<%# Eval("UserId") %>">
                                <img width="48" height="48" src="/Images/<%# Eval("User.PictureId") %>" />
                            </a>
                        </td>
                        <td colspan="2" style="width: auto; padding-left: 10px;">
                            <%# FormatHelper.FormatPost((string)Eval("Message")) %>
                            <div class="description" style="margin-top: 20px; border-top: 1px solid #CCC">
                                <asp:LinkButton ID="DeletePost" CommandName="Delete" runat="server" CausesValidation="false"
                                    Style="text-decoration: none" Visible='<%# Fudge.Framework.Database.User.LoggedInUser != null && (CanModifyPost ||
                            (Fudge.Framework.Database.User.LoggedInUser.UserId == (int)Eval("UserId"))) %>'>
                                    <%=DeleteButtonText %>
                                </asp:LinkButton>
                            </div>
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="DeletePost"
                                DisplayModalPopupID="ModalPopupExtender1">
                            </ajaxToolkit:ConfirmButtonExtender>
                            <ajaxToolkit:ModalPopupExtender TargetControlID="DeletePost" ID="ModalPopupExtender1"
                                runat="server" PopupControlID="PopupPanel" BackgroundCssClass="modalBackground"
                                OkControlID="ButtonOk" CancelControlID="ButtonCanel" />
                            <asp:Panel ID="PopupPanel" runat="server" Style="display: none; background-color: White;
                                border-width: 2px; border-color: Black; border-style: solid; padding: 10px;">
                                <table width="200px">
                                    <tr>
                                        <td>
                                            Are you sure you want to delete this post?
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Button ID="ButtonOk" runat="server" Text="Ok" />
                                            <asp:Button ID="ButtonCanel" runat="server" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <EmptyDataTemplate>
                <% if (ShowEmpty) { %>
                <div class="fudge_message" style="margin-top: 10px">
                    No posts.
                </div>
                <% } %>
            </EmptyDataTemplate>
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
