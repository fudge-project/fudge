<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Scoreboard.aspx.cs"
    Inherits="Contests_Scoreboard" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .scoreboard td
        {
            border-bottom: 1px solid #CCC;
            text-align: center;
            padding: 4px;
        }
        .scoreboard .active td
        {
            background-color: #FFFFCC;
        }
        .scoreboard th
        {
            border-top: 1px solid #CCC;
            background-color: #F5F5F5;
            text-align: center;
            padding: 4px;
            font-size: 13px;
        }
        .scoreboard table
        {
            margin-top: 10px;
            border-collapse: collapse;
            width: 100%;
        }
        .scoreboard_problems td
        {
            border-style: none;
            text-align: center;
            font-size: 12px;
        }
        .scoreboard_problems table
        {
            width: 100%;
            border-collapse: collapse;
        }
        .scoreboard_problems .badrun
        {
            color: red;
            font-weight: bold;
            text-decoration: none;
        }
        .scoreboard_problems .accepted
        {
            color: green;
            font-weight: bold;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">                
        var currentRequest = null;
        var page = 1;
        var timeout = 3000;
        var timerId;
        
        function initPolling() {
            showLoading();
            var parts = document.URL.split('/');
            currentRequest = PageMethods._staticInstance.GetScoreboard(parts[parts.length-1], page, onFirstDataReceived, onError);
        }
        
        function showLoading() {        
            $get('sb').innerHTML = '<img src="/site/images/loading.gif" /> Loading scoreboard...';
        }
        
        function onFirstDataReceived(res) {        
            $get('sb').innerHTML = res;
            poll();
        }
        
        function poll() {            
            startPolling();
        }
        
        function selectPage(p) {
            page = p;
            resetPolling();    
        }
        
        function startPolling() {            
            var parts = document.URL.split('/');
            currentRequest = PageMethods._staticInstance.GetScoreboard(parts[parts.length-1], page, onDone, onError);
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
        
        function resetPolling() {
            //stop the current request
            stopPolling();
            //restart polling with new values
            startPolling();
        }
        
        function onDone(res) {
            $get('sb').innerHTML = res;
            timerId = setTimeout(poll, timeout)
        }
        
        function onError(res) {
            timerId = setTimeout(poll, timeout)
        }                               
    </script>

    <script type="text/javascript">
//<![CDATA[
var PageMethods = function() {
PageMethods.initializeBase(this);
this._timeout = 0;
this._userContext = null;
this._succeeded = null;
this._failed = null;
}
PageMethods.prototype = {
_get_path:function() {
 var p = this.get_path();
 if (p) return p;
 else return PageMethods._staticInstance.get_path();},
GetScoreboard:function(urlName,page,succeededCallback, failedCallback, userContext) {
/// <param name="urlName" type="String">System.String</param>
/// <param name="page" type="Number">System.Int32</param>
/// <param name="succeededCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="failedCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="userContext" optional="true" mayBeNull="true"></param>
return this._invoke(this._get_path(), 'GetScoreboard',false,{urlName:urlName,page:page},succeededCallback,failedCallback,userContext); }}
PageMethods.registerClass('PageMethods',Sys.Net.WebServiceProxy);
PageMethods._staticInstance = new PageMethods();
PageMethods.set_path = function(value) {
PageMethods._staticInstance.set_path(value); }
PageMethods.get_path = function() { 
/// <value type="String" mayBeNull="true">The service url.</value>
return PageMethods._staticInstance.get_path();}
PageMethods.set_timeout = function(value) {
PageMethods._staticInstance.set_timeout(value); }
PageMethods.get_timeout = function() { 
/// <value type="Number">The service timeout.</value>
return PageMethods._staticInstance.get_timeout(); }
PageMethods.set_defaultUserContext = function(value) { 
PageMethods._staticInstance.set_defaultUserContext(value); }
PageMethods.get_defaultUserContext = function() { 
/// <value mayBeNull="true">The service default user context.</value>
return PageMethods._staticInstance.get_defaultUserContext(); }
PageMethods.set_defaultSucceededCallback = function(value) { 
 PageMethods._staticInstance.set_defaultSucceededCallback(value); }
PageMethods.get_defaultSucceededCallback = function() { 
/// <value type="Function" mayBeNull="true">The service default succeeded callback.</value>
return PageMethods._staticInstance.get_defaultSucceededCallback(); }
PageMethods.set_defaultFailedCallback = function(value) { 
PageMethods._staticInstance.set_defaultFailedCallback(value); }
PageMethods.get_defaultFailedCallback = function() { 
/// <value type="Function" mayBeNull="true">The service default failed callback.</value>
return PageMethods._staticInstance.get_defaultFailedCallback(); }
PageMethods.set_path("/Contests/Scoreboard.aspx");
PageMethods.GetScoreboard= function(urlName,page,onSuccess,onFailed,userContext) {
/// <param name="urlName" type="String">System.String</param>
/// <param name="page" type="Number">System.Int32</param>
/// <param name="succeededCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="failedCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="userContext" optional="true" mayBeNull="true"></param>
PageMethods._staticInstance.GetScoreboard(urlName,page,onSuccess,onFailed,userContext); }
function WebForm_OnSubmit() {
null;
return true;
}
//]]>
    </script>

    <div class="heading" style="border-bottom-style: none">
        Live Scoreboard -
        <%=Contest.Name %>
    </div>
    <center>
        <div class="scoreboard" id="sb" style="margin-top: 10px;">
        </div>
    </center>
</asp:Content>
