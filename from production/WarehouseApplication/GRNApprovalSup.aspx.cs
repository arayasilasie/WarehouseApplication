using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class GRNApprovalSup : System.Web.UI.Page
    {
       // public static int stepId;
        static int status;
        static Guid GRNID;
        static Guid CurrentWarehouse;
        static DataTable dtbl;
        static DataTable dtbl1;
        static bool edit;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            btnApprove.Style["visibility"] = "hidden";
            CurrentWarehouse = new Guid(Session["CurrentWarehouse"].ToString());
           
            //For Insert
            if (Session["StepID"] != null)
            {               
                status = 2;
                BindLIC(status);
                 
            }
            //For Edit
            if (Request.QueryString["GRNStatus"] != null)
            {
                btnSearch.Enabled = false;
                btnApprove.Text = "Update";
                edit = true;
                GRNID = new Guid(Request.QueryString["GRNID"].ToString());
                BindGRNApprovalGridviewForEdit();
            }
        }

        public void BindLIC(int status)
        {
            ddLIC.Items.Clear();
            ddLIC.Items.Add(new ListItem("Select LIC", ""));

            ddLIC.DataSource = GRNApprovalModel.GetLICsForGRNApproval(CurrentWarehouse,status);
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }

        public void BindGRNApprovalGridview()
        {
            dtbl1=GRNApprovalModel.GetGRNForApproval(CurrentWarehouse, status, new Guid(ddLIC.SelectedValue), txtGRNNo.Text, txtClientId.Text);
            grvGRNApproval.DataSource = dtbl1; 
            grvGRNApproval.DataBind();
        }

        public void BindGRNApprovalGridviewForEdit()
        {
            dtbl = GRNApprovalModel.GetGRNApprovalForSupervisorEdit(CurrentWarehouse, GRNID);
            grvGRNApproval.DataSource = dtbl;
            grvGRNApproval.DataBind();

            if (dtbl.Rows[0]["ApprovedStatus"].ToString() == "1") // if Supervisor already accept it, Can't Edit
            grvGRNApproval.Enabled = false;
            else
            ((DropDownList)grvGRNApproval.Rows[0].FindControl("drpLICStatus")).SelectedValue = "2";
            lblDetail.Text = "Supervisor GRN Approval Edit";
        }
       
        protected void GRNApprovalBySupervisor()
        {        
            string dateEntered;
            string timeEntered;
            string GRNNo;
            string GRN_No;
            int response;
            Messages1.ClearMessage();
            int countError = 0;

            string GRNApprovalXML = "<GRNApproval>";

            foreach (GridViewRow gvr in this.grvGRNApproval.Rows)
            {

                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    // check if date and time is not empty
                    dateEntered = ((TextBox)grvGRNApproval.Rows[gvr.RowIndex].FindControl("txtDateTimeLICSigned")).Text;
                    timeEntered = ((TextBox)grvGRNApproval.Rows[gvr.RowIndex].FindControl("txtTimeLICSigned")).Text;
                    GRNNo = grvGRNApproval.DataKeys[gvr.RowIndex].Value.ToString();
                    GRN_No = ((Label)grvGRNApproval.Rows[gvr.RowIndex].FindControl("lblGRNNo")).Text;
                    response = int.Parse(((DropDownList)grvGRNApproval.Rows[0].FindControl("drpLICStatus")).SelectedValue);
                   
                    // if Superevisor Accept it create WarehouseReciept....
                    if (response == 1)
                    {
                        GRN_BL objGrnNew = new GRN_BL();
                        GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRN_No);
                        WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
                        objWR.Save();
                    }  
             
                    if (dateEntered == "" || timeEntered == "")
                    {
                         Messages1.SetMessage("Enter values to GRN No: <b>" + GRN_No + "</b><br/>", WarehouseApplication.Messages.MessageType.Warning);
                         countError++;
                    }
                    if (countError == 0)
                    {
                        GRNApprovalXML +=
                        "<GRNApprovalItem> <ID>" + GRNNo + "</ID>" +
                            "<WarehouseSupervisorApprovedBy>" + UserBLL.CurrentUser.UserId + "</WarehouseSupervisorApprovedBy>" +
                            "<WarehouseSupervisorApprovedDateTime>" + dateEntered + " " + timeEntered + "</WarehouseSupervisorApprovedDateTime>" +
                            "<WarehouseSupervisorApprovedTimeStamp>" + DateTime.Now + "</WarehouseSupervisorApprovedTimeStamp>" +
                            "<WarehouseSupervisorStatus>" + response + "</WarehouseSupervisorStatus>" +
                            "<Status>" + 3 + "</Status>" +
                            "<StackID>" + ((Label)grvGRNApproval.Rows[gvr.RowIndex].FindControl("lblStackID")).Text + "</StackID>" +
                         "</GRNApprovalItem>";
                    }
                }
            }
            if (countError == 0)
            {
                try
                {
                    GRNApprovalXML += "</GRNApproval>";
                    GRNApprovalModel.ApproveGRNBySupervisor(GRNApprovalXML);
                    BindGRNApprovalGridview();
                    Messages1.SetMessage("Approved successfully.", Messages.MessageType.Success);
                    BindLIC(status);
                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, Messages.MessageType.Error);
                }
            }
            else
                btnApprove.Style["visibility"] = "visible";

        }

        protected void GRNApprovalEdit()
        {
            string dateEntered;
            string timeEntered;
            Guid GRNNo;
            string GRN_No;
            int response;
            Messages1.ClearMessage();
            int countError = 0;

            dateEntered = ((TextBox)grvGRNApproval.Rows[0].FindControl("txtDateTimeLICSigned")).Text;
            timeEntered = ((TextBox)grvGRNApproval.Rows[0].FindControl("txtTimeLICSigned")).Text;
            GRNNo = new Guid(grvGRNApproval.DataKeys[0].Value.ToString());
            GRN_No = ((Label)grvGRNApproval.Rows[0].FindControl("lblGRNNo")).Text;
            response=int.Parse(((DropDownList)grvGRNApproval.Rows[0].FindControl("drpLICStatus")).SelectedValue);

            if (dateEntered == "" || timeEntered == "")
            {
                Messages1.SetMessage("Enter values to GRN No: <b>" + GRN_No + "</b><br/>", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
            }
            
            if (countError == 0)
            {
                try
                {
                    GRNApprovalModel.UpdateGRNSupervisorApproval(GRNNo, UserBLL.CurrentUser.UserId, response, (DateTime.Parse(dateEntered + " " + timeEntered)), DateTime.Now);
                       
                    // if Superevisor Accept it create WarehouseReciept....
                    if (response == 1)
                    {
                        GRN_BL objGrnNew = new GRN_BL();
                        GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRN_No);
                        WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
                        objWR.Save();
                    } 

                    BindGRNApprovalGridviewForEdit();
                    Messages1.SetMessage("Updated successfully.", Messages.MessageType.Success);
                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, Messages.MessageType.Error);
                }
            }
            else
                btnApprove.Style["visibility"] = "visible";

        }
        
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                GRNApprovalEdit();
            }
            else
            {
                GRNApprovalBySupervisor();
            }               
        }
                 
        protected void grvGRNApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvGRNApproval.PageIndex = e.NewPageIndex;
            grvGRNApproval.DataSource = dtbl1;
            grvGRNApproval.DataBind();
        }

        protected void grvGRNApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Label)e.Row.FindControl("lblLICStatus")).Text.Equals("2"))
                {
                    DropDownList ddResult = (DropDownList)e.Row.FindControl("drpLICStatus");
                    BindRejectOnly(ddResult);
                }               
            }
            else if (Request.QueryString["GRNStatus"]!= null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                        if (((Label)e.Row.FindControl("lblLICStatus")).Text.Equals("2"))
                        {
                            DropDownList ddResult = (DropDownList)e.Row.FindControl("drpLICStatus");
                            BindRejectOnly(ddResult);
                        }
                        else
                        {
                            DropDownList ddResult = (DropDownList)e.Row.FindControl("drpLICStatus");
                            ddResult.SelectedValue = dtbl.Rows[0]["ApprovedStatus"].ToString();
                        }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow) // future date validation and GRNCreatedDate validation
            {
                RangeValidator rv = (RangeValidator)e.Row.FindControl("DateRangeValidator");
                rv.MinimumValue = ((Label)e.Row.FindControl("lblGRNCreatedDate")).Text;
                rv.MaximumValue = DateTime.Today.ToShortDateString();

                // if client is C10000 don't enable approval.
                if (((Label)e.Row.FindControl("lblClientID")).Text == "C10000")
                    e.Row.Enabled = false;
            }
                    
        }

        void BindRejectOnly(DropDownList ddResult)
        {
            ddResult.Items.Clear();
            ListItem item = new ListItem("Reject", "2");
            ddResult.Items.Add(item);
            ddResult.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            if (ddLIC.SelectedValue != string.Empty)
            {
                BindGRNApprovalGridview();
            }
            else
            {
                Messages1.SetMessage("Please select LIC.", Messages.MessageType.Warning);
            }
        }


                     
    }
}