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
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIClientAcceptGRN : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.DataBind();
            if (IsPostBack != true)
            {
                string GRN_number = "";
                if (Session["GRNID"] != null)
                {
                    //Get Commodity deposite Request Id  from the GRN;
                    GRN_number = Session["GRNID"].ToString();
                    LoadGRNInformation(new Guid(GRN_number));
                    Session["GRNID"] = null;
                    this.txtClientAcceptedTimeStamp.Text = DateTime.Now.ToShortDateString();
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            bool ca = false;
            DateTime cd ;
            try
            {
                ca = Convert.ToBoolean(int.Parse(this.cboClientAccpted.SelectedValue.ToString()));
            }
            catch
            {
                this.lblmsg.Text = "please select client Response";
                this.cboClientAccpted.Focus();
                return;
            }
            cd = DateTime.Parse(this.txtClientAcceptedTimeStamp.Text);
            GRNBLL obj = new GRNBLL();
            string TransactionNo = "";
            TransactionNo = Request.QueryString["TranNo"];
            GRNStatus prevStatus = (GRNStatus)(int.Parse(this.hfStatus.Value));
            isSaved = obj.ClientAcceptance(this.lblGRN.Text, cd, ca, TransactionNo,prevStatus);
            if(isSaved == true)
            {
                Response.Redirect("ListInbox.aspx");
                
            }
            else
            {
                this.lblmsg.Text = "Unable to process the request";
            }
        }
        private void LoadGRNInformation(Guid GRN)
        {
            LoadGRNType();
            GRNBLL obj = new GRNBLL();
            obj = obj.GetbyGRN_Number(GRN);
            if (obj != null)
            {
                if (obj.GRN_Number != "")
                {
                    this.hfGRNID.Value = obj.Id.ToString();
                    this.lblGRN.Text = obj.GRN_Number;
                    this.lblTrackingNo.Text = obj.TrackingNo;
                    this.lblProductionYear.Text = obj.ProductionYear.ToString();
                    this.lblCode.Text = obj.GradingCode;
                    this.lblDateDeposited.Text = obj.DateDeposited.ToShortDateString();
                    this.lblBags.Text = obj.TotalNumberOfBags.ToString();
                    this.lblGrossWeight.Text = obj.GrossWeight.ToString();
                    this.lblNetWeight.Text = obj.NetWeight.ToString();
                    this.lblOriginalQuantity.Text = obj.OriginalQuantity.ToString();
                    this.lblCurrentQuantity.Text = obj.CurrentQuantity.ToString();
                    lblGRNCreatedDate.Text = obj.GRNCreatedDate.ToShortDateString();
                    cmpSampGen.ValueToCompare = obj.CreatedTimestamp.ToShortDateString();
                    this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
                    ClientBLL objClient = new ClientBLL();
                    //objClient = ClientBLL.GetClinet(obj.ClientId);
                    //this.lblClinet.Text = objClient.ClientName;
                    LoadSampling(obj.GradingId);
                    LoadGrading(obj.GradingId);
                    LoadDepositeRequest(obj.CommodityRecivingId);
                    this.cboGRNType.SelectedValue = obj.GRNTypeId.ToString();
                    BagTypeBLL objBag = new BagTypeBLL();
                    objBag.GetBagTypeById(obj.BagTypeId);
                    this.lblBagType.Text = objBag.BagTypeName;
                    this.cboStatus.SelectedValue = obj.Status.ToString();
                    if (obj.ClientAccepted == true)
                    {
                        this.cboClientAccpted.SelectedValue = "1";
                    }
                    else
                    {
                        this.cboClientAccpted.SelectedValue = "2";
                    }
                    this.txtClientAcceptedTimeStamp.Text = obj.ClientAcceptedTimeStamp.ToString();
                    this.hfStatus.Value = obj.Status.ToString();
                    if ( this.cboStatus.SelectedValue == "4" && this.cboStatus.SelectedValue == "5" && this.cboStatus.SelectedValue == "6")
                    {
                        this.cboStatus.Enabled = false;
                        this.cboClientAccpted.Enabled = false;
                        this.btnAdd.Enabled = false;
                        this.txtClientAcceptedTimeStamp.Enabled = false;
                        this.txtClientAcceptedTimeStamp.Visible = false;
                        this.cboClientAccpted.Visible = false;
                        this.btnAdd.Visible = false;
                        this.lblmsg.Text = "The Client can not accpet or reject this GRN as the status of the GRN is not new or approved.";
                    }
                }
            }


        }    
        private void LoadDepositeRequest(Guid commDepositeId)
        {
            CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
            objCDR = objCDR.GetCommodityDepositeDetailById(commDepositeId);
            if (objCDR != null)
            {

                //this.lblProductionYear.Text = objCDR.ProductionYear.ToString();
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
            try
            {
                objClient = ClientBLL.GetClinet(ClientId);
                if (objClient != null)
                {
                   
                    this.lblClinet.Text = objClient.ClientId + " - " + objClient.ClientName.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load client data", ex);
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
                this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(objGrade.CommodityGradeId);
                this.lblProductionYear.Text = objGrade.ProductionYear.ToString();
            }
        }
        private void LoadUnloading(Guid commDepositeId)
        {
            UnloadingBLL objUnloading = new UnloadingBLL();
            objUnloading = objUnloading.GetApprovedUnloadingByCommodityDepositeId(commDepositeId);
            if (objUnloading != null)
            {
                this.lblBags.Text = objUnloading.TotalNumberOfBags.ToString();
                this.lblDateDeposited.Text = objUnloading.DateDeposited.ToShortDateString();
                this.hfBagTypeId.Value = objUnloading.BagTypeId.ToString();
                this.hfUnloadingId.Value = objUnloading.Id.ToString();
                BagTypeBLL objBagType = new BagTypeBLL();

                //this.lblBagType.Text = objUnloading.
            }
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
            this.cboGRNType.Enabled = false;
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
        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnAdd")
            {
                cmd = new List<object>();
                cmd.Add(this.btnAdd);
            }
            return cmd;
        }
       

        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
        
    }
}