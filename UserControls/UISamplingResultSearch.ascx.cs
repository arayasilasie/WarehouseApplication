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
using System.Text;

namespace WarehouseApplication.UserControls
{
    public partial class UISamplingSearch : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void gvGradingResult_PageIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvGradingResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGradingResult.PageIndex = e.NewPageIndex;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            string trackingNo = this.txtTrackingNo.Text;
            string samplecode = "";

                    if (this.txtSamplingCode.Text == "")
                    {
                        samplecode = null;
                    }
                    else
                    {
                      
                        samplecode = this.txtSamplingCode.Text;
                    }
               
            
         
                List<SamplingResultBLL> list;
                SamplingResultBLL obj = new SamplingResultBLL();
                list = obj.Search(trackingNo, samplecode);
                if (list != null)
                {
                    if (list.Count < 1)
                    {
                        this.lblMsg.Text = "Your Search returns 0 results.";
                    }
                    else
                    {
                        this.gvGradingResult.DataSource = list;
                        this.gvGradingResult.DataBind();
                    }
                }
                else
                {
                    this.lblMsg.Text = "Your Search returns 0 results.";
                }
            }

        protected void gvGradingResult_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGradingResult.EditIndex = e.NewEditIndex;
            GridViewRow rw = this.gvGradingResult.Rows[this.gvGradingResult.EditIndex];
            if (rw != null)
            {
                Label id = (Label)rw.FindControl("lblId");
                if (id != null)
                {
                    Session["SamplingResultId"] = id.Text;
                    Response.Redirect("EditSamplingResult.aspx");
                }

            }
            else
            {
                //TODO
                //this.lblmsg = "Unable to complete the request.";
            }
        }

        protected void gvGradingResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvGradingResult.Rows[index];
                Label lblCode = (Label)row.FindControl("lblSamplingCode");
                if (lblCode != null)
                {
                    if (lblCode.Text != "")
                    {

                        Session["SamplingResultCode"] = lblCode.Text;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script>");
                        sb.Append("window.open('ReportSamplingResult.aspx");
                        sb.Append("', '', 'toolbar=0');");
                        sb.Append("</scri");
                        sb.Append("pt>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowReport",
                            sb.ToString(), false);
                    }
                }
            }
        }



        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnSearch")
            {
                cmd = new List<object>();
                cmd.Add(btnSearch);
                return cmd;
            }
            else if (name == "cmdEdit")
            {
                foreach (TableRow row in this.gvGradingResult.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
            else if (name == "cmdPrint")
            {
                foreach (TableRow row in this.gvGradingResult.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdPrint"));
                }
            }
            return cmd;
        }

        #endregion
    }
}