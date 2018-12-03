using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Data;
namespace WarehouseApplication
{
    public partial class ListInboxDetailNew : System.Web.UI.Page
    {
        static int StepID;
        static int TypeID;
        static DataTable dtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["StepID"] != null)
            {
                //if(Request.QueryString["Task"]!=null)   
                 lblDetail.Text = Request.QueryString["Task"].ToString();
                 ViewState["Task"] = Request.QueryString["Task"].ToString();
                 StepID = int.Parse(Request.QueryString["StepID"].ToString());
                 TypeID = int.Parse(Request.QueryString["TypeID"].ToString()); 
                 BindDetailGridview();
            }
        }
        public void BindDetailGridview()
        {
            if(!IsPostBack)
                dtbl = InboxModel.GetInboxDetailList(new Guid(Session["CurrentWarehouse"].ToString()), StepID, TypeID);
            grvDetail.DataSource = dtbl;
            grvDetail.DataBind();
        }
        
        protected void grvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           grvDetail.PageIndex = e.NewPageIndex;
           BindDetailGridview();
           
        }

        protected void grvDetail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string taskName = "";
            taskName =  ViewState["Task"].ToString();
            if (e.CommandName == "Detail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.grvDetail.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblTrackingNo");
                    if (id != null)
                    {
                        
                        Response.Redirect("PageSwictherNew.aspx?TranNo=" + id.Text + "&task=" + taskName) ;
                    }

                }
            }
        }

    }
}