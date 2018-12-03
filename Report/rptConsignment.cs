using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for testRpt.
    /// </summary>
    public partial class rptConsignment : DataDynamics.ActiveReports.ActiveReport
    {

        public rptConsignment()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }
        public rptConsignment(DataTable rpt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.DataSource = rpt;
        }
        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {

        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.lblDate.Text = DateTime.Now.ToString();
        }
    }
}
