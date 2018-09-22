<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Runs.aspx.cs"
    Inherits="Problems_Runs" %>

<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #runsTable
        {
        	text-align:center;
        }
        
        #runsTable table
        {
            border: 1px solid #CCCCCC;
            border-collapse: collapse;
            width: 100%;
        }
        #runsTable table tr th
        {
            border-bottom: 1px solid #CCCCCC;
            text-align: center;
            background-color: #F5F5F5;
            padding: 3px;
        }
        #runsTable table tr td
        {
            border-top: 1px solid #F5F5F5;
            text-align: center;
            padding: 4px;
        }
        #runsTable .badrun
        {
            color: red;
            font-weight: bold;
            text-decoration: none;
        }
        #runsTable .accepted
        {
            color: green;
            font-weight: bold;
            text-decoration: none;
        }
        #err img
        {
            vertical-align: middle;
        }
        .popup_content
        {
            width: 400px;
            position: absolute;
            overflow: auto;
            height: 160px;
            text-align: justify;
            margin-left: 60px;
            margin-top: 5px;
            padding-right: 10px;
        }
        .runheader table
        {
            width: 100%;
            margin-bottom: 10px;
        }
        .runheader table tr td
        {
            padding: 3px;
            text-align: center;
        }
        .autocomplete_completionListElement
        {
            visibility: hidden;
            margin: 0px !important;
            background-color: #fff;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            text-align: left;
            list-style-type: none;
            padding: 0;
            z-index: 10;
        }
    </style>

    <script type="text/javascript">        
        //timeout between requests
        var timeout = 2000;
        var timerId = 0;     
        //request in progress
        var currentRequest = null;
        var pageState = { 
                          UserId : null, 
                          ProblemId : null, 
                          LanguageId : null, 
                          SortExpression : "", 
                          SortDirection : 0, 
                          CurrentPage : 1,                         
                          GetQueryStringValues : function() {
                            this.UserId = null;
                            this.ProblemId = null;
                            this.LanguageId = null;
                            var idx = document.URL.indexOf('?');            
                            if(idx >= 0) {
                                var values = document.URL.substring(idx+1).split('&');
                                for(var i = 0; i < values.length; ++i) {
                                    var pair = values[i].split('=');
                                    if(pair[0] == "uid") {
                                        this.UserId = parseInt(pair[1]);
                                    }
                                    else if(pair[0] == "pid") {
                                        this.ProblemId = parseInt(pair[1]);
                                    }
                                    else if(pair[0] == "lid") {
                                        this.LanguageId = parseInt(pair[1]);
                                    }
                                }
                            }
                          },
                          SortBy : function(expression) {                            
                            if(expression == this.SortExpression) {
                                this.SortDirection = this.SortDirection == 0 ? 1 : 0;
                            }
                            else {
                                this.SortDirection = 0;
                            }                            
                            this.SortExpression = expression;                
                          },
                          SelectPage : function(page) {
                            this.CurrentPage = page;
                          }
                        };
                        
        
        //called on page load
        function initPolling() {
            showLoading();
            //add mouse handler for popup
            $addHandler(document, 'mousedown', handleClick);
            //get the querystring values
            pageState.GetQueryStringValues();            
            currentRequest = PageMethods._staticInstance.GetRunTable(pageState, onFirstDataReceived, onError);
        }
        
        function handleClick(e) {
            var el = e.target;
            var isValid = false;
            while(el) {
                if(el.id == "popup") {
                    isValid = true;
                    break;
                }
                el = el.parentNode;
            }
            if(!isValid) {                        
                var v = parseInt(e.target.id) + "";
                if(v == "NaN") {                    
                    $get('popup').style.visibility = 'hidden';
                }                
            }
        }
        
        function showLoading() {        
            updateTable('<img src="/site/images/loading.gif" /> Loading submissions...');
        }
        
        function startPolling() {
            //show loading
            showLoading();                                                            
            currentRequest = PageMethods._staticInstance.GetRunTable(pageState, onFirstDataReceived, onError);
        }
        
        function onFirstDataReceived(res) {
            updateTable(res);            
            pollForRuns();
        }               
        
        function pollForRuns() {             
            //pageState.GetQueryStringValues();                     
            //alert(" { pid => " + pageState.ProblemId + " ,uid => " + pageState.UserId + " }");
            currentRequest = PageMethods._staticInstance.GetRunTable(pageState, onDataRecieved, onError);
        }  
        
        function selectPage(page) {
            pageState.SelectPage(page);
            //hide the popup
            hidePopup();                        
            resetPolling();            
        }
        
        function hidePopup() {
            $get('popup').style.visibility = 'hidden';
        }
        
        function stopPolling() {            
            clearTimeout(timerId);
            
            if (currentRequest != null) {                
                var executor = currentRequest.get_executor();
                if (executor.get_started()) {
                    executor.abort();
                }                
                currentRequest = null;
            }
        }
        
        function onDataRecieved(res) {            
            updateTable(res);            
            timerId = setTimeout(pollForRuns, timeout);
        }
        
        function updateTable(html) {
            $get('runsTable').innerHTML = html;
        }               
        
        function onError(res) {            
            //TODO:log errors          
            //updateTable('<div class="error">'+res.get_message()+'</div>');            
            timerId = setTimeout(pollForRuns, timeout);
        }
        
        function viewErrorPopup(id, errorId) {                       
            $get('err').innerHTML = $get(errorId).innerHTML;
            var popup = $get('popup');
            var elem = $get(id);            
            pos = Sys.UI.DomElement.getLocation(elem);
            bounds = Sys.UI.DomElement.getBounds(popup);            
            var left = pos.x + (bounds.width / 5);
            var top = pos.y - (bounds.height / 3);
            
            //HACK!
            if(elem.innerText == "Compilation Error!") {
                left += 20;
            }
            popup.style.left = left + 'px';
            popup.style.top = top + 'px';
            popup.style.visibility = 'visible';
        }
                        
        function sortBy(expression) {
            pageState.SortBy(expression);
            hidePopup();
            resetPolling();                      
        }
        
        function resetPolling() {
            //stop the current request
            stopPolling();
            //restart polling with new values
            startPolling();
        }               
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        <%=PageTitle %>
    </div>
    <div class="heading_description">        
    </div>
    <div class="runheader">
        <table style="width: 100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <b>User:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="email" runat="server" Width="200"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="autoUser" MinimumPrefixLength="1" runat="server" TargetControlID="email"
                                    ServiceMethod="GetCompletionList" ServicePath="~/Services/Users.asmx">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <b>Problem:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="problem" runat="server" Width="200"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender MinimumPrefixLength="2" ID="autoComplete" runat="server"
                                    TargetControlID="problem" ServiceMethod="GetCompletionList" ServicePath="~/Services/Problems.asmx"
                                    CompletionListCssClass="autocomplete_completionListElement">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <b>Language:</b>
                            </td>
                            <td>
                                <fudge:LanguagesDropDown runat="server" AddDefaultCase="true" ID="language" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="runFilter" runat="server" Text="Find" OnClick="runFilter_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="popup" class="popupDialog" style="visibility: hidden">
        <div id="err" class="popup_content">
        </div>
    </div>
    <div id="runsTable">
    </div>
</asp:Content>
