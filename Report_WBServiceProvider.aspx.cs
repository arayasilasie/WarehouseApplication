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
    public partial class Report_WBServiceProvider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindWarehouse();
            Messages1.ClearMessage();
        }
        public void BindWarehouse()
        {
            cboWarehouse.DataSource = WarehouseBLL.GetAllActiveWarehouse();
            cboWarehouse.DataTextField = "WarehouseName";
            cboWarehouse.DataValueField = "WarehouseId";
            cboWarehouse.DataBind();
        }

        protected void cboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int position = cboWBServiceProvider.Items.Count; position > 1; position--)
            {
                cboWBServiceProvider.Items.RemoveAt(position - 1);
            }
            if (cboWarehouse.SelectedIndex == 0)
                return;
            GRN_BL objGRN = new GRN_BL();
            cboWBServiceProvider.DataSource = objGRN.GetAllWBServiceProviders(new Guid(cboWarehouse.SelectedValue));
            cboWBServiceProvider.DataTextField = "ServiceProviderName";
            cboWBServiceProvider.DataValueField = "Id";
            cboWBServiceProvider.DataBind();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Messages1.ClearMessage();
                GRN_BL objGrn = new GRN_BL();
                rptWBServiceProvider rpt = new rptWBServiceProvider();
                Guid warehouseId = new Guid(cboWarehouse.SelectedValue);
                int wbServiceProviderId = cboWBServiceProvider.SelectedIndex == 0 ? -2 : int.Parse(cboWBServiceProvider.SelectedValue);
                DateTime startDate = string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Parse("1/1/2010") : DateTime.Parse(txtStartDate.Text);
                DateTime endDate = string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now : DateTime.Parse(txtEndDate.Text);
                int serviceType = int.Parse(cboServiceType.SelectedValue);
                DataTable dt = objGrn.GetWBServiceProviderReport(warehouseId, wbServiceProviderId, startDate, endDate, serviceType);
                decimal sumNumberOfBags, sumNetWeight;
                
                int rowsCount = dt.Rows.Count;

                string NoOfRegords="";
                if (rowsCount == 0)
                {
                    Messages1.SetMessage("No record found with specified criteria.", WarehouseApplication.Messages.MessageType.Warning);
                    WebViewer1.ClearCachedReport();
                    return;
                }
                else
                {
                    sumNumberOfBags = decimal.Parse(dt.Compute("Sum(NumberOfBags)", "").ToString());
                    sumNetWeight = decimal.Parse(dt.Compute("Sum(NetWeight)", "").ToString());
                    NoOfRegords = rowsCount == 1 ? "1 record found." : rowsCount + " records found.";
                    Messages1.SetMessage(NoOfRegords, WarehouseApplication.Messages.MessageType.Success);
                    rpt.SumNumberOfBags = sumNumberOfBags;
                    rpt.SumNetWeight = sumNetWeight;
                }
                rpt.Warehouse = cboWarehouse.SelectedItem.Text;
                rpt.WBServiceProvider = cboWBServiceProvider.SelectedIndex == 0 ? "All" : cboWBServiceProvider.SelectedItem.Text;
                rpt.DateFrom = string.IsNullOrEmpty(txtStartDate.Text) ? "No Limit" : txtStartDate.Text;
                rpt.DateTo = string.IsNullOrEmpty(txtEndDate.Text) ? "Present Date" : txtEndDate.Text;
                rpt.DataSource = dt;

                WebViewer1.Report = rpt;
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            }
        }

        private DataDynamics.ActiveReports.Export.Xls.XlsExport xlsExport1;

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.IO.MemoryStream m_stream = new System.IO.MemoryStream();
            GRN_BL objGrn = new GRN_BL();
            rptWBServiceProvider rpt = new rptWBServiceProvider();
            Guid warehouseId = new Guid(cboWarehouse.SelectedValue);
            int wbServiceProviderId = cboWBServiceProvider.SelectedIndex == 0 ? -2 : int.Parse(cboWBServiceProvider.SelectedValue);
            DateTime startDate = string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Parse("1/1/2010") : DateTime.Parse(txtStartDate.Text);
            DateTime endDate = string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now : DateTime.Parse(txtEndDate.Text);
            int serviceType = int.Parse(cboServiceType.SelectedValue);
            DataTable dt = objGrn.GetWBServiceProviderReport(warehouseId, wbServiceProviderId, startDate, endDate, serviceType);
            decimal sumNumberOfBags, sumNetWeight;

            int rowsCount = dt.Rows.Count;

            string NoOfRegords = "";
            if (rowsCount == 0)
            {
                Messages1.SetMessage("No record found with specified criteria.", WarehouseApplication.Messages.MessageType.Warning);
                WebViewer1.ClearCachedReport();
                return;
            }
            else
            {
                sumNumberOfBags = decimal.Parse(dt.Compute("Sum(NumberOfBags)", "").ToString());
                sumNetWeight = decimal.Parse(dt.Compute("Sum(NetWeight)", "").ToString());
                NoOfRegords = rowsCount == 1 ? "1 record found." : rowsCount + " records found.";
                Messages1.SetMessage(NoOfRegords, WarehouseApplication.Messages.MessageType.Success);
                rpt.SumNumberOfBags = sumNumberOfBags;
                rpt.SumNetWeight = sumNetWeight;
            }
            rpt.Warehouse = cboWarehouse.SelectedItem.Text;
            rpt.WBServiceProvider = cboWBServiceProvider.SelectedIndex == 0 ? "All" : cboWBServiceProvider.SelectedItem.Text;
            rpt.DateFrom = string.IsNullOrEmpty(txtStartDate.Text) ? "No Limit" : txtStartDate.Text;
            rpt.DateTo = string.IsNullOrEmpty(txtEndDate.Text) ? "Present Date" : txtEndDate.Text;
            rpt.DataSource = dt;

            rpt.Run();
            if (this.xlsExport1 == null)
            {
                this.xlsExport1 = new DataDynamics.ActiveReports.Export.Xls.XlsExport();
            }
            // this.xlsExport1.MinColumnWidth = (float)(0.5);
            this.xlsExport1.Export(rpt.Document, m_stream);
            m_stream.Position = 0;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=MyExport.xls");
            Response.BinaryWrite(m_stream.ToArray());
            Response.End();
        }
    }
}