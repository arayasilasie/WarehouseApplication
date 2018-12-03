using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UISearchScaling : System.Web.UI.UserControl , ISecurityConfiguration
    {
        private static List<ScalingBLL> list;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            string ScaleTicketNo ;
            Nullable<DateTime> startDateWeighed = null;  
            Nullable<DateTime> endDateWeighed = null; 
            string TrackingNo;
            string GradingCode;
            ScaleTicketNo = this.txtScalingNo.Text;
            if(this.txtStratDate.Text != "")
            {
                try
                {
                    startDateWeighed =DateTime.Parse(this.txtStratDate.Text);
                }
                catch
                {
                }
            }
            if(this.txtEndDate.Text != "")
            {
                try
                {
                     endDateWeighed =DateTime.Parse(this.txtEndDate.Text);
                }
                catch
                {
                }
            }
            TrackingNo = this.txtTrackingNo.Text ;
            GradingCode = this.txtGradingCode.Text ;
            ScalingBLL obj = new ScalingBLL();
            try
            {
                list = null;
                list = obj.Search(ScaleTicketNo, startDateWeighed,endDateWeighed, TrackingNo, GradingCode);
            }
            catch( Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }
            
            this.gvScaling.DataSource = list;
            this.gvScaling.DataBind();
            if (list == null)
            {
                this.lblMessage.Text = "No records Found";
            }
            if (list == null || list.Count == 0)
            {
                this.lblMessage.Text = "No records Found";
            }
            

        }



        protected void gvScaling_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvScaling_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnSearch")
            {
                cmd.Add(this.btnSearch);
            }
            else if( name == "cmdEdit")
            {
                foreach (TableRow row in this.gvScaling.Rows)
                {
                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
            return cmd;
        }

        #endregion

        protected void gvScaling_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditScaling")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvScaling.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["ScalingInfoEdit"] = id.Text;
                        Response.Redirect("EditScaling.aspx");
                    }

                }
                else
                {
                    //TODO
                    this.lblMessage.Text = "Unable to complete the request.";
                }
            }
        }
    }
}