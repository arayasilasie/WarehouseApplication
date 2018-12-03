using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UITrackingNumberCheck : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strTrackingNo = "";
            strTrackingNo = this.txtTrackingNo.Text;
            this.lblstatus.Text = "";
            this.lblMessage.Text = "";
            if (!(string.IsNullOrEmpty(strTrackingNo)))
            {
                try
                {
                    this.lblstatus.Text = WFTransaction.GetMessage(strTrackingNo);
                }
                catch
                {
                    this.lblMessage.Text = "Can't Find this tracking No.";
                }
            }
            else
            {
                this.lblMessage.Text = "please enter Tracking No.";
            }
        }

        //#region ISecurityConfiguration Members

        //public List<object> GetSecuredResource(string scope, string name)
        //{
        //    List<object> cmd = null;
        //    if (name == "btnSearch")
        //    {
        //        cmd = new List<object>();
        //        cmd.Add(btnSearch);

        //    }
        //    return cmd;
        //}

        //#endregion

    }
}