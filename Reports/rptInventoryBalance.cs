using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptInventoryBalance.
    /// </summary>
    public partial class rptInventoryBalance : DataDynamics.ActiveReports.ActiveReport
    {

        public rptInventoryBalance()
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
