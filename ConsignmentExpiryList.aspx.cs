using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WarehouseApplication
{
    public partial class ConsignmentExpiry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = WarehouseApplication.BLL.ExpiredConsignment.SearchConsExpieredList(WarehouseApplication.BLL.UserBLL.GetCurrentWarehouse());
            celViewer.Report = new WarehouseApplication.Report.rptConsignment(dt);
            celViewer.Visible = true;
        }
    }
}