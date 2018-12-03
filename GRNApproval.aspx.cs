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
    public partial class GRNApproval : System.Web.UI.Page
    {
        // public static int stepId;
        // static int status;
        //  static Guid GRNID;
        // static Guid CurrentWarehouse;
        //static DataTable dtbl;
        //static DataTable dtbl1;
        //static bool edit;
        // static DateTime ApprovedDate;
        int countError;

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

            btnApprove.Style["visibility"] = "hidden";

            //For Edit
            if (Request.QueryString["GRNStatus"] != null)
            {
                btnSearch.Enabled = false;
                btnApprove.Text = "Update";
                ViewState.Add("edit", true);
                BindGRNApprovalGridviewForEdit();
            }
            //For Insert
            else
            {
                BindLIC(1);
                ViewState.Add("edit", false);
                Session["StepID"] = null;
            }


        }

        public void BindLIC(int status)
        {
            ddLIC.Items.Clear();
            ddLIC.Items.Add(new ListItem("Select LIC", ""));

            ddLIC.DataSource = GRNApprovalModel.GetLICsForGRNApproval(new Guid(Session["CurrentWarehouse"].ToString()), 1);
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }

        public void BindGRNApprovalGridview()
        {
            if (ddLIC.SelectedValue != "")
            {
                DataTable dt = GRNApprovalModel.GetGRNForApproval(new Guid(Session["CurrentWarehouse"].ToString()), 1, new Guid(ddLIC.SelectedValue), txtGRNNo.Text, txtClientId.Text);
                ViewState.Add("dtblI", dt);
                grvGRNApproval.DataSource = dtblI;
                grvGRNApproval.DataBind();

                if (dtblI.Rows.Count == 0)
                    BindLIC(1);
            }
        }

        public void BindGRNApprovalGridviewForEdit()
        {
            DataTable dt = GRNApprovalModel.GetGRNApprovalForEdit(new Guid(Session["CurrentWarehouse"].ToString()), new Guid(Request.QueryString["GRNID"].ToString()));
            ViewState.Add("dtblE", dt);
            grvGRNApproval.DataSource = dtblE;
            grvGRNApproval.DataBind();

            ((DropDownList)grvGRNApproval.Rows[0].FindControl("drpLICStatus")).SelectedValue = dtblE.Rows[0]["ApprovedStatus"].ToString();

            if (Request.QueryString["GRNStatus"] == "3") // LIC can't edit if Supervisor already Approve
                grvGRNApproval.Enabled = false;
            lblDetail.Text = "Data assistant GRN Approval Edit";
        }

        bool isValidDateTime(string dateEntered, string timeEntered, DateTime PreviousDate)
        {
            DateTime t;

            if (dateEntered == "" || timeEntered == "")
            {
                Messages1.SetMessage("Please enter date and time ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }

            else if (!(DateTime.TryParse((dateEntered + " " + timeEntered), out t)))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }
            else if ((DateTime.Parse(dateEntered) < PreviousDate))//+ " " + timeEntered)) < PreviousDate))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }
            else if (DateTime.Parse(dateEntered) > (DateTime.Now))//+ " " + timeEntered)) > DateTime.Now))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }
            else
            {
                // ApprovedDate = DateTime.Parse(dateEntered + " " + timeEntered);
                return true;
            }
        }

        protected void GRNApprovalByLIC()
        {
            countError = 0;
            DateTime GRNCreationDate;
            string dateEntered;
            string timeEntered;
            string GRNNo;
            string GRN_No;
            Messages1.ClearMessage();


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
                    GRNCreationDate = DateTime.Parse(((Label)grvGRNApproval.Rows[gvr.RowIndex].FindControl("lblGRNCreatedDate")).Text);

                    if (isValidDateTime(dateEntered, timeEntered, GRNCreationDate))
                    {
                        GRNApprovalXML +=
                       "<GRNApprovalItem> <ID>" + GRNNo + "</ID>" +
                           "<LICApprovedBy>" + UserBLL.CurrentUser.UserId + "</LICApprovedBy>" +
                           "<LICApprovedDateTime>" + dateEntered + " " + timeEntered + "</LICApprovedDateTime>" +
                           "<LICApprovedTimeStamp>" + DateTime.Now + "</LICApprovedTimeStamp>" +
                           "<LICStatus>" + ((DropDownList)grvGRNApproval.Rows[gvr.RowIndex].FindControl("drpLICStatus")).SelectedValue + "</LICStatus>" +
                           "<Status>" + 2 + "</Status>" +
                       "</GRNApprovalItem>";
                    }
                }
            }
            if (countError == 0)
            {
                try
                {
                    GRNApprovalXML += "</GRNApproval>";
                    GRNApprovalModel.ApproveGRN(GRNApprovalXML);
                    BindGRNApprovalGridview();
                    Messages1.SetMessage("Approved successfully.", WarehouseApplication.Messages.MessageType.Success);
                    ///////BindLIC(status);   
                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                }
            }
            else
                btnApprove.Style["visibility"] = "visible";
        }

        protected void GRNApprovalEdit()
        {
            DateTime GRNCreationDate;
            string dateEntered;
            string timeEntered;
            Guid GRNNo;
            string GRN_No;
            int response;
            Messages1.ClearMessage();

            dateEntered = ((TextBox)grvGRNApproval.Rows[0].FindControl("txtDateTimeLICSigned")).Text;
            timeEntered = ((TextBox)grvGRNApproval.Rows[0].FindControl("txtTimeLICSigned")).Text;
            GRNNo = new Guid(grvGRNApproval.DataKeys[0].Value.ToString());
            GRN_No = ((Label)grvGRNApproval.Rows[0].FindControl("lblGRNNo")).Text;
            response = int.Parse(((DropDownList)grvGRNApproval.Rows[0].FindControl("drpLICStatus")).SelectedValue);
            GRNCreationDate = DateTime.Parse(((Label)grvGRNApproval.Rows[0].FindControl("lblGRNCreatedDate")).Text);

            if (isValidDateTime(dateEntered, timeEntered, GRNCreationDate))
            {
                try
                {
                    GRNApprovalModel.UpdateGRNApproval(GRNNo, UserBLL.CurrentUser.UserId, response, (DateTime.Parse(dateEntered + " " + timeEntered)), DateTime.Now);
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
            if (((bool)ViewState["edit"]))
            {
                GRNApprovalEdit();
            }
            else
            {
                GRNApprovalByLIC();
            }
        }

        protected void grvGRNApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvGRNApproval.PageIndex = e.NewPageIndex;
            grvGRNApproval.DataSource = dtblI;
            grvGRNApproval.DataBind();
        }

        protected void grvGRNApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Request.QueryString["GRNStatus"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddResult = (DropDownList)e.Row.FindControl("drpLICStatus");
                    ddResult.SelectedValue = dtblE.Rows[0]["ApprovedStatus"].ToString();
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