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
    public partial class InventoryTransferWhenLICResign : System.Web.UI.Page
    {
        static Guid CurrentWarehouse;
        static DataTable dtbl;
        static int countError;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            CurrentWarehouse = new Guid(Session["CurrentWarehouse"].ToString());
            BindLIC();

            RangeValidatorDate.MinimumValue = DateTime.Now.AddYears(-1).ToShortDateString();
            RangeValidatorDate.MaximumValue = DateTime.Now.ToShortDateString();

        }
        public void BindLIC()
        {
            ddLIC.DataSource = InventoryTransferModel.GetLICsForInventoryTransfer(CurrentWarehouse);
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }
        public void BindLIC2()
        {
            ddLIC2.Items.Clear();
            ddLIC2.Items.Add(new ListItem("Select LIC", ""));
            ddLIC2.DataSource = InventoryTransferModel.GetLICsToAssignInventory(CurrentWarehouse, new Guid(ddLIC.SelectedValue));
            ddLIC2.DataTextField = "Name";
            ddLIC2.DataValueField = "ID";
            ddLIC2.DataBind();
        }
        public void BindGridviewInvTransfer()
        {
            dtbl=InventoryTransferModel.GetInventoryTransferByLIC(CurrentWarehouse, new Guid(ddLIC.SelectedValue));
            grvInvTransferLICResign.DataSource = dtbl;
            grvInvTransferLICResign.DataBind();
        }
        protected void ddLIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddLIC.SelectedValue != "")
            {
                BindGridviewInvTransfer();
                BindLIC2();
            }
        }
        bool isValidTransferDetail(string phyCount,string phyWeight)
        {
            int count;
            float weight;

            if (phyCount == "" || phyWeight == "")
            {
                Messages1.SetMessage("Please physical count and weight. ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }

            else if (!(int.TryParse(phyCount , out count)))
            {
                Messages1.SetMessage("Please enter valid number. ", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }
            else if (!(float.TryParse(phyWeight, out weight)))
            {
                Messages1.SetMessage("Please enter valid weight.", WarehouseApplication.Messages.MessageType.Warning);
                countError++;
                return false;
            }            
            else
            {
                return true;
            }
        }

        bool IsValidTransfer()
        {
            DateTime transferDate;
            if (txtTransferDate.Text == "" ||ddLIC.SelectedValue == "" || ddLIC2.SelectedValue == "")
            {
                Messages1.SetMessage("Please enter all values.", WarehouseApplication.Messages.MessageType.Success);
                countError++;
                return false;
            }
            else if (!(DateTime.TryParse(txtTransferDate.Text, out  transferDate)))
            {
                Messages1.SetMessage("Please enter valid date.", WarehouseApplication.Messages.MessageType.Success);
                countError++;
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            countError=0;
            Guid ID = Guid.NewGuid();
            string phyCount;
            string phyWeight;
            string InventoryTransferXML;
            string TransferDetailXML = "<InventoryTransfer>";

            if (IsValidTransfer())
            {
                foreach (GridViewRow gvr in this.grvInvTransferLICResign.Rows)
                {
                    phyCount = ((TextBox)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("txtPhysicalCount")).Text;
                    phyWeight = ((TextBox)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("txtPhysicalWeight")).Text;

                    if (isValidTransferDetail(phyCount, phyWeight))
                    {
                        TransferDetailXML +=
                         "<InventoryTransferItem>" +
                         "<ID>" + Guid.NewGuid() + "</ID>" +
                          "<TransferID>" + ID + "</TransferID>" +
                         "<ShedID>" + ((Label)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("lblShedID")).Text + "</ShedID>" +
                         "<StackNo>" + ((Label)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("lblID")).Text + "</StackNo>" +
                         "<PhysicalCount>" + ((TextBox)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("txtPhysicalCount")).Text + "</PhysicalCount>" +
                         "<SystemCount>" + ((Label)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("lblSystemCount")).Text + "</SystemCount>" +
                         "<PhysicalWeight>" + ((TextBox)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("txtPhysicalWeight")).Text + "</PhysicalWeight>" +
                         "<SystemWeight>" + ((Label)grvInvTransferLICResign.Rows[gvr.RowIndex].Cells[0].FindControl("lblSystemWeigh")).Text + "</SystemWeight>" +
                         "</InventoryTransferItem>";
                    }
                }
                TransferDetailXML += "</InventoryTransfer>";

                if (countError == 0)
                {

                    InventoryTransferXML = "<InventoryTransfer>" +
                            "<ID>" + ID + "</ID>" +
                            "<WarehouseID>" + new Guid(Session["CurrentWarehouse"].ToString()) + "</WarehouseID>" +
                            "<LICID>" + ddLIC.SelectedValue + "</LICID>" +
                            "<LICIDTo>" + ddLIC2.SelectedValue + "</LICIDTo>" +
                            "<TransitionDate>" + txtTransferDate.Text + "</TransitionDate>" +
                            "<CreatedBy>" + UserBLL.CurrentUser.UserId + "</CreatedBy>" +
                            "<CreatedTimestamp>" + DateTime.Now + "</CreatedTimestamp>" +
                            "<Status>" + 1 + "</Status>" +
                            "</InventoryTransfer>";
                    try
                    {
                        InventoryTransferModel.InventoryTransferLICResign(InventoryTransferXML, TransferDetailXML);
                        Messages1.SetMessage("Record added successfully.", WarehouseApplication.Messages.MessageType.Success);
                        BindGridviewInvTransfer();

                        if (ID != null)
                        {
                            Session["ID"] = ID;
                            ScriptManager.RegisterStartupScript(this,
                                                this.GetType(),
                                                "ShowReport",
                                                "<script type=\"text/javascript\">" +
                                                string.Format("javascript:window.open(\"ReportInvTransfer.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                                                "</script>",
                                                false);
                        }

                    }
                    catch (Exception ex)
                    {
                        Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            BindInventoryTransfer();
        }

        void BindInventoryTransfer()
        {
            grvInvTransferSearch.DataSource = InventoryTransferModel.GetInvTransferLICRsnForEdit
                (new Guid(Session["CurrentWarehouse"].ToString()), DateTime.Parse(txtTrannsferDateSrch.Text));
            grvInvTransferSearch.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetUrl(object ID)
        {
            Session["ID"] = ID.ToString();
            string url = "~/ReportInvTransfer.aspx";
            return url;

        }

        protected void grvInvTransferSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InventoryTransferModel.CancelInvTransferLICResign(new Guid(grvInvTransferSearch.SelectedDataKey[0].ToString()));
                Messages1.SetMessage("Record cancelled successfully.", WarehouseApplication.Messages.MessageType.Success);
                DateTime test;
                if (DateTime.TryParse(txtTrannsferDateSrch.Text, out test))
                    BindInventoryTransfer();
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);

            }
        }

        protected void grvInvTransferLICResign_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInvTransferLICResign.PageIndex = e.NewPageIndex;
            grvInvTransferLICResign.DataSource = dtbl;
            grvInvTransferLICResign.DataBind();
        }
               
    }
}