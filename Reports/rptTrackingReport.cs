using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using WarehouseApplication.BLL;
using System.Web;
using WarehouseApplication.Reports;
using DataDynamics.ActiveReports;

namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for rptTrackingReport.
    /// </summary>
    public partial class rptTrackingReport : DataDynamics.ActiveReports.ActiveReport
    {

        public rptTrackingReport()
        {
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            txtDateGenerated.Text = DateTime.Now.ToShortDateString();
            //    Guid ArrivalId;
            //    if (HttpContext.Current.Session["CommodityRequestId"] == null)
            //    {
            //        throw new Exception("Your Session has expired");
            //    }
            //    ArrivalId = new Guid(HttpContext.Current.Session["CommodityRequestId"].ToString());

            //    BLL.PreArrival objPreArrival = new BLL.PreArrival();
            //    DataTable dt = objPreArrival.PopulatePreArrival(ArrivalId);
            //    rptTrackingReport rpt = new rptTrackingReport();
            //    rpt.DataSource = dt;

            //    barcode1.DataField = dt.Columns[0].ToString();//dt.Rows[0]["TrackingNumber"].ToString();

            //    txtTrackingNumber.Text = dt.Rows[0]["TrackingNumber"].ToString();
            //    txtClinetId.Text = dt.Rows[0]["ClientId"].ToString();
            //    txtClientName.Text = dt.Rows[0]["ClientName"].ToString();
            //    txtVoucherNumber.Text = dt.Rows[0]["VoucherNumber"].ToString();
            //    txtTruckPlateNumber.Text = dt.Rows[0]["TruckPlateNumber"].ToString();
            //    txtTrailerPlateNumber.Text = dt.Rows[0]["TrailerPlateNumber"].ToString();
            //    txtDateReceived.Text = dt.Rows[0]["DateReceived"].ToString();
            //    txtWarehouse.Text = dt.Rows[0]["WarehouseName"].ToString();
            //    chkIsTruckInCompound.Checked = bool.Parse(dt.Rows[0]["IsTruckInCompound"].ToString());

            //    HttpContext.Current.Session["CommodityRequestId"] = null;//make session null to 
        }
    }
}
