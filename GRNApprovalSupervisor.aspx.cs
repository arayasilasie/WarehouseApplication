using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Transactions;
namespace WarehouseApplication
{
    public partial class GRNApprovalSupervisor : System.Web.UI.Page
    {
        DataTable dtblE
        {
            get
            {
                if (ViewState["dtblE"] != null)
                    return (DataTable)(ViewState["dtblE"]);
                else
                    return null;
            }
        }
        DataTable dtblI
        {
            get
            {
                if (ViewState["dtblI"] != null)
                    return (DataTable)(ViewState["dtblI"]);
                else
                    return null;
            }
        }
        bool? edit
        {
            get
            {
                if (ViewState["edit"] != null)
                    return (bool)ViewState["edit"];
                else
                    return null;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //For Edit
            if (Request.QueryString["GRNStatus"] != null)
            {
                btnSearch.Enabled = false;
                ViewState.Add("edit", true);
                BindGRNApprovalGridviewForEdit();
            }
            //For Insert
            else
            {
                ViewState.Add("edit", false);
                BindLIC(2);
                Session["StepID"] = null;
            }
        }

        public void BindLIC(int status)
        {
            ddLIC.Items.Clear();
            ddLIC.Items.Add(new ListItem("Select LIC", ""));

            ddLIC.DataSource = GRNApprovalModel.GetLICsForGRNApproval(new Guid(Session["CurrentWarehouse"].ToString()), status);
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }

        public void BindGRNApprovalGridview()
        {
            if (ddLIC.SelectedValue != "")
            {
                DataTable dt = GRNApprovalModel.GetGRNForApproval(new Guid(Session["CurrentWarehouse"].ToString()), 2, new Guid(ddLIC.SelectedValue), txtGRNNo.Text, txtClientId.Text);
                ViewState.Add("dtblI", dt);
                grvGRNApproval.DataSource = dtblI;
                grvGRNApproval.DataBind();
                if (dtblI.Rows.Count == 0)
                    BindLIC(2);
            }
        }

        public void BindGRNApprovalGridviewForEdit()
        {
            DataTable dt = GRNApprovalModel.GetGRNApprovalForSupervisorEdit(new Guid(Session["CurrentWarehouse"].ToString()), new Guid(Request.QueryString["GRNID"].ToString()));
            ViewState.Add("dtblE", dt);
            grvGRNApproval.DataSource = dtblE;
            grvGRNApproval.DataBind();

            if (dt.Rows[0]["ApprovedStatus"].ToString() == "1") // if Supervisor already accept it, Can't Edit
                grvGRNApproval.Enabled = false;
            else
                ((DropDownList)grvGRNApproval.Rows[0].FindControl("drpLICStatus")).SelectedValue = "2";
            lblDetail.Text = "Supervisor GRN Approval Edit";
        }

        protected void grvGRNApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvGRNApproval.PageIndex = e.NewPageIndex;
            grvGRNApproval.DataSource = dtblI;
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
            else if (Request.QueryString["GRNStatus"] != null)
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
                        ddResult.SelectedValue = dtblE.Rows[0]["ApprovedStatus"].ToString();
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

        bool isValidDateTime(string dateEntered, string timeEntered, DateTime PreviousDate)
        {
            DateTime t;

            if (dateEntered == "" || timeEntered == "")
            {
                Messages1.SetMessage("Please enter date and time ", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            else if (!(DateTime.TryParse((dateEntered + " " + timeEntered), out t)))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            else if ((DateTime.Parse((dateEntered + " " + timeEntered)) < PreviousDate))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            else if (DateTime.Parse(dateEntered) > (DateTime.Now))//+ " " + timeEntered)) > DateTime.Now))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            else
            {
                ////ApprovedDate = DateTime.Parse(dateEntered + " " + timeEntered);
                return true;
            }
        }

        protected void grvGRNApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid ID, StackID;
            DateTime LICApprovedDate;
            string dateEntered, timeEntered, GRN_No, message;
            int response;

            ID = new Guid(grvGRNApproval.SelectedDataKey.Value.ToString());
            StackID = new Guid(((Label)grvGRNApproval.SelectedRow.FindControl("lblStackID")).Text);
            dateEntered = ((TextBox)grvGRNApproval.SelectedRow.FindControl("txtDateTimeLICSigned")).Text;
            timeEntered = ((TextBox)grvGRNApproval.SelectedRow.FindControl("txtTimeLICSigned")).Text;
            GRN_No = ((Label)grvGRNApproval.SelectedRow.FindControl("lblGRNNo")).Text;
            response = int.Parse(((DropDownList)grvGRNApproval.SelectedRow.FindControl("drpLICStatus")).SelectedValue);
            LICApprovedDate = DateTime.Parse(((Label)grvGRNApproval.SelectedRow.FindControl("lblGRNCreatedDate")).Text);

            if (isValidDateTime(dateEntered, timeEntered, LICApprovedDate))
            {
                try
                {
                    if (((bool)ViewState["edit"]))
                    {
                        GRNApprovalModel.UpdateGRNSupervisorApproval(ID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
                        BindGRNApprovalGridviewForEdit();
                        message = "Updated successfully.";
                    }
                    else
                    {

                        if (response == 1) //if Superevisor Accept it create WarehouseReciept....
                        {
                            GRN_BL objGrnNew = new GRN_BL();
                            GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRN_No);

                            // Create the TransactionScope to execute the commands
                            TransactionOptions option = new TransactionOptions();
                            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                            option.Timeout = new TimeSpan(0, 0, 60);

                            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, option))
                            {
                                GRNApprovalModel.ApproveGRNBySupervisor(ID, StackID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
                                BindGRNApprovalGridview();

                                //create WarehouseReciept
                                WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
                                objWR.Save2();

                                // The Complete method commits the transaction
                                transaction.Complete();
                            }
                        }

                        else
                        {
                            GRNApprovalModel.ApproveGRNBySupervisor(ID, StackID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
                            BindGRNApprovalGridview();
                        }

                        message = "Approved successfully";
                        Messages1.SetMessage(message, Messages.MessageType.Success);
                    }
                }

                catch (Exception ex)
                {
                    Messages1.SetMessage("Unable to save record. Please try again.", WarehouseApplication.Messages.MessageType.Error);
                }
            }
        }

        //protected void grvGRNApproval_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DateTime LICApprovedDate;
        //    string dateEntered;
        //    string timeEntered;
        //    Guid ID;
        //    Guid StackID;
        //    string GRN_No;
        //    int response;
        //    string message;

        //    ID = new Guid(grvGRNApproval.SelectedDataKey.Value.ToString());
        //    StackID = new Guid(((Label)grvGRNApproval.SelectedRow.FindControl("lblStackID")).Text);
        //    dateEntered = ((TextBox)grvGRNApproval.SelectedRow.FindControl("txtDateTimeLICSigned")).Text;
        //    timeEntered = ((TextBox)grvGRNApproval.SelectedRow.FindControl("txtTimeLICSigned")).Text;
        //    GRN_No = ((Label)grvGRNApproval.SelectedRow.FindControl("lblGRNNo")).Text;
        //    response = int.Parse(((DropDownList)grvGRNApproval.SelectedRow.FindControl("drpLICStatus")).SelectedValue);
        //    LICApprovedDate = DateTime.Parse(((Label)grvGRNApproval.SelectedRow.FindControl("lblGRNCreatedDate")).Text);

        //    if (isValidDateTime(dateEntered, timeEntered, LICApprovedDate))
        //    {
        //        //try
        //        //{

        //        //    if (((bool)ViewState["edit"]))
        //        //    {
        //        //        GRNApprovalModel.UpdateGRNSupervisorApproval(ID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
        //        //        BindGRNApprovalGridviewForEdit();
        //        //        message = "Updated successfully.";
        //        //    }
        //        //    else
        //        //    {
        //        //        GRNApprovalModel.ApproveGRNBySupervisor(ID, StackID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
        //        //        BindGRNApprovalGridview();
        //        //        message = "Approved successfully.";
        //        //    }

        //        //    // if Superevisor Accept it create WarehouseReciept....
        //        //    if (response == 1)
        //        //    {
        //        //        GRN_BL objGrnNew = new GRN_BL();
        //        //        GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRN_No);
        //        //        WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
        //        //        objWR.Save();
        //        //    }
        //        //    Messages1.SetMessage(message, Messages.MessageType.Success);


        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
        //        //}
        //        try
        //        {

        //            if (((bool)ViewState["edit"]))
        //            {
        //                GRNApprovalModel.UpdateGRNSupervisorApproval(ID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
        //                BindGRNApprovalGridviewForEdit();
        //                message = "Updated successfully.";
        //            }
        //            else
        //            {
        //                GRN_BL objGrnNew = new GRN_BL();
        //                GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRN_No);

        //                //// Create the TransactionScope to execute the commands
        //                //using (TransactionScope scope = new TransactionScope())
        //                //{
        //                    GRNApprovalModel.ApproveGRNBySupervisor(ID, StackID, UserBLL.CurrentUser.UserId, response, DateTime.Parse(dateEntered + " " + timeEntered), DateTime.Now);
        //                    BindGRNApprovalGridview();
        //                    message = "Approved successfully";


        //                    //// if Superevisor Accept it create WarehouseReciept....
        //                    if (response == 1)
        //                    {
        //                        WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
        //                        bool isWRCreated = objWR.Save();
        //                        if (!isWRCreated)                                
        //                        {
        //                            message = message + " unable to create Warehouse Reciept";
        //                        }
        //                    }
        //                    Messages1.SetMessage(message, Messages.MessageType.Success);

        //                    //// The Complete method commits the transaction
        //                    //scope.Complete();
        //                //}
        //            }
        //        }

        //        //catch (TransactionAbortedException ex)
        //        //{
        //        //    Messages1.SetMessage(ex.Message, Messages.MessageType.Error);
        //        //}

        //        catch (Exception ex)
        //        {
        //            Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
        //        }

        //    }
        //}
    }
}