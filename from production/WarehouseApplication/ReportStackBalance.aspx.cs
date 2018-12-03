using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;
using WarehouseApplication.BLL;
using DataDynamics.ActiveReports;
using WarehouseApplication.Report;
using System.Data;

namespace WarehouseApplication
{
    public partial class ReportStackBalance : System.Web.UI.Page
    {
        private DataDynamics.ActiveReports.Export.Xls.XlsExport xlsExport1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FillLIC(drpInventoryCoordinatorLoad, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator        
            drpInventoryCoordinatorLoad.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ActiveReport rpt = null;
            rpt = new Report.rptStackBalance();
            GRN_BL objSGRN = new GRN_BL();
            DataTable dt = new DataTable();
            dt = objSGRN.GetGRNStackBalanceReport(UserBLL.GetCurrentWarehouse(), new Guid(drpShed.SelectedValue), new Guid(drpInventoryCoordinatorLoad.SelectedValue), new Guid(drpStackNo.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                rpt.DataSource = dt;
                rpt.PageSettings.Margins.Top = 0;
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
                // rpt.PageSettings.Orientation=DataDynamics.ActiveReports.Document.Align

                WebViewer1.Report = rpt;
            }
            //Session["ReportType"] = "StackBalance";
            //ScriptManager.RegisterStartupScript(this,
            //                                              this.GetType(),
            //                                              "ShowReport",
            //                                              "<script type=\"text/javascript\">" +
            //                                              string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

            //                                              "</script>",
            //                                              false);
        }
        private void FillLIC(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICAll(UserBLL.GetCurrentWarehouse());
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        private void PopulateStack(Guid LIC)
        {
            List<WarehouseOperator> list = null;
            list = WarehouseOperator.GetLICByShed(UserBLL.GetCurrentWarehouse(), new Guid(drpShed.SelectedValue));
            if (list != null)
            {
                drpStackNo.DataSource = list;
                drpStackNo.DataTextField = "StackNumber";
                drpStackNo.DataValueField = "ID";
                drpStackNo.DataBind();
            }
        }
        private void PopulateShed()
        {
            drpShed.DataSource = null;
            drpShed.DataSource = WarehouseOperator.ShedByLic(UserBLL.GetCurrentWarehouse(), new Guid(drpInventoryCoordinatorLoad.SelectedValue));
            drpShed.DataTextField = "ShedNumber";
            drpShed.DataValueField = "ShedID";
            drpShed.DataBind();
            drpShed.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void drpShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
            drpStackNo.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void drpInventoryCoordinatorLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateShed();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            System.IO.MemoryStream m_stream = new System.IO.MemoryStream();
            rptStackBalance rpt = new rptStackBalance();



            GRN_BL objSGRN = new GRN_BL();
            rpt.DataSource = objSGRN.GetGRNStackBalanceReport(UserBLL.GetCurrentWarehouse(), new Guid(drpShed.SelectedValue), new Guid(drpInventoryCoordinatorLoad.SelectedValue), new Guid(drpStackNo.SelectedValue.ToString()));
            rpt.PageSettings.Margins.Top = 0;
            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

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
        private void InitializeComponent()
        {
            this.xlsExport1 = new DataDynamics.ActiveReports.Export.Xls.XlsExport();
            // 
            // xlsExport1
            // 
            this.xlsExport1.Tweak = 0;

        }
    }
}