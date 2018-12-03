using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WarehouseApplication
{
    public class ErrorMessageDisplayer
    {
        private ITextControl txtMessageDisplayer;

        public ErrorMessageDisplayer(ITextControl txtMessageDisplayer)
        {
            this.txtMessageDisplayer = txtMessageDisplayer;
        }

        public void ShowErrorMessage(string message)
        {
            ((WebControl)txtMessageDisplayer).Visible = true;
            txtMessageDisplayer.Text = message;
        }

        public void ClearErrorMessage()
        {
            txtMessageDisplayer.Text = string.Empty;
            ((WebControl)txtMessageDisplayer).Visible = false;
        }
    }
}
