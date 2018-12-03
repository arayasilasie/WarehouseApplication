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
    public partial class UIEditGRN : System.Web.UI.UserControl, ISecurityConfiguration
    {
        private Guid CommodityGradeID = Guid.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.DataBind();
            if (IsPostBack != true)
            {
                Nullable<Guid> GRN_number = null;

                if (Session["GRNID"] != null)
                {
                    try
                    {
                        //Get Commodity deposite Request Id  from the GRN;
                        GRN_number = new Guid(Session["GRNID"].ToString());
                        if (Session["GRNTrackingNo"] != null)
                        {
                            this.lblCurrentTrackingNo.Text = Session["GRNTrackingNo"].ToString();
                        }
                        else
                        {
                            GRNBLL objGRN = new GRNBLL();
                            objGRN = objGRN.GetbyGRN_Number((Guid)GRN_number);
                            if (objGRN != null)
                            {
                                GradingBLL objGrading = new GradingBLL();
                                objGrading = objGrading.GetById(objGRN.GradingId);
                                this.lblCurrentTrackingNo.Text = objGrading.TrackingNo;
                            }

                        }
                        LoadGRNInformation((Guid)GRN_number);
                        LoadStatus();
                        Session["GRNID"] = null;
                        Session["GRNTrackingNo"] = null;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    LoadServices();
                }

                this.btnAdd.Attributes.Add("btnGRNService_Click", "ValidateGRNService");
                if (Session["msg"] != null)
                {
                    ECXWF.CMessage mess = (ECXWF.CMessage)Session["msg"];
                    if (mess.Name == "EditGRN")
                    {
                        this.pnl.Visible = false;
                    }
                }
            }
        }
        private void LoadGRNInformation(Guid GRNId)
        {
            LoadGRNType();
            GRNBLL obj = new GRNBLL();
            obj = obj.GetbyGRN_Number(GRNId);
            if (obj != null)
            {
                if (obj.GRN_Number != "")
                {
                    this.hfGRNId.Value = obj.Id.ToString();
                    this.lblGRN.Text = obj.GRN_Number;
                    this.lblTrackingNo.Text = obj.TrackingNo;
                    //this.lblProductionYear.Text = obj.ProductionYear.ToString();
                    this.lblCode.Text = obj.GradingCode;
                    this.lblDateDeposited.Text = obj.DateDeposited.ToShortDateString();
                    this.lblBags.Text = obj.TotalNumberOfBags.ToString();
                    this.lblGrossWeight.Text = obj.GrossWeight.ToString();
                    this.lblNetWeight.Text = obj.NetWeight.ToString();
                    this.lblOriginalQuantity.Text = obj.OriginalQuantity.ToString();
                    this.lblCurrentQuantity.Text = obj.CurrentQuantity.ToString();
                    this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
                    ClientBLL objClient = new ClientBLL();
                    LoadSampling(obj.GradingId);
                    LoadGrading(obj.GradingId);
                    LoadDepositeRequest(obj.CommodityRecivingId);
                    GetGRNServices(GRNId);
                    this.cboGRNType.SelectedValue = obj.GRNTypeId.ToString();
                    BagTypeBLL objBag = new BagTypeBLL();
                    objBag.GetBagTypeById(obj.BagTypeId);
                    this.lblBagType.Text = objBag.BagTypeName;
                    this.hfStatus.Value = obj.Status.ToString();
                    if (obj.ClientAcceptedTimeStamp != null)
                    {
                        this.txtClientAcceptedTimeStamp.Text = obj.ClientAcceptedTimeStamp.ToShortDateString();
                    }
                    this.chkClientAccepted.Checked = obj.ClientAccepted;
                    if (this.chkClientAccepted.Checked != true)
                    {
                        //TOdo- GRN Statas.
                    }
                    if (this.cboStatus.SelectedValue == "6" || this.cboStatus.SelectedValue == "3")
                    {
                        this.cboStatus.Enabled = false;
                        this.btnAdd.Enabled = false;

                    }
                }
            }


        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (this.cboStatus.SelectedValue == "6")// need to consume the webservice.
            {

                bool isSaved = false;
                GRNBLL objGRN = new GRNBLL();
                objGRN = objGRN.GetbyGRN_Number(new Guid(this.hfGRNId.Value.ToString()));
                objGRN.ApprovedTimeStamp = DateTime.Now;
                objGRN.TrackingNo = this.lblCurrentTrackingNo.Text;
                
                objGRN.ManagerApprovedDateTime = DateTime.Parse(this.txtMADate.Text + " " + this.txtTime.Text );


                isSaved = objGRN.Update(this.lblGRN.Text, (GRNStatus)int.Parse(this.cboStatus.SelectedValue), objGRN, objGRN.TrackingNo, objGRN.ManagerApprovedDateTime);
                if (isSaved == true)
                {
                    this.lblmsg.Text = "GRN Updated Successfully";
                    LoadGRNInformation(new Guid(this.hfGRNId.Value.ToString()));
                    Response.Redirect("ListInbox.aspx");
                }
                else
                {
                    this.lblmsg.Text = "Can not update the GRN.";
                }
            }
            else if (this.cboStatus.SelectedValue == "3" )
            {
                GRNBLL objGRN = new GRNBLL();
                objGRN = objGRN.GetbyGRN_Number(new Guid(this.hfGRNId.Value.ToString()));

                bool isSaved = objGRN.Update(objGRN.GRN_Number, GRNStatus.Cancelled, objGRN, objGRN.TrackingNo, objGRN.ManagerApprovedDateTime);
                if (isSaved == false)
                {
                    this.lblmsg.Text = "Can not update the GRN.";
                    return;
                }
                else
                {
                    this.lblmsg.Text = "GRN Cancelled.";
                }
            }
        }
        private void LoadPendingGRN()
        {
            ////TODO Warehouse from security manager.
            //Guid warehouseId = new Guid("fa0a52e8-9308-4d5e-b323-88ca5ba232ed");
            //List<GRNBLL> list = new List<GRNBLL>();
            //GRNBLL objGRN = new GRNBLL();
            //try
            //{
            //    list = objGRN.GetPendingGRN(warehouseId);
            //    if (list != null)
            //    {
            //        if (list.Count > 0)
            //        {

            //            this.cboGradingCode.Items.Clear();
            //            this.cboGradingCode.Items.Add(new ListItem("Please Select Tracking No", ""));
            //            this.cboGradingCode.DataTextField = "GradingCode";
            //            this.cboGradingCode.DataValueField = "CommodityRecivingId";
            //            this.cboGradingCode.AppendDataBoundItems = true;
            //            this.cboGradingCode.DataSource = list;
            //            this.DataBind();

            //        }
            //        else
            //        {
            //            this.lblmsg.Text = "No Pending records";
            //        }
            //    }
            //    else
            //    {
            //        this.lblmsg.Text = "No Pending records";
            //    }
            //}
            //catch
            //{
            //    this.lblmsg.Text = "Can not process this request please try again.";
            //}
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
            objClient = ClientBLL.GetClinet(ClientId);
            try
            {
                
                if (objClient != null)
                {
                    this.lblClinet.Text = objClient.ClientId.ToString() + " - " + objClient.ClientName.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception ( "Unable to load client data", ex);
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
                this.lblCommodityGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(objGrade.CommodityGradeId);
                this.CommodityGradeID = objGrade.CommodityGradeId;
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
        private void LoadStatus()
        {
            this.cboStatus.Items.Clear();
            if (this.hfStatus.Value == "4")
            {
                this.cboStatus.Items.Add(new ListItem("Client Approved", "4"));
                this.cboStatus.Items.Add(new ListItem("Manager Approved", "6"));
                this.cboStatus.Items.Add(new ListItem("Cancelled", "3"));
            }
            if (this.hfStatus.Value == "1")
            {
                this.cboStatus.Items.Add(new ListItem("New", "1"));
                this.cboStatus.Items.Add(new ListItem("Cancelled", "3"));
            }
            if (this.hfStatus.Value == "2")
            {
                this.cboStatus.Items.Add(new ListItem("Active", "1"));
                this.cboStatus.Items.Add(new ListItem("Cancelled", "3"));
            }
            if (this.hfStatus.Value == "3")
            {
                this.cboStatus.Items.Add(new ListItem("Cancelled", "3"));
            }
            if (this.hfStatus.Value == "5")
            {
                this.cboStatus.Items.Add(new ListItem("Client Rejected", "5"));
            }
            if (this.hfStatus.Value == "6")
            {
                this.cboStatus.Items.Add(new ListItem("Manager Approved", "6"));
            }
            this.cboStatus.SelectedValue = this.hfStatus.Value;
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void WFM(string stTran)
        {
            string TransactionNo = "";
            if (stTran == "")
            {
                TransactionNo = Request.QueryString["TranNo"];
            }
            else
            {
                TransactionNo = stTran;
            }

            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            ECXWF.CMessage[] mess = eng.Request(TransactionNo, UserBLL.GetCurrentUser(), new string[] { "" });
            mess[0].IsCompleted = true;
            eng.Response(TransactionNo, mess[0]);
            //Response.Redirect("~/PageSwicther.aspx?TranNo=" + TransactionNo);
        }
        private void GetGRNServices(Guid GRNId)
        {
            GRNServiceBLL objServices = new GRNServiceBLL();
            List<GRNServiceBLL> list = objServices.GetByGRNId(GRNId);
            ViewState["grnServiceList"] = list;
            this.gvGRNService.DataSource = list;
            this.gvGRNService.DataBind();

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
            else if (name == "cboStatus")
            {
                cmd.Add(this.cboStatus);
                return cmd;
            }
            return null;
        }

        #endregion

        protected void gvGRNService_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row = -1;
            row = e.RowIndex;
            if (row != -1)
            {
                Label lblId = (Label)this.gvGRNService.Rows[row].FindControl("lblId");
                if(lblId == null)
                {
                    this.lblmsg.Text ="unable to get GRN service Id";
                    return;
                }
                if(lblId.Text == "")
                {
                    this.lblmsg.Text ="unable to get GRN service Id";
                    return;
                }
                Guid GSId = new Guid(lblId.Text);
                List<GRNServiceBLL> list = new List<GRNServiceBLL>();
                list = (List<GRNServiceBLL>)ViewState["grnServiceList"];
                try
                {
                    GRNServiceBLL objGS = new GRNServiceBLL();
                    if (objGS.Cancel(GSId) == true)
                    {
                        GetGRNServices(new Guid(this.hfGRNId.Value.ToString()));
                        this.lblmsg.Text = "Data updated Successfully";

                    }
                    else
                    {
                        this.lblmsg.Text = "Unable to update data";
                    }
                    
                   
                    
                }
                catch
                {
                    this.lblmsg.Text = "Unable to remove item!";
                }

        }
    }

        protected void btnGRNService_Click(object sender, EventArgs e)
        {
            GRNServiceBLL objGS = new GRNServiceBLL();
            objGS.Id = Guid.NewGuid();
            objGS.GRNId = new Guid(this.hfGRNId.Value.ToString());
            objGS.ServiceId = new Guid(this.cboGRNService.SelectedValue.ToString());
            objGS.ServiceName = this.cboGRNService.SelectedItem.ToString();
            objGS.CreatedBy = UserBLL.GetCurrentUser();
            objGS.Status = GRNServiceStatus.Active;
            if (objGS.Save() == true)
            {
                
                this.lblmsg.Text = "Data Updated Successfully";
                List<GRNServiceBLL> list = new List<GRNServiceBLL>();
                list = (List<GRNServiceBLL>)ViewState["grnServiceList"];
                list.Add(objGS);
                this.gvGRNService.DataSource = list;
                this.gvGRNService.DataBind();

            }
            else
            {
                this.lblmsg.Text = "Unable to Update data.";
            }
        }
        private void LoadServices()
        {
            WarehouseServicesBLL objWS = new WarehouseServicesBLL();
            List<WarehouseServicesBLL> listWS = null;
            listWS = objWS.GetActiveServices();
            if (listWS != null)
            {
                this.cboGRNService.Items.Clear();
                this.cboGRNService.Items.Add(new ListItem("Please Select Service", ""));
                if (listWS.Count > 0)
                {
                    foreach (WarehouseServicesBLL i in listWS)
                    {
                        this.cboGRNService.Items.Add(new ListItem(i.Name, i.Id.ToString()));
                    }
                }
            }
        }

        protected void gvGRNService_PageIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvGRNService_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvGRNService.PageIndex = e.NewPageIndex;
             List<GRNServiceBLL> list = new List<GRNServiceBLL>();
            list = (List<GRNServiceBLL>)ViewState["grnServiceList"];
            this.gvGRNService.DataSource = list;
            this.gvGRNService.DataBind();
        }
    }
}