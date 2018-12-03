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
    public partial class UIEditApprovedGRNCancelRequest : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["ApprovedGRNCancelRequestId"] == null)
                {
                    throw new Exception("Your session has expired");
                }
                else
                {
                    Guid Id = new Guid(Session["ApprovedGRNCancelRequestId"].ToString());
                    LoadData(Id);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();

            obj.Id = new Guid(this.hfGRNID.Value.ToString());
            obj.DateRequested = DateTime.Parse(this.txtDateRequested.Text);
            obj.Remark = this.txtRemark.Text;
            obj.Status = (RequestforApprovedGRNCancelationStatus)int.Parse((this.cboStatus.SelectedValue.ToString()));
            try
            {
                if (obj.Update() == true)
                {
                    this.lblMessage.Text = "Update sucessfull.";
                    return;
                }
                else
                {
                    this.lblMessage.Text = "Unable to Update Data.";
                    return;
                }
            }
            catch
            {
                this.lblMessage.Text = " An Error has occured please try again";
                return;
            }
        }
        private void LoadData(Guid Id)
        {
            RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
            obj = obj.GetById(Id);
            if (obj != null)
            {
                if (obj.Id != null)
                {
                    hfGRNID.Value = obj.Id.ToString();
                }
                if (obj.GRN_Number != null)
                {
                    this.txtGRNNo.Text = obj.GRN_Number;
                }
                if (obj.DateRequested != null)
                {
                    this.txtDateRequested.Text = obj.DateRequested.ToString();
                }
                if (obj.Remark != null)
                {
                    this.txtRemark.Text = obj.Remark;
                }
                hfTrackingNo.Value = obj.TrackingNo;

                this.cboStatus.SelectedValue = ((int)obj.Status).ToString();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WFTransaction.WorkFlowManager(this.hfTrackingNo.Value.ToString());
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnEdit")
            {
                cmd.Add(this.btnEdit);
            }
            return cmd;
        }

        #endregion
    }
}