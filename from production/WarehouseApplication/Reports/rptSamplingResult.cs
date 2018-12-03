using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using WarehouseApplication.BLL;
using System.Web;
using System.Web.Security;

namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for rptSamplingResult.
    /// </summary>
    public partial class rptSamplingResult : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSamplingResult()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void rptSamplingResult_DataInitialize(object sender, EventArgs e)
        {
            SamplingResultBLL objSamplingResult = null;
            if (HttpContext.Current.Session["SamplingResultCode"] != null)
            {
                string Code = HttpContext.Current.Session["SamplingResultCode"].ToString();
                objSamplingResult = new SamplingResultBLL();
                objSamplingResult = objSamplingResult.GetSamplingResultBySamplingResultCode(Code);
            }
            else
            {
                throw new Exception("Session expired");
            }
            if (objSamplingResult != null)
            {
                this.txtCode.Text = objSamplingResult.SamplingResultCode;
                this.txtNoBags.Text = objSamplingResult.NumberOfBags.ToString();
                SamplingBLL objSampling = new SamplingBLL();
                objSampling = objSampling.GetSampleDetail(objSamplingResult.SamplingId);
                this.txtDateSampled.Text = objSampling.GeneratedTimeStamp.ToString("dd MMM-yyyy");
                this.txtDateGenerated.Text = DateTime.Now.ToString("dd MMM-yyyy");
                DriverInformationBLL objDriver = new DriverInformationBLL();
                List<DriverInformationBLL> listDriver = objDriver.GetActiveDriverInformationByReceivigRequestId(objSampling.ReceivigRequestId);
                if (listDriver != null)
                {
                    this.txtPlateNo.Text = listDriver[0].TrailerPlateNumber.ToString();
                    this.txtTrailerPlateNo.Text = listDriver[0].TrailerPlateNumber.ToString();
                }
            }
            this.txtDateGenerated.Text = DateTime.Now.ToString("dd MMM-yyyy");
        }
    }
}
