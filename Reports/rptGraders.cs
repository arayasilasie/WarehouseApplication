using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using WarehouseApplication.BLL;
using System.Web;
using System.Web.Security;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGraders.
    /// </summary>
    public partial class rptGraders : DataDynamics.ActiveReports.ActiveReport
    {

        public rptGraders()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
             GradingByCollection obj = new GradingByCollection();
            if (HttpContext.Current.Session["GradersCodeReport"] != null)
            {
                obj = (GradingByCollection)HttpContext.Current.Session["GradersCodeReport"];
               
            }
        }

        private void rptGraders_DataInitialize(object sender, EventArgs e)
        {
          
        }

        private void rptGraders_ReportEnd(object sender, EventArgs e)
        {
      
        }
    }
}
