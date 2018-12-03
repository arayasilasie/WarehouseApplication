using System;
using WarehouseApplication.BLL;
using System.Data;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGradingNew.
    /// </summary>
    public partial class rptGradingNew : DataDynamics.ActiveReports.ActiveReport
    {
        public rptGradingNew()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            rptGRNnew.count++;
            rptGradingFactorsResultReport rpt = new rptGradingFactorsResultReport();
            GRN_BL objGRN = new GRN_BL();
            DataTable dt=objGRN.GetGradingResultFactorReport(lblGradingCode.Text);
            rpt.DataSource = dt;
            subReport1.Report = rpt;
            if (rptGRNnew.count == rptGRNnew.rows)
            {
                pageBreak1.Enabled = false;
                rptGRNnew.count = 0;
            }
          
        }
    }
}
