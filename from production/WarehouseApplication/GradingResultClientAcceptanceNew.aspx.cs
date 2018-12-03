using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using GradingBussiness;
using System.Collections.Generic;
using WarehouseApplication.BLL;
using GINBussiness;

namespace WarehouseApplication
{
    public partial class GradingResultClientAcceptanceNew : System.Web.UI.Page
    {
        private string GradingCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            if (!IsPostBack)
            {
               
                if (Request.QueryString["GradingCode"] == null)
                {
                    Messages.SetMessage("Please Log out and try again.", WarehouseApplication.Messages.MessageType.Error);                   
                    return;
                }
                if (Convert.ToBoolean(Request.QueryString["EditMode"]) == true)
                {
                    Session["EditMode"] = true;
                }
                else
                    Session["EditMode"] = false;
                Session["GradingCode"] = Request.QueryString["GradingCode"];
                GradingCode = Request.QueryString["GradingCode"];
                List<GradingModel> lst = GradingModel.GetgradingsInfo(GradingCode.ToString());
                ViewState["GradingCode"] = Request.QueryString["GradingCode"];
              
                lblGradeCode.Text = GradingCode.ToString();
                lblProductionYearValue.Text = lst[0].ProductionYear.ToString();
                FillShed(drpShed, lst[0].ProductionYear, lst[0].CommodityGradeID);
                drpShed.Items.Insert(0, new ListItem("Select", string.Empty));
                FillGradingResultStatus(cboGradingRecivedStatus);
                cboGradingRecivedStatus.Items.Insert(0, new ListItem("Select", string.Empty));
                FillGradingsStatus(cboAcceptanceStatus);
                cboAcceptanceStatus.Items.Insert(0, new ListItem("Select", string.Empty));
                FillSecurityMarshal(drpSecurityMarshal);
                drpSecurityMarshal.Items.Insert(0, new ListItem("Select", string.Empty));
                GetGradings(lst);
                if (Session["EditMode"] != null)
                {
                    bool editMode = (bool)Session["EditMode"];
                    if (editMode)
                    {
                        RePopulateGradings();
                    }
                }
            }
        }
        private void RePopulateGradings()
        {

            List<GradingModel> lst = GradingModel.GetGradingsEdit(GradingCode.ToString());
            if (lst[0].GradingResultStatusID == (int)GradingBussiness.GradingResultStatus.SegrigationRequested)
            {               
                cboAcceptanceStatus.SelectedValue = "3";//Set the status accepted by default              
                pnlSegregationRequested.Visible = false;
                pnlSegrigationNo.Visible = true;              
                txtSegrigationNo.Text = lst[0].NumberofSeparations.ToString();              
                cboGradingRecivedStatus.SelectedValue = lst[0].GradingResultStatusID.ToString();
                cboAcceptanceStatus.SelectedValue = lst[0].GradingsStatusID.ToString();
            }

            if ( lst[0].GradingsStatusID.ToString() == "5")
            {
                drpShed.Visible = false;
                drpLIC.Visible = false;
                lblShed.Text = "Cash Receipt No";
                lblLIC.Text = "Amount";
                txtAmount.Visible = true;
                txtCashReceiptNo.Visible = true;
                txtAmount.Enabled = false;
                txtCashReceiptNo.Enabled = false;
                drpLIC.ClearSelection();
                drpShed.ClearSelection();              
            }
            else  if (lst[0].GradingsStatusID.ToString() == "4")
            {
                drpShed.Visible = false;
                drpLIC.Visible = false;
                lblShed.Text = "Cash Receipt No";
                lblLIC.Text = "Amount";
                txtAmount.Visible = true;
                txtCashReceiptNo.Visible = true;

                drpLIC.ClearSelection();
                drpShed.ClearSelection();

                txtCashReceiptNo.Text = lst[0].CashReceiptNo.ToString();
                txtAmount.Text = lst[0].Amount.ToString();
            }
            else
            {
                if (lst[0].GradingResultStatusID != (int)GradingBussiness.GradingResultStatus.SegrigationRequested)
                {

                    drpShed.SelectedValue = lst[0].ShedID.ToString();
                    drpShed.SelectedItem.Text = lst[0].ShedNo;
                    FillLIC(drpLIC);
                    drpLIC.SelectedValue = lst[0].LICID.ToString();
                }
            }
            cboGradingRecivedStatus.SelectedValue = lst[0].GradingResultStatusID.ToString();
            cboAcceptanceStatus.SelectedValue = lst[0].GradingsStatusID.ToString();
            drpSecurityMarshal.SelectedValue = lst[0].SecurityMarshalID.ToString();
            txtDateOfAcceptance.Text = lst[0].ClientAcceptanceTimeStamp.ToShortDateString();
            txtTimeodAcceptance.Text = lst[0].ClientAcceptanceTimeStamp.ToShortTimeString();
        }
        private void GetGradings(List<GradingModel> lst)
        {
            
            ViewState["ID"] = lst[0].ID;
            lblClientValue.Text = lst[0].ClientName;
            lblCommodityGradeValue.Text = lst[0].CommodityGradeName;
            if (lst[0].GRNID != Guid.Empty)
            {
                btnUpdate.Visible = false;
                Messages.SetMessage("GRN No assigned for this data; it is not editable.", WarehouseApplication.Messages.MessageType.Warning);
            }
            if (lst[0].NewSampleCodeGenerated == true)
            {
                btnUpdate.Visible = false;
                Messages.SetMessage("New Sample Code Generated for this data; it is not editable.", WarehouseApplication.Messages.MessageType.Warning);
            }
            if (lst[0].GradingResultStatusID == (int)GradingBussiness.GradingResultStatus.MoistureFailed || lst[0].GradingResultStatusID == (int)GradingBussiness.GradingResultStatus.GeneralRequirmentfail || lst[0].GradingResultStatusID == (int)GradingBussiness.GradingResultStatus.Sort)
            {
                btnUpdate.Visible = false;
                Messages.SetMessage("For grading Result Sort ,Moisture Failed and General Requirment fail Client Acceptance not Available.", WarehouseApplication.Messages.MessageType.Warning);
            }
            this.cmpSampGen.ValueToCompare = lst[0].GradeRecivedDateTime.ToShortDateString();

            lblGradingReceivedDateValue.Text = lst[0].GradeRecivedDateTime.ToShortDateString() + " " + lst[0].GradeRecivedDateTime.ToShortTimeString();
            GradingReceivedDatehiden.Value = lblGradingReceivedDateValue.Text;
            cboGradingRecivedStatus.SelectedValue = lst[0].GradingResultStatusID.ToString();
            cboGradingRecivedStatus.Enabled = false;
            if (lst[0].GradingResultStatusID == (int)GradingBussiness.GradingResultStatus.SegrigationRequested)
            {
                 
                cboAcceptanceStatus.SelectedValue = "3";//Set the status accepted by default                
                pnlSegregationRequested.Visible = false;
                pnlSegrigationNo.Visible = true;               
                txtSegrigationNo.Text = lst[0].NumberofSeparations.ToString();
                txtDateOfAcceptance.Text = DateTime.Now.ToShortDateString();
                txtTimeodAcceptance.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                pnlSegregationRequested.Visible = true;
                pnlSegrigationNo.Visible = false;
            }
        }
        private void FillSecurityMarshal(DropDownList ddl)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.WarehouseOperators(UserBLL.GetCurrentWarehouse()).FindAll(p => p.Type % (int)19 == 0);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        private void FillLIC(DropDownList ddl)
        {
             List<GradingModel> lst=null;
             if (drpShed.SelectedIndex > 0)
                 lst = GradingModel.LICs(new Guid(drpShed.SelectedValue));
            if (drpShed.SelectedValue == string.Empty)
            {
                 
                ddl.DataSource = null;
                ddl.DataBind();
            }
                    
            if (lst!=null&& lst.Count > 0)
            {
                
                ddl.DataSource = lst;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "ID";
                ddl.DataBind();
            }
          
        }
        private void FillShed(DropDownList ddl, int ProductionYear, Guid CommodityGradeID)
        {
            ddl.DataSource = null;
            ddl.DataSource = GradingBussiness.GradingModel.Shed(BLL.UserBLL.GetCurrentWarehouse(), ProductionYear, CommodityGradeID);
            ddl.DataTextField = "ShedNo";
            ddl.DataValueField = "ShedID";
            ddl.DataBind();
        }
        private void FillGradingResultStatus(DropDownList ddl)
        {
            ddl.DataSource = null;
            ddl.DataSource = GradingBussiness.GradingModel.GetGradingResultStatus();
            ddl.DataTextField = "GradingResultStatus";
            ddl.DataValueField = "GradingResultStatusID";
            ddl.DataBind();
        }
        private void FillGradingsStatus(DropDownList ddl)
        {
            ddl.DataSource = null;
            ddl.DataSource = GradingBussiness.GradingModel.GetGradingsStatus();
            ddl.DataTextField = "GradingsStatus";
            ddl.DataValueField = "GradingsStatusID";
            ddl.DataBind();
        }
        protected void drpShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLIC(drpLIC);
            drpLIC.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void cboAcceptanceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAcceptanceStatus.SelectedValue == "5")
            {
                drpLIC.ClearSelection();
                drpShed.ClearSelection();
                drpShed.Visible = true;
                drpLIC.Visible = true;
                drpShed.Enabled = false;
                drpLIC.Enabled = false;
                txtAmount.Visible = false;
                txtCashReceiptNo.Visible = false;
                lblShed.Text = "Shed";
                lblLIC.Text = "LIC";
            }
            else if (cboAcceptanceStatus.SelectedValue == "4")
            {
                drpShed.Visible = false;
                drpLIC.Visible = false;
                lblShed.Text = "Cash Receipt No";
                lblLIC.Text = "Amount";
                txtAmount.Visible = true;
                txtCashReceiptNo.Visible = true;
                drpLIC.ClearSelection();
                drpShed.ClearSelection();
            }
            else
            {
                lblShed.Text = "Shed";
                lblLIC.Text = "LIC";
                txtAmount.Visible = false;
                txtCashReceiptNo.Visible = false;
                drpShed.Enabled = true;
                drpLIC.Enabled = true;
                drpShed.Visible = true;
                drpLIC.Visible = true;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (doValidMain())
            {
                GradingModel objSave = new GradingModel();
                if((bool)(Session["EditMode"]))
                    objSave.Edit=true;
                else
                     objSave.Edit=false;
                objSave.ID = new Guid(ViewState["ID"].ToString());
                objSave.GradingCode = ViewState["GradingCode"].ToString();
                if (txtSegrigationNo.Visible)
                {
                    Messages.ClearMessage();
                    if (txtSegrigationNo.Text.Trim() == string.Empty)
                    {
                        Messages.SetMessage("Segrigation No is required .", WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }
                    if (cboAcceptanceStatus.SelectedIndex == 0)
                    {
                        Messages.SetMessage("Status is required .", WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }
                    objSave.NumberofSeparations = int.Parse(txtSegrigationNo.Text);
                    objSave.GradingResultStatusID = Convert.ToInt32(cboGradingRecivedStatus.SelectedValue.ToString());
                    objSave.GradingsStatusID = Convert.ToInt32(cboAcceptanceStatus.SelectedValue.ToString());
                    objSave.UserId = BLL.UserBLL.GetCurrentUser();
                    objSave.UpdateAcceptanceForSeg();
                    btnUpdate.Visible = false;
                    btnNext.Visible = true;
                    Messages.SetMessage("Record updated successfully.", WarehouseApplication.Messages.MessageType.Success);
                    btnPrint.Visible = true;
                    return;
                }
                if (cboAcceptanceStatus.SelectedValue.ToString() == "4" )
                {
                    Messages.ClearMessage();
                    if (txtCashReceiptNo.Text.Trim() == string.Empty)
                    {
                        Messages.SetMessage("Cash Receipt No is required .", WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }
                    if (txtAmount.Text.Trim() == string.Empty)
                    {
                        Messages.SetMessage("Amount is required .", WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }
                    objSave.LICID = Guid.Empty;
                    objSave.ShedID = Guid.Empty;
                    objSave.ShedNo = string.Empty;
                    objSave.CashReceiptNo = txtCashReceiptNo.Text;
                    objSave.Amount = Convert.ToDecimal(txtAmount.Text);
                }
                else if ( cboAcceptanceStatus.SelectedValue.ToString() == "5")
                {
                    objSave.LICID = Guid.Empty;
                    objSave.ShedID = Guid.Empty;
                    objSave.ShedNo = string.Empty;
                    objSave.CashReceiptNo = string.Empty;
                    objSave.Amount = 0;
                }
                else if (cboAcceptanceStatus.SelectedValue.ToString() == "3")
                {
                    Messages.ClearMessage();
                    if (drpLIC.SelectedIndex == 0)
                    {
                        Messages.SetMessage("LIC is required .", WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }
                    if (drpShed.SelectedIndex == 0)
                    {
                        Messages.SetMessage("Shed is required .", WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }
                    objSave.LICID = new Guid(drpLIC.SelectedValue);
                    objSave.ShedID = new Guid(drpShed.SelectedValue);
                    objSave.ShedNo = drpShed.SelectedItem.Text;
                    objSave.CashReceiptNo = string.Empty;
                    objSave.Amount = 0;
                }
                objSave.UserId = BLL.UserBLL.GetCurrentUser();
                objSave.ClientAcceptanceTimeStamp = Convert.ToDateTime(txtDateOfAcceptance.Text + " " + txtTimeodAcceptance.Text);
                objSave.GradingResultStatusID = Convert.ToInt32(cboGradingRecivedStatus.SelectedValue.ToString());
                objSave.GradingsStatusID = Convert.ToInt32(cboAcceptanceStatus.SelectedValue.ToString());
                
                if(drpSecurityMarshal.SelectedIndex==0)
                    objSave.SecurityMarshalID =Guid.Empty;
                else
                objSave.SecurityMarshalID = new Guid(drpSecurityMarshal.SelectedValue);
                objSave.UpdateAcceptance();
                btnNext.Visible = true;
                Messages.SetMessage("successfully Updated.", WarehouseApplication.Messages.MessageType.Success);
                btnPrint.Visible = true;
                btnUpdate.Visible = false;
            }
        }
        private bool doValidMain()
        {
            Messages.ClearMessage();
            if (txtDateOfAcceptance.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Please enter date of acceptance.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (lblGradingReceivedDateValue.Text.Trim() == string.Empty)
            {
                Messages.SetMessage(" Please enter grading received date.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (Convert.ToDateTime(txtDateOfAcceptance.Text + " " + txtTimeodAcceptance.Text) <= Convert.ToDateTime(lblGradingReceivedDateValue.Text))
            {
                Messages.SetMessage("Date of acceptance must be later than Grading received date.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }

            if (cboAcceptanceStatus.SelectedIndex == 0 )
            {
                Messages.SetMessage("Status is required.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            return true;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (pnlSegrigationNo.Visible)
            {
                Session["ReportType"] = "GradingResultForSegrigation";
            }
            else if (cboAcceptanceStatus.SelectedValue == "4")
            {
                Session["ReportType"] = "GradingResultNoDeposit";
            }
            else
            {
                Session["ReportType"] = "GradingResult";
            }

            ScriptManager.RegisterStartupScript(this,
                                                this.GetType(),
                                                "ShowReport",
                                                "<script type=\"text/javascript\">" +
                                                string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                "</script>",
                                                false);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }
    }
}
