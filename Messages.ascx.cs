using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WarehouseApplication
{
    public partial class Messages : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public enum MessageType : int
        {
            Error = 0,
            Warning = 1,
            Success = 2 ,
            Information
        }

    

        public void SetMessage(string sMessage)
        {
            SetMessage(sMessage, MessageType.Error);
        }

        public void SetMessage(string sMessage, MessageType messageType)
        {
            pnlMessage.CssClass = "messages messages-error";
            if (messageType == MessageType.Warning)
            {
                pnlMessage.CssClass = "messages messages-warning";
            }
            else if (messageType == MessageType.Success)
            {
                pnlMessage.CssClass = "messages messages-success";
            }

            pnlMessage.Visible = (sMessage.Length > 0);
            litMessage.Text = sMessage;
            litMessage.Visible = true;
            litSpace.Text = "<br />";
        }

        public void ClearMessage()
        {
            litMessage.Text = "";
            litSpace.Text = "";
            pnlMessage.Visible = false;
        }
    }
}