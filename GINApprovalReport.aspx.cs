using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using GINBussiness;

namespace WarehouseApplication
{
    public partial class GINApprovalReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FillLIC(drpLIC, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator 
            drpLIC.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        private void FillLIC(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICAll(UserBLL.GetCurrentWarehouse());
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            if (drpLIC.SelectedItem.Text == "Select")
            {
                Messages.SetMessage("Please Select LIC!", Messages.MessageType.Warning); 
            }
            else
            {
                Session["SelectedLIC"] = drpLIC.SelectedValue;
                Session["LICName"] = drpLIC.SelectedItem.ToString();
                Session["ReportType"] = "GINApproval";
                ScriptManager.RegisterStartupScript(this,
                                                             this.GetType(),
                                                             "ShowReport",
                                                             "<script type=\"text/javascript\">" +
                                                             string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                                                             "</script>",
                                                             false);
            }
        }
    }
}