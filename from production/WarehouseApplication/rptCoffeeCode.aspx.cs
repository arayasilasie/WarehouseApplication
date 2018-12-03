﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;
using WarehouseApplication.Reports;
using WarehouseApplication.BLL;
using System.ComponentModel;

namespace WarehouseApplication
{
    public partial class rptCoffeeCode : ECXWarehousePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GradingByCollection objCollection = (GradingByCollection)Session["GradersCodeReport"];
            ActiveReport rt = new WarehouseApplication.Reports.CoffeeCodingReport();
          
            rt.Run(false);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline; filename=SampleTicket.PDF");

            rt.DataSource = objCollection;

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