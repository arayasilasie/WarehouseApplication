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
    public partial class rptPSAReport : DataDynamics.ActiveReports.ActiveReport
    {
        public rptPSAReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }
//-------Update Start ----- NOV 27 2013
        public rptPSAReport(string status)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            lblStatus.Text = status;
            lblStatus.Visible = (status == "Approved") ? false : true;
            lblStatus.Text = (status == "Approved") ? "" : "*";
        }




        private void detail_Format(object sender, EventArgs e)
        {
            //if Unapproved
            if (lblStatus.Text == "*")
            {
                rptSubPSA rp = new rptSubPSA();
                rp.DataSource = GINBussiness.GINModel.GetRemainingPAS(new Guid(textBox5.Text));
                subReportPSA.Report = rp;
            }
            //if Approved
            else
            {
                label3.Text = "Operation Controller:";
                rptSubPSAApproval rp = new rptSubPSAApproval();
                rp.DataSource = GINBussiness.GINModel.GetRemainingPASApproval(new Guid(textBox5.Text));
                subReportPSA.Report = rp;
                //lblInventeryInspector.DataField = "InspectorName";
            }

        }
//-------Update End   ----- Nov 27 2013
        private void groupHeader1_Format(object sender, EventArgs e)
        {
            //GINModel objPUN = new GINModel();
            //rptSubGIN rp = new rptSubGIN();
            //rp.DataSource = GINBussiness.GINModel.GetRemainingGin(new Guid(textBox5.Text));
            //subReportGin.Report = rp;
        }
    }
}
