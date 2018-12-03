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
    public partial class UICancelGRN : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                this.UIEditGRN1.cboStatus.SelectedValue = ((int)GRNStatus.Cancelled).ToString();
                this.UIEditGRN1.btnAdd.Visible = false;
                this.hfTrackingNo.Value = Session["CancelGRNTrackingNo"].ToString();
                Session["CancelGRNTrackingNo"] = null;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            GRNBLL objGRN = new GRNBLL();
            string TrackingNo = "";
            TrackingNo = this.hfTrackingNo.Value;
            objGRN.GRN_Number = this.UIEditGRN1.lblGRN.Text;
            objGRN.Id = new Guid(this.UIEditGRN1.hfGRNId.Value.ToString());
            objGRN.GradingId = new Guid(this.UIEditGRN1.hfGradingId.Value.ToString());
            isSaved = objGRN.Update(this.UIEditGRN1.lblGRN.Text, GRNStatus.Cancelled, objGRN, TrackingNo,DateTime.Now);
            if (isSaved == true)
            {
                this.UIEditGRN1.lblmsg.Text = "Update Sucessfull";
                this.btnCancel.Enabled = false;
                return;
            }
            else
            {
                this.UIEditGRN1.lblmsg.Text = "Unable to cancel the GRN.";
                return;
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnCancel")
            {
                cmd.Add(this.btnCancel);
            }
            return cmd;
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            WFTransaction.WorkFlowManager(this.hfTrackingNo.Value);
        }
    }
}