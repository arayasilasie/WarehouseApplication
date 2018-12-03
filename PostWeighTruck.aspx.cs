using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WarehouseApplication
{
    public partial class PostWeighTruck : ECXWarehousePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Tran = Request.QueryString["TranNo"];
            Response.Redirect("AddScalingInformation.aspx?TranNo=" + Tran);
        }
    }
}
