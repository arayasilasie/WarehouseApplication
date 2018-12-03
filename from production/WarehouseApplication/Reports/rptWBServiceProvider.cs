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
    /// Summary description for rptWBServiceProvider.
    /// </summary>
    public partial class rptWBServiceProvider : DataDynamics.ActiveReports.ActiveReport
    {
        public object Warehouse { set; get; }
        public object WBServiceProvider { set; get; }
        public object DateFrom { set; get; }
        public object DateTo { set; get; }

        public decimal SumNumberOfBags { get; set; }
        public decimal SumNetWeight { get; set; }

        public rptWBServiceProvider()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            lblWarehouse.Text = Warehouse.ToString();
            lblWBServiceProvider.Text = WBServiceProvider.ToString();
            lblFromDate.Text = DateFrom.ToString();
            lblToDate.Text=DateTo.ToString();
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            txtSumNoOfBags.Text = SumNumberOfBags.ToString();
            txtSumNetWeight.Text = SumNetWeight.ToString();
        }
    }
}
