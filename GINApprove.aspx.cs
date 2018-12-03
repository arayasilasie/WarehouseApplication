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
//test
namespace WarehouseApplication
{
    public partial class GINApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillLIC(drpLIC, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator 
                drpLIC.Items.Insert(0, new ListItem("--Select LIC---", string.Empty));
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
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICAll(UserBLL.GetCurrentWarehouse());
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Name";
            ddl.DataBind();
        }
        private void BindData()
        {
            int warehouseReceipt;
            string LICName;
            string clientId = txtClientId.Text;
            if (drpLIC.SelectedItem.Value != string.Empty)
                LICName = drpLIC.SelectedItem.Value.ToString();
            else
                LICName = string.Empty;
            string GinNo = txtGINNo.Text;
            Guid warehouseId = UserBLL.GetCurrentWarehouse();

            if (txtWareHouseReceipt.Text.Equals(string.Empty))
                warehouseReceipt = 0;
            else
                warehouseReceipt = Convert.ToInt32(txtWareHouseReceipt.Text);


            List<GINModel> gmList = new List<GINModel>();
            gmList = GINModel.getUnApprovedGin(clientId, warehouseReceipt, LICName, GinNo, warehouseId);
            gvApproval.DataSource = gmList;
            gvApproval.DataBind();
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                Session["GinId"] = new Guid(gvApproval.DataKeys[gvr.RowIndex].Value.ToString());
            }
            //Show the modalpopupextender
            //ModalPopupExtender1.Show();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                GINModel objApp = new GINModel();
                objApp.ID = new Guid(Session["GinId"].ToString());
                Messages.SetMessage("SUCCESS: The Gin Approved and it is out of unapproved list!", Messages.MessageType.Success);
            }
            BindData();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string ginApprovalInfoXML = "<GINApproval>";
            DateTime ClientSignedDate, LICSignedDate,DateIssued;
            string GINNO;
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    ClientSignedDate = Convert.ToDateTime(((TextBox)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("txtDateTimeClientSigned")).Text + " " + ((TextBox)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("txtTimeClientSigned")).Text);
                    LICSignedDate = Convert.ToDateTime(((TextBox)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("txtDateTimeLICSigned")).Text + " " + ((TextBox)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("txtTimeLICSigned")).Text);
                    
                    DateIssued=Convert.ToDateTime((gvApproval.Rows[gvr.RowIndex].Cells[7].Text));
                    GINNO = ((Label)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("lblGINNo")).Text;

                    if ((ClientSignedDate > DateTime.Now) || (LICSignedDate > DateTime.Now))
                    {
                        Messages.SetMessage(" Client Signed Date and LIC Signed Date MUST NOT BE greater than todday!! on GIN Number" + GINNO, Messages.MessageType.Error);
                        return;
                    }
                    if (ClientSignedDate >= DateIssued && LICSignedDate >= ClientSignedDate)
                    {
                        ginApprovalInfoXML +=
                        "<GINApprovalItem> <ID>" + gvApproval.DataKeys[gvr.RowIndex].Value.ToString() + "</ID>" +
                          "<ClientSignedDate>" + ClientSignedDate + "</ClientSignedDate>" +
                           "<ClientSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</ClientSignedName>" +
                           "<LICSignedDate>" + LICSignedDate + "</LICSignedDate>" +
                           "<LICSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</LICSignedName>" +
                           "<GINStatusID>" + CalculateGINStatus(gvr) + "</GINStatusID>" +
                           "</GINApprovalItem>";
                    }
                    else
                    {
                        Messages.SetMessage(" Client Signed Date and LIC Signed Date have to greater or equal to Date Issued. on GIN Number" + GINNO, Messages.MessageType.Warning);
                        return;
                    }
                }
            }
            ginApprovalInfoXML += "</GINApproval>";
            GINModel.Approve(ginApprovalInfoXML);
            BindData();

            Messages.SetMessage("The Gin Approved and it is out of unapproved list!", Messages.MessageType.Success);

        }
        private object CalculateGINStatus(GridViewRow gvr)
        {
            return Convert.ToInt32(((DropDownList)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("drpClientStatus")).SelectedValue) *
                Convert.ToInt32(((DropDownList)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("drpLICStatus")).SelectedValue);
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
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
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
            btnApprove.Style["visibility"] = "visible";
            btnCancel.Style["visibility"] = "visible";
        }
    }
}
