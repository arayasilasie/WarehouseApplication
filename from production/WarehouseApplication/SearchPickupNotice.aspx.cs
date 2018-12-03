using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using GINBussiness;
using System.Web.UI.MobileControls;
using WarehouseApplication.BLL;
using System.Configuration;

namespace WarehouseApplication
{
    public partial class SearchPickupNotice : System.Web.UI.Page
    {
        static PickupNoticeModel lstSerch = new PickupNoticeModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            ECX.DataAccess.SQLHelper.ClearCach();
            if (!IsPostBack)
            {

                FillPickupNoticeStatus();
                btnGINProcess.Style["visibility"] = "hidden";
                btnPrintPUN.Style["visibility"] = "hidden";
                btnProcessPSA.Style["visibility"] = "hidden";

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

            List<PickupNoticeModel> names = new List<PickupNoticeModel>();
            string punIds = string.Empty;
            int WeightInKg = 0;
            foreach (GridViewRow gvr in this.gvSearchPickupNotice.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    PickupNoticeModel g = new PickupNoticeModel();
                    if (!string.IsNullOrEmpty(punIds))
                        punIds += ",";
                    punIds += gvSearchPickupNotice.DataKeys[gvr.RowIndex].Value.ToString();
                    g.ID = new Guid(gvSearchPickupNotice.DataKeys[gvr.RowIndex].Value.ToString());
                    names.Add(g);
                    WeightInKg = WeightInKg + Convert.ToInt32(gvSearchPickupNotice.DataKeys[gvr.RowIndex].Values["WeightInKg"]);
                }
            }


            List<PickupNoticeModel> pikListcheck = PickupNoticeModel.PrintPUNChecking(punIds);
            if (pikListcheck.Count > 0)
            {
                Session["ReportType"] = "DoNuthing";
                Messages.SetMessage("You may contact system administrator for assistance. From selected PUN list the following WHR# are already printed: " + pikListcheck.Select(s => s.WarehouseReceiptNo.ToString()).Aggregate((str, nex) => str + "," + nex), Messages.MessageType.Warning);
            }
            else
            {
                List<PickupNoticeModel> pikList = PickupNoticeModel.PreparePUNId(punIds);
                if (pikList.Count > 0)
                {


                    Session["ReportType"] = "PUN";
                    Session["PUNID"] = pikList.Select(s => s.ID.ToString()).Aggregate((str, nex) => str + "," + nex);
                    ScriptManager.RegisterStartupScript(this,
                                                           this.GetType(),
                                                           "ShowReport",
                                                           "<script type=\"text/javascript\">" +
                                                           string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                           "</script>",
                                                           false);
                }
                else
                    Messages.SetMessage("The system can’t print this Pun please contact Administrator " , Messages.MessageType.Warning);
            }
            btnGINProcess.Style["visibility"] = "Visible";
            btnPrintPUN.Style["visibility"] = "Visible";
            btnProcessPSA.Style["visibility"] = "Visible";

        }
        protected void btnGINProcess_Click(object sender, EventArgs e)
        {          
                Session["ProcessType"] = "GIN";
                Process();            
        }
        private void Process()
        {
            string punIds = string.Empty;
            int WeightInKg = 0;
            int MaxLimit = 0;
            int count = 0;
            foreach (GridViewRow gvr in this.gvSearchPickupNotice.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    System.Web.UI.WebControls.Label lblUnApprovedGINCount = ((System.Web.UI.WebControls.Label)gvr.FindControl("lblUnApprovedGINCount"));
                    int UnApprovedGINCount = 0;
                    if (Session["ProcessType"].ToString() == "PSA" && int.TryParse(lblUnApprovedGINCount.Text, out UnApprovedGINCount) && UnApprovedGINCount > 0)
                    {
                        Messages.SetMessage("The selected PUN in row " + (gvr.RowIndex + 1).ToString() + " has unapproved " + UnApprovedGINCount + " GIN under it. Please approve that first!"
                             , WarehouseApplication.Messages.MessageType.Warning);
                        return;
                    }

                    if (!string.IsNullOrEmpty(punIds))
                        punIds += ",";
                    punIds += gvSearchPickupNotice.DataKeys[gvr.RowIndex].Value.ToString();
                    Session["PUNID"] = punIds;
                    count = count + 1;
                    WeightInKg = WeightInKg + Convert.ToInt32(gvSearchPickupNotice.DataKeys[gvr.RowIndex].Values["WeightInKg"]);
                    MaxLimit = Convert.ToInt32(gvSearchPickupNotice.DataKeys[gvr.RowIndex].Values["MaxLimit"]);
                }
            }

            //if (WeightInKg > MaxLimit)
            //{
            //    Messages.SetMessage("The selected PUN list exceeded the maximum capacity by : " + (Convert.ToInt32(CommodityMaximumRangeEnum.MaximumRange) - WeightInKg) + " You can only process maximum of " + Convert.ToInt32(CommodityMaximumRangeEnum.MaximumRange) + " for one truck/GIN.", Messages.MessageType.Warning);
            //}
            //else
            //{
                GINModel ob = new GINModel();
                List<PickupNoticeModel> pikListcheck = PickupNoticeModel.PrintPUNChecking(punIds);

                if (pikListcheck.Count != count)
                {
                    Messages.SetMessage("From selected PUN list some of them are not printed.You can’t process GIN before PUN is printed. ", Messages.MessageType.Warning);
                }
                else
                {


                    ob.ID = Guid.NewGuid();
                    ob.PrepareGIN(punIds);
                    Guid gradeID;
                    string shedName;
                    gradeID = ob.PickupNoticesList[0].CommodityGradeID;
                    shedName = ob.PickupNoticesList[0].ShedName;
                    ob.PickupNoticesList[0].ShedName = shedName;
                    int ProductionYear = ob.PickupNoticesList[0].ProductionYear;
                    ob.PickupNoticesList[0].ProductionYear = ProductionYear;
                    bool hasError = false;
                    foreach (PickupNoticeModel pm in ob.PickupNoticesList)
                    {
                        if (pm.ProductionYear != ProductionYear ||
                            pm.CommodityGradeID != gradeID ||
                            pm.ShedName != shedName)
                        {
                            hasError = true;
                            Messages.SetMessage("ERROR: Please check warehouse receipts are of the same grade, shed and production year!");
                            break;
                        }
                    }
                    if (hasError) return;
                    ob.PUNPrintDateTime = pikListcheck[0].PUNPrintDateTime;
                    ob.PUNPrintedBy = pikListcheck[0].PUNPrintedBy;
                    ob.GINNumber = ob.GetNewGINNumber()[0];
                    ob.AutoNumber = int.Parse(ob.GetNewGINNumber()[1]);
                    //  ob.TransactionId = WFTransaction.GetTransaction(new Guid(ConfigurationSettings.AppSettings["GINNew"])).ToString();
                    ob.WarehouseID = UserBLL.GetCurrentWarehouse();
                    ob.CommodityGradeID = gradeID;
                    ob.ProductionYear = ProductionYear;
                    Session["GINMODEL"] = ob;
                    Session["EditMode"] = false;
                    if (Session["ProcessType"].ToString() == "GIN")
                        Response.Redirect("GIN.aspx");
                    else if (Session["ProcessType"].ToString() == "PSA")
                        Response.Redirect("GINPSA.aspx");
                //}

            }
            btnGINProcess.Style["visibility"] = "Visible";
            btnPrintPUN.Style["visibility"] = "Visible";
            btnProcessPSA.Style["visibility"] = "Visible";
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
                DateTime dtExp = DateTime.Parse( exDate);
                DateTime dtNow = DateTime.Parse( exNow);
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
        protected void gvSearchPickupNotice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnProcessPSA_Click(object sender, EventArgs e)
        {
            Session["ProcessType"] = "PSA";
            Process();
        }
    }
}
