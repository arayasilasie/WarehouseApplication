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
using WarehouseApplication.BLL;
using WarehouseApplication.DAL;
using System.Collections.Generic;
using WarehouseApplication.SECManager;
using System.Text;


namespace WarehouseApplication.UserControls
{
    public partial class AddDriverInformation : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string TransactionNo = "";
            TransactionNo = Request.QueryString["TranNo"];
          
            if(String.IsNullOrEmpty(TransactionNo) == true)
            {
                this.btnComplete.Enabled = false;
            }
            //Check if Driver Information is Added.

          

            if (IsPostBack != true)
            {
                ViewState["TruckRegisterId"] = null;
                ViewState["TrailerRegisterId"] = null;
                if (this.isNonTruck.Checked != true)
                {
                    
                    Page.Validate();
                }

                if (Session["CommodityRequestId"] != null)
                {
                    this.CommodityDepositRequestId.Value = Session["CommodityRequestId"].ToString();
                   
                }
                Guid Id;
                try
                {
                     Id = new Guid(this.CommodityDepositRequestId.Value.ToString());
                     ViewState["Id"] = Id.ToString();
                }
                catch
                {
                    this.lblMessage.Text = "An error has occured please try again.";
                    return;
                }

                BindData(Id);

                
               
            }
            ToggleNext();
            IsEditable();
            SingleDriver();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            bool isSaved = false;
            // Save the Driver Information.
            DriverInformation objDriverInfo = new DriverInformation();
            Guid CommodityDepositRequestId = new Guid(this.CommodityDepositRequestId.Value.ToString());

            Guid CreatedBy = UserBLL.GetCurrentUser();
            if (this.txtRemark.Text == null)
            {
                this.txtRemark.Text = String.Empty;
            }
            Guid TruckRegisterId = Guid.Empty ;
            

                if (txtPlateNo.Text != "" )
                {
                //Registering Truck
                TruckRegisterBLL objTruckRegister = new TruckRegisterBLL();
                objTruckRegister.IsTrailer = false;
                if (cboModelYear.SelectedValue.ToString() != "")
                {
                    objTruckRegister.TruckModelYearId = new Guid(cboModelYear.SelectedValue.ToString());
                }
                else
                {
                    this.lblMessage.Text = "Please Select Truck Model Year.";
                    return;
                }
                objTruckRegister.TruckNumber = txtPlateNo.Text;
                objTruckRegister.Status = TruckStatus.Active;
                objTruckRegister.Add();
                }
                if (txtTrailerPlateNo.Text != "")
                {
                    //Registering Truck
                    TruckRegisterBLL objTrailerTruckRegister = new TruckRegisterBLL();
                    objTrailerTruckRegister.IsTrailer = true;
                    if (cboTrailerModelYear.SelectedValue.ToString() != "")
                    {
                        objTrailerTruckRegister.TruckModelYearId = new Guid(cboTrailerModelYear.SelectedValue.ToString());
                    }
                    else
                    {
                        this.lblMessage.Text = "Please Select Trailer Model Year.";
                        return;
                    }
                    objTrailerTruckRegister.TruckNumber = txtTrailerPlateNo.Text;
                    objTrailerTruckRegister.Status = TruckStatus.Active;
                    objTrailerTruckRegister.Add();
                }
            

            DriverInformationBLL obj = new DriverInformationBLL(CommodityDepositRequestId,this.txtDriverName.Text,
                this.txtLicenseNo.Text,this.txtPlaceIssued.Text,this.txtPlateNo.Text,this.txtTrailerPlateNo.Text,
                1,this.txtRemark.Text,CreatedBy);
            try
            {

                isSaved = obj.SaveDriverInformation();
            }
            catch (DuplicateDriverInformationException ex)
            {
                this.lblMessage.Text = ex.msg;
                return;
            }
            catch( Exception ex)
            {
                throw ex;
            }
            if (isSaved == true)
            {
                this.lblMessage.Text = "Record Added Successfully";
                this.btnSave.Enabled = false;
                ClearForm();
                this.CommodityDepositRequestId.Value = CommodityDepositRequestId.ToString();
                BindData(CommodityDepositRequestId);
            }
            else
            {
                this.lblMessage.Text = "Unable to Add the data please check the form and try agian.";
            }
            ToggleNext();


        }
        public void BindData(Guid Id )
        {
            DataTable dt = new DataTable("tblDriverInfo");
            DataSet dsResult = new DataSet("DriverInformation");
            DriverInformation objDriverInfo = new DriverInformation();
            dsResult  =objDriverInfo.GetDriverInformationByReceivigRequestId(Id);
            if (dsResult != null)
            {
                dt = dsResult.Tables[0];
                this.gvDriverInformation.DataSource = dt;
                this.gvDriverInformation.DataBind();
            }
        }
        public void ClearForm()
        {
            this.lblMessage.Text = "";
            this.txtDriverName.Text  = "";
            this.txtLicenseNo.Text = "";
            this.txtPlaceIssued.Text = "";
            this.txtPlateNo.Text = "";
            this.txtRemark.Text = "";
            this.txtTrailerPlateNo.Text = "";
            cboTruckType_CascadingDropDown.SelectedValue = "";
            cboTruckType2_CascadingDropDown1.SelectedValue = "";
            cboTruckType.SelectedIndex = -1;
            cboTruckType2.SelectedIndex = -1;
            this.cboModelYear_CascadingDropDown.SelectedValue = "";
            this.cboTrailerModelYear_CascadingDropDown3.SelectedValue = "";
            this.cboTrailerModelYear.SelectedIndex = -1;

        }
        protected void gvDriverInformation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.lblMessage.Text = "";
            gvDriverInformation.EditIndex = e.NewEditIndex;
            Guid Id = new Guid(this.CommodityDepositRequestId.Value.ToString());
            BindData(Id);
            

        }
        protected void gvDriverInformation_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
        protected void gvDriverInformation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.lblMessage.Text = "";
            Guid DriverInformationId ;
            string DriverName, LicenseNo, LicenseIssuedPlace, PlateNumber, TrailerPlateNo, Remark;
            int Status;
            bool isSaved = false;
            GridViewRow row = gvDriverInformation.Rows[e.RowIndex];
            if (row != null)
            {
                TextBox txtDriverName =(TextBox) row.FindControl("txtEditDriverName");
                Label lblId = (Label)row.FindControl("lblId");
                TextBox txtLicenseNo = (TextBox)row.FindControl("txtLicenseNumber");
                TextBox txtPlaceIssued = (TextBox)row.FindControl("txtLicenseIssuedPlace");
                TextBox txtPlateNumber = (TextBox)row.FindControl("txtPlateNumber");
                TextBox txtTrailerPlateNo = (TextBox)row.FindControl("txtTrailerPlateNumber");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                DropDownList cboStatus = (DropDownList)row.FindControl("cboStatusEdit");

                if (lblId != null)
                {
                    DriverInformationId = new Guid(lblId.Text.ToString());
                }
                else
                {
                    lblMessage.Text = "An error occured can you please try agin.If the error persists contact the administrator.";
                    return;
                }
                if (txtDriverName != null)
                {
                    DriverName = txtDriverName.Text;
                }
                else
                {
                    lblMessage.Text = "Can not find Driver Name";
                    return;
                }
                if (txtLicenseNo != null)
                {
                    LicenseNo = txtLicenseNo.Text;
                }
                else
                {
                    lblMessage.Text = "Can not find License Number.";
                    return;
                }
                if (txtPlaceIssued != null)
                {
                    LicenseIssuedPlace = txtPlaceIssued.Text;
                }
                else
                {
                    lblMessage.Text = "Can not find License Place Issued.";
                    return;
                }
                if (txtPlateNo != null)
                {
                    PlateNumber = txtPlateNumber.Text;
                }
                else
                {
                    lblMessage.Text = "Can not find License Plate Number.";
                    return;
                }
                if (txtTrailerPlateNo != null)
                {
                    TrailerPlateNo = txtTrailerPlateNo.Text;
 
                }
                else
                {
                    lblMessage.Text = "Can not find Trailer Plate Number.";
                    return;
                }
                if (txtRemark != null)
                {
                    Remark = txtRemark.Text;
                }
                else
                {
                    lblMessage.Text = "Can not find Remark.";
                    return;
                }
                if (cboStatus != null)
                {
                   Status = Convert.ToInt32(cboStatus.SelectedValue.ToString());

                }
                else
                {
                    lblMessage.Text = "Can not find Status.";
                    return;
                }
                if (Status == 0)
                {
                    if (Remark == "" || Remark == String.Empty)
                    {
                        lblMessage.Text = "Please provide Cancellation reason.";
                        return;
                    }
                }
                DriverInformationBLL objEdit = new DriverInformationBLL();
                objEdit = objEdit.GetById(DriverInformationId);
                DriverInformationBLL obj = new DriverInformationBLL();
                obj.ReceivigRequestId =   new Guid(this.CommodityDepositRequestId.Value.ToString());
                obj.Id = DriverInformationId;
                obj.DriverName = DriverName;
                obj.LicenseNumber = LicenseNo;
                obj.LicenseIssuedPlace = LicenseIssuedPlace;
                obj.PlateNumber = PlateNumber;
                obj.TrailerPlateNumber = TrailerPlateNo;
                obj.Remark = Remark;
                obj.Status = Status;
                try
                {

                    isSaved = obj.EditDriverInformation(objEdit);
                    if (isSaved == true)
                    {
                        this.lblMessage.Text = "Data updated Successfully.";
                        gvDriverInformation.EditIndex = -1;


                        BindData(obj.ReceivigRequestId);
                    }
                    else
                    {
                        this.lblMessage.Text = "Unable to update data.Please try agian.";
                        gvDriverInformation.EditIndex = -1;
                        BindData(obj.ReceivigRequestId);
                    }
                }
                catch (GRNNotOnUpdateStatus exGRN )
                {
                    this.lblMessage.Text = exGRN.msg;
                    return;
                }
                catch ( Exception ex)
                {
                    throw ex;
                }
   
            }
            ToggleNext();
            SingleDriver();


        }
        protected void gvDriverInformation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          
        }
        protected void gvDriverInformation_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            this.lblMessage.Text = "";
            if (ViewState["Id"] != null)
            {
                gvDriverInformation.EditIndex = -1;
                this.CommodityDepositRequestId.Value = ViewState["Id"].ToString();
                Guid Id = new Guid(this.CommodityDepositRequestId.Value.ToString());
                BindData(Id);
            }
            else
            {
                this.lblMessage.Text = "Unable to update data.";
            }
            ToggleNext();
            SingleDriver();
        }
        protected void gvDriverInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void gvDriverInformation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.lblMessage.Text = "";
            this.gvDriverInformation.PageIndex = e.NewPageIndex;
            this.CommodityDepositRequestId.Value = Request.QueryString.Get("id");
            Guid Id = new Guid(this.CommodityDepositRequestId.Value.ToString());
            BindData(Id);
            
        }
        protected void btnComplete_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            Nullable<Guid> Id = null;
            string TransactionNo = "";
            TransactionNo = Request.QueryString["TranNo"];
            if (string.IsNullOrEmpty(TransactionNo))
            {
                return;
            }

            if (Session["CommodityRequestId"] == null)
            {
              
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailByTrackingNo(TransactionNo);
                Id = objCDR.Id;
                Session["CommodityRequestId"] = Id.ToString();
                
            }
            else
            {
                Id = new Guid(Session["CommodityRequestId"].ToString());
            }
           // if the Step is in Driver Info.
            //if (WFTransaction.GetMessage(TransactionNo) == "AddDriverInformation")
            //{
            //    WFTransaction.WorkFlowManager(TransactionNo);
            //}
            //Check if Driver Information is Added.
            List<DriverInformationBLL> listDriverInfo = null;
            DriverInformationBLL objDriverInfo = new DriverInformationBLL();
            if (Session["CommodityRequestId"] != null)
            {
                Guid CdrId = new Guid(Session["CommodityRequestId"].ToString());
                listDriverInfo = objDriverInfo.GetActiveDriverInformationByReceivigRequestId(CdrId);
                if (listDriverInfo == null)
                {
                    this.btnComplete.Enabled = false;
                }
                else
                {
                    if (listDriverInfo.Count <= 0)
                    {
                        this.btnComplete.Enabled = false;
                    }
                    else
                    {
                        if (Id != null)
                        {
                            ECXWF.CMessage msg = (ECXWF.CMessage)HttpContext.Current.Session["msg"];
                            if (msg != null)
                            {
                                if (msg.Name == "AddDriverInformation")
                                {
                                    WFTransaction.WorkFlowManager(TransactionNo);
                                    Response.Redirect("ListInbox.aspx");
                                }
                            }

                        }
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
            else if (name == "btnComplete")
            {
                cmd.Add(this.btnComplete);
                return cmd;
            }
            else if (name == "btnEdit")
            {
                //string Id = string.Empty;
                //Id = "btnEdit";
                //foreach (TableRow row in this.gvDriverInformation.Rows)
                //{
                //    if (row.FindControl(Id) != null)
                //    {
                //        cmd.Add(row.FindControl(Id));
                //    }
                //}
                //return cmd;
            }
            return null;
        }
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            this.btnComplete.UseSubmitBehavior = false;

            btnComplete.OnClientClick = "javascript:";

            //if (btnSave.CausesValidation)
            //    btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ) ";

            btnComplete.OnClientClick += "this.disabled=true; this.value='Please Wait...';";
        }
        #endregion

        protected void txtPlateNo_TextChanged(object sender, EventArgs e)
        {
            ViewState["TruckRegisterId"] = null;
            Nullable<Guid> TruckTypeId , TruckModelId;
            TruckTypeId = null;
            TruckModelId = null;

            TruckRegisterBLL obj = new TruckRegisterBLL();
            obj = obj.GetTruckInfoByTruckNumber(this.txtPlateNo.Text, false);
            if (obj != null)
            {
                TruckModelYearBLL objTMY = new TruckModelYearBLL();
                objTMY = objTMY.GetbyId(obj.TruckModelYearId);
                if (objTMY != null)
                {
                    TruckModelBLL objTM = new TruckModelBLL();
                    objTM = objTM.GetbyId(objTMY.TruckModelId);
                    if (objTM != null)
                    {
                        TruckTypeBLL objTT = new TruckTypeBLL();
                        objTT = objTT.GetbyId(objTM.TruckTypeId);
                        TruckModelId = objTM.Id;
                        if (objTT != null)
                        {
                            TruckTypeId = objTT.Id;
                        }
                    }

                }
   
           
            if (TruckTypeId != null)
            {
                this.cboTruckType_CascadingDropDown.SelectedValue = TruckTypeId.ToString();
               
            }
            if (TruckModelId != null)
            {
                this.cboTruckModel_CascadingDropDown.SelectedValue = TruckModelId.ToString();
               
            }
            if( obj.TruckModelYearId != null )
            {
                this.cboModelYear_CascadingDropDown.SelectedValue = obj.TruckModelYearId.ToString();
                
            }
            this.cboTruckModel.Enabled = false;
            ViewState["TruckRegisterId"] = obj.Id.ToString(); 
            
            }// Big if 

        }

        protected void txtTrailerPlateNo_TextChanged(object sender, EventArgs e)
        {
            ViewState["TrailerRegisterId"] = null;
            Nullable<Guid> TruckTypeId, TruckModelId;
            TruckTypeId = null;
            TruckModelId = null;

            TruckRegisterBLL obj = new TruckRegisterBLL();
            obj = obj.GetTruckInfoByTruckNumber(this.txtTrailerPlateNo.Text, true);
            if (obj != null)
            {
                TruckModelYearBLL objTMY = new TruckModelYearBLL();
                objTMY = objTMY.GetbyId(obj.TruckModelYearId);
                if (objTMY != null)
                {
                    TruckModelBLL objTM = new TruckModelBLL();
                    objTM = objTM.GetbyId(objTMY.TruckModelId);
                    if (objTM != null)
                    {
                        TruckTypeBLL objTT = new TruckTypeBLL();
                        objTT = objTT.GetbyId(objTM.TruckTypeId);
                        TruckModelId = objTM.Id;
                        if (objTT != null)
                        {
                            TruckTypeId = objTT.Id;
                        }
                    }

                }

           
            if (TruckTypeId != null)
            {
                this.cboTruckType2_CascadingDropDown1.SelectedValue = TruckTypeId.ToString();

            }
            if (TruckModelId != null)
            {
                this.cboTrailerModel_CascadingDropDown2.SelectedValue = TruckModelId.ToString();

            }
            if (obj.TruckModelYearId != null)
            {
                this.cboTrailerModelYear_CascadingDropDown3.SelectedValue = obj.TruckModelYearId.ToString();

            }
            this.cboTruckModel.Enabled = false;
            ViewState["TrailerRegisterId"] = obj.Id.ToString();
            }// Big if 
        }

        private void ToggleNext()
        {
            List<DriverInformationBLL> listDriverInfo = null;
            DriverInformationBLL objDriverInfo = new DriverInformationBLL();
            if (Session["CommodityRequestId"] != null)
            {
                Guid CdrId = new Guid(Session["CommodityRequestId"].ToString());
                listDriverInfo = objDriverInfo.GetActiveDriverInformationByReceivigRequestId(CdrId);
                if (listDriverInfo == null)
                {
                    this.btnComplete.Enabled = false;
                }
                else
                {
                    if (listDriverInfo.Count <= 0)
                    {
                        this.btnComplete.Enabled = false;
                    }
                    else
                    {
                        this.btnComplete.Enabled = true;
                    }
                }
            }
        }
        private void IsEditable()
        {
            string TransactionNo = "";
            TransactionNo = Request.QueryString["TranNo"];
            ECXWF.CMessage msg = (ECXWF.CMessage)HttpContext.Current.Session["msg"];
            if (msg != null)
            {
                if (msg.Name != "AddDriverInformation")
                {
                    //this.pnlDriverInfo.Enabled = false;
                    this.btnComplete.Enabled = false;
                    //this.lblMessage.Text = "You can't make modification as this data is used in other tasks.";
                }
            }
            else
            {
                this.btnComplete.Enabled = false;
            }
        }
        private void SingleDriver()
        {
            List<DriverInformationBLL> listDriverInfo = null;
            DriverInformationBLL objDriverInfo = new DriverInformationBLL();
            if (Session["CommodityRequestId"] != null)
            {
                Guid CdrId = new Guid(Session["CommodityRequestId"].ToString());
                listDriverInfo = objDriverInfo.GetActiveDriverInformationByReceivigRequestId(CdrId);
                if (listDriverInfo == null)
                {
                    this.btnComplete.Enabled = false;
                }
                else
                {
                    if (listDriverInfo.Count <= 0)
                    {
                        this.btnSave.Enabled = true;
                    }
                    else if(listDriverInfo.Count > 0)
                    {
                        this.btnSave.Enabled = false;
                    }
                }
            }
        }

    }
}