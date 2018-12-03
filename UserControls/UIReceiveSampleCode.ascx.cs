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
    public partial class UIReceiveSampleCode : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodeSampleRecivedGradingId"] != null)
            {
                this.Page.DataBind();
                Guid Id = new Guid(Session["CodeSampleRecivedGradingId"].ToString());
                LoadPage(Id);
            }
            else
            {
                this.lblMessage.Text = "Your session has expired or an error has occured please log out and try again.If the error persists contact the administrator";
                this.btnSave.Enabled = false;
                return;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Guid Id = Guid.Empty;
            string strRemark = string.Empty;
            if (hfId.Value != null)
            {
                Id = new Guid(hfId.Value);
            }
            if (ViewState["GradingTrackingNo"] == null)
            {
                this.lblMessage.Text = "An error has occured please try agian.if the error persists contact the administrator.";
                return;
            }
            Nullable<DateTime> receivedDateTime = null;
            receivedDateTime =  DateTime.Parse(txtDateRecived.Text + " " + txtTimeRecived.Text);
            strRemark = this.txtLabTechRemark.Text;
            GradingBLL obj = new GradingBLL();
            obj.Id = Id;
            obj.IsCodeReceivedAtLab = true;
            obj.CodeReceivedTimeStamp = receivedDateTime;
            obj.TrackingNo = ViewState["GradingTrackingNo"].ToString();
            if (obj.UpdateSampleCodeReceived())
            {
                this.lblMessage.Text = "Data updated sucessfully.";
                this.btnClear.Enabled = true;
                this.btnSave.Enabled = false;
                return;
            }
            else
            {
                this.lblMessage.Text = "Unable to update data,please try agian.";
                return;
            }

        }
        private void LoadPage(Guid Id)
        {
            GradingBLL obj = new GradingBLL();
            obj = obj.GetById(Id);
            if (obj != null)
            {
                hfId.Value = Id.ToString();
                this.lblGradingCode.Text = obj.GradingCode;
                this.txtDateCoded.Text = obj.DateCoded.ToShortDateString(); 
                this.cmpCodeGen.ValueToCompare = obj.DateCoded.ToShortDateString();
                ViewState["GradingTrackingNo"] = obj.TrackingNo;
            }
            else
            {
                this.lblMessage.Text = "An error has occured please log out and try again.If the error persists contact the administrator";
                this.btnSave.Enabled = false;
                return;
            }
        }
        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            cmd.Add(this.btnSave);
            return cmd;
        }

        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
    }
}