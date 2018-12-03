using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGRNService.
    /// </summary>
    public partial class rptGRNService : DataDynamics.ActiveReports.ActiveReport
    {
        private SqlDataReader reader;
        public Guid GradingIdSubReport;
        public rptGRNService(Guid GradingId)
        {
            //
            // Required for Windows Form Designer support
            //
            this.GradingIdSubReport = GradingId;
            InitializeComponent();
        }
        public rptGRNService()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void rptGRNService_ReportStart(object sender, EventArgs e)
        {
            GRNServiceBLL objGS = new GRNServiceBLL();
            this.reader = objGS.GetActiveByGRNId(this.GradingIdSubReport);
        }

        private void rptGRNService_DataInitialize(object sender, EventArgs e)
        {
            Fields.Add("ServiceName");
            Fields.Add("Qty");
        }

        private void rptGRNService_FetchData(object sender, FetchEventArgs eArgs)
        {
            try
            {
                reader.Read();
                if (reader["ServiceId"] != DBNull.Value)
                {
                    string Sn = "";
                    Sn = WarehouseServicesBLL.GetServiceNameById(new Guid( reader["ServiceId"].ToString()));
                    Fields["ServiceName"].Value = Sn;
                }
                if (reader["Quantity"] != DBNull.Value)
                {
                    Fields["Qty"].Value = reader["Quantity"].ToString();
                }
                eArgs.EOF = false;
            }
            catch 
            {
                eArgs.EOF = true;
            }
        }

        private void rptGRNService_ReportEnd(object sender, EventArgs e)
        {
            try
            {

                reader.Close();
                reader.Dispose();
            }
            catch
            {
            }
        }


    }
}
