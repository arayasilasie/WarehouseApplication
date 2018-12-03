using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;
using System.Data;
using WarehouseApplication.BLL;
using System.Configuration;

namespace WarehouseApplication
{
    public partial class PSAApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillGINStatus();
                FillLIC(drpLIC, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator 
                btnApprove.Style["visibility"] = "hidden";
                btnCancel.Style["visibility"] = "hidden";
            }
        }
        private void FillGINStatus()
        {
            GINBussiness.GINModel.FillGINStatus(drpLIC);
        }
        private void FillLIC(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.Items.Clear();
            ddl.Items.Add(" - - Select LIC - - ");
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICAll(UserBLL.GetCurrentWarehouse());
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        private void BindData()
        {
            int warehouseReceipt;
            string clientId = txtClientId.Text;

            Guid? licID = null;
            if (drpLIC.SelectedIndex > 0)
                licID = new Guid(drpLIC.SelectedItem.Value);
            string PSANo = txtPSANo.Text;
            Guid warehouseId = UserBLL.GetCurrentWarehouse();

            if (txtWareHouseReceipt.Text.Equals(string.Empty))
                warehouseReceipt = 0;
            else
                warehouseReceipt = Convert.ToInt32(txtWareHouseReceipt.Text);
            //if (clientId.Trim().Length <= 0 && licID == null && warehouseReceipt == 0 && PSANo.Trim().Length <= 0)
            //{
            //    Messages.SetMessage("Please select/enter on one of the search criteria", WarehouseApplication.Messages.MessageType.Warning);
            //    return;
            //}

            List<GINModel> gmList = new List<GINModel>();
            gmList = GINModel.getUnApprovedPSA(clientId, warehouseReceipt, licID, PSANo, warehouseId);
            gvApproval.DataSource = gmList;
            gvApproval.DataBind();
            if ((txtPSANo.Text.Trim().Length > 0 || txtWareHouseReceipt.Text.Trim().Length > 0) && gmList != null && gmList.Count > 0)
                try
                {
                    drpLIC.SelectedValue = gmList[0].LeadInventoryControllerID.ToString();
                }
                catch
                {
                    drpLIC.SelectedIndex = 0;
                    for (int i=1; drpLIC.Items.Count > i; i++)
                    {
                        if (drpLIC.Items[i].Text == gmList[0].LeadInventoryController)
                        {
                            drpLIC.SelectedIndex = i;
                            break;
                        }
                    }
                    if (drpLIC.SelectedIndex == 0)
                        Messages.SetMessage("LIC " + gmList[0].LeadInventoryController + " not found in the list" , WarehouseApplication.Messages.MessageType.Warning);
                }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                ViewState["PSAId"] = new Guid(gvApproval.DataKeys[gvr.RowIndex].Value.ToString());
            }
            //Show the modalpopupextender
            ModalPopupExtender1.Show();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                GINModel objApp = new GINModel();

                objApp.ID = new Guid(ViewState["PSAId"].ToString());


                //objApp.Approve();
                Messages.SetMessage("SUCCESS: The PSA Approved and it is out of unapproved list!", Messages.MessageType.Success);
            }
            BindData();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            BindData();
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            string ginApprovalInfoXML = "<PSAApproval>";
            string errorMsg = ""; DateTime dtValue = new DateTime();
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    TextBox remark=(TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtrmrk");
                    if (string.IsNullOrWhiteSpace(remark.Text))
                    {
                        errorMsg += "Remark is Required! Please Insert Remark!";
                    }
                    DropDownList cStatus = (DropDownList)gvApproval.Rows[gvr.RowIndex].FindControl("drpClientStatus");
                    if (cStatus.SelectedIndex <= 0)
                        errorMsg += "the client signed status must me selected! ";
                    DropDownList licStatus = (DropDownList)gvApproval.Rows[gvr.RowIndex].FindControl("drpLICStatus");
                    if (licStatus.SelectedIndex <= 0)
                        errorMsg += "the LIC signed status must me selected! ";
                    TextBox cdate = (TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeClientSigned");
                    TextBox ctime = (TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtTimeClientSigned");
                    if (!DateTime.TryParse(cdate.Text + " " + ctime.Text, out dtValue) || dtValue > DateTime.Now)
                        errorMsg += "please select current Client signed date and time! ";
                    TextBox licdate = (TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeLICSigned");
                    TextBox lictime = (TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtTimeLICSigned");
                    if (!DateTime.TryParse(licdate.Text + " " + lictime.Text, out dtValue) || dtValue > DateTime.Now)
                        errorMsg += "please select current LIC signed date and time! ";
                    if (errorMsg != string.Empty)
                    {
                        errorMsg = "For the PSA with number '" + ((Label)gvr.FindControl("lblPSA")).Text + "' " + errorMsg;
                        Messages.SetMessage(errorMsg, WarehouseApplication.Messages.MessageType.Error);
                        return;
                    }
                    ginApprovalInfoXML +=
                    "<PSAApprovalItem> <ID>" + gvApproval.DataKeys[gvr.RowIndex].Value.ToString() + "</ID>" +
                    "<Remark>" + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtrmrk")).Text + "</Remark>" +
                      "<ClientSignedDate>" + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeClientSigned")).Text + " " + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtTimeClientSigned")).Text + "</ClientSignedDate>" +
                       "<ClientSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</ClientSignedName>" +
                       "<LICSignedDate>" + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeLICSigned")).Text + " " + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtTimeLICSigned")).Text + "</LICSignedDate>" +
                       "<LICSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</LICSignedName>" +
                       "<PSAStatusID>" + CalculatePSAStatus(gvr) + "</PSAStatusID>" +
                       "<PSAStatusID>" + CalculatePSAStatus(gvr) + "</PSAStatusID>" +
                       "<UserId>" + BLL.UserBLL.CurrentUser.UserId + "</UserId>" +
                       "</PSAApprovalItem>";
                }
            }
            ginApprovalInfoXML += "</PSAApproval>";
            GINModel.ApprovePSA(ginApprovalInfoXML);
            Messages.SetMessage("SUCCESS: The PSA Approved and it is out of unapproved list!", Messages.MessageType.Success);
            btnApprove.Style["visibility"] = "hidden";
            BindData();

        }
        private object CalculatePSAStatus(GridViewRow gvr)
        {
            return Convert.ToInt32(((DropDownList)gvApproval.Rows[gvr.RowIndex].FindControl("drpClientStatus")).SelectedValue) *
                Convert.ToInt32(((DropDownList)gvApproval.Rows[gvr.RowIndex].FindControl("drpLICStatus")).SelectedValue);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string ginIds = string.Empty;
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    if (!string.IsNullOrEmpty(ginIds))
                        ginIds += ",";
                    ginIds += gvApproval.DataKeys[gvr.RowIndex].Value.ToString();
                }
            }
            GINModel.CancelGIN(ginIds);
            BindData();
        }
        protected void drpClientStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ginIds = string.Empty;
            bool chkedExits = false;
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                    chkedExits = true;

                if (((DropDownList)gvr.FindControl("drpClientStatus")).SelectedItem.Text == "Reject")
                {
                    ((DropDownList)gvr.FindControl("drpLICStatus")).SelectedValue = "7";
                    ((DropDownList)gvr.FindControl("drpLICStatus")).Enabled = false;
                }
                else if (((DropDownList)gvr.FindControl("drpClientStatus")).SelectedItem.Text == "Accept")
                {
                    ((DropDownList)gvr.FindControl("drpLICStatus")).Enabled = true;
                }
            }

            if (chkedExits)
                btnApprove.Style["visibility"] = "visible";
            else
                btnApprove.Style["visibility"] = "hidden";
            //btnCancel.Style["visibility"] = "visible";
        }    
    }
}
