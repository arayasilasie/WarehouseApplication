using System;
using System.Data;
using WarehouseApplication.BLL;
using System.Data;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGRNnew.
    /// </summary>
    public partial class rptGRNnew : DataDynamics.ActiveReports.ActiveReport
    {
        public static int count = 0;
        public static int rows = 0;
        public rptGRNnew()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //count++;
            rptGradingNew rpt = new rptGradingNew();
            GRN_BL objGRN = new GRN_BL();  
            DataTable dt=objGRN.GetGradingsWithSameGRNReport(lblGRN_No.Text);
            rows = dt.Rows.Count;
            rpt.DataSource = dt;
            subReport1.Report = rpt;
        }

    }
}
