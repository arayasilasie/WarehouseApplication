using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    public partial class UISearchSamplingTicket : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<SamplingBLL> list = null;
            string TrackingNo = "";
            string SamplingCode = "";
            Nullable<DateTime> from = null;
            Nullable<DateTime> to = null;

            TrackingNo = this.txtTrackingNo.Text;
            SamplingCode = this.txtSampleCode.Text;
            try
            {
                from = DateTime.Parse(this.txtStratDate.Text);
                to = DateTime.Parse(this.txtEndDate.Text);
            }
            catch
            {
            }

            list = SamplingBLL.SearchSampling(TrackingNo, SamplingCode, from, to);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    this.gvScaling.DataSource = list;
                    this.gvScaling.DataBind();
                }
            }
            else
            {
                this.lblMessage.Text = "No Records find.";
                return;
            }

        }

        protected void gvScaling_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rw = this.gvScaling.Rows[index];
            if (e.CommandName == "edit")
            {
                Label lblId = (Label)rw.FindControl("lblId");
                if (lblId.Text != "")
                {
                    Session["SamplingId"] = lblId.Text;
                    Response.Redirect("EditSampling.aspx");
                }
            }
            else if (e.CommandName == "Print")
            {
                Label lblId = (Label)rw.FindControl("lblId");
                if (lblId.Text != "")
                {
                    Guid SId = Guid.Empty;
                    SId = new Guid(lblId.Text);
                    SamplingBLL objSample = new SamplingBLL();
                    objSample = objSample.GetSampleDetail(SId);
                    if (objSample == null)
                    {
                        this.lblMessage.Text = "Unable to print sample Ticket";
                        return;
                    }
                    objSample.Id = SId;
                    Session["Sample"] = objSample;
                    SamplerBLL objSampler = new SamplerBLL();
                    objSampler = objSampler.GetSamplerBySamplingId(SId)[0];

                    Session["Sampler"] = objSampler;

                    ScriptManager.RegisterStartupScript(this,
          this.GetType(),
          "ShowReport",
          "<script type=\"text/javascript\">" +
              string.Format("javascript:window.open(\"ReportSampleTicket.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", lblId.Text.ToString()) +
          "</script>",
          false);
                }
            }
        }
    }
}