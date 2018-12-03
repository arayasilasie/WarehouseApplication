using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.BLL;
using WarehouseApplication.DAL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGrading.
    /// </summary>
    public partial class rptGrading : DataDynamics.ActiveReports.ActiveReport
    {
        private SqlDataReader reader;
        public Guid GradingIdSubReport;
        SqlConnection conn = Connection.getConnection();
        public rptGrading(Guid GradingId)
        {
            //
            // Required for Windows Form Designer support
            //
            this.GradingIdSubReport = GradingId;
            InitializeComponent();
        }

        private void rptGrading_ReportStart(object sender, EventArgs e)
        {
            try
            {
                GradingResultDetailBLL objGradingResultDetail = new GradingResultDetailBLL();
                reader = objGradingResultDetail.GetGradingResultDetailByGradingIdDataReader(this.GradingIdSubReport, conn);
            }
            catch (Exception ex)
            {
                if (this.conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                throw ex;
            }
        }

        private void rptGrading_DataInitialize(object sender, EventArgs e)
        {
            Fields.Add("GradingFactorName");
            Fields.Add("ReceivedValue");
        }

        private void rptGrading_FetchData(object sender, FetchEventArgs eArgs)
        {
            try
            {
                reader.Read();
                if (reader["GradingFactorName"] != DBNull.Value)
                {
                    Fields["GradingFactorName"].Value = reader["GradingFactorName"].ToString();
                }
                if (reader["ReceivedValue"] != DBNull.Value)
                {
                    Fields["ReceivedValue"].Value = reader["ReceivedValue"].ToString();
                }
                eArgs.EOF = false;
            }
            catch
            {
                eArgs.EOF = true;
            }
        }

        private void rptGrading_ReportEnd(object sender, EventArgs e)
        {
            try
            {

                reader.Close();
                reader.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch
            {

            }
        }

    }
}
