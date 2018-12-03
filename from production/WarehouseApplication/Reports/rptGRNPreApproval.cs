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
    /// Summary description for rptGRNPreApproval.
    /// </summary>
    public partial class rptGRNPreApproval : DataDynamics.ActiveReports.ActiveReport
    {

        public rptGRNPreApproval()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {

        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }
    }
}
