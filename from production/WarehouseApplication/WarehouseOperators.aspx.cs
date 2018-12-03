using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using GINBussiness;
using System.Data;

namespace WarehouseApplication
{
    public partial class WarehouseOperators : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblShed.Visible = false;
                drpShed.Visible = false;
                Session.Remove("TypeNo");
                FillWareHouse();
                FillType();
                FillOperator();
            }
        }
        private void FillWareHouse()
        {
            drpWarehouse.Items.Clear();
            drpWarehouse.Items.AddRange(
                (from warehouse in WarehouseBLL.GetAllActiveWarehouse()
                 select new ListItem() { Text = warehouse.WarehouseName, Value = warehouse.WarehouseId.ToString() }).ToArray());
            drpWarehouse.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void gvWareHouseoperators_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }
        private void FillShed()
        {
            drpShed.DataSource = null;
            drpShed.DataSource = WarehouseOperator.Shed(new Guid(drpWarehouse.SelectedValue));
            drpShed.DataTextField = "ShedNo";
            drpShed.DataValueField = "ShedID";
            drpShed.DataBind();
            drpShed.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        private void FillType()
        {
            drpType.DataSource = null;
            drpType.DataSource = WarehouseOperator.GetWarehouseOperatorType();
            drpType.DataTextField = "Description";
            drpType.DataValueField = "ID";
            drpType.DataBind();
            drpType.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        private void FillOperators()
        {
            gvWareHouseoperators.DataSource = WarehouseOperator.WarehouseOperatorsList(new Guid(drpWarehouse.SelectedValue), Convert.ToInt32(drpType.SelectedValue));
            gvWareHouseoperators.DataBind();
        }
        private void FillOperator()
        {
            drpOperator.DataSource = null;
            drpOperator.DataSource = WarehouseOperator.GetWarehouseOperators();
            drpOperator.DataTextField = "Name";
            drpOperator.DataValueField = "ID";
            drpOperator.DataBind();
            drpOperator.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void gvWareHouseoperators_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWareHouseoperators.PageIndex = e.NewPageIndex;
            FillOperators();
        }
        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            if (drpWarehouse.SelectedValue == "")
            {
                Messages.SetMessage("Warehouse is required", WarehouseApplication.Messages.MessageType.Warning);
                return;
            }
            if (Convert.ToInt32(drpType.SelectedValue) == (int)WareHouseOperatorTypeEnum.LIC)
            {
                lblShed.Visible = true;
                drpShed.Visible = true;
                FillShed();
            }
            else
            {
                lblShed.Visible = false;
                drpShed.Visible = false;
            }            
        }
        private void SateOperator()
        {
            WarehouseOperator OperatorModel = new WarehouseOperator();
            OperatorModel.ID = Guid.NewGuid();
            OperatorModel.OperatorId = new Guid(drpOperator.SelectedValue);
            OperatorModel.WarehouseID = new Guid(drpWarehouse.SelectedValue);
            ////if (Session["TypeNo"] == null)
                OperatorModel.Type = Convert.ToInt32(drpType.SelectedValue);
            ////else
            ////    OperatorModel.Type = Convert.ToInt32(Session["TypeNo"]) * Convert.ToInt32(drpType.SelectedValue);
            if (drpShed.SelectedValue != "")
            {
                OperatorModel.ShedID = new Guid(drpShed.SelectedValue);
                OperatorModel.ShedNo = drpShed.SelectedItem.Text;
            }
            else
            {
                OperatorModel.ShedID = Guid.Empty;
                OperatorModel.ShedNo = "";
            }
            OperatorModel.CreatedBy = UserBLL.GetCurrentUser();
            OperatorModel.CreatedDate = DateTime.Now;
            OperatorModel.LastModifiedBy = UserBLL.GetCurrentUser();
            OperatorModel.LastModifiedDate = DateTime.Now;
            OperatorModel.Save();
            Session.Remove("TypeNo");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DoValid())
            {
                SateOperator();
                FillWareHouse();
                FillType();
                FillOperator();
               // drpShed.SelectedIndex = 0;
                Messages.SetMessage("Successfully saved", WarehouseApplication.Messages.MessageType.Success);
            }
        }
        private bool DoValid()
        {
            if (drpWarehouse.SelectedValue == "")
            {
                Messages.SetMessage("Warehouse is required", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (drpType.SelectedValue == "")
            {
                Messages.SetMessage("Operator Type is required", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (drpOperator.SelectedValue == "")
            {
                Messages.SetMessage("Operator is required", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            return true;
        }
        protected void drpOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = WarehouseOperator.GetWarehouseOperatorTypeNo(new Guid(drpOperator.SelectedValue));
            if (dt.Rows.Count > 0)
                Session["TypeNo"] = dt.Rows[0]["Type"];
        }
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            if (DoValid())
            {
                DisableOperator();
                FillWareHouse();
                FillType();
                FillOperator();
                //drpShed.SelectedIndex = 0;
                Messages.SetMessage("Successfully updated", WarehouseApplication.Messages.MessageType.Success);
            }
        }
        private void DisableOperator()
        {
            WarehouseOperator OperatorModel = new WarehouseOperator();

            OperatorModel.OperatorId = new Guid(drpOperator.SelectedValue);
            OperatorModel.WarehouseID = new Guid(drpWarehouse.SelectedValue);
            OperatorModel.Type = Convert.ToInt32(drpType.SelectedValue);
            if (drpShed.SelectedValue != "")
            {
                OperatorModel.ShedID = new Guid(drpShed.SelectedValue);
            }
            else
            {
                OperatorModel.ShedID = Guid.Empty;
            }
            OperatorModel.LastModifiedBy = UserBLL.GetCurrentUser();
            OperatorModel.LastModifiedDate = DateTime.Now;
            OperatorModel.DisableWareHouseOperator();    
        }
    }
}
