using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIReCreateGRN : System.Web.UI.UserControl , ISecurityConfiguration
    {
        private Guid CommodityGradeID = Guid.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["ReCreateGRNID"] != null && Session["ReCreateGRNTrackingNo"] != null)
                {
                   ViewState["ReCreateGRNTrackingNo"] = Session["ReCreateGRNTrackingNo"].ToString();
                    Guid GRNID = new Guid(Session["ReCreateGRNID"].ToString());
                    if (GRNID != Guid.Empty)
                    {
                        LoadData(GRNID);
                        LoadGRNType();

                    }
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            GRNBLL obj = new GRNBLL();
            obj.Id = new Guid (this.hfGRNID.Value.ToString());
            obj.ClientId = new Guid( this.hfClientId.Value.ToString());
            obj.BagTypeId = new Guid(this.hfBagTypeId.Value.ToString());
            obj.GRNTypeId = new Guid(this.cboGRNType.SelectedValue.ToString());
            obj.DateDeposited = DateTime.Parse(this.lblDateDeposited.Text);
            obj.TotalNumberOfBags = int.Parse(this.lblBags.Text);
            obj.GrossWeight = float.Parse(this.lblGrossWeight.Text);
            obj.NetWeight = float.Parse(this.lblNetWeight.Text);
            obj.OriginalQuantity = float.Parse(this.lblOriginalQuantity.Text);
            obj.CurrentQuantity = obj.OriginalQuantity;
            obj.TrackingNo = ViewState["ReCreateGRNTrackingNo"].ToString();
            isSaved = obj.ReCreateGRN();

            if (isSaved == true)
            {
                this.lblmsg.Text = "GRN Re-Created Successfully";
                return;
            }
            else
            {
                this.lblmsg.Text = "Unable to Re-create GRN";
                return;
            }


        }
        private void LoadData(Guid GRNID)
        {
            // load GRN Data 
            GRNBLL objGRN = new GRNBLL();
            objGRN = objGRN.GetbyGRN_Number(GRNID);
            if (objGRN != null)
            {
                if (objGRN.Id != null)
                {
                    this.hfGRNID.Value = objGRN.Id.ToString();
                }
                else
                {
                    throw new Exception("Unable to Load GRN Related Data.");
                }
                if (objGRN.CommodityRecivingId != null)
                {
                    hfReceivigRequestId.Value = objGRN.CommodityRecivingId.ToString();
                    //Load Commodity Deposite Information.
                    LoadDepositeRequest(objGRN.CommodityRecivingId);
                }
                if (objGRN.TrackingNo != "")
                {
                    hfTrackingNo.Value = objGRN.TrackingNo;
                    this.lblTrackingNo.Text = objGRN.TrackingNo;
                }
                if (objGRN.VoucherId != null)
                {
                     hfVoucherId.Value= objGRN.VoucherId.ToString();
                }
                this.lblGRN_Number.Text = objGRN.GRN_Number;
                LoadSampling(objGRN.GradingId);
                LoadGrading(objGRN.GradingId);
                LoadUnloading(objGRN.GradingId);
                ScalingInformation(objGRN.GradingId);
                LoadNetWeight();
                LoadQuantity();
                LoadVoucherInformation(objGRN.CommodityRecivingId);

            }


        }
        private void LoadVoucherInformation(Guid Commoditydepositid)
        {
            VoucherInformationBLL objVoucher = new VoucherInformationBLL();
            objVoucher = objVoucher.GetVoucherInformationByCommodityDepositRequestId(Commoditydepositid);
            if (objVoucher != null)
            {
                if (objVoucher.Id != null)
                {
                    hfVoucherId.Value = objVoucher.Id.ToString();
                }
                else
                {
                    this.lblmsg.Text = "Unable to get Voucher information";
                    return;
                }
            }
            else
            {
                this.lblmsg.Text = "Unable to get Voucher information";
                return;
            }
        }
        private void LoadDepositeRequest(Guid commDepositeId)
        {
            CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
            objCDR = objCDR.GetCommodityDepositeDetailById(commDepositeId);
            if (objCDR != null)
            {

                this.lblProductionYear.Text = objCDR.ProductionYear.ToString();
                this.lblDateRecived.Text = objCDR.DateTimeRecived.ToString();
                this.lblTrackingNo.Text = objCDR.TrackingNo;
                this.hfReceivigRequestId.Value = commDepositeId.ToString();
                this.hfWarehouseId.Value = objCDR.WarehouseId.ToString();
                LoadClient(objCDR.ClientId);
            }
        }
        private void LoadClient(Guid ClientId)
        {
            ClientBLL objClient = new ClientBLL();
            objClient = ClientBLL.GetClinet(ClientId);
            try
            {
                if (objClient != null)
                {
                    
                    this.lblClinet.Text = objClient.ClientId + " - " + objClient.ClientName.ToString();
                    hfClientId.Value = ClientId.ToString();
                }
            }
            catch 
            {
                this.lblmsg.Text = "Unable to load client data";
            }
        }
        private void LoadSampling(Guid GradingId)
        {
            SamplingBLL objSampling = new SamplingBLL();
            objSampling = objSampling.GetApprovedSamplesByGradingId(GradingId);
            if (objSampling != null)
            {
                this.lblSampledDate.Text = objSampling.GeneratedTimeStamp.ToShortDateString();
                this.lblSampleTicket.Text = objSampling.SampleCode;
                this.hfSamplingId.Value = objSampling.Id.ToString();
            }


        }
        private void LoadGrading(Guid GradingId)
        {
            GradingResultBLL objGrade = new GradingResultBLL();
            objGrade = objGrade.GetApprovedGradingResultByGradingId(GradingId);
            if (objGrade != null)
            {
                this.hfCommodityGradeId.Value = objGrade.ID.ToString();
                this.hfGradingId.Value = objGrade.GradingId.ToString();
                this.lblCode.Text = objGrade.GradingCode.ToString();
                this.lblProductionYear.Text = objGrade.ProductionYear.ToString();
                this.hfCommodityGradeId.Value = objGrade.CommodityGradeId.ToString();
                this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(objGrade.CommodityGradeId);
                this.CommodityGradeID = new Guid(this.hfCommodityGradeId.Value.ToString());
            }
        }
        private void LoadUnloading(Guid GradingId)
        {
            UnloadingBLL objUnloading = new UnloadingBLL();
            objUnloading = objUnloading.GetApprovedUnloadingByGradingId(GradingId);
            if (objUnloading != null)
            {
                this.lblBags.Text = objUnloading.TotalNumberOfBags.ToString();
                this.lblDateDeposited.Text = objUnloading.DateDeposited.ToShortDateString();
                this.hfBagTypeId.Value = objUnloading.BagTypeId.ToString();
                this.hfUnloadingId.Value = objUnloading.Id.ToString();
                //Get Bag Info
                BagTypeBLL objBagType = new BagTypeBLL();
                if (string.IsNullOrEmpty(objUnloading.BagTypeId.ToString()) != true)
                {
                    objBagType.GetBagTypeById(objUnloading.BagTypeId);
                    if (objBagType != null)
                    {
                        if (objBagType.BagTypeName != "")
                        {
                            this.lblBagType.Text = objBagType.BagTypeName.ToString();
                        }
                        else
                        {
                            this.lblmsg.Text = "Unable To get Bag Type";
                            return;
                        }
                    }
                    else
                    {
                        this.lblmsg.Text = "Unable To get Bag Type";
                        return;
                    }
                }

            }
        }
        private void ScalingInformation(Guid GradingId)
        {
            float weight;
            try
            {
                weight = ScalingBLL.GetApprovedWeightByCommoditydeposite(GradingId);
                this.lblGrossWeight.Text = weight.ToString();

            }
            catch
            {
                this.lblmsg.Text = "An error has occured please try again";
                return;
            }
            Guid ScalingId = Guid.Empty;
            ScalingBLL objScaling = new ScalingBLL();
            try
            {
                ScalingId = objScaling.GetScalingIdByGradingId(GradingId);
                this.hfScalingId.Value = ScalingId.ToString();
            }
            catch
            {
                this.lblmsg.Text = "An error has occured please try again";
                return;
            }



        }
        private void LoadNetWeight()
        {
            float grossweight;
            try
            {
                grossweight = float.Parse(this.lblGrossWeight.Text);
            }
            catch
            {
                this.lblmsg.Text = "Gross weight in not availble please check and try again.";
                return;
            }

            Nullable<Guid> BagId = null;
            if (DataValidationBLL.isGUID(this.hfBagTypeId.Value.ToString(), out BagId) != true)
            {
                this.lblmsg.Text = "Bag type in not availble please check and try again.";
                return;
            }
            Nullable<int> NoBags = null;
            if (DataValidationBLL.isInteger(this.lblBags.Text, out NoBags) != true)
            {
                this.lblmsg.Text = "Number of bags in not availble please check and try again.";
                return;
            }

            float netWeight;
            GRNBLL objNetWeight = new GRNBLL();
            try
            {
                netWeight = (float)objNetWeight.CalculateNetWeight((float)grossweight, (Guid)BagId, (int)NoBags);
                this.lblNetWeight.Text = netWeight.ToString();
            }
            catch
            {
                this.lblmsg.Visible = true;
                this.lblmsg.Text = "Error";
            }


        }
        private void LoadQuantity()
        {
            this.CommodityGradeID = new Guid(this.hfCommodityGradeId.Value.ToString());
            float netWeight;
            int NoBags =-1;

            float qty;
            try
            {
                netWeight = float.Parse(this.lblNetWeight.Text);
            }
            catch
            {
                this.lblmsg.Text = "Net Weight is Invalid.";
                return;
            }
            if(int.TryParse(lblBags.Text,out NoBags ) == false)
            {
                this.lblmsg.Text = "Number of Bags is Invalid.";
                return;
            }
            Guid commodityGrade = Guid.Empty ;
            commodityGrade = new Guid(this.hfCommodityGradeId.Value.ToString());

            GRNBLL objQty = new GRNBLL();

            qty = GRNBLL.CalculateGRNQuantity(netWeight, NoBags, (Guid)commodityGrade);
            this.lblCurrentQuantity.Text = qty.ToString();
            this.lblOriginalQuantity.Text = qty.ToString();

        }
        private void LoadGRNType()
        {
            List<GRNTypeBLL> list = new List<GRNTypeBLL>();
            GRNTypeBLL obj = new GRNTypeBLL();
            try
            {
                list = obj.GetActiveGRNType();
                //Load to cbo
                this.cboGRNType.Items.Clear();
                this.cboGRNType.Items.Add(new ListItem("Please select GRN Type", ""));
                this.cboGRNType.AppendDataBoundItems = true;
                foreach (GRNTypeBLL o in list)
                {
                    this.cboGRNType.Items.Add(new ListItem(o.Name, o.Id.ToString()));
                }
            }
            catch
            {
                this.lblmsg.Text = "Unable to Fullfill this request.";
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnAdd")
            {
                cmd = new List<object>();
                cmd.Add(btnAdd);

            }
            return cmd;
        }

        #endregion
    }
}