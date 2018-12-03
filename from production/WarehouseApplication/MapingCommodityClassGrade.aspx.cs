using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SamplingBussiness;
using WarehouseApplication.BLL;
using System.Data;
using CommoditySymbolBussiness;
using System.Collections;
using GradingBussiness;
using System.Configuration;

namespace WarehouseApplication
{
    public partial class MapingCommodityClassGrade : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState.Add("COFFEEID", ConfigurationManager.AppSettings["CoffeeId"].ToUpper().Trim());

                FillCommodity();
                BindCombo(cboRegion, LookupTypeEnum.Regions, "Please Select Region...");
                BindCombo(drpSrhRegion, LookupTypeEnum.Regions, "Please Select Region...");
                gvCommodityClassGrade.DataSource = new DataTable();
                gvCommodityClassGrade.DataBind();
                gvCommodityGrade.DataSource = new DataTable();
                gvCommodityGrade.DataBind();
                gvClassWereda.DataSource = new DataTable();
                gvClassWereda.DataBind();
                ViewState["PageIndex"] = 0;
            }
            if (rbCommodityClassGrade.SelectedValue == "1")
            {
                gvCommodityGrade.Style.Add("display", "none");
                gvCommodityClassGrade.Style.Add("display", "inline-block");
                pnSrhLocation.Style.Add("display", "inline-block");
                panelForClass.Style.Add("display", "inline-block");
                panelForGrade.Style.Add("display", "none");
                lblClassSymbol.Style.Add("display", "none");
                txtClassSymbol.Style.Add("display", "none");
                lblGrade.Style.Add("display", "none");
                txtGrade.Style.Add("display", "none");
            }
            else
            {
                gvCommodityClassGrade.Style.Add("display", "none");
                gvCommodityGrade.Style.Add("display", "inline-block");
                panelForClass.Style.Add("display", "none");
                pnSrhLocation.Style.Add("display", "none");
                panelForGrade.Style.Add("display", "inline-block");
                lblClassSymbol.Style.Add("display", "inline-block");
                txtClassSymbol.Style.Add("display", "inline-block");
                lblGrade.Style.Add("display", "inline-block");
                txtGrade.Style.Add("display", "inline-block");
            }

        }

        void FillCommodity()
        {
            cboCommodity.DataSource = GradingBussiness.GradingModel.GetCommodity();
            cboCommodity.DataTextField = "Description";
            cboCommodity.DataValueField = "ID";
            cboCommodity.DataBind();
        }

        void FillWoreda(Guid CommodityID)
        {
            //Predicate<LookupValue> p = new Predicate<LookupValue>(l => l.CommodityID == CommodityID);
            //BindCombo(cboWoreda, LookupTypeEnum.Woredas, p, "Select Woreda");
            cboWoreda.DataSource = ModelCommoditySymbol.GetWoreda(CommodityID);
            cboWoreda.DataTextField = "Description";
            cboWoreda.DataValueField = "ID";
            cboWoreda.DataBind();
        }

        void FillCommodityClass(Guid CommodityID)
        {
            cboCommodityClass.Items.Clear();
            cboCommodityClass.Items.Add(new ListItem("-Select Grading Class-", ""));
            cboCommodityClass.DataSource = ModelCommoditySymbol.GetCommodityClassByCommodity(CommodityID);
            cboCommodityClass.DataTextField = "Class";
            cboCommodityClass.DataValueField = "ClassID";
            cboCommodityClass.DataBind();
        }

        void FillCommodityType()
        {
            drpCommodityType.Items.Clear();
            drpCommodityType.Items.Add(new ListItem("-Select Commodity Type-", ""));
            DataTable dt = ModelCommoditySymbol.GetAllCommodityTypeForClassMap();
            drpCommodityType.DataSource = dt;
            drpCommodityType.DataTextField = "ProcessingGroup";
            drpCommodityType.DataValueField = "ProcessingGroup";
            drpCommodityType.DataBind();
        }

        void FillCommodityClassifications(Guid CommodityID)
        {
            //chkList.Items.Clear();
            //chkList.Items.Add(new ListItem("Is Under Screen", "5"));
            //chkList.Items.Add(new ListItem("Is Semi-Washed", "3"));
            DataTable dt = ModelCommoditySymbol.GetCommodityClassification(CommodityID);
            if (dt != null && dt.Rows.Count > 0 && bool.Parse(dt.Rows[0]["Optional"].ToString()))
            {
                rbList.Items.Clear();
                chkList.DataSource = dt;
                chkList.DataTextField = "Name";
                chkList.DataValueField = "Value";
                chkList.DataBind();
            }
            else if (dt != null && dt.Rows.Count > 0)
            {
                chkList.Items.Clear();
                rbList.DataSource = dt;
                rbList.DataTextField = "Name";
                rbList.DataValueField = "Value";
                rbList.DataBind();
            }
        }

        void FillGradingFactorGroup(Guid CommodityID)
        {
            cboFactorGroup.Items.Clear();
            cboFactorGroup.Items.Add(new ListItem("-Select Factor Group-", ""));
            cboFactorGroup.DataSource = ModelCommoditySymbol.GetFactorGroupForCommodity(CommodityID);
            cboFactorGroup.DataTextField = "Name";
            cboFactorGroup.DataValueField = "ID";
            cboFactorGroup.DataBind();
        }

        void FillCommoditySymbolGrid(GridView gv, Guid CommodityID)
        {
            bool forGrade = false;
            Guid? WoredaID = null;
            if (rbCommodityClassGrade.SelectedValue != "1")
                forGrade = true;
            else
            {
                if(drpSrhWoreda.SelectedIndex > 0)
                    WoredaID = new Guid(drpSrhWoreda.SelectedValue);
            }

            gv.DataSource = ModelCommoditySymbol.GetCommoditySymbol(CommodityID, txtSymbol.Text.Trim(),
                /*(cboCommodityClassForSrh.SelectedIndex < 0 || !forGrade ? "" : cboCommodityClassForSrh.SelectedItem.Text)*/
                                                   txtClassSymbol.Text.Trim(), txtGrade.Text.Trim(), forGrade, chkShowInActive.Checked, WoredaID);
            if (ViewState["PageIndex"] != null)
                gv.PageIndex = int.Parse(ViewState["PageIndex"].ToString());
            gv.DataBind();
            gv.SelectedIndex = -1;
        }

        void FillWoredaGrid(Guid CommodityClassID)
        {
            DataTable dt = ModelCommoditySymbol.GetWoredaForClass(CommodityClassID);

            List<woreda> classWoreda = new List<woreda>();
            foreach (DataRow dr in dt.Rows)
            {
                woreda w = new woreda();
                w.WoredaID = dr["WoredaID"].ToString();
                w.WoredaName = dr["WoredaName"].ToString();
                classWoreda.Add(w);
            }
            ViewState.Add("ClassWoreda", classWoreda);

            gvClassWereda.DataSource = classWoreda;
            gvClassWereda.DataBind();
        }

        void Save()
        {
            Messages.ClearMessage();
            if (gvCommodityClassGrade.SelectedIndex < 0 && gvCommodityGrade.SelectedIndex < 0)
            {
                Messages.SetMessage("Please select one item from the Grid!", WarehouseApplication.Messages.MessageType.Error);
                UpdatePanel1.Update();
                return;
            }

            ModelCommoditySymbol mcs = new ModelCommoditySymbol();
            SetValues(mcs);
            mcs.Save();
            lbtnReload_Click(null, null);
            Messages.SetMessage("Record saved successfully!", WarehouseApplication.Messages.MessageType.Success);
        }

        void Clear()
        {
            chkInActiveClass.Checked = chkInActiveGrade.Checked = false;
            cboCommodityClass.Items.Clear();
            cboFactorGroup.Items.Clear();
            if (cboCommodityClass.Items.Count > 0)
                cboCommodityClass.SelectedIndex = 0;
            if (cboFactorGroup.Items.Count > 0)
                cboFactorGroup.SelectedIndex = 0;
            txtMaxTotalValue.Text = txtMinTotalValue.Text = txtGradeValue.Text = string.Empty;
            //chkIsSemiWashed.Checked = chkIsUnderScreen.Checked = false;
            if (chkList.Items.Count > 0)
                foreach (ListItem li in chkList.Items)
                {
                    li.Selected = false;
                }
            else if (rbList.Items.Count > 0)
                rbList.SelectedIndex = -1;
            gvCommodityClassGrade.DataSource = new DataTable();
            gvCommodityClassGrade.DataBind();
            gvCommodityGrade.DataSource = new DataTable();
            gvCommodityGrade.DataBind();
            gvClassWereda.DataSource = new DataTable();
            gvClassWereda.DataBind();
        }

        void SetValues(ModelCommoditySymbol mcs)
        {
            if (rbCommodityClassGrade.SelectedValue == "1")
            {
                GridViewRow gvr = gvCommodityClassGrade.SelectedRow;
                Label lbl = null;
                lbl = (Label)gvr.FindControl("lblID");
                mcs.ID = new Guid(lbl.Text);
                mcs.GradingFactorGroupID = new Guid(cboFactorGroup.SelectedValue);
                mcs.Grade = "0";
                mcs.InActive = chkInActiveClass.Checked;
                if (drpCommodityType.SelectedIndex >= 0)
                    mcs.CommodityGroupI = drpCommodityType.SelectedValue;
                if (ViewState["ClassWoreda"] != null)
                {
                    List<woreda> classWoreda = (List<woreda>)ViewState["ClassWoreda"];
                    foreach (woreda w in classWoreda)
                    {
                        mcs.addWoreda(w);
                    }
                }
            }
            else
            {
                GridViewRow gvr = gvCommodityGrade.SelectedRow;
                Label lbl = null;
                lbl = (Label)gvr.FindControl("lblID");
                mcs.ID = new Guid(lbl.Text);
                mcs.GradingFactorGroupID = null;
                mcs.ParentID = new Guid(cboCommodityClass.Text);
                mcs.InActive = chkInActiveGrade.Checked;
                int tempNo = 0;
                int.TryParse(txtMaxTotalValue.Text, out tempNo);
                mcs.MaximumTotalValue = tempNo;
                int.TryParse(txtMinTotalValue.Text, out tempNo);
                mcs.MinimumTotalValue = tempNo;
                mcs.Grade = txtGradeValue.Text.Trim();
                tempNo = 1;
                int classification = 1;
                lbl = (Label)gvr.FindControl("lblClassificationNo");
                int.TryParse(lbl.Text, out classification);
                if (classification == 0) classification = 1;
                if (chkList.Items.Count > 0)
                    foreach (ListItem li in chkList.Items)
                    {
                        if (li.Selected)
                        {
                            if (classification % int.Parse(li.Value) != 0) classification *= int.Parse(li.Value);
                        }
                        else
                            if (classification % int.Parse(li.Value) == 0) classification /= int.Parse(li.Value);
                    }
                else if (rbList.Items.Count > 0)
                    classification = int.Parse(rbList.SelectedValue);
                if (classification > 1)
                    mcs.Classification = classification;
                else
                    mcs.Classification = null;
                mcs.woredaIdList = null;
            }
        }

        protected void lbtnReload_Click(object sender, EventArgs e)
        {
            Clear();
            Messages.ClearMessage();
            if (cboCommodity.SelectedIndex < 0)
            {
                Messages.SetMessage("Commodity selected is invalid!", WarehouseApplication.Messages.MessageType.Error);
                UpdatePanel1.Update();
                return;
            }
            Guid commodityID = new Guid(cboCommodity.SelectedValue);
            if (sender != null)
                ViewState["PageIndex"] = 0;
            if (rbCommodityClassGrade.SelectedValue == "1")
            {
                FillCommoditySymbolGrid(gvCommodityClassGrade, commodityID);
                FillGradingFactorGroup(commodityID);
                if (ViewState["COFFEEID"].ToString().ToUpper().Equals(commodityID.ToString().ToUpper()))
                {
                    FillCommodityType();
                    rfdrpCommodityType.Enabled = true;
                    drpCommodityType.Enabled = true;
                }
                else
                {
                    drpCommodityType.Items.Clear();
                    rfdrpCommodityType.Enabled = false;
                    drpCommodityType.Enabled = false;
                }
            }
            else
            {
                FillCommoditySymbolGrid(gvCommodityGrade, commodityID);
                FillCommodityClass(commodityID);
                FillCommodityClassifications(commodityID);
            }
            upClass.Update();
            upGrade.Update();
            UpdatePanel1.Update();
        }

        protected void gvCommodityClassGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Messages.ClearMessage();

            Label lbl = null;
            Label lblCommodityType = null;
            if (rbCommodityClassGrade.SelectedValue == "1")
            {
                GridViewRow gvr = gvCommodityClassGrade.SelectedRow;
                lbl = (Label)gvr.FindControl("lblGradingFactorGroupID");
                lblCommodityType = (Label)gvr.FindControl("lblCommodityType");
                if (lblCommodityType.Text.Trim() != string.Empty)
                {
                    drpCommodityType.SelectedValue = lblCommodityType.Text.Trim();
                }
                chkInActiveClass.Checked = chkShowInActive.Checked;
                if (lbl.Text.Trim() != string.Empty)
                    try
                    {
                        cboFactorGroup.SelectedValue = lbl.Text;
                    }
                    catch { Messages.SetMessage("The Factor group of the class don't exits in the list.Please select other!", WarehouseApplication.Messages.MessageType.Error); }
                lbl = (Label)gvr.FindControl("lblID");
                FillWoredaGrid(new Guid(lbl.Text));
                upClass.Update();
            }
            else
            {
                GridViewRow gvr = gvCommodityGrade.SelectedRow;
                lbl = (Label)gvr.FindControl("lblParentID");
                chkInActiveGrade.Checked = chkShowInActive.Checked;
                if (lbl.Text.Trim() != string.Empty)
                    try
                    {
                        cboCommodityClass.SelectedValue = lbl.Text;
                    }
                    catch { Messages.SetMessage("The class of the grade don't exits in the class list.Please select other", WarehouseApplication.Messages.MessageType.Error); }
                else if (cboCommodityClass.Items.Count > 0)
                    cboCommodityClass.SelectedIndex = 0;
                lbl = (Label)gvr.FindControl("lblMinimumTotalValue");
                txtMinTotalValue.Text = lbl.Text;
                lbl = (Label)gvr.FindControl("lblMaximumTotalValue");
                txtMaxTotalValue.Text = lbl.Text;
                lbl = (Label)gvr.FindControl("lblGrade");
                txtGradeValue.Text = lbl.Text;
                lbl = (Label)gvr.FindControl("lblClassificationNo");
                if (lbl.Text.Trim() != string.Empty)
                {
                    int no = 0;
                    int.TryParse(lbl.Text, out no);
                    foreach (ListItem li in chkList.Items)
                    {
                        li.Selected = no > 0 && no % int.Parse(li.Value) == 0 ? true : false;
                    }
                    rbList.SelectedValue = no.ToString();
                }
                else
                {
                    //foreach (ListItem li in chkList.Items)
                    //{
                    //    li.Selected = false;
                    //}
                    rbList.SelectedIndex = -1; chkList.SelectedIndex = -1;
                }
                upGrade.Update();
            }
            UpdatePanel1.Update();
        }

        protected void btnMapFactorGroup_Click(object sender, EventArgs e)
        {
            Save();
            UpdatePanel3.Update();
        }

        protected void btnMapCommodityGrade_Click(object sender, EventArgs e)
        {
            Save();
            UpdatePanel3.Update();
        }

        protected void btnAddWoreda_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            if (ViewState["ClassWoreda"] == null)
            {
                Messages.SetMessage("Please select one item from the Grid!", WarehouseApplication.Messages.MessageType.Error);
                UpdatePanel1.Update();
                return;
            }

            List<woreda> classWoreda = (List<woreda>)ViewState["ClassWoreda"];
            if (!classWoreda.Exists(w => w.WoredaID == cboWoreda.SelectedValue))
            {
                woreda w = new woreda();
                w.WoredaID = cboWoreda.SelectedValue;
                w.WoredaName = cboWoreda.SelectedItem.Text;
                classWoreda.Insert(0, w);
                ViewState["ClassWoreda"] = classWoreda;

                gvClassWereda.DataSource = classWoreda;
                gvClassWereda.DataBind();
            }
            else
            {
                Messages.SetMessage("Woreda entered already exists!", WarehouseApplication.Messages.MessageType.Error);
                UpdatePanel1.Update();
            }
        }

        public static int CompareByString(woreda x, woreda y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return x.WoredaName.CompareTo(y.WoredaName);
            }
        }

        protected void lbtnRemoveWoreda_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor lb = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            List<woreda> classWoreda = (List<woreda>)ViewState["ClassWoreda"];
            classWoreda.RemoveAll(w => w.WoredaID == lb.Attributes["woredaid"]);
            ViewState["ClassWoreda"] = classWoreda;

            gvClassWereda.DataSource = classWoreda;
            gvClassWereda.DataBind();
        }

        protected void cboCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rbCommodityClassGrade.SelectedValue != "1")
            //{
            //    cboCommodityClassForSrh.DataSource = ModelCommoditySymbol.GetCommodityClassByCommodity(new Guid(cboCommodity.SelectedValue));
            //    cboCommodityClassForSrh.DataTextField = "Class";
            //    cboCommodityClassForSrh.DataValueField = "ClassID";
            //    cboCommodityClassForSrh.DataBind();
            //}
        }

        protected void gvCommodityClassGrade_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (rbCommodityClassGrade.SelectedValue == "1")
            {
                gvCommodityClassGrade.PageIndex = e.NewPageIndex;
                ViewState["PageIndex"] = e.NewPageIndex;
                FillCommoditySymbolGrid(gvCommodityClassGrade, new Guid(cboCommodity.SelectedValue));
            }
            else
            {
                gvCommodityGrade.PageIndex = e.NewPageIndex;
                ViewState["PageIndex"] = e.NewPageIndex;
                FillCommoditySymbolGrid(gvCommodityGrade, new Guid(cboCommodity.SelectedValue));
            }
        }

        protected void cboZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZone.SelectedIndex == 0)
                return;
            string zone = cboZone.SelectedValue;
            Guid reID = new Guid(zone);
            cboWoreda.Items.Clear();
            Predicate<LookupValue> p = new Predicate<LookupValue>(l => l.ZoneID == reID);
            BindCombo(cboWoreda, LookupTypeEnum.Woredas, p, "Select Woreda");
        }

        protected void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRegion.SelectedIndex == 0)
                return;
            string regionID = cboRegion.SelectedValue;
            Guid reID = new Guid(regionID);
            cboZone.Items.Clear();
            cboWoreda.Items.Clear();
            Predicate<LookupValue> p = new Predicate<LookupValue>(l => l.RegionID == reID);
            BindCombo(cboZone, LookupTypeEnum.Zones, p, "Select Zone");
        }

        protected void gvClassWereda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Label lbl = (Label)gvCommodityClassGrade.SelectedRow.FindControl("lblID");
            gvClassWereda.PageIndex = e.NewPageIndex;
            List<woreda> classWoreda = (List<woreda>)ViewState["ClassWoreda"];
            gvClassWereda.DataSource = classWoreda;
            gvClassWereda.DataBind();
            upClass.Update();
        }

        protected void drpSrhRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSrhRegion.SelectedIndex == 0)
                return;
            string regionID = drpSrhRegion.SelectedValue;
            Guid reID = new Guid(regionID);
            drpSrhZone.Items.Clear();
            drpSrhWoreda.Items.Clear();
            Predicate<LookupValue> p = new Predicate<LookupValue>(l => l.RegionID == reID);
            BindCombo(drpSrhZone, LookupTypeEnum.Zones, p, "Select Zone");
        }

        protected void drpSrhZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSrhZone.SelectedIndex == 0)
                return;
            string zone = drpSrhZone.SelectedValue;
            Guid reID = new Guid(zone);
            drpSrhWoreda.Items.Clear();
            Predicate<LookupValue> p = new Predicate<LookupValue>(l => l.ZoneID == reID);
            BindCombo(drpSrhWoreda, LookupTypeEnum.Woredas, p, "Select Woreda");
        }
    }
}