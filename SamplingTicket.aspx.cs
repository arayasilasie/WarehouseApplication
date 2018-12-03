using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SamplingBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class SamplingTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboSampleStatus.Items.Clear();
                cboSampleStatus.DataSource = SamplingModel.GetSamplingStatus(0, "");
                cboSampleStatus.DataTextField = "Description";
                cboSampleStatus.DataValueField = "ID";
                cboSampleStatus.DataBind();
            }
        }

        protected void lbtnReload_Click(object sender, EventArgs e)
        {
            populateGrid();
        }

        protected void btnGetSampleTicket_Click(object sender, EventArgs e)
        {
            if (gvSampleTicketList.Rows.Count <= 0) return;

            GridViewRow rw = gvSampleTicketList.SelectedRow;
            Guid id = new Guid(((Label)rw.FindControl("lblID")).Text);

            Session["ReportType"] = "SampleTicket";
            Session["SampleId"] = id;
            ScriptManager.RegisterStartupScript(this,
                                                   this.GetType(),
                                                   "ShowReport",
                                                   "<script type=\"text/javascript\">" +
                                                   string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                   "</script>",
                                                   false);
        }

        private void populateGrid()
        {
            List<SamplingModelDetail> ls = null;
            ls = SamplingModel.GetSamplesDetail(UserBLL.GetCurrentWarehouse(), txtTrackNo.Text.Trim(), txtSampleCode.Text.Trim(), int.Parse(cboSampleStatus.SelectedValue));
            //if (ls != null && ls.Count > 0)
            // {
            this.gvSampleTicketList.DataSource = ls;
            this.gvSampleTicketList.DataBind();
            this.gvSampleTicketList.SelectedIndex = 0;
            // }
            if (ls == null || ls.Count <= 0)
            {
                Messages.SetMessage("There is no Sampling with the given tracking No.!", WarehouseApplication.Messages.MessageType.Warning);
            }
            else
            {
                Messages.ClearMessage();
            }
            UpdatePanel1.Update();
        }

        protected void btnEditSampleTicket_Click(object sender, EventArgs e)
        {

        }
    }
}