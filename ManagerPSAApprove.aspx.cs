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
    public partial class ManagerPSAApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillGINStatus();
                FillLIC(drpLIC, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator 
                btnApprove.Style["visibility"] = "hidden";
                btnCancel.Style["visibility"] = "hidden";
                bool approved = false;
                if (Request.Params["approved"] != null && bool.TryParse(Request.Params["approved"], out approved))
                    Messages.SetMessage("SUCCESS: The PSA Approved and it is out of unapproved list!", Messages.MessageType.Success);
            }
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
        private void FillGINStatus()
        {
            GINBussiness.GINModel.FillGINStatus(drpLIC);
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
            gmList = GINModel.getUnApprovedPSAManager(clientId, warehouseReceipt, licID, PSANo, warehouseId);
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
                    for (int i = 1; drpLIC.Items.Count > i; i++)
                    {
                        if (drpLIC.Items[i].Text == gmList[0].LeadInventoryController)
                        {
                            drpLIC.SelectedIndex = i;
                            break;
                        }
                    }
                    if (drpLIC.SelectedIndex == 0)
                        Messages.SetMessage("LIC " + gmList[0].LeadInventoryController + " not found in the list", WarehouseApplication.Messages.MessageType.Warning);
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
            string psaApprovalInfoXML = "<PSAApproval>";
            string errorMsg = ""; DateTime dtValue = new DateTime();
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    DropDownList mStatus = (DropDownList)gvApproval.Rows[gvr.RowIndex].FindControl("drpManagerStatus");
                    if (mStatus.SelectedIndex <= 0)
                        errorMsg += "the client signed status must me selected! ";
                    TextBox mdate = (TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeManagerSigned");
                    TextBox mtime = (TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtTimeManagerSigned");
                    if (!DateTime.TryParse(mdate.Text + " " + mtime.Text, out dtValue) || dtValue > DateTime.Now)
                        errorMsg += "please select current client signed date time is invalid! ";
                    if (errorMsg != string.Empty)
                    {
                        errorMsg = "For the PSA with number '" + gvr.Cells[1].Text + "' " + errorMsg;
                        Messages.SetMessage(errorMsg, WarehouseApplication.Messages.MessageType.Error);
                        return;
                    }

                    psaApprovalInfoXML +=
                    "<PSAApprovalItem> <ID>" + gvApproval.DataKeys[gvr.RowIndex].Values["Id"].ToString() + "</ID>" +
                       "<ManagerSignedDate>" + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeManagerSigned")).Text + " " + ((TextBox)gvApproval.Rows[gvr.RowIndex].FindControl("txtTimeManagerSigned")).Text + "</ManagerSignedDate>" +
                       "<ManagerSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</ManagerSignedName>" +
                       "<PSAStatusID>" + CalculatePSAStatus(gvr) + "</PSAStatusID>" +
                       "</PSAApprovalItem>";
                }
            }
            psaApprovalInfoXML += "</PSAApproval>";
            GINModel.ApproveManagerPSA(psaApprovalInfoXML);
            Messages.SetMessage("SUCCESS: The PSA Approved and it is out of unapproved list!", Messages.MessageType.Success);
            BindData();
        }

        private object CalculatePSAStatus(GridViewRow gvr)
        {
            return Convert.ToInt32(gvApproval.DataKeys[gvr.RowIndex].Values["GINStatusID"]) *
            Convert.ToInt32(((DropDownList)gvApproval.Rows[gvr.RowIndex].FindControl("drpManagerStatus")).SelectedValue);
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

        protected void lbtnApprove_Click(object sender, EventArgs e)
        {

        }

        protected void gvApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Messages.SetMessage("gvApproval_SelectedIndexChanged", Messages.MessageType.Success);
            string psaApprovalInfoXML = "<PSAApproval>";
            string errorMsg = "";
            DateTime dtValue = new DateTime();
            DateTime dtValue1 = new DateTime();
            int rowIndex = gvApproval.SelectedRow.RowIndex;

            DropDownList mStatus = (DropDownList)gvApproval.Rows[rowIndex].FindControl("drpManagerStatus");
            if (mStatus.SelectedIndex <= 0)
                errorMsg += "the client signed status must me selected! ";
            TextBox mdate = (TextBox)gvApproval.Rows[rowIndex].FindControl("txtDateTimeManagerSigned");
            TextBox mtime = (TextBox)gvApproval.Rows[rowIndex].FindControl("txtTimeManagerSigned");
            if (!DateTime.TryParse(mdate.Text + " " + mtime.Text, out dtValue) || dtValue >= DateTime.Now)
                errorMsg += "please select current manager signed date time! ";
            Label lictime = (Label)gvApproval.Rows[rowIndex].FindControl("lblDateTimeLICSigned");
            if (DateTime.TryParse(mdate.Text + " " + mtime.Text, out dtValue) && DateTime.TryParse(lictime.Text, out dtValue1) &&
                 dtValue < dtValue1)
                errorMsg += "please select manager signed date time greater than LIC Signed Date Time!";
            if (errorMsg != string.Empty)
            {
                errorMsg = "The PSA selected for approval " + errorMsg;
                Messages.SetMessage(errorMsg, WarehouseApplication.Messages.MessageType.Error);
                return;
            }

            psaApprovalInfoXML +=
            "<PSAApprovalItem> <ID>" + gvApproval.DataKeys[rowIndex].Values["Id"].ToString() + "</ID>" +
               "<ManagerSignedDate>" + ((TextBox)gvApproval.Rows[rowIndex].FindControl("txtDateTimeManagerSigned")).Text + " " + ((TextBox)gvApproval.Rows[rowIndex].FindControl("txtTimeManagerSigned")).Text + "</ManagerSignedDate>" +
               "<ManagerSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</ManagerSignedName>" +
               "<PSAStatusID>" + CalculatePSAStatus(gvApproval.SelectedRow) + "</PSAStatusID>" +
               "<UserId>" + BLL.UserBLL.CurrentUser.UserId + "</UserId>" +
               "</PSAApprovalItem>";

            Label lblLICID = (Label)gvApproval.Rows[rowIndex].FindControl("lblLICID");
            Label lblLICShedID = (Label)gvApproval.Rows[rowIndex].FindControl("lblLICShedID");
            Label lblCommodityGradeID = (Label)gvApproval.Rows[rowIndex].FindControl("lblCommodityGradeID");
            Label lblProductionYear = (Label)gvApproval.Rows[rowIndex].FindControl("lblProductionYear");
            psaApprovalInfoXML += "</PSAApproval>";
            int selectedStatus = Convert.ToInt32(((DropDownList)gvApproval.Rows[gvApproval.SelectedRow.RowIndex].FindControl("drpManagerStatus")).SelectedValue);
            if (selectedStatus == 11)
            {
                Session.Add("psaApprovalInfoXML", psaApprovalInfoXML);
                Response.Redirect("InventoryControlAdjustment.aspx?shedId=" + lblLICShedID.Text + "&LICID=" + lblLICID.Text + "&CGID=" + lblCommodityGradeID.Text + "&PY=" + lblProductionYear.Text);
            }
            else if (selectedStatus == 13)
            {
                GINModel.ApproveManagerPSA(psaApprovalInfoXML);
                Messages.SetMessage("SUCCESS: The PSA rejected and it is out of unapproved list!", Messages.MessageType.Success);
                BindData();
            }
        }

        protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((Label)gvr.FindControl("lblstatusLIC")).Text == "Reject")
                {
                    ((DropDownList)gvr.FindControl("drpManagerStatus")).SelectedValue = "13";
                    ((DropDownList)gvr.FindControl("drpManagerStatus")).Enabled = false;
                }
            }


        }
    }
}
