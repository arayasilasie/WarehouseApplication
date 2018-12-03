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
    /// Summary description for rptSubGINStackBalance.
    /// </summary>
    public partial class rptSubGINStackBalance : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSubGINStackBalance()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {

        }
    }
}
