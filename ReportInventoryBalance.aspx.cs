using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;
using WarehouseApplication.Reports;

namespace WarehouseApplication
{
    public partial class ReportInventoryBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindWarehouse();          
        }

        public void BindWarehouse()
        {
            ddlWarehouse.DataSource = WarehouseBLL.GetAllActiveWarehouse();
            ddlWarehouse.DataTextField = "WarehouseName";
            ddlWarehouse.DataValueField = "WarehouseId";
            ddlWarehouse.DataBind();
        }
      
        public void  BindShed()
        {       
            ddlShed.Items.Clear();
            ddlShed.Items.Add(new ListItem("Select Shed", ""));
            ddlShed.DataSource = StackModel.GetWarehouseSheds(new Guid(ddlWarehouse.SelectedValue));
            ddlShed.DataTextField = "ShedNumber";
            ddlShed.DataValueField = "ID";
            ddlShed.DataBind();      
        }

        public void BindLIC()
        {
            ddlLIC.Items.Clear();
            ddlLIC.Items.Add(new ListItem("Select LIC", ""));
            ddlLIC.DataSource = StackModel.GetLICbyShed(new Guid(ddlWarehouse.SelectedValue),new Guid (ddlShed.SelectedValue));
            ddlLIC.DataTextField = "LIC";
            ddlLIC.DataValueField = "LICID";
            ddlLIC.DataBind();
        }

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWarehouse.SelectedValue != string.Empty)
            {
                BindShed();              
            }
            else
            {
                ddlShed.Items.Clear();
                ddlShed.Items.Add(new ListItem("Select Shed", ""));
                ddlLIC.Items.Clear();
                ddlLIC.Items.Add(new ListItem("Select LIC", ""));
            }
        }

        protected void ddlShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWarehouse.SelectedValue != string.Empty)
            {
                BindLIC();
            }
            else
            {
                ddlLIC.Items.Clear();
                ddlLIC.Items.Add(new ListItem("Select LIC", "")); 
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

            rptInventoryBalance rpt = new rptInventoryBalance();
            DataTable dtbl = InventoryTransferModel.GetInventoryBalance(
                 new Guid(ddlWarehouse.SelectedValue), new Guid(ddlShed.SelectedValue), new Guid(ddlLIC.SelectedValue));
            rpt.DataSource = dtbl;
            WebViewer1.Report = rpt;
            
        }
    }
}