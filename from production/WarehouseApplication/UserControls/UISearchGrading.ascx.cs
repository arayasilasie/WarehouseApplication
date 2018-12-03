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
    public partial class UISearchGrading : System.Web.UI.UserControl , ISecurityConfiguration
    {
        private static List<GradingBLL> list = new List<GradingBLL>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                list = null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            list = null;
            GradingBLL obj = new GradingBLL();
            string TrackingNo = this.txtTrackingNo.Text;
            string GradingCode = this.txtGradingCode.Text;
            string SamplingResultCode = this.txtSamplingCode.Text;
            Nullable<DateTime> from = null;
            Nullable<DateTime> to = null;
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
            try
            {
                list = obj.Search(TrackingNo, GradingCode, SamplingResultCode, from, to);
                BindGrid();
            }
            catch (NULLSearchParameterException )
            {

                this.lblmsg.Text = "Please provide Searching parameters";
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "cmdEdit")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow rw = this.gvDetail.Rows[index];
            //    Label id = (Label)rw.FindControl("lblId");

            //    if (id != null && id.Text != "")
            //    {
            //        Session["GradingRecivedGradingId"] = id.Text;
            //        Response.Redirect("EditGradingReceived.aspx");
            //    }
            //    else
            //    {
            //        this.lblmsg.Text = "An error has occured.Please enter your Search parameter and try again.";
            //    }
                
            //}
        }
        private void BindGrid()
        {
            if (list.Count <= 0)
            {
                this.lblmsg.Text = "No records found.";
            }
            this.gvDetail.DataSource = list;
            this.DataBind();
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
            else if( name == "cmdEdit" )
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