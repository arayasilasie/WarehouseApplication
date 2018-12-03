using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using DataDynamics.ActiveReports.Export.Pdf;
using System.Data;
using System.Web;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptSubInvTransferDetail.
    /// </summary>
    public partial class rptSubInvTransferDetail : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSubInvTransferDetail()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
        }

       
    }
}
