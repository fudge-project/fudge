<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollapsibleMenu.ascx.cs"
    Inherits="Controls_CollapsibleMenu" %>
<div class="collapsible_menu">
    <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="header"
        HeaderSelectedCssClass="header" FadeTransitions="false" FramesPerSecond="40"
        TransitionDuration="3" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="false">
        <Panes>
            <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    <!--Header template-->
                </Header>
                <Content>
                    <!--Content template-->
                </Content>
            </ajaxToolkit:AccordionPane>
        </Panes>
    </ajaxToolkit:Accordion>
</div>
