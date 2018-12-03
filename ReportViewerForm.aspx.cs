using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;
using WarehouseApplication.Reports;

namespace WarehouseApplication
{
    public partial class ReportViewerForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActiveReport report = ReportFactory.GetReport();
            PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
            string requestedReport = (string)(transferedData.GetTransferedData("RequestedReport"));
            report.Run(false);
//            Response.AddHeader("Cache-Control", "no-cache");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("inline; filename={0}.PDF", requestedReport));

            // Create the PDF export object
            PdfExport pdf = new PdfExport();
            // Create a new memory stream that will hold the pdf output
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            // Export the report to PDF:
            pdf.Export(report.Document, memStream);
            // Write the PDF stream out
            Response.BinaryWrite(memStream.ToArray());
            // Send all buffered content to the client
            Response.End();

            transferedData.RemoveAllData();
        }
    }

    public class ReportFactory
    {
        public static ActiveReport GetReport()
        {
            ActiveReport report = null;
            PageDataTransfer transferedData =new PageDataTransfer(HttpContext.Current.Request.Path);
            string requestedReport = (string)(transferedData.GetTransferedData("RequestedReport"));
            IGINProcess ginProcess = null;
            switch (requestedReport)
            {
                case "rptGINReport":
                    ginProcess = GINProcessWrapper.GetGINProcess(false);
                    ILookupSource lookupSource = ginProcess.LookupSource;
                    report = new Reports.rptGINReport();
                    report.DataSource = new GINReportDataCollection(ginProcess.GetGINReport(ginProcess.GINProcessInformation.Trucks[0].TruckId), lookupSource);
                    return report;
                case "rptPUNTrackingReport":
                    ginProcess = GINProcessWrapper.GetGINProcess(false);
                    report = new Reports.rptPUNTrackingReport();
                    report.DataSource = new TrackingReportDataCollection(ginProcess.PUNTrackingReportData);
                    return report;
                case "rptGINTrackingReport":
                    report = new Reports.rptGINTrackingReport();
                    GINTrackingReportDataCollection gtrDataSource = new GINTrackingReportDataCollection();
                    gtrDataSource.AddList((List<GINTrackingReportData>)transferedData.GetTransferedData("GINTrackingReportData"));
                    report.DataSource = gtrDataSource;
                    return report;
                case "rptPUNReport":
                    report = new Reports.rptPUNReport();
                    PUNReportDataCollection puDataSource = new PUNReportDataCollection((PUNReportData)transferedData.GetTransferedData("PUNReportData"));
                    report.DataSource = puDataSource;
                    return report;
            }
            return null;
        }
    }
}
