using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using Fudge.Framework.Database;

namespace Extensions {

    public static class ControlExtensions {                
        /// <summary>
        /// Generic overload for FindControl, eliminates the need for casting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="id">The identifier of the control to be found.</param>
        /// <returns></returns>
        public static T FindControl<T>(this Control control, string id) where T : Control {
            return (T)control.FindControl(id);
        }

        public static void Validate(this CustomValidator validator, Func<string, bool> pred) {
            Validate(validator, pred, value => validator.ErrorMessage);
        }

        public static void ValidateData(this CustomValidator validator, Func<FudgeDataContext, string, bool> pred, Func<string, string> error) {
            validator.ServerValidate += (s, e) => {
                FudgeDataContext db = new FudgeDataContext();
                e.IsValid = pred(db, e.Value);
                validator.ErrorMessage = error(e.Value);
            };
        }

        public static void Validate(this CustomValidator validator, Func<string, bool> pred, Func<string, string> error) {
            validator.ServerValidate += (s, e) => {
                e.IsValid = pred(e.Value);
                validator.ErrorMessage = error(e.Value);
            };
        }
    }

}