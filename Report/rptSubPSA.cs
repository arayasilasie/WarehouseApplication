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
    public partial class rptSubPSA : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSubPSA()
        {
            
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
        }

        private void rptSubPSA_ReporStart(object sender, EventArgs e)
        {
            this.label13.Text = this.Parameters["ColumnName"].Value;
           
        }

        private void rptSubPSA_ReportEnd(object sender, EventArgs e)
        {
            
        }

        private void rptSubPSA_PageEnd(object sender, EventArgs e)
        {
            //if (this.Parameters["ColumnName"].Value != "NetWeight")
            //{
            //    DataDynamics.ActiveReports.Label l = (DataDynamics.ActiveReports.Label)this.ParentReport.Sections["pageHeader"].Controls["label1"];
            //    l.Text="PSA Issue Note For OverDelivery";               
            //}
        }

    }
}
