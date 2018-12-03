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
    public partial class UIListRequestCancelForApprovedGRN : System.Web.UI.UserControl , ISecurityConfiguration
    {
        static List<RequestforApprovedGRNCancelationBLL> list = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvGRNEditRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = this.gvGRNEditRequest.Rows[index];
                Label lblId = (Label)row.FindControl("lblId");
                if (lblId != null)
                {
                    Session["ApprovedGRNCancelRequestId"] = lblId.Text;
                    Response.Redirect("EditApprovedGRNCancelRequest.aspx");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (list != null)
            {
                list.Clear();
            }

            string GRNNo = string.Empty;
            string TrackingNo = string.Empty;
            Nullable<DateTime> from = null;
            Nullable<DateTime> to = null;
            Nullable<RequestforEditGRNStatus> status = null;
            GRNNo = this.txtGRN.Text;
            TrackingNo = this.txtTrackingNo.Text;
            try
            {
                status = (RequestforEditGRNStatus)int.Parse(this.cboStatus.SelectedValue);
            }
            catch
            {
                status = null;
            }
            try
            {
                from = DateTime.Parse(this.txtFrom.Text);
            }
            catch
            {
                from = null;
            }
            try
            {
                to = DateTime.Parse(this.txtTo.Text);
            }
            catch
            {
                to = null;
            }
            RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();

            try
            {
                list = obj.Search(GRNNo, TrackingNo, status, from, to);
                if (list != null)
                {
                    if (list.Count <= 0)
                    {
                        this.lblMessage.Text = "No recoreds Found";
                        this.gvGRNEditRequest.DataSource = list;
                        this.gvGRNEditRequest.DataBind();
                        return;
                    }
                    else
                    {
                        this.gvGRNEditRequest.DataSource = list;
                        this.gvGRNEditRequest.DataBind();
                    }

                }
                else
                {
                    this.lblMessage.Text = "No recoreds Found";
                    this.gvGRNEditRequest.DataSource = list;
                    this.gvGRNEditRequest.DataBind();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
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
            else if(name == "cmdEdit")
            {
                cmd = new List<object>();
                foreach( TableRow row in this.gvGRNEditRequest.Rows )
                {

                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
            return cmd;
        }

        #endregion
    }
}