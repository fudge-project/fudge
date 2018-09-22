using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;

/// <summary>
/// Base class for all fudge pages
/// </summary>
public class FudgePage : Page {
    public enum MenuItem : int {
        Problems,
        Contests,
        Teams,
        Community,
        MyProfile,
        News,
        Help,
        None
    }

    public class VerificationRule {
        public string Key { get; set; }
        public bool Required { get; set; }
        public Func<string, bool> Rule { get; set; }
        public string ErrorMessage { get; set; }
    }

    private bool _requiresLogin;
    //selected menu item
    private MenuItem _selectedMenuItem;


    //rules to validate requests    
    private Dictionary<string, List<VerificationRule>> _queryValidationRules = new Dictionary<string, List<VerificationRule>>();

    public FudgePage(MenuItem selectedMenuItem)
        : this(selectedMenuItem, true) {
    }

    public FudgePage(MenuItem selectedMenuItem, bool requiresLogin) {
        _selectedMenuItem = selectedMenuItem;
        _requiresLogin = requiresLogin;
    }

    protected void AddVerificationRule(VerificationRule rule) {
        if (!_queryValidationRules.ContainsKey(rule.Key)) {
            _queryValidationRules[rule.Key] = new List<VerificationRule>();
        }
        _queryValidationRules[rule.Key].Add(rule);
    }

    protected void VerifyQueryString(string key, Func<string, bool> predicate, string errorMessage) {
        AddVerificationRule(new VerificationRule {
            Key = key,
            ErrorMessage = errorMessage,
            Required = true,
            Rule = predicate
        });
    }

    protected void VerifyQueryStringInt(string key, Func<int, bool> predicate, string errorMessage) {
        VerifyQueryString(key, qid => {
            int id;
            return Int32.TryParse(qid, out id) && predicate(id);
        }, errorMessage);
    }

    protected void VerifyQueryString(string key, Func<string, bool> predicate, string errorMessage, bool required) {
        AddVerificationRule(new VerificationRule {
            Key = key,
            ErrorMessage = errorMessage,
            Required = required,
            Rule = predicate
        });
    }

    protected void VerifyQueryStringInt(string key, Func<int, bool> predicate, string errorMessage, bool required) {
        VerifyQueryString(key, qid => {
            int id;
            return Int32.TryParse(qid, out id) && predicate(id);
        }, errorMessage, required);
    }

    protected override void OnLoad(EventArgs e) {
        Title = "(*Fudge)";        
        Session["requested_url"] = Request.RawUrl;
        if (_requiresLogin) {            
            ErrorHelper.RequireLogin();
        }        
        //register menu item on page load
        ClientScript.RegisterStartupScript(typeof(Page), "startup", String.Format("current={0};", (int)_selectedMenuItem), true);

        var pageBody = Master.FindControl<HtmlGenericControl>("body");
        if (pageBody != null) {
            pageBody.Attributes["onload"] = "handleLoad()";
        }

        string badKey = String.Empty;
        int badRule = 0;

        //check each rule
        foreach (var rule in _queryValidationRules) {
            for (int i = 0; i < rule.Value.Count; i++) {
                VerificationRule r = rule.Value[i];
                if (r.Required && (Request.IsQueryStringNull(r.Key) || !r.Rule(Request.QueryString[r.Key]))) {
                    badRule = i;
                    badKey = rule.Key;
                    break;
                }
                else if (!r.Required && !Request.IsQueryStringNull(r.Key) && !r.Rule(Request.QueryString[r.Key])) {
                    badRule = i;
                    badKey = rule.Key;
                    break;
                }
            }
        }

        if (String.IsNullOrEmpty(badKey)) {
            base.OnLoad(e);
        }
        else {
            var contentPanel = Master.FindControl<Panel>("contentPanel");
            contentPanel.Visible = false;
            var message = Master.FindControl<HtmlGenericControl>("message");
            message.Visible = true;
            message.InnerText = _queryValidationRules[badKey][badRule].ErrorMessage;
        }
    }

    /// <summary>
    /// Reference to the logged in user
    /// </summary>
    public User FudgeUser {
        get {
            return Fudge.Framework.Database.User.LoggedInUser;
        }
    }
}
