using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Configuration;
namespace WarehouseApplication
{
    public partial class StackManagement : System.Web.UI.Page
    {
        StackModel stack = new StackModel();
        DataTable dtbl
        {
            get
            {
                if (ViewState["dtbl"] != null)
                    return (DataTable)(ViewState["dtbl"]);
                else
                    return null;
            }
        }
        bool isClosed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            BindDropdownlists();

            RangeValidatorDateStarted.MinimumValue = DateTime.Now.AddYears(-7).ToShortDateString();
            RangeValidatorDateStarted.MaximumValue = DateTime.Now.ToShortDateString();
        }

        public void BindDropdownlists()
        {
            BindShed();
            BindLIC();
            BindCommodity();
            BindProductionYear();
        }

        public void BindProductionYear()
        {
            int currentYear = int.Parse(ConfigurationManager.AppSettings["CurrentEthiopianYear"]);
            int startYear = currentYear - 7;
            ddlProductionYear.Items.Clear();
            ListItem item;
            item = new ListItem("Select Production Year", "");
            ddlProductionYear.Items.Add(item);
            while (startYear <= currentYear)
            {
                item = new ListItem(startYear.ToString(), startYear.ToString());
                ddlProductionYear.Items.Add(item);
                startYear++;
            }
            ddlProductionYear.DataBind();
        }

        public void BindShed()
        {
            DataTable sheds = StackModel.GetWarehouseSheds(new Guid(Session["CurrentWarehouse"].ToString()));
            ddlShedSearch.DataSource = sheds;
            ddlShedSearch.DataTextField = "ShedNumber";
            ddlShedSearch.DataValueField = "ID";
            ddlShedSearch.DataBind();
        }

        public void BindShedByLIC()
        {
            DataTable sheds = StackModel.GetShedByLICForNewStack(new Guid(Session["CurrentWarehouse"].ToString()), new Guid(ddLIC.SelectedValue));
            ddlShedID.DataSource = sheds;
            ddlShedID.DataTextField = "ShedNumber";
            ddlShedID.DataValueField = "ShedID";
            ddlShedID.DataBind();
        }

        public void BindLIC()
        {
            ddLIC.DataSource = StackModel.GetWarehouseLICs(new Guid(Session["CurrentWarehouse"].ToString()));
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }

        public void BindCommodity()
        {
            ddlCommodity.DataSource = StackModel.GetCommodyList();
            ddlCommodity.DataTextField = "Description";
            ddlCommodity.DataValueField = "ID";
            ddlCommodity.DataBind();

            Messages1.ClearMessage();
        }

        public void BindBagTypes()
        {
            ddlBagType.Items.Clear();
            ddlBagType.Items.Add(new ListItem("Select Bag Types", ""));

            if (ddlCommodity.SelectedValue != string.Empty)
            {
                ddlBagType.DataSource = StackModel.GetBagTypeList(new Guid(ddlCommodity.SelectedValue));
                ddlBagType.DataTextField = "BagType";
                ddlBagType.DataValueField = "BagGuid";
                ddlBagType.DataBind();
            }
        }

        public void BindCommodityClass()
        {
            ddlCommodityClass.Items.Clear();
            ddlCommodityClass.Items.Add(new ListItem("Select Commodity Class", ""));

            if (ddlCommodity.SelectedValue != string.Empty)
            {
                ddlCommodityClass.DataSource = StackModel.GetCommodyClassList(new Guid(Session["CurrentWarehouse"].ToString()), new Guid(ddlCommodity.SelectedValue));
                ddlCommodityClass.DataTextField = "Description";
                ddlCommodityClass.DataValueField = "Guid";
                ddlCommodityClass.DataBind();
            }

            ddlCommodityGrade.Items.Clear();
            ddlCommodityGrade.Items.Add(new ListItem("Select Commodity Symbol", ""));
        }

        public void BindCommodityGrade()
        {
            ddlCommodityGrade.Items.Clear();
            ddlCommodityGrade.Items.Add(new ListItem("Select Commodity Symbol", ""));

            if (ddlCommodityClass.SelectedValue != string.Empty)
            {
                ddlCommodityGrade.DataSource = StackModel.GetCommodySymbolList(new Guid(ddlCommodityClass.SelectedValue));
                ddlCommodityGrade.DataTextField = "Symbol";
                ddlCommodityGrade.DataValueField = "ID";
                ddlCommodityGrade.DataBind();
            }
        }

        public void BindPhysicalAddress()
        {
            ddlPhysicalAddress.Items.Clear();
            ddlPhysicalAddress.Items.Add(new ListItem("Select Physical Address", ""));

            if (ddlShedID.SelectedValue != string.Empty)
            {
                ddlPhysicalAddress.DataSource = StackModel.GetPhysicalAddresses(new Guid(ddlShedID.SelectedValue));
                ddlPhysicalAddress.DataTextField = "AddressName";
                ddlPhysicalAddress.DataValueField = "ID";
                ddlPhysicalAddress.DataBind();
            }
        }

        public void BindStacksGrisview()
        {
            if ((ddlShedSearch.SelectedValue != string.Empty) && (ddlPhyAddressSearch.SelectedValue != string.Empty))
            {
                DataTable dt = StackModel.GetStacks(new Guid(Session["CurrentWarehouse"].ToString()),
                   new Guid(ddlShedSearch.SelectedValue), new Guid(ddlPhyAddressSearch.SelectedValue));

                ViewState.Add("dtbl", dt);
                grvStacks.DataSource = dtbl;
                grvStacks.DataBind();
            }
        }

        protected void ddlShedID_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPhysicalAddress();
            txtStackNo.Text = "";
        }

        protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCommodityClass();
            BindBagTypes();
        }

        protected void ddlCommodityClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCommodityGrade();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (InitializeStack())
            {
                try
                {
                    stack.InsertStacks();
                    Messages1.SetMessage("Record added successfully.", WarehouseApplication.Messages.MessageType.Success);
                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                }
            }
        }
        bool IsValidStack()
        {
            int balance;
            float weight;
            DateTime startDate;
            if (txtBeginingBalance.Text == "" || txtBeginingWeight.Text == "" || txtDateStarted.Text == "" ||
                ddLIC.SelectedValue == "" || ddlPhysicalAddress.SelectedValue == "" || ddlProductionYear.SelectedValue == "" || ddlShedID.SelectedValue == "" || ddlBagType.SelectedValue == "")
            {
                Messages1.SetMessage("Please enter all values.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(int.TryParse(txtBeginingBalance.Text, out  balance)))
            {
                Messages1.SetMessage("Please enter valid balance.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }

            else if (!(float.TryParse(txtBeginingWeight.Text, out weight)))
            {
                Messages1.SetMessage("Please enter valid weight.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(DateTime.TryParse(txtDateStarted.Text, out  startDate)))
            {
                Messages1.SetMessage("Please enter valid Date.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InitializeStack()
        {
            if (IsValidStack())
            {
                stack.BeginingBalance = int.Parse(txtBeginingBalance.Text);
                stack.BeginingWeight = float.Parse(txtBeginingWeight.Text);
                stack.CurrentBalance = int.Parse(txtBeginingBalance.Text);
                stack.CurrentWeight = float.Parse(txtBeginingWeight.Text);
                stack.CommodityGradeID = new Guid(ddlCommodityGrade.SelectedValue);
                stack.CreatedBy = UserBLL.CurrentUser.UserId;
                stack.CreatedTimestamp = DateTime.Now;
                stack.DateStarted = DateTime.Parse(txtDateStarted.Text);
                stack.LICID = new Guid(ddLIC.SelectedValue);
                stack.PhysicalAddressID = new Guid(ddlPhysicalAddress.SelectedValue);
                stack.ProductionYear = int.Parse(ddlProductionYear.SelectedValue);
                stack.ShedID = new Guid(ddlShedID.SelectedValue);
                stack.WarehouseID = new Guid(Session["CurrentWarehouse"].ToString());
                stack.BagTypeID = new Guid(ddlBagType.SelectedValue);
                stack.Status = 1;
                txtStackNo.Text = GetStackNo();
                stack.StackNumber = txtStackNo.Text;

                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetStackNo()
        {
            string stackNo = ddlPhysicalAddress.SelectedItem.ToString().Trim() + "-" + ddlCommodityGrade.SelectedItem + "-" +
                ddlProductionYear.SelectedValue.Substring(2) + "-" + txtDateStarted.Text;
            return stackNo;
        }

        protected void ddlShedSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPhyAddressSearch.Items.Clear();
            ddlPhyAddressSearch.Items.Add(new ListItem("Select Physical Address", ""));

            if (ddlShedSearch.SelectedValue != string.Empty)
            {
                ddlPhyAddressSearch.DataSource = StackModel.GetPhysicalAddressInStack(new Guid(ddlShedSearch.SelectedValue));
                ddlPhyAddressSearch.DataTextField = "AddressName";
                ddlPhyAddressSearch.DataValueField = "ID";
                ddlPhyAddressSearch.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            BindStacksGrisview();
        }

        protected void grvStacks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // disable cancel for stacks whose in use
                if (((Label)e.Row.FindControl("lblBeginingBalance")).Text != ((Label)e.Row.FindControl("lblCurrentBalance")).Text)
                {
                    e.Row.Cells[10].Enabled = false;
                }
            }
        }

        protected void grvStacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isClosed)
                {
                    StackModel.CloseStack(new Guid(grvStacks.SelectedDataKey[0].ToString()), 3, UserBLL.CurrentUser.UserId, DateTime.Now);
                    Messages1.SetMessage("Record closed successfully.", WarehouseApplication.Messages.MessageType.Success);
                }
                else
                {
                    StackModel.UpdateStacks(new Guid(grvStacks.SelectedDataKey[0].ToString()), 2, UserBLL.CurrentUser.UserId, DateTime.Now);
                    Messages1.SetMessage("Record cancelled successfully.", WarehouseApplication.Messages.MessageType.Success);
                }

                BindStacksGrisview();

            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            }
        }

        protected void grvStacks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStacks.PageIndex = e.NewPageIndex;
            grvStacks.DataSource = dtbl;
            grvStacks.DataBind();
        }

        protected void ddLIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlShedID.Items.Clear();
            ListItem item = new ListItem("Select Shed", "");
            ddlShedID.Items.Add(item);

            if (ddLIC.SelectedValue != string.Empty)
            {
                BindShedByLIC();
            }
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            isClosed = true;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            isClosed = false;
        }
    }
}