using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptGINApproval.
    /// </summary>
    public partial class rptGINApproval : DataDynamics.ActiveReports.ActiveReport
    {

        public rptGINApproval()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //lblGINPreparedV.Text= BLL.UserBLL.CurrentUser.UserName;


        }
    }
}
