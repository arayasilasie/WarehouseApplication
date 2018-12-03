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
    public partial class GRNClientSign : System.Web.UI.Page
    {
        static Guid CurrentWarehouse;
        static DataTable dtbl;
        static DateTime ApprovedDate;
        static int countError;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            btnApprove.Style["visibility"] = "hidden";
            CurrentWarehouse = new Guid(Session["CurrentWarehouse"].ToString());
            BindLIC(0);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            if (ddLIC.SelectedValue != string.Empty)
            {
                BindGRNClientSignGridView();
            }
            else
            {
                Messages1.SetMessage("Please select LIC.", Messages.MessageType.Warning);
            }
        }

        public void BindLIC(int status)
        {
            ddLIC.Items.Clear();
            ddLIC.Items.Add(new ListItem("Select LIC", ""));

            ddLIC.DataSource = GRNApprovalModel.GetLICsForGRNApproval(CurrentWarehouse, status);
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }
        public void BindGRNClientSignGridView()
        {
            dtbl = GRNApprovalModel.GetGRNForClientSign(CurrentWarehouse, txtGRNNo.Text, txtClientId.Text, new Guid(ddLIC.SelectedValue));
            grvGRNClientSign.DataSource = dtbl;
            grvGRNClientSign.DataBind();
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
            else if ((DateTime.Parse((dateEntered + " " + timeEntered)) < PreviousDate))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }
            else if ((DateTime.Parse((dateEntered + " " + timeEntered)) > DateTime.Now))
            {
                Messages1.SetMessage("Please enter valid date and time ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }
            else
            {
                ApprovedDate = DateTime.Parse(dateEntered + " " + timeEntered);
                return true;
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DateTime GRNCreationDate;
            string dateEntered;
            string timeEntered;
            string GRNNo;
            string GRN_No;
            Messages1.ClearMessage();
            countError = 0;

            string GRNApprovalXML = "<GRNApproval>";
            foreach (GridViewRow gvr in this.grvGRNClientSign.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    // check if date and time is not empty
                    dateEntered = ((TextBox)grvGRNClientSign.Rows[gvr.RowIndex].FindControl("txtDateTimeLICSigned")).Text;
                    timeEntered = ((TextBox)grvGRNClientSign.Rows[gvr.RowIndex].FindControl("txtTimeLICSigned")).Text;
                    GRNNo = grvGRNClientSign.DataKeys[gvr.RowIndex].Value.ToString();
                    GRN_No = ((Label)grvGRNClientSign.Rows[gvr.RowIndex].FindControl("lblGRNNo")).Text;
                    GRNCreationDate = DateTime.Parse(((Label)grvGRNClientSign.Rows[gvr.RowIndex].FindControl("lblGRNCreatedDate")).Text);
                     
                    if (isValidDateTime(dateEntered, timeEntered, GRNCreationDate))
                    {
                        GRNApprovalXML +=
                        "<GRNApprovalItem> <GRNID>" + GRNNo + "</GRNID>" +
                        "<CreatedBy>" + UserBLL.CurrentUser.UserId + "</CreatedBy>" +
                        "<ClientSignedDate>" + dateEntered + " " + timeEntered + "</ClientSignedDate>" +
                        "<CreatedTimeStamp>" + DateTime.Now + "</CreatedTimeStamp>" +
                        "</GRNApprovalItem>";
                    }
                }
            }
            GRNApprovalXML += "</GRNApproval>";

            if (countError == 0)
            {
                try
                {
                    GRNApprovalModel.GRNSigned(GRNApprovalXML);
                    BindGRNClientSignGridView();
                    btnApprove.Style["visibility"] = "hidden";
                    Messages1.SetMessage("Signed successfully.", WarehouseApplication.Messages.MessageType.Success);
                    BindLIC(0);
                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                }
            }
            else
                btnApprove.Style["visibility"] = "visible";
        }

        protected void grvGRNClientSign_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvGRNClientSign.PageIndex = e.NewPageIndex;
            grvGRNClientSign.DataSource = dtbl;
            grvGRNClientSign.DataBind();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;
            if (chkStatus.Checked == false)
            {
                ((Label)row.FindControl("lblDateRequired")).Visible = false;
                ((Label)row.FindControl("lblTimeRequired")).Visible = false;
            }
        }

        protected void grvGRNClientSign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) // future date validation and GRNCreatedDate validation
            {
                RangeValidator rv = (RangeValidator)e.Row.FindControl("DateRangeValidator");
                rv.MinimumValue = ((Label)e.Row.FindControl("lblGRNCreatedDate")).Text;
                rv.MaximumValue = DateTime.Today.ToShortDateString();

                ///// if client is C10000 don't enable approval.
                if (((Label)e.Row.FindControl("lblClientID")).Text == "C10000")
                    e.Row.Enabled = false;
            }
        }


    }
}