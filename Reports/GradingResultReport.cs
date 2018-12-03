using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Web;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for GradingResultReport.
    /// </summary>
    public partial class GradingResultReport : DataDynamics.ActiveReports.ActiveReport
    {

        public GradingResultReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void GradingResultReport_ReportStart(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["rptGradingResultId"] == null)
            {
                throw new Exception("Session expired");
            }
            else
            {
                Guid Id = Guid.Empty ;
                Id = new Guid(HttpContext.Current.Session["rptGradingResultId"].ToString());
                GradingResultBLL obj = new GradingResultBLL();
                obj = obj.GetGradingResultById(Id);
                if (obj == null)
                {
                    throw new Exception("Object Null");
                }
                else
                {
                    txtTrackingNo.Text = obj.TrackingNo;
                    txtResultDate.Text = obj.GradeRecivedTimeStamp.ToString("dd MMM-yyyy");
                    txtCode.Text = obj.GradingCode.ToString();
                    if (obj.Status != GradingResultStatus.GeneralRequiementfail && obj.Status != GradingResultStatus.MoistureFailed)
                    {
                        txtGradeReceived.Text = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
                    }
                    else if(obj.Status  == GradingResultStatus.MoistureFailed )
                    {
                        txtGradeReceived.Text = "Moisture Failed";
                    }
                    else if(obj.Status == GradingResultStatus.GeneralRequiementfail)
                    {
                        txtGradeReceived.Text = "General Requiement Fail";
                    }
                }
            }

        }
    }
}
