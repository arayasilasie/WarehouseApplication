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
    public partial class UIMoistureResamplingRequest : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                ReSamplingBLL obj = new ReSamplingBLL();
                List<ReSamplingBLL> list;
                list = obj.GetPendingResampling(UserBLL.GetCurrentWarehouse());
                if (list != null)
                {
                    this.cboSamplingCode.Items.Add( new ListItem("Please Select Sampling Code"));
                    foreach(ReSamplingBLL item in list)
                    {
                        if ((item.SampleCode != 0) && (item.SamplingResultId != Guid.Empty || item.SamplingResultId != null))
                        {
                            this.cboSamplingCode.Items.Add(new ListItem(item.SampleCode.ToString(), item.SamplingResultId.ToString()));

                        }
                    }
                }
                else
                {
                    this.lblmsg.Text = "No Results Pendindg Resampling.";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            DateTime dtRecivedDateTime;
            dtRecivedDateTime = DateTime.Parse(this.txtDate.Text + " " + this.txtTime.Text);
            Guid SamplingResultId = Guid.Empty;
            SamplingResultId = new Guid(this.cboSamplingCode.SelectedValue.ToString());
            // Get Related Data.
            ReSamplingBLL objReSampling = new ReSamplingBLL();
            objReSampling.LoadSamplingRealtedData(SamplingResultId, dtRecivedDateTime);
            isSaved = objReSampling.Save();
            if (isSaved == true)
            {
                this.lblmsg.Text = "Data Updated Successfully.";
                this.btnSave.Enabled = false;
                return;
            }
            else
            {
                this.lblmsg.Text = "Unable to record the data. Please try again.";
                return;
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            if (name == "btnSave")
            {
                List<object> cmd = new List<object>();
                cmd.Add(this.btnSave);
                return cmd;
            }
            return null;
        }

        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
    }
}