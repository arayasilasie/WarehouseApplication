using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class GINSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPickupNoticeStatus();

            }
        }
        private void FillPickupNoticeStatus()
        {
            GINBussiness.PickupNoticeModel.FillPickupNoticeStatus(drpStatus);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int warehouseReceipt;
            string clientId = txtClientId.Text;
            string status = drpStatus.SelectedItem.Value;
            string GinNo = txtGINNo.Text;
            Guid warehouseId = UserBLL.GetCurrentWarehouse();

            if (txtWareHouseReceipt.Text.Equals(string.Empty))
                warehouseReceipt = 0;
            else
                warehouseReceipt = Convert.ToInt32(txtWareHouseReceipt.Text);
            //List<GINModel> gmList = new List<GINModel>();
            //gmList = GINModel.SearchGIN(clientId, warehouseReceipt, status, GinNo,warehouseId);
            //gvSearchGIN.DataSource = gmList;
            //gvSearchGIN.DataBind();

            gvSearchGIN.DataSource = GINModel.SearchGIN2(clientId, warehouseReceipt, status, GinNo, warehouseId);
            gvSearchGIN.DataBind();
        }
        protected void gvSearchGIN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Search")
            {
                String gn = (String)(e.CommandArgument);
                Session["EditMode"] = true;
                GINModel gm = GINModel.GetGIN(gn);
                gm.PrepareGINEdit(gm.PickupNoticeId, gm.ID);
                Session["GINMODEL"] = gm;
                Response.Redirect("GIN.aspx");
            }
        }

        protected void gvSearchGIN_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // disable going to GIN page when it's cancel
                if ((((Label)e.Row.FindControl("lblGINStatus")).Text).Substring(0, 6).Equals("Cancel"))
                {
                    e.Row.Cells[0].Enabled = false;
                }
            }
        }
    }
}
