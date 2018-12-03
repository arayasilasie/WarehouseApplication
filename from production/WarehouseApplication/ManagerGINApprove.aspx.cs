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
    public partial class ManagerGINApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["btnApprove"] = true;
                //FillGINStatus();
                FillLIC(drpLIC, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator 
                drpLIC.Items.Insert(0, new ListItem("--Select LIC---", string.Empty));
                btnApprove.Style["visibility"] = "hidden";
                btnCancel.Style["visibility"] = "hidden";
            }
        }
        private void FillLIC(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICAll(UserBLL.GetCurrentWarehouse());
           
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Name";
            ddl.DataBind();
        }
        private void FillGINStatus()
        {
            GINBussiness.GINModel.FillGINStatus(drpLIC);
        }
        private void BindData()
        {
            int warehouseReceipt;
            string licName;
            string clientId = txtClientId.Text;
            if (drpLIC.SelectedItem.Value != string.Empty)
                licName = drpLIC.SelectedItem.Value.ToString();
            else
                licName = string.Empty;          
            string GinNo = txtGINNo.Text;
            Guid warehouseId = UserBLL.GetCurrentWarehouse();
            if (txtWareHouseReceipt.Text.Equals(string.Empty))
                warehouseReceipt = 0;
            else
                warehouseReceipt = Convert.ToInt32(txtWareHouseReceipt.Text);
            List<GINModel> gmList = new List<GINModel>();
            gmList = GINModel.getUnApprovedGinManager(clientId, warehouseReceipt, licName, GinNo, warehouseId);
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
        //protected void btnOk_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow gvr in this.gvApproval.Rows)
        //    {
        //        GINModel objApp = new GINModel();

        //        objApp.ID = new Guid(Session["GinId"].ToString());


        //        //objApp.Approve();
        //        Messages.SetMessage("SUCCESS: The Gin Approved and it is out of unapproved list!", Messages.MessageType.Success);
        //    }
        //    BindData();
        //}
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string ginApprovalInfoXML = "<GINApproval>";
            DateTime LICSignedDate, ManagerSignedDate;
            string GINNO;
            foreach (GridViewRow gvr in this.gvApproval.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    if (((Label)gvr.FindControl("lblstatusLIC")).Text == "Reject")
                        Session["btnApprove"] = false;

                    LICSignedDate = Convert.ToDateTime(((Label)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("lblDateTimeLICSigned")).Text);
                    ManagerSignedDate= Convert.ToDateTime(((TextBox)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("txtDateTimeManagerSigned")).Text + " " + ((TextBox)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("txtTimeManagerSigned")).Text) ;
                    GINNO = ((Label)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("lblGINNo")).Text;
                    if (ManagerSignedDate > DateTime.Now)
                    {
                        Messages.SetMessage(" Manager Signed Date MUST NOT BE greater than todday!! on GIN Number" + GINNO, Messages.MessageType.Error);
                        return;
                    }
                     if (ManagerSignedDate >= LICSignedDate )
                     {
                         ginApprovalInfoXML +=
                         "<GINApprovalItem> <ID>" + gvApproval.DataKeys[gvr.RowIndex].Values["Id"].ToString() + "</ID>" +
                            "<ManagerSignedDate>" + ManagerSignedDate + "</ManagerSignedDate>" +
                            "<ManagerSignedName>" + BLL.UserBLL.CurrentUser.FullName + "</ManagerSignedName>" +
                            "<GINStatusID>" + CalculateGINStatus(gvr) + "</GINStatusID>" +
                            "</GINApprovalItem>";
                     }
                     else
                     {
                         Messages.SetMessage("  Manager Signed Date have to greater or equal to  LIC Signed Date." , Messages.MessageType.Warning);
                         return;
                     }
                }
            }
            if (!Convert.ToBoolean(Session["btnApprove"]))
            {
                Messages.SetMessage("You Can NOT Approve Rejected GIN!", Messages.MessageType.Error);
                Session["btnApprove"] = true;
                return;
            }
            ginApprovalInfoXML += "</GINApproval>";
            GINModel.ApproveManager(ginApprovalInfoXML);
            BindData();
            Messages.SetMessage("The Gin Approved and it is out of unapproved list!", Messages.MessageType.Success);
        }
        private object CalculateGINStatus(GridViewRow gvr)
        {
            return Convert.ToInt32(gvApproval.DataKeys[gvr.RowIndex].Values["GINStatusID"]) *
            Convert.ToInt32(((DropDownList)gvApproval.Rows[gvr.RowIndex].Cells[0].FindControl("drpManagerStatus")).SelectedValue);
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
                    //Session["GINID"] = ginIds;
                }
            }
            GINModel.CancelGIN(ginIds);
            BindData();
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
