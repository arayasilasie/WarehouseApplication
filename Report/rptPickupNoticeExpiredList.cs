using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using GINBussiness;
using WarehouseApplication.BLL;
//using WarehouseApplication.GINLogic;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptPUNReport.
    /// </summary>
    public partial class rptPickupNoticeExpiredList : DataDynamics.ActiveReports.ActiveReport
    {
        public rptPickupNoticeExpiredList()
        {

            InitializeComponent();
        }
        private void rptPUNReport_ReportStart(object sender, EventArgs e)
        {

        }
        private void detail_Format(object sender, EventArgs e)
        {
              if(txtStatusName.Text.Trim()=="Open")
            txtPUNPrintedDate.Text = string.Empty;
        }

      

        private void txtStatusName_Disposed(object sender, EventArgs e)
        {
         
        }
    }
}
