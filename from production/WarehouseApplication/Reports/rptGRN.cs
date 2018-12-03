using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Web;
using WarehouseApplication.BLL;


namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGRN.
    /// </summary>
    public partial class rptGRN : DataDynamics.ActiveReports.ActiveReport
    {
        private rptGrading  rpt;
        private rptGRNService rptGS;
        public rptGRN()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void rptGRN_ReportStart(object sender, EventArgs e)
        {
            if ( HttpContext.Current.Session["GRNIDPrint"] == null)
            {
                throw new Exception("Session expired");
            }
            Guid GRNId = Guid.Empty;
            GRNId = new Guid(HttpContext.Current.Session["GRNIDPrint"].ToString());
            Guid GradingId = Guid.Empty;
            GRNBLL objGRN = new GRNBLL();
            objGRN = objGRN.GetbyGRN_Number(GRNId);
            GradingId = objGRN.GradingId;
            this.lblGRN_No.Text = objGRN.GRN_Number;
            this.lblClient.Text = ClientBLL.GetClinetNameById(objGRN.ClientId);
            
            this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(objGRN.CommodityGradeId);
            this.lblWarehouse.Text = WarehouseBLL.GetWarehouseNameById(objGRN.WarehouseId);
            this.lblOriginalQuantity.Text = objGRN.OriginalQuantity.ToString();
            this.lblNetWeight.Text = objGRN.NetWeight.ToString();
            this.lblDateDeposited.Text = objGRN.DateDeposited.ToShortDateString();
            this.lblTimeDeposited.Text = objGRN.DateDeposited.ToShortTimeString();
            this.lblNoBags.Text = objGRN.TotalNumberOfBags.ToString();
            //Bag Type
            BagTypeBLL objBt = new BagTypeBLL();
            objBt.GetBagTypeById(objGRN.BagTypeId);
            lblBagType.Text = objBt.BagTypeName;

            //Driver Information 
            
            List<DriverInformationBLL> list = null;
            DriverInformationBLL objDI = new DriverInformationBLL();
            list = objDI.GetActiveDriverInformationByReceivigRequestId(objGRN.CommodityRecivingId);
            if (list != null)
            {
                string driverName = "";
                string plateNo = "";
                string driverLicense = "";
                string licensceIssuedPlace = "";
                foreach (DriverInformationBLL o in list )
                {
                    if (driverName == "")
                    {
                        driverName = o.DriverName;
                    }
                    else
                    {
                        driverName += "," + o.DriverName;
                    }
                    if (plateNo == "")
                    {
                        if (String.IsNullOrEmpty(o.TrailerPlateNumber) != true)
                        {
                            plateNo = o.PlateNumber + "-" + o.TrailerPlateNumber;
                        }
                        else
                        {
                            plateNo = o.PlateNumber ;
                        }
                    }
                    else
                    {
                       
                        if (String.IsNullOrEmpty(o.TrailerPlateNumber) != true)
                        {
                            plateNo += " , " + o.PlateNumber + "-" + o.TrailerPlateNumber;
                        }
                        else
                        {
                            plateNo += " , " + o.PlateNumber ;
                        }
                    }
                    if (driverLicense == "")
                    {
                        driverLicense = o.LicenseNumber;
                    }
                    else
                    {
                        driverLicense += " , " + o.LicenseNumber;
                    }
                    if (licensceIssuedPlace == "")
                    {
                        licensceIssuedPlace = o.LicenseIssuedPlace;
                    }
                    else
                    {
                        licensceIssuedPlace += " , " +  o.LicenseIssuedPlace;
                    }
                }
                this.lblDriverName.Text = driverName;
                this.lblPlateNo.Text = plateNo;
                this.lblDriverLicense.Text = driverLicense;
                this.lblPlaceIssued.Text = licensceIssuedPlace;
            }
            // Scaling 
            ScalingBLL objScaling = new ScalingBLL();
            objScaling = objScaling.GetById(objGRN.ScalingId);
            if (objScaling != null)
            {
                if (objScaling.WeigherId != null)
                {
                    try
                    {
                        this.lblWeigherName.Text = UserRightBLL.GetUserNameByUserId(objScaling.WeigherId);
                    }
                    catch
                    {
                    }
                }
            }
            // Sampler
            SamplerBLL objSampler = new SamplerBLL();
            objSampler = objSampler.GetActiveSamplingSupBySamplingId(objGRN.SamplingTicketId);
            if (objSampler != null)
            {
                this.lblSampler.Text = UserRightBLL.GetUserNameByUserId(objSampler.SamplerId);
            }
            //Graders
            GradingByBLL objGrader = new GradingByBLL();
            this.lblGrader.Text = objGrader.GetSupGraderNameByGradingId(objGRN.GradingId);

            if (objGRN.ApprovedBy != null)
            {
                try
                {
                    this.lblApprovedBy.Text = UserRightBLL.GetUserNameByUserId(objGRN.ApprovedBy);
                }
                catch
                {
                }
            }
            if (objGRN.ApprovedTimeStamp != null)
            {
                this.lblDateAproved.Text = objGRN.ApprovedTimeStamp.ToShortDateString();
            }
            rpt = new rptGrading(GradingId);
            rptGS = new rptGRNService(GRNId);
            this.txtDateGenerated.Text = DateTime.Now.ToString();
            ScalingBLL objSacling = new ScalingBLL();
            objSacling = objSacling.GetById(objGRN.ScalingId);
            if (objSacling != null)
            {
                this.lblScaleTicketNo.Text = objSacling.ScaleTicketNumber;
            }

            this.subReport1.Report = rpt;
            this.subReport2.Report = this.rptGS;
            
        }
    }
}
