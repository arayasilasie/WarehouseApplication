using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string safeMsg = string.Empty;
            try
            {
                long errorId = (long)Session["ErrorId"];
                safeMsg = String.Format("<p style='font-weight:bold;font-size:large'>A problem has occurred in the web site.</p>" +
                    "<p>You may contact the system administrator with the reference No: {0} of the problem " +
                    "you have encountered</p>", errorId);
            }
            catch
            {
                safeMsg = string.Format("<p style='font-weight:bold'> A critical problem has occured in the web site.</p>" +
                    "<p>You may not be able to work with the web site before the critical problem " +
                    "is resolved. Please contact the system administrator immediatly");
            }
            lblErrorMessage.Text = safeMsg;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("portal.ecx.com.et?CMD=logoff", true);
        }
    }
}
