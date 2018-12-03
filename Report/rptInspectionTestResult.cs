using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using GradingBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptInspectionTestResult.
    /// </summary>
    public partial class rptInspectionTestResult : DataDynamics.ActiveReports.ActiveReport
    {

        public rptInspectionTestResult()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            GradingModel objPUN = new GradingModel();
            rptInspectionTestResultDetail rp = new rptInspectionTestResultDetail();
            rp.DataSource = GradingModel.GetInspectionTestResultDetail(txtGradingCode.Text);
            subReport1.Report = rp;
        }
    }
}
