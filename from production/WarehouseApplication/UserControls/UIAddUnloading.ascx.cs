using System;
using System.Collections;
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
using System.Data.SqlClient;
using System.Collections.Generic;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;
using AjaxControlToolkit;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddUnloading : System.Web.UI.UserControl, ISecurityConfiguration
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            if (IsPostBack != true)
            {
                hfTrackingNo.Value = "";
                LoadPage();
                if (Session["AddUnLoadingId"] != null && Session["AddUnLoadingIdGradingCode"] != null)
                {
                    cboGradingCode.Items.Add(new ListItem(Session["AddUnLoadingIdGradingCode"].ToString(), Session["AddUnLoadingId"].ToString()));
                    cboGradingCode.SelectedValue = Session["AddUnLoadingId"].ToString();
                    selectedGradechanged();
                }
                this.cboUnloadedBy.Items.Clear();
                this.cboUnloadedBy.Items.Add(new ListItem("Please select Inveroty Controller", ""));
                List<UserBLL> list  = UserRightBLL.GetUsersWithRight("InventoryController");
                if (list != null)
                {
                    if (list.Count > 0)
                    {

                        foreach (UserBLL u in list)
                        {
                            this.cboUnloadedBy.Items.Add(new ListItem(u.FullName, u.UserId.ToString()));
                        }
                    }
                }
                ViewState["TotalBagsSoFar"] = "0";
            }
            cboGradingCode.Enabled = false;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.lblMsg2.Text = "";
            this.lblmsg.Text  = "";
            int totalNoBags = int.Parse(this.txtNumberOfBags.Text);
            int totalNoBagsSoFar = int.Parse(ViewState["TotalBagsSoFar"].ToString())+ int.Parse( this.txtStackNoBags.Text);
            if (totalNoBags < totalNoBagsSoFar)
            {
                this.lblmsg.Text = "The sum of Unloaded bags exceeded Total Number of bags.";
                return;
            }
            List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
            list = (List<StackUnloadedBLL>) ViewState["StackUnloaded"];
            if (list == null)
            {
                list = new List<StackUnloadedBLL>();
            }
            StackUnloadedBLL obj = new StackUnloadedBLL();
            obj.Id = Guid.NewGuid();
            Nullable<Guid> stackId = null;
            if( DataValidationBLL.isGUID(this.cboStackNo.SelectedValue.ToString(),out stackId)== false)
            {
                this.lblmsg.Text = "please select Stack No.";
                bindGrid();
                return;
            }
            else
            {
                obj.StackId = (Guid)stackId;
            }
            //check if a stack is already there.
            foreach (StackUnloadedBLL i in list)
            {
                if (obj.StackId == i.StackId)
                {
                    this.lblMsg2.Text = "The Stack has already been used.";
                    bindGrid();
                    return;
                }
            }



            Nullable<Int32> noBags = null;
            if (DataValidationBLL.isInteger(this.txtStackNoBags.Text,out noBags) == false)
            {
                this.lblmsg.Text = "please enter no. bags.";
                this.txtStackNoBags.Focus();
                bindGrid();
                return;
            }
            else
            {
                obj.NumberOfbags = (int)noBags;
            }
            Nullable<Guid> user = null;
            if (DataValidationBLL.isGUID(this.cboUnloadedBy.SelectedValue.ToString(), out user) == false)
            {
                this.lblmsg.Text = "please select inventory Controller.";
                bindGrid();
                return;
            }
            else
            {
                obj.UserId = (Guid)user;
            }
            obj.Status = StackUnloadedStatus.New;
            obj.CreatedBy = UserBLL.GetCurrentUser();
            obj.StackNo = this.cboStackNo.SelectedItem.ToString();
            obj.InventoryControllerName = this.cboUnloadedBy.SelectedItem.ToString();
            obj.Remark = this.txtRemark.Text;
            
            list.Add(obj);
            ViewState["StackUnloaded"] = list;
            ViewState["TotalBagsSoFar"] = totalNoBagsSoFar;
            bindGrid();
            stackBagsClear();
        }
        private void bindGrid()
        {
            List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
            try
            {
                list = (List<StackUnloadedBLL>)ViewState["StackUnloaded"];
            }
            catch 
            {
                list = null;
            }
            this.gvStackUnloaded.DataSource = list;
            this.gvStackUnloaded.DataBind();
        }
        protected void gvStackUnloaded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int  row= -1;
            row = e.RowIndex;
            if (row != -1)
            {
                List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();

                list = (List<StackUnloadedBLL>)ViewState["StackUnloaded"];
                try
                {
                    ViewState["TotalBagsSoFar"] = ((int.Parse(ViewState["TotalBagsSoFar"].ToString()) - list[e.RowIndex].NumberOfbags));
                    list.RemoveAt(e.RowIndex);
                    bindGrid();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    this.lblMsg2.Text = ex.Message;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
       


             
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get the one lotsize.
            if (ViewState["UnloadingCommGradeId"] == null)
            {
                this.lblmsg.Text = "An error has occured please try again, if the error persists contact the administrator";
                return;
            }
            int lotsizeInBags = 0;
            Guid CommGradeId = new Guid(ViewState["UnloadingCommGradeId"].ToString());
            lotsizeInBags = CommodityGradeBLL.GetCommodityGradeLotSizeInBagsById(CommGradeId);         
            if (lotsizeInBags == 0)
            {
                this.lblmsg.Text = "Unbale to get lot size in bags.";
                return;
            }
            Nullable<int> totalnumberofBags = null;
            totalnumberofBags = int.Parse(this.txtNumberOfBags.Text);
            int remainder;

            this.lblMsg2.Text = "";
            this.lblmsg.Text = "";
            Nullable<Guid> gradingresultid = null;
            if (DataValidationBLL.isGUID(this.cboGradingCode.SelectedValue.ToString(), out gradingresultid) == false)
            {
                this.lblmsg.Text = "please select Grading Code.";
                return;
            }
           
         
            DateTime depositedate ;
            if ((DateTime.TryParse((this.txtDateDeposited.Text) + " " + this.txtTimeDeposited.Text, out depositedate))== false)
            {
                this.lblmsg.Text = "Invalid Date Deposited.";
                return;
            }
            if (string.IsNullOrEmpty(hfTrackingNo.Value) == true)
            {
                this.lblmsg.Text = "Unable to get tracking No Please try again.";
                return;
            }
            Guid BagTypeId = Guid.Empty;
            if (string.IsNullOrEmpty(this.cboBagType.SelectedValue.ToString()) == true)
            {
                this.lblmsg.Text = "Unable to get Bag Type No Please try again.";
                return;
            }
            else
            {
                try
                {
                    BagTypeId = new Guid(this.cboBagType.SelectedValue.ToString());
                }
                catch
                {
                    this.lblmsg.Text = "Unable to get Bag Type No Please try again.";
                    return;
                }
            }
            // 
            UnloadingBLL obj = new UnloadingBLL();
            obj.GradingResultId = (Guid) gradingresultid;
            obj.TotalNumberOfBags = (int)totalnumberofBags;
            obj.DateDeposited =(DateTime) depositedate;
            obj.CreatedBy = UserBLL.GetCurrentUser();
            obj.ReceivigRequestId = new Guid(this.hfRecivingRequestId.Value.ToString());
            obj.TrackingNo = hfTrackingNo.Value;
            obj.BagTypeId = BagTypeId;
            List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
            list = (List<StackUnloadedBLL>) ViewState["StackUnloaded"];
            int totCount = 0;
            if (list != null)
            {
                foreach (StackUnloadedBLL i in list)
                {
                    totCount = totCount + i.NumberOfbags;
                }
            }
            if (totCount != totalnumberofBags)
            {
                this.lblmsg.Text = "Please Check Total Number of bags against Bags Unloaded in each stack";
                bindGrid();
                return;
            }
            int SamplerNoBags = -1; ;
            if (ViewState["SamplingNoBags"] != null)
            {
                SamplerNoBags = int.Parse(ViewState["SamplingNoBags"].ToString());
            }
            if (SamplerNoBags != -1)
            {
                if(((int)totalnumberofBags) != SamplerNoBags)
                {
                    //Check 
                    string str = " The number of bags counted by the sampler is " + 
                         SamplerNoBags.ToString()+ " which is different from what you entered.  Are you sure you want to save this record?";
                   
                    txtReason.InnerText = str ;
                    ModalPopupExtender1.Show();
                    
                }
            }
            ModalPopupExtender mdl = new ModalPopupExtender();
            mdl = ModalPopupExtender1;
            remainder = ((int)totalnumberofBags) % lotsizeInBags;

            if (((int)remainder) != 0)
            {
                
                txtReason.InnerText = "Per ECX rule, the number of bags entered is not acceptable.  Do you still want to save?";
                ModalPopupExtender1.Show();

            }
            
            if (obj.Add(list) == true)
            {
                this.lblmsg.Text = "Data Successfully Updated";
                Response.Redirect("ListInbox.aspx");

            }
            else
            {
                this.lblmsg.Text = "Data can't be added.Please try again.";
            }


        }
        protected void cboGradingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGradechanged();
        }
        protected void cboShed_SelectedIndexChanged(object sender, EventArgs e)
        {
           //TODO :Remove the ff
            if (this.cboShed.SelectedValue.ToString() == "")
            {
                this.lblmsg.Text = "Please Selected Shed";
                return;
            }
            
          
            ////populate Stack from the list.
            this.cboStackNo.Items.Add(new ListItem("Please select Stack",""));
            this.cboStackNo.AppendDataBoundItems = true;
            List<StackBLL> list = new List<StackBLL>();
            StackBLL obj = new StackBLL();
            Guid CG = Guid.Empty;
            if (ViewState["UnloadingCommGradeId"] != null)
            {
                CG = new Guid(ViewState["UnloadingCommGradeId"].ToString());
            }
            int productionYear = -1;
            if (ViewState["productionYear"] != null)
            {
                productionYear  = int.Parse(ViewState["productionYear"].ToString());
            }


            list = obj.GetActiveStackbyShedId(new Guid(this.cboShed.SelectedValue.ToString()), CG,  productionYear);
            if (list.Count < 1)
            {
                this.lblMsg2.Text = "Ther are no stacks in this shed.";
                this.btnSave.Enabled = false;
            }
            else
            {
                
                foreach (StackBLL s in list)
                {
                    this.cboStackNo.Items.Add(new ListItem(s.StackNumber, s.Id.ToString()));
                }
            }
        }
        protected void btnNotOkay_Click(object sender, EventArgs e)
        {
            bindGrid();
        }
        protected void btnCancelPopUp_Click(object sender, EventArgs e)
        {
            bindGrid();
        }
        private void clear()
        {
            ViewState["StackUnloaded"] = "";
            this.lblMsg2.Text = "";
            cboGradingCode.SelectedValue = "";
            this.cboShed.SelectedValue = "";
            this.lblCommodityGrade.Text = "";
            this.txtNumberOfBags.Text = "";
            this.txtDateDeposited.Text = "";
            this.cboStackNo.SelectedIndex  = -1;
            this.txtStackNoBags.Text = "";
            this.txtRemark.Text = "";
            bindGrid();
            LoadPage();

        }
        private void LoadPage()
        {
            List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
            ViewState["StackUnloaded"] = list;
            // load controls.'
            GradingResultBLL obj = new GradingResultBLL();

            //TODO : change by warhouse id.
            Guid warehouseId = UserBLL.GetCurrentWarehouse();

            //Loading Grading Results pending Unloading.
            this.cboGradingCode.Items.Clear();
            List<GradingResultBLL> grdinglist = new List<GradingResultBLL>();
            try
            {
                grdinglist = obj.GetAcceptedresultsPendingUnloading(warehouseId);
            }
            catch (Exception ex)
            {
                this.lblmsg.Text = ex.Message;
            }
            if (grdinglist != null)
            {
                if (grdinglist.Count > 0)
                {
                    this.cboGradingCode.Items.Add(new ListItem("Please Select Code", ""));
                    this.cboGradingCode.AppendDataBoundItems = true;
                    this.cboGradingCode.DataSource = grdinglist;
                    this.cboGradingCode.DataTextField = "GradingCode";
                    this.cboGradingCode.DataValueField = "Id";
                    this.cboGradingCode.DataBind();
                    this.cboGradingCode.AppendDataBoundItems = false;
                }
                else
                {
                    this.lblmsg.Text = "There are no pending results awaiting Unloading.";
                }
            }
            else
            {
                this.lblmsg.Text = "There are no pending results awaiting Unloading.";
            }

            

            //TODO Remove Comment

           //  Loading shedby warehouse.
            cboShed.Items.Clear();
            ShedBLL shed = new ShedBLL();
            List<ShedBLL> shedlist = new List<ShedBLL>();
            shedlist = shed.GetActiveShedByWarehouseId(warehouseId);
            cboShed.Items.Add(new ListItem("Select Shed",""));
            cboShed.AppendDataBoundItems = true;
            if (shedlist.Count > 0)
            {
                foreach (ShedBLL s in shedlist)
                {
                    cboShed.Items.Add(new ListItem(s.ShedNumber, s.Id.ToString()));
                }
                cboShed.AppendDataBoundItems = false;
                this.btnSave.Enabled = true;
            }
            else
            {
                this.btnSave.Enabled = false;
            }
        }
        private void selectedGradechanged()
        {
            // get the values for the hidden fileds.
            // Reciving Request Id , commmodity grade.
            Nullable<Guid> id = null;
            if (DataValidationBLL.isGUID(this.cboGradingCode.SelectedValue.ToString(), out id) == false)
            {
                this.lblmsg.Text = "please select grading code.";
                return;
            }
            else
            {
                GradingResultBLL obj = new GradingResultBLL();
                obj = obj.GetGradingResultById((Guid)id);
                hfTrackingNo.Value = obj.TrackingNo;
                if (obj != null)
                {
                        //Productionyearstack
                        this.lblProductionYear.Text = obj.ProductionYear.ToString();
                        ViewState["productionYear"] = obj.ProductionYear.ToString();


                        this.hfRecivingRequestId.Value = obj.CommodityDepositRequestId.ToString();
                        CommodityGradeBLL objCG = new CommodityGradeBLL();
                        string CGName;
                        CGName = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
                        ViewState["UnloadingCommGradeId"] = obj.CommodityGradeId;
                        this.lblCommodityGrade.Text = CGName;
                        lblCADateTime.Text = ((DateTime)obj.ClientAcceptanceTimeStamp).ToShortDateString();
                        //try
                        //{
                        //    cmpSampGen.ValueToCompare = ((DateTime)obj.ClientAcceptanceTimeStamp).ToShortDateString();
                        //}
                        //catch
                        //{
                        //    cmpSampGen.ValueToCompare = ((DateTime.Now).AddDays(-100)).ToShortDateString();
                        //}

                   
                    // Get Grading Id
                    GradingBLL objGrading = new GradingBLL();
                    objGrading = objGrading.GetById(obj.GradingId);
                    if (objGrading != null)
                    {
                        SamplingResultBLL objSamplingResult = new SamplingResultBLL();
                        objSamplingResult = objSamplingResult.GetSamplingResultById(objGrading.SamplingResultId);
                        if (objSamplingResult != null)
                        {
                            ViewState["SamplingNoBags"] = objSamplingResult.NumberOfBags;
                        }
                        else
                        {
                            this.lblmsg.Text = "An error has occured please try agin.If the error persists contact the administrator";
                            return;
                        }

                    }
                    else
                    {
                        this.lblmsg.Text = "An error has occured please try agin.If the error persists contact the administrator";
                        return;
                    }


                }
                else
                {
                    this.lblmsg.Text = "please select grading code and try again.";
                    return;
                }
                //Load Commodity Grade Bags
                List<BagTypeBLL> list = new List<BagTypeBLL>();
                BagTypeBLL objBagType = new BagTypeBLL();
                list = objBagType.GetCommodityGradeBag(obj.CommodityGradeId);
                this.cboBagType.Items.Clear();
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        this.cboBagType.Items.Add(new ListItem("Please Select Bag Type", ""));
                        foreach (BagTypeBLL i in list)
                        {
                            this.cboBagType.Items.Add(new ListItem(i.BagTypeName, i.Id.ToString()));
                        }
                    }
                }
            }

        }
        private void selectedGradechanged(Guid Id)
        {

            GradingResultBLL obj = new GradingResultBLL();
            obj = obj.GetGradingResultById(Id);
            this.cboGradingCode.Items.Add(new ListItem(obj.GradingCode.ToString(), obj.GradingCode.ToString()));
            this.cboGradingCode.SelectedValue = obj.GradingCode.ToString();
            this.
            hfTrackingNo.Value = obj.TrackingNo;
            if (obj != null)
            {

                this.hfRecivingRequestId.Value = obj.CommodityDepositRequestId.ToString();
                CommodityGradeBLL objCG = new CommodityGradeBLL();
                string CGName;
                CGName = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
                ViewState["UnloadingCommGradeId"] = obj.CommodityGradeId;
                this.lblCommodityGrade.Text = CGName;
                lblCADateTime.Text = ((DateTime)obj.ClientAcceptanceTimeStamp).ToShortDateString();
                //try
                //{
                //    cmpSampGen.ValueToCompare = ((DateTime)obj.ClientAcceptanceTimeStamp).ToShortDateString();
                //}
                //catch
                //{
                //    cmpSampGen.ValueToCompare = ((DateTime.Now).AddDays(-100)).ToShortDateString();
                //}
                // Get Grading Id
                GradingBLL objGrading = new GradingBLL();
                objGrading = objGrading.GetById(obj.GradingId);
                if (objGrading != null)
                {
                    SamplingResultBLL objSamplingResult = new SamplingResultBLL();
                    objSamplingResult = objSamplingResult.GetSamplingResultById(objGrading.SamplingResultId);
                    if (objSamplingResult != null)
                    {
                        ViewState["SamplingNoBags"] = objSamplingResult.NumberOfBags;
                    }
                    else
                    {
                        this.lblmsg.Text = "An error has occured please try agin.If the error persists contact the administrator";
                        return;
                    }

                }
                else
                {
                    this.lblmsg.Text = "An error has occured please try agin.If the error persists contact the administrator";
                    return;
                }


            }
            else
            {
                this.lblmsg.Text = "please select grading code and try again.";
                return;
            }
            //Load Commodity Grade Bags
            List<BagTypeBLL> list = new List<BagTypeBLL>();
            BagTypeBLL objBagType = new BagTypeBLL();
            list = objBagType.GetCommodityGradeBag(obj.CommodityGradeId);
            this.cboBagType.Items.Clear();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    this.cboBagType.Items.Add(new ListItem("Please Select Bag Type", ""));
                    foreach (BagTypeBLL i in list)
                    {
                        this.cboBagType.Items.Add(new ListItem(i.BagTypeName, i.Id.ToString()));
                    }
                }
            }


        }


        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        { 
            List<object> cmd = new List<object>();
            if (name == "btnSave")
            {
              
                cmd.Add(this.btnSave);
                return cmd;
            }
            else if (name == "cboGradingCode")
            {
                cmd.Add(this.cboGradingCode);
                return cmd;
            }
            else
            {
                return null;
            }
        }

        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            this.btnSave.UseSubmitBehavior = false;

            btnSave.OnClientClick = "javascript:";

            if (btnSave.CausesValidation)
            {
                btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
                btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }


        }
        private void stackBagsClear()
        {
            txtStackNoBags.Text = "";
            cboUnloadedBy.SelectedIndex = -1;
            txtRemark.Text = "";
        }



       
    }
}