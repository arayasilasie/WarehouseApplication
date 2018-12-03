using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Web;
using WarehouseApplication.BLL;
using System.Data;
namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptInvTransferLICResign.
    /// </summary>
    public partial class rptInvTransferLICResign : DataDynamics.ActiveReports.ActiveReport
    {

        public rptInvTransferLICResign()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {

            rptSubInvTransferDetail rpt = new rptSubInvTransferDetail();
            DataTable dt = InventoryTransferModel.GetInvTransferDetail((new Guid(HttpContext.Current.Session["ID"].ToString())));
            rpt.DataSource = dt;
            subReport1.Report = rpt;
        }
    }
}
