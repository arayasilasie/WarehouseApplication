using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using WarehouseApplication.Report;
using GINBussiness;


namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGINReport.
    /// </summary>
    public partial class rptGINReport : DataDynamics.ActiveReports.ActiveReport
    {
        public rptGINReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            GINModel objPUN = new GINModel();
            rptSubGIN rp = new rptSubGIN();
            rp.DataSource = GINBussiness.GINModel.GetRemainingGin(new Guid(textBox5.Text),Convert.ToBoolean(txtisEdit.Text));
            subReportGin.Report = rp;

           
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            //GINModel objPUN = new GINModel();
            //rptSubGIN rp = new rptSubGIN();
            //rp.DataSource = GINBussiness.GINModel.GetRemainingGin(new Guid(textBox5.Text));
            //subReportGin.Report = rp;
        }
    }
}
