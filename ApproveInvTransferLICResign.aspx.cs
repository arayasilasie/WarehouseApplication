using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class ApproveInvTransferLICResign : System.Web.UI.Page
    {
        static Guid TransferID;
        static DataTable dtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindApprovalGridview();
        }

        public void BindApprovalGridview()
        {
            grvInvTransferApproval.DataSource = InventoryTransferModel.GetInvTransferForApprovalLICResign(new Guid(Session["CurrentWarehouse"].ToString()));
            grvInvTransferApproval.DataBind();
        }
        protected void grvInvTransferApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid ID = new Guid(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblID")).Text);
            Guid ApprovedByID = UserBLL.CurrentUser.UserId;
            DateTime DateApproved = DateTime.Now;

            try
            {
                InventoryTransferModel.ApproveInvTransferLICResign(ID, ApprovedByID, DateApproved);
                Messages1.SetMessage("Record approved successfully.", WarehouseApplication.Messages.MessageType.Success);
                BindApprovalGridview();

            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            }
        }

        protected void grvInvTransferApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}