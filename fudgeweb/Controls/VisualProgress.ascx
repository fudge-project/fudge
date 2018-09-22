<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VisualProgress.ascx.cs"
    Inherits="VisualProgress" %>    
<div style="height: 20px; background-color: <%=BackgroundColor%>; width: <%=MaxValue %>px;
    border: 1px solid <%=BorderColor %>; text-align: left; cursor: default">
    <div style="position: relative; text-align: center; z-index: 20; height: 20px;color: <%=TextColor %>">
        <% if (!RenderTextAsLink) { %>
            <%=Text %>
        <% }
           else { %>
            <a style="text-decoration:none" href="<%=Href %>"><%=Text %></a>
        <% } %>
    </div>
    <div style="position: relative; height: 20px; z-index: 10;
        text-align: center; margin-top: -20px; background-color: <%=ProgressIndicatorColor%>;
        width: <%=ProgressWidth%>px">
    </div>
</div>
