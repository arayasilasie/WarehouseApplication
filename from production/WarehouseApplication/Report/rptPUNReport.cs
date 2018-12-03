using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using GINBussiness;
using System.Data;
//using WarehouseApplication.GINLogic;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptPUNReport.
    /// </summary>
    public partial class rptPUNReport : DataDynamics.ActiveReports.ActiveReport
    {
        public bool flag=false;
        public rptPUNReport()
        {
          
            InitializeComponent();
        }

      
        private void rptPUNReport_ReportStart(object sender, EventArgs e)
        {
         
        }

        private void detail_Format(object sender, EventArgs e)
        {
            rptSubPUNWRReport rp = new rptSubPUNWRReport();
            DataTable dt = new DataTable();
            dt=PickupNoticeModel.PrintSubReportPUN(new Guid(textBox4.Text));
            if (dt.Rows.Count > 0)
            {
                flag = false;
                txtRemainingWeight.Visible = true;
                txtRemainingWeight1.Visible = false;
            }
            else
            {
                txtRemainingWeight.Visible = false;
                txtRemainingWeight1.Visible = true;
                txtRemainingWeight1.Text = txtWeightInKg1.Text;
               
            }
            rp.DataSource = dt;
            subWarehouseReceiptList.Report = rp;
        }

        private void txtRemainingWeight_Disposed(object sender, EventArgs e)
        {
         
        }
    }
}
