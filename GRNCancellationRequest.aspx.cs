using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Data;
namespace WarehouseApplication
{
    public partial class GRNCancellationRequest : System.Web.UI.Page
    {
        DataTable dtbl
        {
            get
            {
                if (ViewState["dtbl"] != null)
                    return (DataTable)(ViewState["dtbl"]);
                else
                    return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

           // BindStatus();
        }

        public void BindStatus()
        {
            ddStatus.Items.Clear();
            ddStatus.Items.Add(new ListItem("Select Status", ""));

            ddStatus.DataSource = GRNCancellationModel.GetGRNCancellationStatus();
            ddStatus.DataTextField = "Description";
            ddStatus.DataValueField = "ID";
            ddStatus.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            pnlReason44.Visible = false;
            txtReason.Text = "";
           
            BindGRNCancellatiolGridview();           
        }

        public void BindGRNCancellatiolGridview()
        {
            {
                DataTable dt = GRNCancellationModel.GetGRNsForCancellation(new Guid(Session["CurrentWarehouse"].ToString()),txtGRNNo.Text);
                grvGRNCancellation.DataSource = dt;
                grvGRNCancellation.DataBind();
            }
        }

        public void BindSearchGridview(Guid WarehouseID, int Status, string GRNNo, DateTime DateRequested, DateTime DateRequested2, DateTime DateApproved, DateTime DateApproved2)
        {
            {

                DataTable dt = GRNCancellationModel.GetGRNCancellationRequestSearch(WarehouseID, Status, GRNNo, DateRequested, DateRequested2, DateApproved, DateApproved2);
                ViewState.Add("dtbl", dt);
                grvSearch.DataSource = dtbl; 
                grvSearch.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GRNCancellationModel.GRNCancelationRequest(new Guid(grvGRNCancellation.SelectedDataKey[0].ToString()), 2, UserBLL.CurrentUser.UserId, DateTime.Now, txtReason.Text);
                Messages1.SetMessage("Record saved successfully.", WarehouseApplication.Messages.MessageType.Success);
                BindGRNCancellatiolGridview();
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            }
            txtReason.Text = "";
            pnlReason44.Visible = false;
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            pnlReason44.Visible = true;
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            PreparedForSearch();
        }

        public void PreparedForSearch()
        {
            string GRNNo = "";
            if (txtGRN.Text != "")
                GRNNo = txtGRN.Text;

            int Status = 0;
            if (ddStatus.SelectedValue.ToString() != "")
                Status = int.Parse(ddStatus.SelectedValue.ToString());

            DateTime DateRequested = DateTime.Parse("1/1/1800");
            if (txtDateIssued.Text != "")
                DateRequested = DateTime.Parse(txtDateIssued.Text);

            DateTime DateRequested2 = DateTime.Parse("1/1/9999");
            if (txtDateIssued2.Text != "")
                DateRequested2 = DateTime.Parse(txtDateIssued2.Text);

            DateTime DateApproved = DateTime.Parse("1/1/1800");
            if (txtDateApproved.Text != "")
                DateApproved = DateTime.Parse(txtDateApproved.Text);

            DateTime DateApproved2 = DateTime.Parse("1/1/9999");
            if (txtDateApproved2.Text != "")
                DateApproved2 = DateTime.Parse(txtDateApproved2.Text);

            BindSearchGridview(new Guid(Session["CurrentWarehouse"].ToString()), Status, GRNNo, DateRequested, DateRequested2, DateApproved, DateApproved2);

        }

        protected void grvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSearch.PageIndex = e.NewPageIndex;
            grvSearch.DataSource = dtbl;
            grvSearch.DataBind();
        }

        protected void grvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(((Label)e.Row.FindControl("lblRejectionReason")).Text!="")
                    e.Row.ToolTip = "Rejection Reason: " + ((Label)e.Row.FindControl("lblRejectionReason")).Text;
                else
                    e.Row.ToolTip = "Request Reason: " + ((Label)e.Row.FindControl("lblRemark")).Text;

            }
        }
    }
}