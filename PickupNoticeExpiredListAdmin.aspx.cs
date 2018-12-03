using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;

namespace WarehouseApplication
{
    public partial class PickupNoticeExpiredListAdmin : System.Web.UI.Page
    {
        static PickupNoticeModel lstSerch = new PickupNoticeModel();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            if (!txtExpirationDateFrom.Text.Equals(string.Empty))
                Session["ExpirationDateFrom"] = Convert.ToDateTime(txtExpirationDateFrom.Text);
            else
                Session["ExpirationDateFrom"] = DateTime.Now;
            if (!txtExpirationDateTo.Text.Equals(string.Empty))
            
                Session["ExpirationDateTo"] = Convert.ToDateTime(txtExpirationDateTo.Text);
               
            
            else
                Session["ExpirationDateTo"] = DateTime.Now.AddYears(-10);

            if (Convert.ToDateTime(txtExpirationDateTo.Text) < Convert.ToDateTime(txtExpirationDateFrom.Text))
            {
                Messages.SetMessage("Expiration Date From must be prior (less than) to Expiration Date To .", Messages.MessageType.Warning);
            }
            else{
                Session["ReportType"] = "ExpierdListAdmin";
                ScriptManager.RegisterStartupScript(this,
                                                             this.GetType(),
                                                             "ShowReport",
                                                             "<script type=\"text/javascript\">" +
                                                             string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=2000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                                                             "</script>",
                                                             false);
            }
        }
    }
    }
