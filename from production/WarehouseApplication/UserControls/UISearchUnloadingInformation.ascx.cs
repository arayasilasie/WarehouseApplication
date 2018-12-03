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
    public partial class UISearchUnloadingInformation : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            Bind();


        }

        protected void gvUnloading_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvUnloading_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUnloading.PageIndex = e.NewPageIndex;
            Bind();
        }

        private void Bind()
        {
            string strTrackingNo = ucSearchControl.TrackingNo;
            string strCode = ucSearchControl.Code;
            List<UnloadingBLL> list = new List<UnloadingBLL>();
            UnloadingBLL obj = new UnloadingBLL();
            list = obj.Search(strCode, strTrackingNo);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    this.gvUnloading.DataSource = list;
                    this.gvUnloading.DataBind();
                }
                else
                {
                    this.lblmsg.Text = "No Matching Records Found";
                }
            }
            else
            {
                this.lblmsg.Text = "No Matching Records Found";
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if(name == "btnSearch" )
            {
                cmd = new List<object>();
                cmd.Add(this.btnSearch);
            }
            else if(name == "cmdEdit" )
            {
                cmd = new List<object>();
                foreach (TableRow row in this.gvUnloading.Rows)
                {
                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
            return cmd;
        }

        #endregion

        protected void gvUnloading_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UnloadingBLL obj = (UnloadingBLL)e.Row.DataItem;
                Label lblCG =(Label) e.Row.Cells[2].FindControl("lblCommodityGrade");
                lblCG.Text = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
            }
        }

        protected void gvUnloading_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "cmdRowEdit")
            {
                int x =-1;
                x = int.Parse( e.CommandArgument.ToString());
                if(x != -1)
                {
                    Label lblId =(Label) this.gvUnloading.Rows[x].FindControl("lblId");
                    if (lblId != null)
                    {
                        Response.Redirect("EditUnloadingInformation.aspx?id=" + lblId.Text.ToString());
                    }

                }
                
            }
        }
    }
}