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
using System.Web.Security;


namespace WarehouseApplication
{
    public partial class StockBalanceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindWarehouse();
            //string currentWarehouse = UserBLL.GetCurrentWarehouse().ToString();
            //int warehouseCount=cboWarehouse.Items.Count;
            //if (!User.IsInRole("WHAdmin"))
            //{
            //    for (int position = warehouseCount - 1; position > 0; position--)
            //    {
            //        if (cboWarehouse.Items[position].Value != currentWarehouse)
            //            cboWarehouse.Items.RemoveAt(position);
            //        else
            //            continue;
            //    }
            //}
        }

        public void BindWarehouse()
        {
            cboWarehouse.DataSource = WarehouseBLL.GetAllActiveWarehouse();
            cboWarehouse.DataTextField = "WarehouseName";
            cboWarehouse.DataValueField = "WarehouseId";
            cboWarehouse.DataBind();
        }

        public void BindShed()
        {
            cboShed.Items.Clear();
            cboShed.Items.Add(new ListItem("Select Shed", ""));
            cboShed.DataSource = StackModel.GetWarehouseSheds(new Guid(cboWarehouse.SelectedValue));
            cboShed.DataTextField = "ShedNumber";
            cboShed.DataValueField = "ID";
            cboShed.DataBind();
        }

        public void BindLIC()
        {
            cboLIC.Items.Clear();
            cboLIC.Items.Add(new ListItem("Select LIC", ""));
            cboLIC.DataSource = StackModel.GetLICbyShed(new Guid(cboWarehouse.SelectedValue), new Guid(cboShed.SelectedValue));
            cboLIC.DataTextField = "LIC";
            cboLIC.DataValueField = "LICID";
            cboLIC.DataBind();
        }

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWarehouse.SelectedValue != string.Empty)
            {
                BindShed();
            }
            else
            {
                cboShed.Items.Clear();
                cboShed.Items.Add(new ListItem("Select Shed", ""));
                cboLIC.Items.Clear();
                cboLIC.Items.Add(new ListItem("Select LIC", ""));
            }
        }

        protected void ddlShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWarehouse.SelectedValue != string.Empty)
            {
                BindLIC();
            }
            else
            {
                cboLIC.Items.Clear();
                cboLIC.Items.Add(new ListItem("Select LIC", ""));
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {                       
            try
            {
                GRN_BL objGrn = new GRN_BL();
                rptStockBalance rpt = new rptStockBalance();
                Guid warehouse = new Guid(cboWarehouse.SelectedValue);
                Guid shed = new Guid(cboShed.SelectedValue);
                Guid LIC = new Guid(cboLIC.SelectedValue);
                DateTime startDate = string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Parse("1/1/2010") : DateTime.Parse(txtStartDate.Text);
                DateTime endDate = string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now : DateTime.Parse(txtEndDate.Text);
                TimeSpan span = endDate.Subtract(startDate);
                if (span.Days > 31)
                {
                    Messages1.SetMessage("Date interval should not exceed one Month", WarehouseApplication.Messages.MessageType.Warning);
                    return;
                }
                DataTable dt = objGrn.GetStockBalanceReport(warehouse, shed, LIC, startDate, endDate);


                DataTable dtb = objGrn.GetStockBalanceReportGIN(warehouse, shed, LIC, startDate, endDate);

                if (dt.Rows.Count == 0 && dtb.Rows.Count == 0)
                {
                    Messages1.SetMessage("No record found with specified criteria.", WarehouseApplication.Messages.MessageType.Warning);
                    WebViewer1.ClearCachedReport();
                    return;
                }
                string record = ((dt.Rows.Count + dtb.Rows.Count) == 1) ? "record found with specified criteria." : "records found with specified criteria.";

                if (dt.Rows.Count != 0 && dtb.Rows.Count != 0)
                {
                    Messages1.SetMessage(dt.Rows.Count + " Unloading and " + dtb.Rows.Count + " Loading " + record, WarehouseApplication.Messages.MessageType.Success);
                }

                else if (dt.Rows.Count != 0 && dtb.Rows.Count == 0)
                {
                    Messages1.SetMessage(dt.Rows.Count + " Unloading " + record, WarehouseApplication.Messages.MessageType.Success);
                }
                else
                {
                    Messages1.SetMessage(dtb.Rows.Count + " Loading " + record, WarehouseApplication.Messages.MessageType.Success);
                }
                rpt.dtbl = dtb;
                rpt.SumNumberOfBags = dt.Compute("Sum(GRNNumberOfBags)", "");
                rpt.SumNumberOfRebagging = dt.Compute("Sum(RebagingQuantity)", "");
                rpt.SumNumberOfNetWeight = dt.Compute("Sum(NetWeight)", "");

                rpt.BagSum = dtb.Compute("Sum(NoOfBags)", "");
                rpt.RebaggingSum = dtb.Compute("Sum(NoOfRebags)", "");
                rpt.NetWeightSum = dtb.Compute("Sum(NetWeight)", "");
                rpt.AdjustmentBagSum = dtb.Compute("Sum(BagAdjustment)", "");
                rpt.AdjustmentWeightSum = dtb.Compute("Sum(WeightAdjustment)", "");

                rpt.Warehouse = cboWarehouse.SelectedItem.Text;
                rpt.Shed = cboShed.SelectedItem.Text;
                rpt.LIC = cboLIC.SelectedItem.Text;
                rpt.DateFrom = string.IsNullOrEmpty(txtStartDate.Text) ? "No Limit" : txtStartDate.Text;
                rpt.DateTo = string.IsNullOrEmpty(txtEndDate.Text) ? "No Limit" : txtEndDate.Text;
                rpt.DataSource = dt;
                WebViewer1.Report = rpt;
            }
            catch
            {
                Messages1.SetMessage("Unable to get data, please try again.", WarehouseApplication.Messages.MessageType.Error);
            }
 
        }

    }
}