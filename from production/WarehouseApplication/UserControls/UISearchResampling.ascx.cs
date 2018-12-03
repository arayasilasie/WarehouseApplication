using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UISearchResampling : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "cmdEdit")
            {
                int index;
                //Todo Check
                try
                {
                    index = Convert.ToInt32(e.CommandArgument);
                }
                catch
                {
                     index = 0;
                }
                GridViewRow rw = this.gvDetail.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["ResamplingEdit"] = id.Text;
                        Response.Redirect("EditReSampling.aspx");
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string TrackingNo = string.Empty;
            Nullable<int> previousSampleCode = null;
            Nullable<DateTime> from = null;
            Nullable<DateTime> to = null;
            Nullable<ReSamplingStatus> status = null;
            #region inputvalidation
            if (this.txtTrackingNo.Text != "")
            {
                TrackingNo = this.txtTrackingNo.Text;
            }
            if (this.txtSamplingCode.Text != null)
            {
                try
                {
                    previousSampleCode = int.Parse(this.txtSamplingCode.Text);
                }
                catch
                {
                }
            }
            if (this.txtFrom.Text != null)
            {
                try
                {
                    from = DateTime.Parse(this.txtFrom.Text);
                }
                catch
                {
                }
            }
            if (this.txtTo.Text != null)
            {
                try
                {
                    to = DateTime.Parse(this.txtTo.Text);
                }
                catch
                {
                }
            }
            if (this.cboStatus.SelectedValue != null || this.cboStatus.SelectedValue != "")
            {
                try
                {
                    status = (ReSamplingStatus)(int.Parse(this.cboStatus.SelectedValue.ToString()));
                }
                catch
                {
                }
            }
            #endregion
            ReSamplingBLL obj = new ReSamplingBLL();
            List<ReSamplingBLL> list = new List<ReSamplingBLL>();
            try
            {
                list = obj.Search(TrackingNo, previousSampleCode, from, to, status);
                if (list != null)
                {
                    if (list.Count == 0)
                    {
                        this.lblmsg.Text = "No Records Found";
                        return;
                    }
                    else
                    {
                        this.gvDetail.DataSource = list;
                        this.gvDetail.DataBind();
                    }
                }
                else
                {
                    this.lblmsg.Text = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                this.lblmsg.Text = ex.Message;
            }
        }


        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnSearch")
            {
                cmd = new List<object>();
                cmd.Add(this.btnSearch);
            }
            else if (name == "cmdEdit")
            {
                cmd = new List<object>();
                foreach (TableRow row in this.gvDetail.Rows)
                {
                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
                return cmd;
        }

        #endregion
    }
}