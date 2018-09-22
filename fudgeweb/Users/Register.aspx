<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Register.aspx.cs"
    Inherits="Register_Default" Title="Fudge Registration" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="head">
    <style type="text/css">
        .langselect
        {
            width: 220px;
        }
        table tr td
        {
            position: relative;
        }
        .error
        {
            top: 5px;
        }
    </style>

    <script type="text/javascript">
        function locateSchool() {            
            var email = $get('<%=userFormView.FindControl("email").ClientID %>').value;            
            //make ajax call for to locate the school
            PageMethods.FindSchool(email, onSchoolLocated);
        }
        
        //callback function when school is located
        function onSchoolLocated(res) {
            var school = $get('school');
            var countries = $get('<%=userFormView.FindControl("countries").ClientID%>');
            var button = $get('<%=userFormView.FindControl("registerUser").ClientID%>');
            var hiddenField = $get('<%=userFormView.FindControl("locatedSchool").ClientID%>');
            if(res) {
                school.innerHTML = res.Name;
                hiddenField.value = res.SchoolId;
                //locate the correct school in the dropdown list
                for(var  i = 0; i < countries.options.length; ++i)  {
                    if(countries.options[i].value == res.CountryId) {
                        countries.selectedIndex = i;
                        break;
                    }
                }
                   
                //enable the button
                button.disabled = false;   
            }
            else {       
                hiddenField.value = "";  
                school.innerHTML = '<div class=\"description\"> <%=Resources.Resource.SchoolNotLocated %> </div>';
                //disable the button if the school cannot be found as the user cannot submit
                button.disabled = true;
                //select the default country
                countries.selectedIndex = 0;
            }
        }
                    
        function checkifEnough() {                   
            var email = $get('<%=userFormView.FindControl("email").ClientID %>').value;    
            var button = $get('<%=userFormView.FindControl("registerUser").ClientID%>');
            //regex to test if an email is valid
            var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if(filter.test(email)) {
                //check the db to see if the school exists    
                locateSchool();
            } 
            else {
                //email is not valid disable the button
                button.disabled = true;
                //set the default country selection
                $get('<%=userFormView.FindControl("countries").ClientID%>').selectedIndex = 0;
                
                //show the user a message
                $get('school').innerHTML = "<div class=\"description\"> <%=Resources.Resource.SchoolWillBeLocated %> </div>";                
                $get('<%=userFormView.FindControl("locatedSchool").ClientID%>').value = '';
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
FindSchool:function(email,succeededCallback, failedCallback, userContext) {
/// <param name="email" type="String">System.String</param>
/// <param name="succeededCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="failedCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="userContext" optional="true" mayBeNull="true"></param>
return this._invoke(this._get_path(), 'FindSchool',false,{email:email},succeededCallback,failedCallback,userContext); }}
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
PageMethods.set_path("/Users/Register.aspx");
PageMethods.FindSchool= function(email,onSuccess,onFailed,userContext) {
/// <param name="email" type="String">System.String</param>
/// <param name="succeededCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="failedCallback" type="Function" optional="true" mayBeNull="true"></param>
/// <param name="userContext" optional="true" mayBeNull="true"></param>
PageMethods._staticInstance.FindSchool(email,onSuccess,onFailed,userContext); }
function WebForm_OnSubmit() {
null;if (typeof(ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) return false;
return true;
}
//]]>
    </script>

    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="Fudge.Framework.Database.FudgeDataContext"
        TableName="Users" EnableInsert="True" OnInserted="OnLinqDataSource1Inserted">
    </asp:LinqDataSource>
    <center>
        <div id="regSuccess" class="fudge_message" runat="server" visible="false">
            <%=Resources.Resource.RegistrationSuccess%>
            <br />
            <b style="color: Red">PLEASE NOTE Your Fudge account will expire after 3 days if not
                validated as described in the email.</b>
        </div>
        <div id="regFailed" class="fudge_message" runat="server" visible="false">
            <%=Resources.Resource.RegistrationFailed%>
        </div>
        <div id="notActivated" class="fudge_message" style="margin-bottom: 10px;" runat="server"
            visible="false">
            Did you register already? An activation email is probably somewhere in you inbox.
            <br />
            Dig through your spam folder and if you still can't find it, send us an <a href="/Help/Contact">
                email.</a>
        </div>
    </center>
    <asp:FormView ID="userFormView" runat="server" Width="100%" DefaultMode="Insert"
        DataSourceID="LinqDataSource1" OnItemInserting="OnUserFormViewItemInserting"
        DataKeyNames="UserId">
        <InsertItemTemplate>
            <asp:HiddenField runat="server" ID="locatedSchool" />
            <table cellpadding="4">
                <tr>
                    <td>
                        <b>First Name:</b>
                    </td>
                    <td>
                        <asp:TextBox runat="server" MaxLength="64" ID="firstName" Text='<%# Bind("FirstName") %>'
                            Width="200" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="firstName"
                            runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Last Name:</b>
                    </td>
                    <td>
                        <asp:TextBox runat="server" MaxLength="64" ID="lastName" Text='<%# Bind("LastName") %>'
                            Width="200" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastName"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>(*Reference):</strong>
                        <div class="description">
                            What do you want to go by?
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="shortName" Width="200" Text='<%# Bind("ShortName")%>' runat="server"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterMode="InvalidChars"
                            FilterType="Custom" InvalidChars="(_|\=+)*&^%$#@!~,<>;:'[]{}/" runat="server"
                            TargetControlID="shortName">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ControlToValidate="shortName" ID="RequiredFieldValidator3"
                            runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="shortNameValidator" runat="server" ControlToValidate="shortName"
                            CssClass="error" ForeColor="Black"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>School Email:</b>
                        <div class="description">
                            Your email will be used to locate your school. <br />
                            You can set up a secondary email address in your profile<br />
                            settings after you register.
                        </div>
                    </td>
                    <td>
                        <asp:TextBox runat="server" MaxLength="320" ID="email" Text='<%# Bind("Email") %>'
                            Width="200" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email"
                            ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="emailValidator" runat="server" ControlToValidate="email"
                            CssClass="error" ForeColor="Black"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>School:</b>
                    </td>
                    <td>
                        <div id="school" style="padding: 5px; height: 15px">
                            <div class="description">
                                <%=Resources.Resource.SchoolWillBeLocated %>
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:CustomValidator CssClass="description" ID="schoolValidator" ErrorMessage="<%=Resource.SchoolNotApproved %>"
                            runat="server"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Country:</b>
                        <div class="description">
                            Select the country you wish to represent.
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="countries" DataTextField="Name" DataValueField="CountryId"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Timezone:</b>
                        <div class="description">
                            All dates on the website will be in your specified timezone.
                        </div>
                    </td>
                    <td>
                        <fudge:TimezonesDropDown runat="server" ID="timezone" SelectedTimezone='<%# Bind("Timezone") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Avatar:</b>
                        <div class="description">
                            <b>Supported files types:</b> jpeg, gif, bmp.
                            <br />
                            <b>Pictures will be resized to 96 x 96.</b>
                        </div>
                    </td>
                    <td>
                        <fudge:ImageUpload ID="avatar" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Password:</b>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" runat="server" ID="password" Text='<%# Bind("Password") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="password"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Confirm Password:</b>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" runat="server" ID="confirmPassword" />
                        &nbsp;
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="password"
                            ControlToValidate="confirmpassword" CssClass="error" ErrorMessage="Passwords do not match"
                            ForeColor="Black"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Language Preference:</b>
                    </td>
                    <td>
                        <fudge:LanguagesDropDown runat="server" ID="language" SelectedLanguageId='<%# Bind("LanguageId") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Text="Register" runat="server" ID="registerUser" CommandName="Insert" />
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
