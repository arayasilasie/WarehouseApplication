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
    public partial class ManageLocations : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillRegion(cboRegion);
                FillZone();
                FillWorada();
            }
            Messages.ClearMessage();
            UpdatePanel1.Update();
        }

        void FillRegion(DropDownList dr)
        {
            dr.Items.Clear();
            dr.Items.Add(new ListItem("- Please Select Region -", ""));
            dr.DataSource = ModelLocation.GetRegion();
            dr.DataValueField = "ID";
            dr.DataTextField = "Description";
            dr.DataBind();
        }

        void FillZone()
        {
            gvZone.DataSource = new DataTable();
            gvZone.DataBind();

            drpZoneRegion.Items.Clear();

            gvWereda.DataSource = new DataTable();
            gvWereda.DataBind();

            drpWoredaZone.Items.Clear();

            if (cboRegion.SelectedIndex > 0)
            {
                string regionID = cboRegion.SelectedValue;
                Guid reID = new Guid(regionID);
                gvZone.DataSource = ModelLocation.GetZone(new Guid(cboRegion.SelectedValue));
                gvZone.DataBind();
                txtAddZone.Text = string.Empty;

                FillRegion(drpZoneRegion);
                drpZoneRegion.SelectedValue = cboRegion.SelectedValue;
            }
        }

        void FillWorada()
        {
            gvWereda.DataSource = new DataTable();
            gvWereda.DataBind();

            drpWoredaZone.Items.Clear();

            if (gvZone.SelectedIndex >= 0 && drpZoneRegion.SelectedIndex > 0)
            {
                gvWereda.DataSource = ModelLocation.GetWoreda(new Guid(((Label)gvZone.SelectedRow.FindControl("lblZoneID")).Text.Trim())
                         , new Guid(drpZoneRegion.SelectedValue));
                gvWereda.DataBind();
                txtAddWoreda.Text = string.Empty;

                FillRegion(drpWoredaRegion);
                FillZoneForWoreda(new Guid(drpZoneRegion.SelectedValue));
                drpWoredaZone.SelectedValue = ((Label)gvZone.SelectedRow.FindControl("lblZoneID")).Text.Trim();
                drpWoredaRegion.SelectedValue = drpZoneRegion.SelectedValue;
            }
        }

        void FillZoneForWoreda(Guid Region)
        {
            drpWoredaZone.Items.Add("");
            drpWoredaZone.DataSource = ModelLocation.GetZone(Region);
            drpWoredaZone.DataValueField = "ID";
            drpWoredaZone.DataTextField = "Description";
            drpWoredaZone.DataBind();
        }

        protected void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvZone.PageIndex = 0;
            gvZone.SelectedIndex = -1;
            gvWereda.SelectedIndex = -1;
            btnUpdateworeda.Visible = false;
            btnUpdateZone.Visible = false;
            txtLocationName.Text = string.Empty;
            if (cboRegion.SelectedIndex > 0)
            {
                txtAddRegion.Text = cboRegion.SelectedItem.Text;
                btnUpdateRegion.Visible = true;
            }
            else
            {
                txtAddRegion.Text = string.Empty;
                btnUpdateRegion.Visible = false;
            }
            FillZone();
        }

        protected void gvZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvZone.PageIndex = e.NewPageIndex;
            gvZone.DataSource = ModelLocation.GetZone(new Guid(cboRegion.SelectedValue));
            gvZone.DataBind();
        }

        protected void gvZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvWereda.PageIndex = 0;
            gvWereda.SelectedIndex = -1;
            btnUpdateworeda.Visible = false;
            txtAddZone.Text = ((Label)gvZone.SelectedRow.FindControl("lblZone")).Text.Trim();
            drpZoneRegion.SelectedValue = ((Label)gvZone.SelectedRow.FindControl("lblRegionID")).Text.Trim();
            FillWorada();
            btnUpdateZone.Visible = true;
        }

        protected void gvWereda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWereda.PageIndex = e.NewPageIndex;
            gvWereda.DataSource = ModelLocation.GetWoreda(new Guid(((Label)gvZone.SelectedRow.FindControl("lblZoneID")).Text.Trim())
                         , new Guid(cboRegion.SelectedValue));
            gvWereda.DataBind();
        }

        protected void btnAddRegion_Click(object sender, EventArgs e)
        {
            SaveRegion(true);
        }

        void SaveRegion(bool isNEW)
        {
            ModelLocation ml = new ModelLocation();
            ml.ID = isNEW ? Guid.NewGuid() : new Guid(cboRegion.SelectedValue);
            ml.Description = txtAddRegion.Text.Trim();
            ml.RegionID = ml.ID;
            ml.ZoneID = null;
            ml.WoredaID = null;
            ml.Save();
            Messages.SetMessage("Location successfully updated!", WarehouseApplication.Messages.MessageType.Success);
            UpdatePanel1.Update();
            FillRegion(cboRegion);
            txtAddRegion.Text = string.Empty;
        }

        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            SaveZone(true);
        }

        void SaveZone(bool isNEW)
        {
            if (cboRegion.SelectedIndex <= 0)
            {
                Messages.SetMessage("Please select Region first!", WarehouseApplication.Messages.MessageType.Warning);
                UpdatePanel1.Update();
                return;
            }
            ModelLocation ml = new ModelLocation();
            ml.ID = isNEW ? Guid.NewGuid() : new Guid(((Label)gvZone.SelectedRow.FindControl("lblZoneID")).Text.Trim());
            ml.Description = txtAddZone.Text.Trim();
            ml.RegionID = new Guid(drpZoneRegion.SelectedValue);
            ml.ZoneID = ml.ID;
            ml.WoredaID = null;
            ml.Save();
            Messages.SetMessage("Location successfully updated!", WarehouseApplication.Messages.MessageType.Success);
            UpdatePanel1.Update();
            FillZone();
        }

        protected void btnAddWoreda_Click(object sender, EventArgs e)
        {
            SaveWoreda(true);
        }

        void SaveWoreda(bool isNEW)
        {
            if (cboRegion.SelectedIndex <= 0)
            {
                Messages.SetMessage("Please select Region first!", WarehouseApplication.Messages.MessageType.Warning);
                UpdatePanel1.Update();
                return;
            }
            else if (gvZone.SelectedIndex < 0)
            {
                Messages.SetMessage("Please select Zone first!", WarehouseApplication.Messages.MessageType.Warning);
                UpdatePanel1.Update();
                return;
            }
            ModelLocation ml = new ModelLocation();
            ml.ID = isNEW ? Guid.NewGuid() : new Guid(((Label)gvWereda.SelectedRow.FindControl("lblWoredaID")).Text.Trim());
            ml.Description = txtAddWoreda.Text.Trim();
            ml.RegionID = new Guid(cboRegion.SelectedValue);
            ml.ZoneID = new Guid(drpWoredaZone.SelectedValue);
            ml.WoredaID = ml.ID;
            ml.Save();
            FillWorada();
            Messages.SetMessage("Location successfully updated!", WarehouseApplication.Messages.MessageType.Success);
            UpdatePanel1.Update();
        }

        protected void gvWereda_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ModelLocation.DeleteLocation(new Guid(((Label)gvWereda.Rows[e.RowIndex].FindControl("lblWoredaID")).Text));
            FillWorada();
        }

        protected void gvZone_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = ModelLocation.GetWoreda(new Guid(((Label)gvZone.Rows[e.RowIndex].FindControl("lblZoneID")).Text.Trim())
                         , new Guid(cboRegion.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                Messages.SetMessage("The zone contains " + dt.Rows.Count +
                     " worada under it. Removing a zone having one or more worada is not allowed!"
                    , WarehouseApplication.Messages.MessageType.Error);
                UpdatePanel1.Update();
                return;
            }

            ModelLocation.DeleteLocation(new Guid(((Label)gvZone.Rows[e.RowIndex].FindControl("lblZoneID")).Text));
            FillZone();
        }

        protected void btnUpdateworeda_Click(object sender, EventArgs e)
        {
            SaveWoreda(false);
        }

        protected void btnUpdateZone_Click(object sender, EventArgs e)
        {
            SaveZone(false);
        }

        protected void gvWereda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAddWoreda.Text = ((Label)gvWereda.SelectedRow.FindControl("lblWoreda")).Text.Trim();
            drpWoredaRegion.SelectedValue = ((Label)gvWereda.SelectedRow.FindControl("lblRegionID")).Text.Trim();
            if (cboRegion.SelectedIndex <= 0)
            {
                FillZoneForWoreda(new Guid(drpWoredaRegion.SelectedValue));
            }
            drpWoredaZone.SelectedValue = ((Label)gvWereda.SelectedRow.FindControl("lblZoneID")).Text.Trim();
            btnUpdateworeda.Visible = true;
        }

        protected void btnUpdateRegion_Click(object sender, EventArgs e)
        {
            SaveRegion(false);
        }

        protected void btnRemoveRegion_Click(object sender, EventArgs e)
        {
            DataTable dt = ModelLocation.GetZone(new Guid(cboRegion.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                Messages.SetMessage("The Region contains " + dt.Rows.Count +
                     " zone under it. Removing Region having one or more zone is not allowed!"
                    , WarehouseApplication.Messages.MessageType.Error);
                UpdatePanel1.Update();
                return;
            }

            ModelLocation.DeleteLocation(new Guid(cboRegion.SelectedValue));
            FillRegion(cboRegion);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            cboRegion.ClearSelection();
            drpWoredaRegion.ClearSelection();
            drpWoredaZone.ClearSelection();
            drpWoredaZone.Items.Clear();
            drpZoneRegion.ClearSelection();
            txtAddRegion.Text = txtAddWoreda.Text = txtAddZone.Text = string.Empty;
            gvWereda.DataSource = new DataTable();
            gvWereda.DataBind();
            gvZone.DataSource = new DataTable();
            gvZone.DataBind();

            if (rbZoneWoreda.SelectedValue == "1")//woreda
            {
                FillRegion(drpWoredaRegion);
                gvWereda.DataSource = ModelLocation.GetWoredaByName(txtLocationName.Text);
                gvWereda.DataBind();
            }
            else if (rbZoneWoreda.SelectedValue == "2")//zone
            {
                FillRegion(drpZoneRegion);
                gvZone.DataSource = ModelLocation.GetZoneByName(txtLocationName.Text);
                gvZone.DataBind();
            }
        }
    }
}