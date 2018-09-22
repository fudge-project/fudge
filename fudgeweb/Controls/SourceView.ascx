<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SourceView.ascx.cs" Inherits="Controls_SourceView" %>
<div class="codeView">
    <pre style="<%= ShowBorder ? "border: 1px solid #CCC;" : "" %> width: auto; overflow:<%=EnableScrolling ? "auto" : "hidden" %>;">
                <fudge:CodeHighlighter runat="server" ID="highlighter">
                </fudge:CodeHighlighter>                                            
            </pre>    
</div>
