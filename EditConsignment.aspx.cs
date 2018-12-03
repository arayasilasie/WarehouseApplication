using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class EditConsignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Guid WarehouseId = Session[""];
            BindApprovalGrid(new Guid(Session["CurrentWarehouse"].ToString()));
        }

        protected void grvGRNApproval_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvGRNApproval_RowDataBound(object sender, EventArgs e)
        {

        }

        public void BindApprovalGrid(Guid warehouseid)
        {
            DataTable dt = new DataTable();
            WarehouseRecieptBLL whrbll=new WarehouseRecieptBLL();
            dt = whrbll.getexpiredWHRsOnTruck(warehouseid);
            grvGRNApproval.DataSource = dt;
            grvGRNApproval.DataBind();

        }
    }
}