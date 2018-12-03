using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    public partial class UISearchableInboxDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                //SetUI();
            }
        }
        public static bool RemovePredicate(TransactionDetail o)
        {
            return true;
        }

        protected void gvInbox_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvDetail.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblTrackingNo");
                    if (id != null)
                    {

                        Response.Redirect("PageSwicther.aspx?TranNo=" + id.Text);
                    }
                }
            }
        }

        private void SetUI()
        {
            if (Session["WarehouseInboxItemName"] != null && Session["Inboxpath"] != null)
            {
                this.lblInboxItemName.Text = Session["WarehouseInboxItemName"].ToString();
            }
            else
            {
                return;
            }
            // Load GIN Information.
            List<TransactionDetail> listDisplay = new List<TransactionDetail>();
            InboxItems item = new InboxItems();
            XMLHelper objHelper = new XMLHelper(Session["Inboxpath"].ToString());
            item = objHelper.SearchByInboxItemName(Session["WarehouseInboxItemName"].ToString());
            listDisplay = item.GetTransactions().OrderByDescending(t => t.TrackNo).ToList();
            if ("Select Trucks For Sampling" == Session["WarehouseInboxItemName"].ToString())
            {

                TransactionDetail obj = new TransactionDetail("", "");
                obj.DisplayName = "Select Trucks for Sampling";
                obj.TrackNo = "GetTrucksReadyForSam";
                listDisplay.RemoveAll(RemovePredicate);
                listDisplay.Add(obj);
            }
            else if ("Confirm Trucks For Sampling" == Session["WarehouseInboxItemName"].ToString())
            {
                TransactionDetail obj = new TransactionDetail("", "");
                obj.DisplayName = "Confirm Truks For Sampling";
                obj.TrackNo = "ConfirmTrucksForSamp";
                listDisplay.RemoveAll(RemovePredicate);
                listDisplay.Add(obj);
            }
            else if ("Assign Sampler".Trim() == Session["WarehouseInboxItemName"].ToString().Trim())
            {
                TransactionDetail obj = new TransactionDetail("", "");
                obj.DisplayName = "Assign Sampler";
                obj.TrackNo = "GetSampleTicket";
                listDisplay.RemoveAll(RemovePredicate);
                listDisplay.Add(obj);
            }

            this.gvDetail.DataSource = listDisplay;
            this.gvDetail.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
#region co
            //string taskNo = txtTaskNo.Text.Trim();
            //if (taskNo != string.Empty)
            //{
            //    XMLHelper objHelper = new XMLHelper(Session["Inboxpath"].ToString());
            //    InboxItems item = objHelper.SearchByInboxItemName(Session["WarehouseInboxItemName"].ToString());
            //    List<TransactionDetail> listDisplay = item.GetTransactions().Where(t => t.TrackNo == taskNo).ToList();
            //    if ("Select Trucks For Sampling" == Session["WarehouseInboxItemName"].ToString())
            //    {
            //        TransactionDetail obj = new TransactionDetail("", "");
            //        obj.DisplayName = "Select Trucks for Sampling";
            //        obj.TrackNo = "GetTrucksReadyForSam";
            //        listDisplay.RemoveAll(RemovePredicate);
            //        listDisplay.Add(obj);
            //    }
            //    else if ("Confirm Trucks For Sampling" == Session["WarehouseInboxItemName"].ToString())
            //    {
            //        TransactionDetail obj = new TransactionDetail("", "");
            //        obj.DisplayName = "Confirm Truks For Sampling";
            //        obj.TrackNo = "ConfirmTrucksForSamp";
            //        listDisplay.RemoveAll(RemovePredicate);
            //        listDisplay.Add(obj);
            //    }
            //    else if ("Assign Sampler".Trim() == Session["WarehouseInboxItemName"].ToString().Trim())
            //    {
            //        TransactionDetail obj = new TransactionDetail("", "");
            //        obj.DisplayName = "Assign Sampler";
            //        obj.TrackNo = "GetSampleTicket";
            //        listDisplay.RemoveAll(RemovePredicate);
            //        listDisplay.Add(obj);
            //    }

            //    this.gvDetail.DataSource = listDisplay;
            //    this.gvDetail.DataBind();
            //}
#endregion
            if( string.IsNullOrEmpty( txtTaskNo.Text ))
            {
                msg.Text = "Please Provide Tracking No.";
                return ;
            }
            string strTR = txtTaskNo.Text.Trim();
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            ECXWF.CMessage[] mess = null;
          
            try
            {
                //eng.UnlockMessageByUser(Request.QueryString["TranNo"], "AddVoucherInformation", UserBLL.GetCurrentUser());
                //eng.RemoveTransactionFromStack(Request.QueryString["TranNo"]);
                mess = eng.Request(strTR, UserBLL.GetCurrentUser(), new string[] { WarehouseBLL.CurrentWarehouse.Location });
                if (mess != null)
                {
                    WFTransaction.UnlockTask(strTR);
                    List<TransactionDetail> lst = new List<TransactionDetail>(new TransactionDetail[]{
                        new TransactionDetail(strTR, strTR)});
                    
                   
                    this.gvDetail.DataSource = lst;
                    this.gvDetail.DataBind();
                }
                else
                {
                    msg.Text = "No Tracking number matchs the supplied criteria";
                }
                
            }
            catch (Exception ex)
            {
                txtTaskNo.Text = "";                  
                msg.Text = "Re-enter the Tracking No and Try Again" ;
                //throw ex;
            }
        }

        protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //SetUI();
            //gvDetail.PageIndex = e.NewPageIndex;
            //gvDetail.DataBind();
        }
    }
}