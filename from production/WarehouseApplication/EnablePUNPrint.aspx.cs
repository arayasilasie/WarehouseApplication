using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class EnablePUNPrint : System.Web.UI.Page
    {
        static PickupNoticeModel lstSerch = new PickupNoticeModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPickupNoticeStatus();
                btnPrintPUN.Style["visibility"] = "hidden";
            }
        }
        private void FillPickupNoticeStatus()
        {
            GINBussiness.PickupNoticeModel.FillPickupNoticeStatus(drpStatus);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            if (txtClientId.Text.Equals(string.Empty))
                Session["ClientId"] = string.Empty;
            else
            {
                Session["ClientId"] = txtClientId.Text;
                if (drpStatus.SelectedIndex == 0)
                {
                    Messages.SetMessage("PUN search is not allowed by Client Id only, but you can search by Client Id in combination with Status and/or Expiration Date.", Messages.MessageType.Warning);
                }
            }
            if (txtWareHouseReceipt.Text.Equals(string.Empty))
                Session["WareHouseReceipt"] = 0;
            else
                Session["WareHouseReceipt"] = txtWareHouseReceipt.Text;
            if (!txtExpirationDateFrom.Text.Equals(string.Empty))
                Session["ExpirationDateFrom"] = Convert.ToDateTime(txtExpirationDateFrom.Text);
            else
                Session["ExpirationDateFrom"] = DateTime.Now.AddYears(-1);
            if (!txtExpirationDateTo.Text.Equals(string.Empty))
                Session["ExpirationDateTo"] = Convert.ToDateTime(txtExpirationDateTo.Text).AddDays(1).AddSeconds(-1);
            else
                Session["ExpirationDateTo"] = DateTime.Now.AddYears(1);

            Session["Status"] = drpStatus.SelectedItem.Value;
            if (Convert.ToInt32(drpStatus.SelectedItem.Value) == Convert.ToInt32(PickupNoticeStatusEnum.OpenExpiered))
            {
                if (!txtExpirationDateFrom.Text.Equals(string.Empty))
                    Session["ExpirationDateFrom"] = Convert.ToDateTime(txtExpirationDateFrom.Text);
                else
                    Session["ExpirationDateFrom"] = DateTime.Now.AddYears(-1);
                if (!txtExpirationDateTo.Text.Equals(string.Empty))
                {
                    Session["ExpirationDateTo"] = Convert.ToDateTime(txtExpirationDateTo.Text).AddDays(1).AddSeconds(-1);
                    if (Convert.ToDateTime(txtExpirationDateTo.Text) < Convert.ToDateTime(txtExpirationDateFrom.Text))
                    {
                        Messages.SetMessage("Expiration Date From must be prior (less than) to Expiration Date To .", Messages.MessageType.Warning);
                    }
                }
                else
                    Session["ExpirationDateTo"] = DateTime.Now.AddDays(1).AddSeconds(-1);
                Session["Status"] = 0;
            }
            if (Convert.ToInt32(drpStatus.SelectedItem.Value) == Convert.ToInt32(PickupNoticeStatusEnum.OpenActive))
            {
                if (!txtExpirationDateFrom.Text.Equals(string.Empty))
                    Session["ExpirationDateFrom"] = Convert.ToDateTime(txtExpirationDateFrom.Text);
                else
                    Session["ExpirationDateFrom"] = DateTime.Now;
                if (!txtExpirationDateTo.Text.Equals(string.Empty))
                    Session["ExpirationDateTo"] = Convert.ToDateTime(txtExpirationDateTo.Text).AddDays(1).AddSeconds(-1);
                else
                    Session["ExpirationDateTo"] = DateTime.Now.AddYears(10);
                Session["Status"] = 0;
            }
            if (Convert.ToInt32(drpStatus.SelectedItem.Value) == Convert.ToInt32(PickupNoticeStatusEnum.BeingIssuedExpiered))
            {
                if (!txtExpirationDateFrom.Text.Equals(string.Empty))
                    Session["ExpirationDateFrom"] = Convert.ToDateTime(txtExpirationDateFrom.Text);
                else
                    Session["ExpirationDateFrom"] = DateTime.Now.AddYears(-1);
                if (!txtExpirationDateTo.Text.Equals(string.Empty))
                    Session["ExpirationDateTo"] = Convert.ToDateTime(txtExpirationDateTo.Text).AddDays(1).AddSeconds(-1);
                else
                    Session["ExpirationDateTo"] = DateTime.Now.AddDays(1).AddSeconds(-1);
                Session["Status"] = 1;
            }
            if (Convert.ToInt32(drpStatus.SelectedItem.Value) == Convert.ToInt32(PickupNoticeStatusEnum.BeingIssued))
            {
                if (!txtExpirationDateFrom.Text.Equals(string.Empty))
                    Session["ExpirationDateFrom"] = Convert.ToDateTime(txtExpirationDateFrom.Text);
                else
                    Session["ExpirationDateFrom"] = DateTime.Now;
                if (!txtExpirationDateTo.Text.Equals(string.Empty))
                    Session["ExpirationDateTo"] = Convert.ToDateTime(txtExpirationDateTo.Text).AddDays(1).AddSeconds(-1);
                else
                    Session["ExpirationDateTo"] = DateTime.Now.AddYears(10);
                Session["Status"] = 1;
            }
            lstSerch.Search(Session["ClientId"].ToString(), Convert.ToInt32(Session["WareHouseReceipt"]), Session["Status"].ToString(), UserBLL.GetCurrentWarehouse(), Convert.ToDateTime(Session["ExpirationDateFrom"]), Convert.ToDateTime(Session["ExpirationDateTo"]));

            gvSearchPickupNotice.DataSource = lstSerch.lstSerchList;
            try
            {
                gvSearchPickupNotice.DataBind();
            }
            catch { }
            if (lstSerch.lstSerchList.Count > 0 && !txtClientId.Text.Equals(string.Empty) && Convert.ToInt32(drpStatus.SelectedItem.Value) != 0)
                Messages.SetMessage("If you search using client ID and status other than open status the system give you recently expiring 50 puns; please use Ware house receipt No to get specific data.", Messages.MessageType.Warning);

        }
        protected void btnPrintPUN_Click(object sender, EventArgs e)
        {
            PickupNoticeModel objWHR = new PickupNoticeModel();
            List<PickupNoticeModel> names = new List<PickupNoticeModel>();
            string punIds = string.Empty, WHRNo = string.Empty;
            
            int WeightInKg = 0;
            foreach (GridViewRow gvr in this.gvSearchPickupNotice.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    PickupNoticeModel g = new PickupNoticeModel();
                    if (!string.IsNullOrEmpty(punIds))
                    {
                        punIds += ",";
                        WHRNo += ",";
                    }
                    punIds += gvSearchPickupNotice.DataKeys[gvr.RowIndex].Value.ToString();
                    WHRNo += gvSearchPickupNotice.DataKeys[gvr.RowIndex].Values["WarehouseReceiptNo"];
                    g.ID = new Guid(gvSearchPickupNotice.DataKeys[gvr.RowIndex].Value.ToString());
                    names.Add(g);
                    WeightInKg = WeightInKg + Convert.ToInt32(gvSearchPickupNotice.DataKeys[gvr.RowIndex].Values["WeightInKg"]);
                }
            }

            List<PickupNoticeModel> pikList = PickupNoticeModel.PreparePUNId(punIds);
            Session["ReportType"] = "PUN";
            Session["PUNID"] = pikList.Select(s => s.ID.ToString()).Aggregate((str, nex) => str + "," + nex);
            ScriptManager.RegisterStartupScript(this,
                                                   this.GetType(),
                                                   "ShowReport",
                                                   "<script type=\"text/javascript\">" +
                                                   string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                   "</script>",
                                                   false);                    
            btnPrintPUN.Style["visibility"] = "Visible";
            objWHR.MWarehouseReceiptNo = WHRNo;
            objWHR.CreatedBy = UserBLL.GetCurrentUser();
            objWHR.Remark = txtRemark.Text;
            objWHR.SaveWHR();
        }
        protected void gvSearchPickupNotice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime expieryDate;
                expieryDate = Convert.ToDateTime(e.Row.Cells[9].Text);
                string issueStatus;
                issueStatus = e.Row.Cells[10].Text;
                string exDate = expieryDate.ToShortDateString();
                string exNow = DateTime.Now.ToShortDateString();
                DateTime dtExp = DateTime.Parse(exDate);
                DateTime dtNow = DateTime.Parse(exNow);
                if (dtExp < dtNow || issueStatus == "Invalid" || issueStatus == "Closed" || issueStatus == "Aborted")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(252, 190, 41);
                    e.Row.Cells[0].Enabled = false;
                }
            }
        }
        protected void ClientName_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            dlAgent.DataSource = lstSerch.lstSerchList.FindAll(p => p.ID == new Guid(gvSearchPickupNotice.DataKeys[clickedRow.RowIndex].Value.ToString()));//.Select;// (p => p.ID == gvSearchPickupNotice.DataKeys[gvr.RowIndex].Value.ToString());
            dlAgent.DataBind();
            ModalPopupExtender1.Show();
        }
    }
}
