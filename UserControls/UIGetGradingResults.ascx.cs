using System;
using System.Collections;
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
using System.Collections.Generic;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIGetGradingResults : System.Web.UI.UserControl , ISecurityConfiguration
    {
        List<GradingResultBLL> list = new List<GradingResultBLL>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.lblMsg.Text = "";
            if (this.txtGradingCode.Text == "" && this.txtTrackingNo.Text == "")
            {
                this.lblMsg.Text = "Please enter Searching Cretria.";
                return;
            }
            else
            {
                BindData();
            }
            
        }

        protected void gvGradingResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvGradingResult_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
        }
        private void BindData()
        {
            GradingResultBLL objSearch = new GradingResultBLL();
            this.list = objSearch.Search(this.txtTrackingNo.Text, this.txtGradingCode.Text);
            if (list != null)
            {
                if (list.Count <= 0)
                {
                    this.lblMsg.Text = "No records Found.";
                    return;
                }
                this.gvGradingResult.DataSource = this.list;
                this.gvGradingResult.DataBind();
            }
            else
            {
                this.lblMsg.Text = "No records Found.";
                return;
            }
        }

        protected void gvGradingResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGradingResult.EditIndex = -1;
        }

        protected void gvGradingResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CA")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvGradingResult.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["GRID"] = id.Text;
                        Response.Redirect("GradingResultClientAcceptance.aspx");
                    }

                }
                else
                {
                    //TODO
                    this.lblMsg.Text  = "Unable to complete the request.";
                }
            }
            else if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvGradingResult.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["GRID"] = id.Text;
                        Response.Redirect("EditGradingReceived.aspx");
                    }

                }
                else
                {
                    //TODO
                    this.lblMsg.Text = "Unable to complete the request.";
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
                cmd.Add(this.btnSearch);
            }
            else if(name == "cmdEdit" )
            {
                foreach (TableRow rw in this.gvGradingResult.Rows)
                {
                    Control ca = new Control();
                    try
                    {
                        cmd = new List<object>();
                        ca = rw.FindControl("cmdEdit");
                        cmd.Add(ca);
                    }
                    catch
                    {
                    }
                }
            }
            else if (name == "cmdCA")
            {
                foreach (TableRow rw in this.gvGradingResult.Rows)
                {
                    cmd = new List<object>();
                    try
                    {
                        cmd.Add(rw.FindControl("cmdCA"));
                    }
                    catch
                    {
                    }
                }
            }
            return cmd;
        }

        #endregion
    }
}