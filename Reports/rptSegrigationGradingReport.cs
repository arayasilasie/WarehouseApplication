using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptSubGIN.
    /// </summary>
    public partial class rptSegrigationGradingReport : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSegrigationGradingReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            txtDateGenerated.Text = DateTime.Today.ToShortDateString();
        }
    }
}
