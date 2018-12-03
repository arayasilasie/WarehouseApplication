using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    
    public partial class UIInboxDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
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
                listDisplay = item.GetTransactions();
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
    }
}