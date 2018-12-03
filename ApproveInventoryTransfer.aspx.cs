using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class ApproveInventoryTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindApprovalGridview();
        }

        public void BindApprovalGridview()
        {
            grvInvTransferApproval.DataSource = InventoryTransferModel.GetInventoryTransferForApproval(new Guid(Session["CurrentWarehouse"].ToString()));
            grvInvTransferApproval.DataBind();
        }
        protected void grvInvTransferApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid ID = new Guid(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblID")).Text);
            Guid StackID = new Guid(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblStackID")).Text);
           
            Guid StackID2 = new Guid(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblStackID2")).Text);
            Guid LIC2 = new Guid(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblLICID2")).Text);
            int PhysicalCount = int.Parse(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblPhysicalCount")).Text);
            int PhysicalCount2 = int.Parse(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblPhysicalCount2")).Text);
            float PhysicalWeight =float.Parse(( (Label)grvInvTransferApproval.SelectedRow.FindControl("lblPhysicalWeight")).Text);
            float PhysicalWeight2 = float.Parse(((Label)grvInvTransferApproval.SelectedRow.FindControl("lblPhysicalWeight2")).Text);
            Guid ApprovedByID = UserBLL.CurrentUser.UserId;
            DateTime DateApproved = DateTime.Now;

            try
            {
                InventoryTransferModel.ApproveInventorysTransfer(ID, ApprovedByID, DateApproved, StackID,StackID2, LIC2, 
                    (PhysicalCount + PhysicalCount2), (PhysicalWeight + PhysicalWeight2));

                Messages1.SetMessage("Record approved successfully.", WarehouseApplication.Messages.MessageType.Success);
                BindApprovalGridview();
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            }          
        }

        protected void grvInvTransferApproval_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
   
                HeaderGridRow.BackColor=Color.White;
                HeaderGridRow.ForeColor = ColorTranslator.FromHtml("#008000");
               
                //Add Transfer 
                HeaderCell.Text = "TRANSFER";             
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BorderWidth = 1;
                HeaderCell.BorderColor = ColorTranslator.FromHtml("#FFCC00");
                HeaderGridRow.Cells.Add(HeaderCell);

                //Add Transfer From
                HeaderCell = new TableCell();
                HeaderCell.Text = "TRANSFER FROM"; 
                HeaderCell.BorderColor = ColorTranslator.FromHtml("#FFCC00");
                HeaderCell.BorderWidth = 1;
                HeaderCell.ColumnSpan = 6;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                //Add Transfer To
                HeaderCell = new TableCell();
                HeaderCell.Text = "TRANSFER TO";
                HeaderCell.BorderColor = ColorTranslator.FromHtml("#FFCC00");
                HeaderCell.BorderWidth = 1;
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                grvInvTransferApproval.Controls[0].Controls.AddAt(0, HeaderGridRow); 

            }
         
        }
    }
}