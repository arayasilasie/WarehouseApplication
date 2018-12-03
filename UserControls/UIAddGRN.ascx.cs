using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WarehouseApplication.BLL;
using AjaxControlToolkit;
using WarehouseApplication.UserControls;
using WarehouseApplication.SECManager;
using System.Text;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddGRN : System.Web.UI.UserControl, ISecurityConfiguration
    {
        private Guid CommodityGradeID = Guid.Empty ;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.DataBind();
            if (IsPostBack != true)
            {
                if (Session["GRNID"] != null)
                {
                    this.lblmsg.Text = Session["GRNID"].ToString();
                    if (Session["GRNID"].ToString() != "")
                    {
                        LoadPendingGRN();
                        LoadGRNType();
                        GradingCodeChanged();
                        this.cboGradingCode.Enabled = false;
                    }
                }
                WarehouseServicesBLL objWS = new WarehouseServicesBLL();
                List<WarehouseServicesBLL> listWS = null;
                listWS = objWS.GetActiveServices();
                if (listWS != null)
                {
                    this.cboGRNService.Items.Clear();
                    this.cboGRNService.Items.Add(new ListItem("Please Select Service",""));
                    if (listWS.Count > 0)
                    {
                        foreach (WarehouseServicesBLL i in listWS)
                        {
                            this.cboGRNService.Items.Add(new ListItem(i.Name, i.Id.ToString()));
                        }
                    }
                }
                ViewState["listGRNService"] = null;
            }
            
            
            
        }
        protected void cboGradingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GradingCodeChanged();
        }
        private void LoadPendingGRN()
        {
            string TrackingNo = Session["GRNID"].ToString(); 
            //TODO Warehouse from security manager.
            Guid warehouseId = UserBLL.GetCurrentWarehouse();
            List<GRNBLL> list = new List<GRNBLL>();
            GRNBLL objGRN = new GRNBLL();

            list = objGRN.GetPendingGRNByTrackingNo(warehouseId, TrackingNo);
            if (list != null)
            {
                if (list.Count ==1)
                {
                    this.cboGradingCode.Items.Add(new ListItem(list[0].GradingCode.ToString(), list[0].GradingId.ToString()));
                    this.cboGradingCode.SelectedValue = list[0].GradingId.ToString();
                }
                else if (list.Count == 1)
                {
                    this.lblmsg.Text = "An error has occred please contact the administrator.";
                    return;
                }
                else
                {
                    this.lblmsg.Text = "No Pending records";
                    return;
                }
            }
            else
            {
                this.lblmsg.Text = "No Pending records";
                 return ;
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
                Guid coffeeId = (Guid)WarehouseApplication.BLL.Utility.GetCommodityId("Coffee");
                if (objCDR.CommodityId == coffeeId)
                {
                    LoadVoucherInformation(commDepositeId);
                    ViewState["hasVoucher"] = true;
                }
                else
                {
                    ViewState["hasVoucher"] = false;
                }
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
        private void LoadClient(Guid ClientId)
        {
            ClientBLL objClient = new ClientBLL();
            try
            {
                objClient = ClientBLL.GetClinet(ClientId);
                if (objClient != null)
                {
                    string strName = "";
                    try
                    {
                        strName = objClient.ClientName.ToString();
                    }
                    catch
                    {
                        this.lblmsg.Text = "unable to get Client related Data";
                        this.lblmsg.CssClass = "message";
                        this.btnAdd.Enabled = false;
                    }
                   
                    this.lblClinet.Text = objClient.ClientId + " - " + strName;
                    hfClientId.Value = ClientId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception ( "Unable to load client data", ex );
               
            }
        }
        private void LoadSampling(Guid GradingId)
        {
            SamplingBLL objSampling = new SamplingBLL();
            objSampling = objSampling.GetApprovedSamplesByGradingId (GradingId);
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
                this.hfTrackingNo.Value = objGrade.TrackingNo;
                this.hfCommodityGradeId.Value = objGrade.CommodityGradeId.ToString();
                this.hfGradingId.Value = objGrade.GradingId.ToString();
                this.lblCode.Text = objGrade.GradingCode.ToString();  
                this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(objGrade.CommodityGradeId);
                this.lblProductionYear.Text = objGrade.ProductionYear.ToString();
                this.CommodityGradeID = objGrade.CommodityGradeId;
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
                this.lblDateScaled.Text = objScaling.GetById(ScalingId).DateWeighed.ToShortDateString();
            }
            catch 
            {
                this.lblmsg.Text = "An error has occured please try again";
                return;
            }
             


        }
        private void LoadNetWeight()
        {
            if (this.CommodityGradeID == Guid.Empty)
            {
                this.lblmsg.Text = "Invalid Commodity Grade";
                return;
            }
           float grossweight;
           try
           {
               grossweight = float.Parse(this.lblGrossWeight.Text) ;
           }
           catch
           {
               this.lblmsg.Text = "Gross weight in not availble please check and try again.";
               return ;
           }
     
            Nullable<Guid> BagId = null;
            if(DataValidationBLL.isGUID(this.hfBagTypeId.Value.ToString(),out BagId)!= true)
            {
               this.lblmsg.Text = "Bag type in not availble please check and try again.";
               return ;
            }
            Nullable<int> NoBags = null;
            if(DataValidationBLL.isInteger(this.lblBags.Text,out NoBags)!= true)
            {
               this.lblmsg.Text = "Number of bags in not availble please check and try again.";
               return ;
            }

            float netWeight;
            GRNBLL objNetWeight  = new GRNBLL();
            try
            {
                netWeight = (float)objNetWeight.CalculateNetWeight((float)grossweight, (Guid)BagId, (int)NoBags, this.CommodityGradeID);
                this.lblNetWeight.Text = netWeight.ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            
        }
        private void LoadQuantity()
        {
            float netWeight;
            int NoBags;
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
            Nullable<Guid> commodityGrade = null;
            if (DataValidationBLL.isGUID(this.hfCommodityGradeId.Value.ToString(), out commodityGrade) != true)
            {
                this.lblmsg.Text = "Commodity Grade in not availble please check and try again.";
                return;
            }
            if( int.TryParse(lblBags.Text , out NoBags) == false)
            {
                this.lblmsg.Text = "Invalid No Bags";
                return;
            }
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
        private void Clear()
        {
            this.lblmsg.Text = "";
            this.hfReceivigRequestId.Value = "";
            this.hfVoucherId.Value = "";
            this.lblTrackingNo.Text = "";
            this.lblClinet.Text = "";
            this.lblProductionYear.Text = "";
            this.lblDateRecived.Text = "";
            this.lblSampleTicket.Text = "";
            this.hfSamplingId.Value = "";
            this.lblSampledDate.Text = "";
            this.lblCode.Text = "";
            this.hfGradingId.Value = "";
            this.lblCommodityGrade.Text = "";
            this.hfCommodityGradeId.Value = "";
            this.lblDateDeposited.Text = "";
            this.hfUnloadingId.Value = "";
            this.hfWarehouseId.Value = "";
            this.lblBags.Text = "";
            this.lblBagType.Text = "";
            this.hfBagTypeId.Value = "";
            this.lblGrossWeight.Text = "";
            this.hfScalingId.Value = "";
            this.lblNetWeight.Text = "";
            this.lblNetWeight.Text = "";
            this.lblOriginalQuantity.Text = "";
            this.lblCurrentQuantity.Text = "";
            this.cboGRNType.SelectedIndex = -1;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<GRNServiceBLL> listGRNService = null;
            if (ViewState["listGRNService"] != null)
            {
                listGRNService = (List<GRNServiceBLL>)ViewState["listGRNService"];
            }
            Nullable<Guid> id = null;
            GRNBLL objGRN = new GRNBLL();
            objGRN.CommodityGradeId = new Guid(this.hfCommodityGradeId.Value.ToString());
            objGRN.CommodityRecivingId = new Guid(this.hfReceivigRequestId.Value.ToString());
            objGRN.WarehouseId = new Guid(this.hfWarehouseId.Value.ToString());
            objGRN.BagTypeId = new Guid(this.hfBagTypeId.Value.ToString());
            try
            {
                objGRN.VoucherId = new Guid(this.hfVoucherId.Value.ToString());
            }
            catch
            {
                //TODO : check if Coffee / commodity with Voucher - then retun.

            }
            objGRN.UnLoadingId = new Guid(this.hfUnloadingId.Value.ToString());
            
            objGRN.ScalingId = new Guid();
            objGRN.GradingId = new Guid(this.hfGradingId.Value.ToString());
            objGRN.SamplingTicketId = new Guid(this.hfSamplingId.Value.ToString());
            objGRN.GRNTypeId = new Guid(this.cboGRNType.SelectedValue.ToString());
            objGRN.DateDeposited = Convert.ToDateTime( this.lblDateDeposited.Text);
            objGRN.Status = (int)GRNStatus.New;
            objGRN.TotalNumberOfBags = Convert.ToInt32(this.lblBags.Text);
            objGRN.GradingCode = this.cboGradingCode.SelectedItem.ToString();
            objGRN.GRNCreatedDate = DateTime.Parse( this.txtDateRecived.Text);
            try
            {
                objGRN.ClientId = new Guid(this.hfClientId.Value.ToString());
            }
            catch
            {

                this.lblmsg.Text = "Invalid Client Id please check and try again.";
                return;
            }
          
            try
            {  
                objGRN.GrossWeight = float.Parse(this.lblGrossWeight.Text);
                objGRN.NetWeight = float.Parse(this.lblNetWeight.Text);
            }
            catch
            {
                this.lblmsg.Text = " Weight Can not be calculated please check and try again.";
                return;
            }
            try
            {
                objGRN.OriginalQuantity = float.Parse(this.lblOriginalQuantity.Text);
                objGRN.CurrentQuantity = float.Parse(this.lblCurrentQuantity.Text);
            }
            catch
            {
                this.lblmsg.Text = "Quantity Can not be calculated please check and try again.";
                return;
            }
            if (string.IsNullOrEmpty(this.hfTrackingNo.Value.ToString()) == true)
            {
                this.lblmsg.Text = "Can not load Tracking No.";
                return;
            }
            else
            {
                objGRN.TrackingNo = this.hfTrackingNo.Value;
            }
            if (bool.Parse(ViewState["hasVoucher"].ToString()) == true)
            {
                if (string.IsNullOrEmpty(this.hfVoucherId.Value.ToString()) == true)
                {
                    this.lblmsg.Text = "Can not load Voucher information.";
                    return;
                }
                else
                {
                    try
                    {
                        objGRN.VoucherId = new Guid(this.hfVoucherId.Value);
                    }
                    catch
                    {
                        this.lblmsg.Text = "Can not load Voucher information.";
                        return;
                    }
                }
            }
            else
            {
                objGRN.VoucherId = Guid.Empty;
            }
            if (string.IsNullOrEmpty(this.hfScalingId.Value.ToString()) == true)
            {
                this.lblmsg.Text = "Can not load Scaling information.";
                return;
            }
            else
            {
                try
                {
                    objGRN.ScalingId = new Guid(this.hfScalingId.Value);
                }
                catch
                {
                    this.lblmsg.Text = "Can not load Voucher information.";
                    return;
                }
            }
            try
            {
                id = objGRN.Add(listGRNService);
                Session["GRNIDPrint"] = id.ToString();

                
              



            }
            catch (IndeterminateGRNCountException )
            {
                this.lblmsg.Text = "Please check if GRN is Created and try again.";
                return;
            }
            catch (MultipleGRNForSingleGradingCodeException )
            {
                this.lblmsg.Text = "A GRN has already been created for this Grading Code.";
                return;
            }
            catch( Exception ex)
            {
                //this.lblmsg.Text = "An error has occured please try again.If the error persists contact the administrator.";
                //return;
                throw ex;
            }
          
            if (id != null)
            {
                Session["GRNID"] =id.ToString();
                this.lblmsg.Text = "New GRN added Successfully.";
                //Response.Redirect("PageSwicther.aspx?TranNo=" + objGRN.TrackingNo);

                ScriptManager.RegisterStartupScript(this,
                  this.GetType(),
                  "ShowReport",
                  "<script type=\"text/javascript\">" +
                      string.Format("javascript:window.open(\"rptGRN.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                  "</script>",
                  false);
            }
            else
            {
                this.lblmsg.Text = "Unable to add new GRN please try Again";
                return;
            }
            //if (id != null)
            //{
              


            //}

        }
        protected void gvgvGRNService_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row = -1;
            row = e.RowIndex;
            if (row != -1)
            {


                List<GRNServiceBLL> list = new List<GRNServiceBLL>();
                list = (List<GRNServiceBLL>)ViewState["listGRNService"];
                try
                {
                    list.RemoveAt(e.RowIndex);
                    this.gvGRNService.DataSource = list;
                    this.gvGRNService.DataBind();
                    
                   
                    
                }
                catch
                {
                    this.lblmsg.Text = "Unable to remove item!";
                    return;
                }



            }
        }
        protected void btnGRNService_Click(object sender, EventArgs e)
        {
            Guid Id = Guid.NewGuid();
            if(this.cboGRNService.SelectedValue == "")
            {
                this.lblmsg.Text = "Please Select GRN Service";
                return;
            }
            Guid SeviceId = new Guid(this.cboGRNService.SelectedValue);
            int index = int.Parse(this.cboGRNService.SelectedIndex.ToString());
            int TotNumberPerunit = int.Parse(txtTotalNumberPerUnit.Text);
            GRNServiceBLL obj = new GRNServiceBLL();
            obj.Id = Id;
            obj.ServiceId = SeviceId;
            obj.Quantity = TotNumberPerunit;
            obj.Status = GRNServiceStatus.Active;
            obj.ServiceName = this.cboGRNService.SelectedItem.ToString();
            List<GRNServiceBLL> list = new List<GRNServiceBLL>();
            list =( List<GRNServiceBLL>) ViewState["listGRNService"];
            if (list != null)
            {
                list.Add(obj);
                ViewState["listGRNService"] = list;
            }
            else
            {
                list = new List<GRNServiceBLL>();
                list.Add(obj);
                ViewState["listGRNService"] = list;
            }
            this.gvGRNService.DataSource = list;
            this.gvGRNService.DataBind();
            this.cboGRNService.SelectedIndex = -1;
            this.txtTotalNumberPerUnit.Text = "";
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnAdd")
            {
                
                cmd.Add(this.btnAdd);
                return cmd;
            }
            else if(name == "cboGradingCode")
            {
                cmd.Add(this.cboGradingCode);
                return cmd;
            }
            else if (name == "cboGRNType")
            {
                cmd.Add(this.cboGRNType);
                return cmd;
            }
            else
            {
                return null;
            }
        }
        private void GradingCodeChanged()
        {
            Clear();
            Nullable<Guid> GradingId = null;
            Guid CommodityDepositeRequestId;
            if (DataValidationBLL.isGUID(this.cboGradingCode.SelectedValue.ToString(), out GradingId) != true)
            {
                this.lblmsg.Text = "Please select Grading No.";
                return;
            }
            GradingBLL objGrading = new GradingBLL();
            objGrading = objGrading.GetById((Guid)GradingId);
            if (objGrading == null)
            {
                this.lblmsg.Text = "Please select Grading No.";
                return;
            }
            if (objGrading.CommodityRecivingId == null)
            {
                this.lblmsg.Text = "Please select Grading No.";
                return;
            }
            else
            {
                CommodityDepositeRequestId = objGrading.CommodityRecivingId;
            }

            LoadDepositeRequest(CommodityDepositeRequestId);
            LoadSampling((Guid)GradingId);
            LoadGrading((Guid)GradingId);
            LoadUnloading((Guid)GradingId);
            ScalingInformation((Guid)GradingId);
            LoadNetWeight();
            LoadQuantity();
            
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.btnAdd.UseSubmitBehavior = false;

            btnAdd.OnClientClick = "javascript:";

            if (btnAdd.CausesValidation)
            {
                btnAdd.OnClientClick += " if ( Page_ClientValidate('" + btnAdd.ValidationGroup + "') ){ ";
                btnAdd.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }
        }

        

   
    }
}