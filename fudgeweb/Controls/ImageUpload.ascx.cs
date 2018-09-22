using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Linq;
using System.IO;

public partial class Controls_ImageUpload : System.Web.UI.UserControl {
    public bool IsValid {
        get {
            imageValidator.Validate();
            return imageValidator.IsValid;
        }
    }


    public bool HasFile {
        get {
            return imageUpload.HasFile;
        }
    }


    public byte[] ImageBytes {
        get {
            string extension = Path.GetExtension(imageUpload.FileName);
            byte[] imageBytes = System.Drawing.Image.FromStream(imageUpload.FileContent).Resize(96, 96).ToByteArray(extension);
            return imageBytes;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        imageValidator.ServerValidate += (s, args) => {            
            if (HasFile) {
                string extension = Path.GetExtension(imageUpload.FileName);
                args.IsValid = Util.IsSupportedImageExtension(extension); ;
            }
            else {
                args.IsValid = true;
            }
        };
    }
}
