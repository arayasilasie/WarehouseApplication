using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;
using System.Web;
using WarehouseApplication.BLL;
using System.Data;
using WarehouseApplication.Reports;
namespace WarehouseApplication
{
    public partial class ReportGRNPreApproval : System.Web.UI.Page
    {
        static Guid CurrentWarehouse;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            CurrentWarehouse = new Guid(Session["CurrentWarehouse"].ToString());
            BindLIC(1);
            
            //ActiveReport rt = new WarehouseApplication.Reports.rptGRNPreApproval();
            //rt.DataSource = GRNApprovalModel.GetGRNsForPreApproval((new Guid(HttpContext.Current.Session["CurrentWarehouse"].ToString())));

            //rt.Run(false);
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");

            //// Create the PDF export object
            //PdfExport pdf = new PdfExport();
            //// Create a new memory stream that will hold the pdf output
            //System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            //// Export the report to PDF:
            //pdf.Export(rt.Document, memStream);
            //// Write the PDF stream out
            //Response.BinaryWrite(memStream.ToArray());
            //// Send all buffered content to the client
            //Response.End();
        }


        public void BindLIC(int status)
        {
            //ddlLIC.Items.Clear();
            //ddlLIC.Items.Add(new ListItem("Select LIC", ""));
            ddlLIC.DataSource = GRNApprovalModel.GetLICsForGRNApproval(CurrentWarehouse, status);
            ddlLIC.DataTextField = "Name";
            ddlLIC.DataValueField = "ID";
            ddlLIC.DataBind();
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            rptGRNPreApproval rpt = new rptGRNPreApproval();
            DataTable dtbl = GRNApprovalModel.GetGRNsForPreApproval(CurrentWarehouse, new Guid(ddlLIC.SelectedValue));

            rpt.DataSource = dtbl;
            WebViewer1.Report = rpt;
        }
    }
}