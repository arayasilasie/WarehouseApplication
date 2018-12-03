using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using GINBussiness;

namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptSubGIN.
    /// </summary>
    public partial class rptSubGIN : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSubGIN()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {

        }

        private void rptSubGIN_ReportStart(object sender, EventArgs e)
        {

        }

        private void detail_Format(object sender, EventArgs e)
        {
            GINModel objPUN = new GINModel();
            rptSUBGINScale rp = new rptSUBGINScale();
            if (textBox6.Text != "textBox6")
                rp.DataSource = GINBussiness.GINModel.GetRemainingGinByScale(new Guid(textBox6.Text) );//Session["ConsType"].ToString()
            subReportGin.Report = rp;
        }
    }
}
