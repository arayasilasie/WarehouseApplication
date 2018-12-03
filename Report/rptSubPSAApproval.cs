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
    /// Summary description for rptSubPSAApproval.
    /// </summary>
    public partial class rptSubPSAApproval : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSubPSAApproval()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void rptSubPSAApproval_ReportStart(object sender, EventArgs e)
        {
            this.label13.Text = this.Parameters["ColumnName"].Value;
        }
    }
}
