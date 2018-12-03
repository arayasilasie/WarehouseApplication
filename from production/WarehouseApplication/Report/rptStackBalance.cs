using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using WarehouseApplication.BLL;
using GINBussiness;
using System.Data;
namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptStackBalance.
    /// </summary>
    public partial class rptStackBalance : DataDynamics.ActiveReports.ActiveReport
    {
        public double GrnTotal;
        public double GinTotal;
        public double GRNBalanceBag;
        public double GINBalanceBag;
        public rptStackBalance()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {         
 
            GINModel objSGIN=new GINModel();          
            rptSubGINStackBalance rpGIN = new rptSubGINStackBalance();                     
            rpGIN.DataSource = objSGIN.GetGINStackBalance(new Guid(label11.Text),new Guid(label10.Text),new Guid(label9.Text),new Guid(label8.Text));         
            subReport2.Report = rpGIN;
           
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            GINModel objSGIN = new GINModel();
            DataTable dt = objSGIN.GetTotalGINStackById(new Guid(label8.Text));
            if (dt.Rows.Count > 0)
            {
                txtTotalGinWeight.Text = dt.Rows[0]["NetWeight"].ToString();
                txtTotalGINBag.Text = dt.Rows[0]["NoOfBug"].ToString();
                GinTotal =Convert.ToDouble( txtTotalGinWeight.Text);
                GINBalanceBag = Convert.ToDouble(txtTotalGINBag.Text);
            }
            double total = Math.Round((GrnTotal - GinTotal),2);
            double totalBag = Math.Round((GRNBalanceBag - GINBalanceBag), 2);
            txtBalanceBag.Text = totalBag.ToString();
            txtGrandTotalWeight.Text = total.ToString();
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            GRN_BL objSGRN = new GRN_BL();
             DataTable dt =objSGRN.GetTotalGRNStackById(new Guid(label8.Text));
             if (dt.Rows.Count > 0)
             {
                 txtTotalGRNWeight.Text = dt.Rows[0]["NetWeight"].ToString();
                 txtTotalGRNBag.Text = dt.Rows[0]["NoOfBug"].ToString();
                 GrnTotal = Convert.ToDouble(txtTotalGRNWeight.Text);
                 GRNBalanceBag = Convert.ToDouble(txtTotalGRNBag.Text);
             }
          
                
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            label22.Text = DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString();
        }
    }
}
