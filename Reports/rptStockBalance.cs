using System;
using System.Web;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptStockBalance.
    /// </summary>
    public partial class rptStockBalance : DataDynamics.ActiveReports.ActiveReport
    {

        public object SumNumberOfBags { set; get; }
        public object SumNumberOfRebagging { set; get; }
        public object SumNumberOfNetWeight { set; get; }

        public object Warehouse { set; get; }
        public object Shed { set; get; }
        public object LIC { set; get; }
        public object DateFrom { set; get; }
        public object DateTo { set; get; }

        public object dtbl { set; get; }
        public object BagSum { set; get; }
        public object RebaggingSum { set; get; }
        public object NetWeightSum { set; get; }
        public object AdjustmentBagSum { set; get; }
        public object AdjustmentWeightSum { set; get; }

        public rptStockBalance()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
            
        }

        private void detail_Format(object sender, EventArgs e)
        {
            rptSubStockBalance rptGIN = new rptSubStockBalance();
            rptGIN.DataSource = dtbl;
            subReport1.Report = rptGIN;

            txtSumBagGIN.Text = BagSum.ToString();
            txtSumReBagGIN.Text = RebaggingSum.ToString();
            txtSumWgtGIN.Text = NetWeightSum.ToString();
            txtAdjustmentBags.Text = AdjustmentBagSum.ToString();
            txtAdjustmentWgt.Text =AdjustmentWeightSum.ToString();
            if (SumNumberOfBags.ToString() != "" && BagSum.ToString() != "")
            {
               
                lblBagBalance.Text = (int.Parse(SumNumberOfBags.ToString()) - int.Parse(BagSum.ToString())).ToString();
                lblWeightBalance.Text = (float.Parse(SumNumberOfNetWeight.ToString()) - float.Parse(NetWeightSum.ToString())).ToString();
            }
            else if (SumNumberOfBags.ToString() != "" && BagSum.ToString() == "")
            {
                lblBagBalance.Text = SumNumberOfBags.ToString();
                lblWeightBalance.Text = SumNumberOfNetWeight.ToString();
            }
            else  
            {
                lblBagBalance.Text = BagSum.ToString();
                lblWeightBalance.Text = NetWeightSum.ToString();
            }

        }
        //int x = 0;
        //decimal  sum = 0;
        private void rptStockBalance_FetchData(object sender, FetchEventArgs eArgs)
        {
            //rptStockBalance rpt = new rptStockBalance();
            //string controlValue = ((Label)rpt.Sections["detail"].Controls["lblNumberOfBags"]).Text;
            //if (!string.IsNullOrEmpty(controlValue))
            //    sum = sum + decimal.Parse(controlValue);
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            lblWarehouse.Text = Warehouse.ToString();
            lblShed.Text = Shed.ToString();
            lblLIC.Text = LIC.ToString();
            lblFromDate.Text = DateFrom.ToString();
            lblToDate.Text = DateTo.ToString();
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            txtSumNoOfBags.Text = SumNumberOfBags.ToString();
            txtSumReBagging.Text = SumNumberOfRebagging.ToString();
            txtSumNetWeight.Text = SumNumberOfNetWeight.ToString();
        }
    }
}
