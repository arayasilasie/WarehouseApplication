using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using InventoryControlBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class InventoryControlAdjustment : System.Web.UI.Page
    {
        string CGID;
        string PY;
        protected List<InventoryInspector> inventoryInspectors
        {
            get
            {
                if (ViewState["InventoryInspectors"] != null)
                    return ((List<InventoryInspector>)ViewState["InventoryInspectors"]);
                else
                    return new List<InventoryInspector>();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid currentWarehouse = UserBLL.GetCurrentWarehouse();
                string shedId = Request.Params["shedId"] == null ? "" : Request.Params["shedId"];
                string LICID = Request.Params["LICID"] == null ? "" : Request.Params["LICID"];
                string Reason = Request.Params["LICID"] == null ? "" :"3";
                ViewState["psaApprovalInfoXML"] = Session["psaApprovalInfoXML"];
                Session["psaApprovalInfoXML"] = null;
                CGID = Request.Params["CGID"] == null ? "" : Request.Params["CGID"];
                PY = Request.Params["PY"] == null ? "" : Request.Params["PY"];
                gvInventoryDetail.DataSource = new DataTable();
                gvInventoryDetail.DataBind();
                //This will load all the sheds in the warehouse
                FillShedByWarehouse(currentWarehouse, shedId, LICID, CGID, PY);
                FillInventoryReason(Reason);
                FillInventoryInspectors(currentWarehouse);
                //load list of inventory inspectors in the warehouse
                gvInventoryInspectors.DataSource = inventoryInspectors;
                gvInventoryInspectors.DataBind();
            }
            RangeValidator2.MaximumValue = DateTime.Now.ToShortDateString();
            unBlockUI();
        }

        void unBlockUI()
        {
            ScriptManager.RegisterStartupScript(upButtons, this.GetType(),
                           "unBlockUI", "<script type='text/javascript'>$.unblockUI();</script>", false);
        }

        private void FillInventoryReason(string Reason)
        {
            drpInventoryReason.DataSource = null;
            drpInventoryReason.Items.Clear();
            drpInventoryReason.Items.Add(new ListItem("- - Please Select Reason - -", ""));
            drpInventoryReason.DataSource = InventoryControlModel.GetInventoryReason();
            drpInventoryReason.DataValueField = "ID";
            drpInventoryReason.DataTextField = "Description";
            drpInventoryReason.DataBind();
            if (Reason != string.Empty && drpInventoryReason.Items.Count > 1)
            {
                drpInventoryReason.SelectedValue = Reason;
                drpInventoryReason.Enabled = false;
            }
        }

        private void FillInventoryInspectors(Guid warehouseID)
        {
            drpInventoryInspector.DataSource = null;
            drpInventoryInspector.Items.Clear();
            drpInventoryInspector.Items.Add(new ListItem("- - Please Select Inspector - -", ""));
            drpInventoryInspector.DataSource = InventoryControlModel.GetOperationControllers(warehouseID);
            drpInventoryInspector.DataValueField = "ID";
            drpInventoryInspector.DataTextField = "Name";
            drpInventoryInspector.DataBind();
        }

        private void FillShedByWarehouse(Guid warehouseID, string shedId, string LICID, string CGID, string PY)
        {
            drpShed.DataSource = null;
            drpShed.Items.Clear();
            drpShed.Items.Add(new ListItem("- - Please Select Shed - -", ""));
            DataRow[] drs = null;
            if (shedId != string.Empty)
            {   
                DataTable dt = InventoryControlModel.GetShedByWarehouse(warehouseID);
                drs = dt.Select("ShedID = '" + shedId + "'");
                if (drs.Count() <= 0)
                {
                    Messages1.SetMessage("Selected Shed for the PSA does not exist!", Messages.MessageType.Error);
                    return;
                }
                object[] obj = drs[0].ItemArray;
                dt.Clear();
                dt.Rows.Add(obj);
                drpShed.DataSource = dt;
            }
            else
                drpShed.DataSource = InventoryControlModel.GetShedByWarehouse(warehouseID);
            drpShed.DataValueField = "ShedID";
            drpShed.DataTextField = "ShedNo";
            drpShed.DataBind();
            if (shedId != string.Empty && drpShed.Items.Count > 1)
            {
                drpShed.SelectedIndex = 1;
                FillLIC(new Guid(drpShed.SelectedValue), LICID, CGID, PY);
                drpShed.Enabled = false;
            }
        }

        private void FillLIC(Guid shedID, string LICID, string CGID, string PY)
        {
            drpLIC.DataSource = null;
            drpLIC.Items.Clear();
            drpLIC.Items.Add(new ListItem("- - Please Select LIC - -", ""));
            DataRow[] drs = null;
            if (LICID != string.Empty)
            {
                DataTable dt = InventoryControlModel.GetLIC(shedID);
                drs = dt.Select("ID = '" + LICID + "'");
                if (drs.Count() <= 0) Messages1.SetMessage("LIC of the PSA DO NOT exits!", Messages.MessageType.Error);
                object[] obj = drs[0].ItemArray;
                dt.Clear();
                dt.Rows.Add(obj);
                drpLIC.DataSource = dt;
            }
            else
                drpLIC.DataSource = InventoryControlModel.GetLIC(shedID);
            
            drpLIC.DataValueField = "ID";
            drpLIC.DataTextField = "Name";
            drpLIC.DataBind();

            if (LICID != string.Empty && drpLIC.Items.Count > 1)
            {
                drpLIC.SelectedIndex = 1;
                FillInventoryDetail(new Guid(drpShed.SelectedValue), new Guid(drpLIC.SelectedValue), new Guid(CGID), PY);
                drpLIC.Enabled = false;
            }
        }

        private void FillInventoryDetail(Guid shedID, Guid LICID, Guid CGID, string PY)
        {
            gvInventoryDetail.DataSource = InventoryControlModel.GetInventoryDetail(shedID, LICID, CGID, PY);
            gvInventoryDetail.DataBind();
        }

        private bool ControlsValidated()
        {
            //validate combo and grids
            string errorMessage = "";
            if (drpShed.SelectedIndex <= 0)
                errorMessage += "Please select Shed! ";
            else if (drpLIC.SelectedIndex <= 0)
                errorMessage += "Please select Lead Inventory Controller! ";
            else if (drpInventoryReason.SelectedIndex <= 0)
                errorMessage += "Please select Inventory Reason";
            else if (gvInventoryInspectors.Rows.Count <= 0)
                errorMessage += "Please add atleast one inspector!";
            else if (gvInventoryDetail.Rows.Count <= 0)
                errorMessage += "One or more inventory detail required!";

            if (errorMessage != string.Empty)
            {
                Messages1.SetMessage(errorMessage, Messages.MessageType.Error);
                return false;
            }
            return true;
        }

        private InventoryControlModel SetValues()
        {
            InventoryControlModel icm = new InventoryControlModel();
            icm.ID = Guid.NewGuid();
            icm.LICID = new Guid(drpLIC.SelectedValue);
            icm.InventoryReasonID = int.Parse(drpInventoryReason.SelectedValue);
            icm.WarehouseID = UserBLL.GetCurrentWarehouse();
            icm.ShedID = new Guid(drpShed.SelectedValue);
            icm.CreatedBy = UserBLL.CurrentUser.UserId;
            icm.CreateTimestamp = DateTime.Now;
            icm.InventoryDate = DateTime.Parse(txtInventoryControlDate.Text);
            icm.PSAStackID = Guid.Empty;
            //if (InventoryControlModel.InventoryControlExits(icm))
            //{
            //    Messages1.SetMessage("Inventory Control exits! You can't insert inventory control for the same Shed, Lead Inventory Controller and Inventory Date!", Messages.MessageType.Error);
            //    return null;
            //}

            if (ViewState["psaApprovalInfoXML"] != null)
            {
                icm.psaApprovalInfoXML = ViewState["psaApprovalInfoXML"].ToString();
                string psaID = icm.psaApprovalInfoXML.Substring(
                    icm.psaApprovalInfoXML.IndexOf("<ID>") + 4, 36);
                icm.PSAID = new Guid(psaID);
                ViewState["psaApprovalInfoXML"] = null;
            }

            InventoryDetail invdetail;
            float count = -1; float weight = -1;
            bool stackSelected = false;
            foreach (GridViewRow gr in gvInventoryDetail.Rows)
            {
                if (((CheckBox)gr.FindControl("rdoPSAstack")).Checked)
                {
                    stackSelected = true;
                    TextBox pc = ((TextBox)gr.FindControl("txtPhysicalCount"));
                    TextBox pw = ((TextBox)gr.FindControl("txtPhysicalWeight"));
                    if (pc.Text.Trim() == string.Empty && pw.Text.Trim() == string.Empty)
                        continue;

                    invdetail = new InventoryDetail();
                    invdetail.InventoryID = icm.ID;
                    invdetail.StackID = new Guid(((Label)gr.FindControl("lblId")).Text);
                    
                    icm.PSAStackID = invdetail.StackID; //b coz PSA occurres in only one stack.

                    if (float.TryParse(((Label)gr.FindControl("lblSystemCount")).Text, out count) && count >= 0)
                    {
                        invdetail.SystemCount = count; count = -1;
                    }
                    else
                        invdetail.SystemCount = 0;
                    if (float.TryParse(((Label)gr.FindControl("lblSystemWeight")).Text, out weight) && weight >= 0)
                    {
                        invdetail.SystemWeight = weight; weight = -1;
                    }
                    else
                        invdetail.SystemWeight = 0;
                    if (float.TryParse(pc.Text, out count) && count >= 0)
                    {
                        invdetail.PhysicalCount = (float)Math.Round(count, 2); count = -1;
                    }
                    else
                    {
                        Messages1.SetMessage("Physical count entry invalid for selected Stack number " + ((Label)gr.FindControl("lblStackNo")).Text +
                              "!.Please insert non-negative integer", Messages.MessageType.Error);
                        return null;
                    }
                    if (float.TryParse(pw.Text, out weight) && weight >= 0)
                    {
                        invdetail.PhysicalWeight = (float)Math.Round(weight); weight = -1;
                    }
                    else
                    {
                        Messages1.SetMessage("Physical weight entry invalid for selected Stack number " + ((Label)gr.FindControl("lblStackNo")).Text +
                              "!.Please insert non-negative decimal number.", Messages.MessageType.Error);
                        return null;
                    }
                    invdetail.AdjustmentCount = invdetail.PhysicalCount - invdetail.SystemCount;
                    invdetail.AdjustmentWeight = invdetail.PhysicalWeight - invdetail.SystemWeight;
                    invdetail.CreatedBy = icm.CreatedBy;
                    invdetail.CreateTimestamp = icm.CreateTimestamp;

                    if (icm.PSAID != null && !icm.PSAID.ToString().Equals(Guid.Empty.ToString()))
                    {
                        invdetail.ApprovalDate = DateTime.Now;
                        invdetail.ApprovedByID = UserBLL.CurrentUser.UserId;
                        invdetail.Status = (int)InventoryDetailStatus.Approved;
                    }
                    icm.addInventoryDetail(invdetail);
                    break;   // b coz PSA occers in only one stack.
                }
            }
            if (!stackSelected)
            {
                Messages1.SetMessage("Please Identifay the stack that PSA Occurred! ", Messages.MessageType.Error);
                return null;
            }
            if (icm.inventoryDetailList == null || icm.inventoryDetailList.Count <= 0)
            {
                Messages1.SetMessage("Physical count and weight required for at least one Stack ", Messages.MessageType.Error);
                return null;
            }

            InventoryInspector invins;
            foreach (GridViewRow gr1 in gvInventoryInspectors.Rows)
            {
                invins = new InventoryInspector();
                invins.InventoryID = icm.ID;
                invins.InspectorID = new Guid(((Label)gr1.FindControl("lblInspectorID")).Text);
                invins.InspectorName = ((Label)gr1.FindControl("lblInspectorName")).Text;
                if (((TextBox)gr1.FindControl("txtPosition")).Text.Trim() != string.Empty)
                {
                    invins.Position = ((TextBox)gr1.FindControl("txtPosition")).Text;
                }
                else
                {
                    Messages1.SetMessage("Please enter position for the Inspector " + invins.InspectorName +
                          "!", Messages.MessageType.Error);
                    return null;
                }
                icm.addInventoryInspector(invins);
            }

            return icm;
        }

        private void Clear()
        {
            drpLIC.ClearSelection();
            drpShed.ClearSelection();
            drpInventoryReason.ClearSelection();
            drpInventoryInspector.ClearSelection();
            gvInventoryDetail.DataSource = new DataTable();
            gvInventoryDetail.DataBind();
            gvInventoryInspectors.DataSource = new DataTable();
            gvInventoryInspectors.DataBind();
            ViewState.Add("InventoryInspectors", null);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            UpdatePanel1.Update();
            UpdatePanel2.Update();
            if (ViewState["psaApprovalInfoXML"] != null)
                Response.Redirect("ManagerPSAApprove.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            if (ControlsValidated())
            {
                InventoryControlModel icm = SetValues();
                if (icm != null)
                {
                    if (icm.PSAID != null && !icm.PSAID.ToString().Equals(Guid.Empty.ToString()))
                    {
                        icm.SaveForPSA_Rev();
                        Response.Redirect("ManagerPSAApprove.aspx?approved=true");
                    }
                    else
                    {
                        icm.Save();
                        Messages1.SetMessage("Inventory Adjustment was succesfully saved!", Messages.MessageType.Success);
                        Clear();
                        UpdatePanel1.Update();
                        UpdatePanel2.Update();
                    }
                }
            }
            upMessage.Update();
        }

        protected void btnAddInventroyinspector_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            if (drpInventoryInspector.SelectedIndex <= 0)
            {
                Messages1.SetMessage("Inventory Adjustment was succesfully saved!", Messages.MessageType.Success);
                upMessage.Update();
                return;
            }

            InventoryInspector invIns = new InventoryInspector();
            invIns.InspectorID = new Guid(drpInventoryInspector.SelectedValue);
            Guid insID = invIns.InspectorID;
            invIns.InspectorName = drpInventoryInspector.SelectedItem.Text;
            List<InventoryInspector> invInsectors = inventoryInspectors;
            if (invInsectors.Exists(iins => iins.InspectorID.Equals(insID)))
            {
                Messages1.SetMessage("The inspector is already added to the list!", Messages.MessageType.Warning);
                upMessage.Update();
                return;
            }
            invInsectors.Add(invIns);
            ViewState.Add("InventoryInspectors", invInsectors);
            gvInventoryInspectors.DataSource = inventoryInspectors;
            gvInventoryInspectors.DataBind();
            drpInventoryInspector.ClearSelection();
            upMessage.Update();
        }

        protected void drpShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpLIC.Items.Clear();
            gvInventoryDetail.DataSource = new DataTable();
            gvInventoryDetail.DataBind();
            if (drpShed.SelectedIndex > 0)
            {
                FillLIC(new Guid(drpShed.SelectedValue), "", CGID, PY);
            }
        }

        protected void drpLIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvInventoryDetail.DataSource = new DataTable();
            gvInventoryDetail.DataBind();
            if (drpLIC.SelectedIndex > 0)
            {
                FillInventoryDetail(new Guid(drpShed.SelectedValue), new Guid(drpLIC.SelectedValue), new Guid(CGID), PY);
            }
        }

        protected void lbtnRemoveInsp_Click(object sender, EventArgs e)
        {
            List<InventoryInspector> invInsectors = inventoryInspectors;
            System.Web.UI.HtmlControls.HtmlAnchor lb = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            invInsectors.RemoveAll(i => i.InspectorID.ToString().Equals(lb.Attributes["inspectorid"]));
            ViewState.Add("InventoryInspectors", invInsectors);
            gvInventoryInspectors.DataSource = inventoryInspectors;
            gvInventoryInspectors.DataBind();
            Messages1.ClearMessage();
            upMessage.Update();
        }

        protected void rdoPSAstack_OnCheckedChanged(object sender, EventArgs e)
        {
            bool flag=false;
            foreach (GridViewRow gr in gvInventoryDetail.Rows)
                if (((CheckBox)gr.FindControl("rdoPSAstack")).Checked)
                { flag = true; break; }

                foreach (GridViewRow gr in gvInventoryDetail.Rows)
                {
                    if (!((CheckBox)gr.FindControl("rdoPSAstack")).Checked)
                        gr.Visible = !flag;
                    else
                        gr.Visible = true;
                }
        }
    }
}