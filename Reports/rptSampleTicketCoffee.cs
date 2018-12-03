using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Web;
using System.Web.Security;
using WarehouseApplication.BLL;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptSampleTicketCoffee.
    /// </summary>
    public partial class rptSampleTicketCoffee : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSampleTicketCoffee()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void rptSampleTicketCoffee_DataInitialize(object sender, EventArgs e)
        {
            this.txtDateGenerated.Text = DateTime.Today.ToString("dd MMM-yyyy");
  
            //Load Sample ticket with data.
            if (HttpContext.Current.Session["Sample"] != null)
            {
                SamplingBLL objSampling = new SamplingBLL();
                objSampling = (SamplingBLL)HttpContext.Current.Session["Sample"];
                objSampling = objSampling.GetSampleDetail(objSampling.Id);
                this.txtDateSampled.Text = objSampling.GeneratedTimeStamp.ToString("dd MMM-yyyy");
                this.txtCode.Text = objSampling.SampleCode.ToString();
                EmployeeAttendanceBLL objEmployee = new EmployeeAttendanceBLL();


                this.txtSamplerName.Text = UserRightBLL.GetUserNameByUserId(objSampling._sampler.SamplerId);
                VoucherInformationBLL objVoucher = new VoucherInformationBLL();
                objVoucher = objVoucher.GetVoucherInformationByCommodityDepositRequestId(objSampling.ReceivigRequestId);
                if (objVoucher != null)
                {
                    this.txtNoBags.Text = objVoucher.NumberofBags.ToString();
                    this.txtPlompTruck.Text = objVoucher.NumberOfPlomps.ToString();
                    this.txtPlompTrailer.Text = objVoucher.NumberOfPlompsTrailer.ToString();
                }
                //Get Driver information.
                List<DriverInformationBLL> list = new List<DriverInformationBLL>();
                DriverInformationBLL obj = new DriverInformationBLL();
                list = obj.GetActiveDriverInformationByReceivigRequestId(objSampling.ReceivigRequestId);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        obj = list[0];
                        this.txtPlateNo.Text = obj.PlateNumber.ToString();
                        this.txtTrailerPlateNo.Text = obj.TrailerPlateNumber.ToString();
                    }
                }
            }
            HttpContext.Current.Session["Sample"] = null;
            HttpContext.Current.Session["Sampler"] = null;
        }

        private void rptSampleTicketCoffee_ReportStart(object sender, EventArgs e)
        {

        }
    }
}
