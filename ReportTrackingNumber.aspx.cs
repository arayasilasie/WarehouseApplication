using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;

namespace WarehouseApplication
{
    public partial class ReportTrackingNumber : ECXWarehousePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActiveReport rt = new WarehouseApplication.rptTrackingReport();
            rt.Run(false);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");

            // Create the PDF export object
            PdfExport pdf = new PdfExport();
            // Create a new memory stream that will hold the pdf output
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            // Export the report to PDF:
            pdf.Export(rt.Document, memStream);
            // Write the PDF stream out
            Response.BinaryWrite(memStream.ToArray());
            // Send all buffered content to the client
            Response.End();

        }
    }
}
