using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Web;
using System.Web.Security;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptSampleTicketCoffee.
    /// </summary>
    public partial class rptSampleTicketCoffeeNew : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSampleTicketCoffeeNew()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void rptSampleTicketCoffee_ReportStart(object sender, EventArgs e)
        {
           txtDateGenerated.Text = txtDateGenerated2.Text = DateTime.Now.ToShortDateString();
        }
    }
}
