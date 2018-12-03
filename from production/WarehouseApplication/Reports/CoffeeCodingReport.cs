using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using WarehouseApplication.BLL;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.DAL;


namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for CoffeeCodingReport.
    /// </summary>
    public partial class CoffeeCodingReport : DataDynamics.ActiveReports.ActiveReport
    {
        private SqlDataReader reader;
        SqlConnection conn = null;
        public CoffeeCodingReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void CoffeeCodingReport_DataInitialize(object sender, EventArgs e)
        {

            Fields.Add("Grader");
            Fields.Add("IsSuper");
        }

        private void detail_Format(object sender, EventArgs e)
        {
            
        }
        
        
        
        private void CoffeeCodingReport_ReportStart(object sender, EventArgs e)
        {
            Guid CommodityDepositeId = Guid.Empty;
            Guid WoredaId = Guid.Empty;
            GradingBLL objCode = new GradingBLL();
            if (HttpContext.Current.Session["CodeReport"] != null)
            {
                objCode = (GradingBLL)HttpContext.Current.Session["CodeReport"];
            }
            else
            {
                throw new Exception("Session expired.");
            }
            if (objCode != null)
            {
                this.txtDateCoded.Text = objCode.DateCoded.ToString("dd MMM-yyyy");
                this.txtCode.Text = objCode.GradingCode.ToString();
                CommodityDepositeId = objCode.CommodityRecivingId;
            }
            if (CommodityDepositeId != Guid.Empty)
            {
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailById(CommodityDepositeId);
                if (objCDR != null)
                {
                    WoredaId = objCDR.WoredaId;
                    WoredaBLL objWoreda = new WoredaBLL();
                    objWoreda = objWoreda.GetWoredabyId(WoredaId);
                    if (objWoreda != null)
                    {
                        this.txtOrigin.Text = objWoreda.WoredaName;
                    }

                }
            }
            this.txtDateGenerated.Text = DateTime.Now.ToString("dd MMM-yyyy");


            conn = Connection.getConnection();
            GradingByBLL objGradersReader = new GradingByBLL();
            reader = objGradersReader.GetGradersByGradingIdDataReader(objCode.Id,conn);
        }

        private void CoffeeCodingReport_FetchData(object sender, FetchEventArgs eArgs)
        {
            string t = "";
            try
            {
                reader.Read();
                if (reader["UserId"] != DBNull.Value)
                {
                    t = UserRightBLL.GetUserNameByUserId(new Guid(reader["UserId"].ToString()));
                    Fields["Grader"].Value = UserRightBLL.GetUserNameByUserId(new Guid(reader["UserId"].ToString()));
                }
                if (reader["isSupervisor"] != DBNull.Value)
                {
                    Fields["IsSuper"].Value = reader["isSupervisor"].ToString();
                }
                eArgs.EOF = false;
            }
            catch( Exception ex )
            {
                eArgs.EOF = true;

            }
        }

        private void CoffeeCodingReport_ReportEnd(object sender, EventArgs e)
        {
            try
            {
                
                reader.Close();
                reader.Dispose();
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            catch
            {
            }

        }
    }
}
