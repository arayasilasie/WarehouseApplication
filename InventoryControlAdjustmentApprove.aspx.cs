using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using InventoryControlBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class InventoryControlAdjustmentApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid currentWarehouse = UserBLL.GetCurrentWarehouse();
                FillShed(currentWarehouse);
                gvInventoryDetail.DataSource = new DataTable();
                gvInventoryDetail.DataBind();
            }
            unBlockUI();
        }

        void unBlockUI()
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(),
                           "unBlockUI", "<script type='text/javascript'>$.unblockUI();</script>", false);
        }

        private void FillShed(Guid warehouseID)
        {
            drpShed.DataSource = null;
            drpShed.Items.Clear();
            drpShed.Items.Add(new ListItem(" - - Select Shed - - ", ""));
            drpShed.DataSource = InventoryControlModel.GetShedForApproval(warehouseID);
            drpShed.DataValueField = "ShedID";
            drpShed.DataTextField = "ShedNo";
            drpShed.DataBind();
        }

        private void GetLICForApproval(Guid shedID)
        {
            drpLIC.DataSource = null;
            drpLIC.Items.Clear();
            drpLIC.Items.Add(new ListItem(" - - Select LIC - - ", ""));
            drpLIC.DataSource = InventoryControlModel.GetLICForApproval(shedID);
            drpLIC.DataValueField = "ID";
            drpLIC.DataTextField = "Name";
            drpLIC.DataBind();
        }

        private void GetInventoryDates(Guid shedID, Guid LICID)
        {
            drpInventoryControlDate.DataSource = null;
            drpInventoryControlDate.Items.Clear();
            drpInventoryControlDate.Items.Add(new ListItem(" - - Select Inventory Date - - ", ""));
            drpInventoryControlDate.DataSource = InventoryControlModel.GetInvDatesForInvApproval(shedID, LICID);
            drpInventoryControlDate.DataValueField = "ID";
            drpInventoryControlDate.DataTextField = "InventoryDate";
            drpInventoryControlDate.DataBind();
        }

        private void FillInventoryDetail(Guid shedID, Guid LICID, DateTime inventoryDate)
        {
            gvInventoryDetail.DataSource = InventoryControlModel.GetInvDetailByForApproval(shedID, LICID, inventoryDate);
            gvInventoryDetail.DataBind();
        }

        private bool ControlsValidated()
        {
            //validate combo and grids
            string errorMessage = "";
            if (drpShed.SelectedIndex <= 0)
                errorMessage += "You must select Shed! ";
            else if (drpLIC.SelectedIndex <= 0)
                errorMessage += "You must select Lead Inventory Controler! ";
            else if (drpInventoryControlDate.SelectedIndex <= 0)
                errorMessage += "You must select Inventory Control Date!";

            if (errorMessage != string.Empty)
            {
                Messages1.SetMessage(errorMessage, Messages.MessageType.Error);
                return false;
            }
            return true;
        }

        private InventoryControlModel SetValues()
        {
            if (gvInventoryDetail.Rows.Count <= 0){
                Messages1.SetMessage("You must add atleast one inventory details to save the approval!", Messages.MessageType.Error);
                return null;
            }

            InventoryControlModel icm = new InventoryControlModel();

            InventoryDetail invdetail;
            string status = "";
            foreach (GridViewRow gr in gvInventoryDetail.Rows)
            {
                status = ((RadioButtonList)gr.FindControl("chkApproval")).SelectedValue;
                invdetail = new InventoryDetail();
                if (status == "Approve") 
                    invdetail.Status = (int)InventoryDetailStatus.Approved;
                else if (status == "Reject") 
                    invdetail.Status = (int)InventoryDetailStatus.Rejected;
                else 
                    continue;
                invdetail.ID = new Guid(((Label)gr.FindControl("lblID")).Text);
                invdetail.InventoryID = new Guid(((Label)gr.FindControl("lblInventoryID")).Text);
                invdetail.StackID = new Guid(((Label)gr.FindControl("lblStackID")).Text);
                invdetail.LastModifiedBy = UserBLL.CurrentUser.UserId;
                invdetail.LastModifiedTimestamp = DateTime.Now;
                invdetail.ApprovalDate = DateTime.Now;
                invdetail.ApprovedByID = UserBLL.CurrentUser.UserId;
                icm.addInventoryDetail(invdetail);
            }
            if (icm.inventoryDetailList!= null && icm.inventoryDetailList.Count > 0)
                icm.ID = icm.inventoryDetailList[0].InventoryID;
            return icm;
        }

        private void Clear()
        {
            drpLIC.ClearSelection();
            drpShed.ClearSelection();
            gvInventoryDetail.DataSource = new DataTable();
            gvInventoryDetail.DataBind();
            btnSave.Enabled = false;
            FillShed(UserBLL.GetCurrentWarehouse());
            drpShed_SelectedIndexChanged(null, null);
        }

        protected void drpShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpLIC.Items.Clear();
            drpInventoryControlDate.Items.Clear();
            gvInventoryDetail.DataSource = new DataTable();
            gvInventoryDetail.DataBind();
            btnSave.Enabled = false;
            if (drpShed.SelectedIndex > 0)
            {
                GetLICForApproval(new Guid(drpShed.SelectedValue));
            }
        }

        protected void drpLIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpInventoryControlDate.Items.Clear();
            gvInventoryDetail.DataSource = new DataTable();
            gvInventoryDetail.DataBind();
            btnSave.Enabled = false;
            if (drpLIC.SelectedIndex > 0)
            {
                GetInventoryDates(new Guid(drpShed.SelectedValue), new Guid(drpLIC.SelectedValue));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            if (!ControlsValidated()) return;
            FillInventoryDetail(new Guid(drpShed.SelectedValue), new Guid(drpLIC.SelectedValue), 
                                                DateTime.Parse(drpInventoryControlDate.SelectedItem.Text));
            btnSave.Enabled = gvInventoryDetail.Rows.Count > 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();

            InventoryControlModel icm = SetValues();
            if (icm.inventoryDetailList == null || icm.inventoryDetailList.Count <= 0)
            {
                Messages1.SetMessage("Noting to update no list is approved or rejected!", Messages.MessageType.Warning);
                return;
            }
            if (icm != null)
            {
                icm.Approve();
                Messages1.SetMessage("Inventory Adjustment was succesfully saved!", Messages.MessageType.Success);
                Clear();
            }
        }
    }
}