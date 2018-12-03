using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for ResultReport.
    /// </summary>
    public partial class rptResultReport : DataDynamics.ActiveReports.ActiveReport
    {

        public rptResultReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
           
        }

        private void rptResultReport_PageStart(object sender, EventArgs e)
        {
            //txtCurentUser.Text = DateTime.Now.ToString("dd MMM-yyyy");
        }

     
    }
}
