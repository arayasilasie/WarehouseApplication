using System;
using System.Configuration;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Collections.Generic;
using System.Data;
using AjaxControlToolkit;
using System.Web.Services;
using System.Web.UI;
using System.Text.RegularExpressions;
namespace WarehouseApplication
{
    public partial class AddArrivalNew : BasePage
    {
        public Guid RepresentativeId { get; set; }

        static string myGuid = null;

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            this.btnSave.UseSubmitBehavior = false;
            btnSave.OnClientClick = "javascript:";

            if (btnSave.CausesValidation)
            {
                btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
                btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }
        }

        public ArrivalModel theArrival
        {
            get
            {
                return Session[SessionKey.Arrival] as ArrivalModel;
            }
            set
            {
                Session.Add(SessionKey.Arrival, value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Messages.ClearMessage();
            Page.DataBind();
            btnNext.Enabled = false;
            btnNext.BackColor = System.Drawing.Color.LightGray;
            chkIsSourceDetermined.Checked = true;
            if (SessionKeyExists(SessionKey.Arrival))
            {
                //Response.Redirect("ErrorPage.aspx");
                //return;
            }
            this.btnSave.UseSubmitBehavior = false;
            btnSave.OnClientClick = "javascript:";

            if (btnSave.CausesValidation)
            {
                btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
                btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }

            if (theArrival == null)
            {
                theArrival = new ArrivalModel();
                Response.Redirect("ErrorPage.aspx");
                return;
            }
            theArrival.RefreshAll();
            FillControls();
            PopulateExistingData();
            GetCommodityTypes();
            if (cboCommodity.SelectedIndex == 0)
            {
                cboCommodityType.Enabled = false;
                RFVCoffeeType.Enabled = false;
            }
            else
            {
                if (cboCommodityType.Items.Count == 1)
                {
                    cboCommodityType.Enabled = false;
                    RFVCoffeeType.Enabled = false;
                }
                else
                {
                    cboCommodityType.Enabled = true;
                    RFVCoffeeType.Enabled = true;
                }
            }
        }

        protected void isNonTruck_CheckedChanged(object sender, EventArgs e)
        {
            if (isNonTruck.Checked == true)
            {
                txtPlateNo.Enabled = false;
                txtTrailerPlateNo.Enabled = false;
                txtNoPlomps.Enabled = false;
                txtTrailerNoPlomps.Enabled = false;
                txtDriverName.Enabled = false;

                txtLicenseNo.Enabled = false;
                txtPlaceIssued.Enabled = false;
                chkIsTruckInCompound.Checked = false;

                txtPlateNo.Text = "";
                txtTrailerPlateNo.Text = "";
                txtNoPlomps.Text = "";
                txtTrailerNoPlomps.Text = "";
                txtDriverName.Text = "";
                txtLicenseNo.Text = "";
                txtPlaceIssued.Text = "";
                chkIsTruckInCompound.Enabled = false;
            }
            else
            {
                txtPlateNo.Enabled = true;
                txtTrailerPlateNo.Enabled = true;
                txtDriverName.Enabled = true;

                txtLicenseNo.Enabled = true;
                txtPlaceIssued.Enabled = true;
                chkIsTruckInCompound.Checked = true;
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                //Response.Redirect("~/AddArrival.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string driverFullName = txtDriverName.Text.ToString().Trim();
                string[] tokenNames = driverFullName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (int.Parse(tokenNames.Length.ToString()) > 3 || int.Parse(tokenNames.Length.ToString()) < 3)
                {
                    throw new Exception("Invalid Driver Name!");
                }
            }
            catch (Exception ex)
            {
                Messages.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                return;
            }
            try
            {
                PageToObject();
                if (theArrival.IsValid())
                    theArrival.Save();
                Messages.SetMessage("Arrival record saved successfully.", WarehouseApplication.Messages.MessageType.Success);
                btnNext.Enabled = true;
                btnNext.BackColor = System.Drawing.ColorTranslator.FromHtml("#88AB2D");
            }
            catch (Exception ex)
            {
                Messages.SetMessage("Arrival record not saved.  Please try again.", WarehouseApplication.Messages.MessageType.Error);
            }
        }

        /// <summary>
        /// Collects values from page and assigns to properties of the object
        /// </summary>
        private void PageToObject()
        {
            try
            {
                theArrival.CommandType = Request.QueryString["CommandType"].ToString();
            }
            catch
            {
                theArrival.CommandType = string.Empty;
            }

            try
            {
                theArrival.ClientID = Guid.Parse(this.ClientSelector1.ClientGUID.Value.ToString());
                theArrival.ClientName = this.ClientSelector1.lblMessage.Text.Replace(']', ' ').Replace('[', ' ').Trim();
            }
            catch
            {
                theArrival.ClientID = theArrival.ClientID;
                theArrival.ClientName = theArrival.ClientName;
            }

            decimal Weight;
            int NumberOfBags;
            string Remark;

            try
            {
                theArrival.CommodityID = Guid.Parse(cboCommodity.SelectedValue);
            }
            catch
            {
                //this.lblMessage.ForeColor = System.Drawing.Color.Tomato;
                //this.lblMessage.Text = "Please Select Commodity";
                Messages.SetMessage("Please Select Commodity", WarehouseApplication.Messages.MessageType.Error);
                return;
            }
            //theArrival.ClientFullName = this.ClientSelector1.ClientName.ToString();
            //theArrival.ClientID = new Guid(this.ClientSelector1.ClientGUID.Value.ToString());
            theArrival.IsLocationKnown = chkIsSourceDetermined.Checked;
            if (theArrival.IsLocationKnown)
            {
                try
                {
                    theArrival.WoredaID = new Guid(this.cboWoreda.SelectedValue);
                }
                catch
                {
                    theArrival.WoredaID = Guid.Empty;
                }
                theArrival.SpecificArea = txtSpecificArea.Text;
                try
                {
                    theArrival.ProcessingCenter = txtProcessingCenter.Text;
                }
                catch
                {
                    theArrival.ProcessingCenter = string.Empty;
                }
            }
            else
            {
                theArrival.WoredaID = Guid.Empty;
                theArrival.SpecificArea = string.Empty;
                theArrival.ProcessingCenter = string.Empty;
            }
            theArrival.SpecificArea = this.txtSpecificArea.Text;
            theArrival.ProductionYear = int.Parse(cboProductionYear.SelectedValue);
            decimal.TryParse(this.txtWeight.Text, out Weight);
            int.TryParse(this.txtNumberOfBags.Text, out NumberOfBags);

            theArrival.NumberofBags = NumberOfBags;
            theArrival.VoucherWeight = Weight;
            Remark = this.txtRemark.Text;
            try
            {
                theArrival.DateTimeReceived = Convert.ToDateTime(this.txtArrivalDate.Text + " " + this.txtTimeArrival.Text);
            }
            catch
            {
                //this.lblMessage.ForeColor = System.Drawing.Color.Tomato;
                //this.lblMessage.Text = "Please enter the correct date time format.";
                Messages.SetMessage("Please enter valid date and time format.", WarehouseApplication.Messages.MessageType.Error);
            }
            theArrival.CreatedBy = UserBLL.GetCurrentUser();
            theArrival.IsNonTruck = isNonTruck.Checked;
            if (theArrival.IsNonTruck)
            {
                theArrival.IsTruckInCompound = false;
                theArrival.DriverName = string.Empty;
                theArrival.LicenseIssuedPlace = string.Empty;
                theArrival.LicenseNumber = string.Empty;
                theArrival.TruckPlateNumber = string.Empty;
                theArrival.TrailerPlateNumber = string.Empty;
            }
            else
            {
                theArrival.IsTruckInCompound = chkIsTruckInCompound.Checked;
                theArrival.DriverName = txtDriverName.Text;
                theArrival.LicenseNumber = txtLicenseNo.Text;
                theArrival.LicenseIssuedPlace = txtPlaceIssued.Text;
                theArrival.TruckPlateNumber = txtPlateNo.Text;
                theArrival.TrailerPlateNumber = txtTrailerPlateNo.Text;
            }
            //theArrival.IsBiProduct = chkIsBiProduct.Checked;
            //Voucher
            theArrival.VoucherNumber = txtVoucherNo.Text;
            //theArrival.VoucherCertificateNo = txtVoucherCertNo.Text;
            bool commodityTypeStatus = (cboCommodityType.SelectedIndex == 0 && cboCommodityType.Enabled == false && cboCommodityType.Items.Count == 1);
            theArrival.VoucherCommodityTypeID = commodityTypeStatus ? Guid.Empty : new Guid(cboCommodityType.SelectedValue);
            theArrival.VoucherNumberOfBags = int.Parse(txtNumberOfBags.Text);
            theArrival.VoucherNumberOfPlomps = string.IsNullOrEmpty(txtNoPlomps.Text) ? 0 : int.Parse(txtNoPlomps.Text);
            theArrival.VoucherNumberOfPlompsTrailer = string.IsNullOrEmpty(txtTrailerNoPlomps.Text) ? 0 : int.Parse(txtTrailerNoPlomps.Text);
            theArrival.Remark = this.txtRemark.Text;
            theArrival.UserID = UserBLL.GetCurrentUser();
        }

        /// <summary>
        /// Fills controls with all possible alternatives, like dropdown-commodity
        /// would be filled by all possible commodites 
        /// </summary>
        private void FillControls()
        {
            BindCombo(cboCommodity, LookupTypeEnum.Commodity, "Please Select ...");
            BindCombo(cboRegion, LookupTypeEnum.Regions, "Please Select Region...");

            //FILL YEARS
            cboProductionYear.Items.Add(new ListItem("Please Select Production Year.", ""));
            for (int i = CurrentEthiopianYear -7; i <= CurrentEthiopianYear; i++)
                this.cboProductionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            //LOAD COFFEE TYPE
            //this.cboCoffeeType.Items.Clear();
            //this.cboCoffeeType.Items.Add(new ListItem("Please Select Coffee Type", ""));
            //List<commodityType> list = commodityType.GetAllCoffeeTypes();
            //if (list != null)
            //{
            //    foreach (commodityType i in list)
            //    {
            //        this.cboCoffeeType.Items.Add(new ListItem(i.Name, i.Id.ToString()));
            //    }
            //}
            //if (cboCommodity.SelectedValue == "") return;
            //DataTable dt = GetCommodityTypesByCommodityId(new Guid(cboCommodity.SelectedValue));
            //GetCommodityTypes();

        }

        /// <summary>
        /// Populates existing information for update purpose, if it is text box then it fills the value
        /// if it is dropdown list then it chooses from one of the alternatives supplied by FillControls() method
        /// </summary>
        private void PopulateExistingData()
        {
            //if (theArrival.IsNew) return;
            GRNStatusNew GRNStatus;
            GRNStatusNew.TryParse(theArrival.GRNStatus.ToString(), out GRNStatus);
            if (GRNStatus == GRNStatusNew.New || GRNStatus == GRNStatusNew.NotCreated)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnSave.ToolTip = "Can't Update Arrival because this step is at " + GRNStatus.ToString() + " Stage";
            }
            if (theArrival.WarehouseID != WarehouseBLL.CurrentWarehouse.WarehouseId)
            {
                Response.Redirect("ErrorPage.aspx");
            }
            try
            {
                lblClient.Text = theArrival.ClientName;
                lblWarehouse.Text = WarehouseBLL.CurrentWarehouse.WarehouseName;
                isNonTruck.Checked = theArrival.IsNonTruck;
                cboCommodity.SelectedValue = theArrival.CommodityID.ToString();
                //chkIsSourceDetermined.Checked = theArrival.IsLocationKnown;
                if (!theArrival.IsLocationKnown)
                    cboRegion.SelectedValue = Guid.Empty.ToString();
                else
                {
                    LookupValue lv = SimpleLookup.Lookup(LookupTypeEnum.Woredas).GetDictionary()[theArrival.WoredaID];
                    cboRegion.SelectedValue = lv.RegionID.ToString();
                    cboRegion_SelectedIndexChanged(this, EventArgs.Empty);
                    cboZone.SelectedValue = lv.ZoneID.ToString();
                    cboZone_SelectedIndexChanged(this, EventArgs.Empty);
                    cboWoreda.SelectedValue = theArrival.WoredaID.ToString();
                    txtSpecificArea.Text = theArrival.SpecificArea;
                }
                cboProductionYear.SelectedValue = theArrival.ProductionYear.ToString();
                txtProcessingCenter.Text = theArrival.ProcessingCenter;
                txtNumberOfBags.Text = theArrival.NumberofBags.ToString() == "0" ? "" : theArrival.NumberofBags.ToString();
                txtWeight.Text = theArrival.VoucherWeight.ToString() == "0" ? "" : theArrival.VoucherWeight.ToString();

                if ("1/1/1901" == theArrival.DateTimeReceived.ToShortDateString() || theArrival.DateTimeReceived.ToShortDateString() == "1/1/0001")
                {
                    txtArrivalDate.Text = "";
                    txtTimeArrival.Text = "";
                }
                else
                {
                    txtArrivalDate.Text = theArrival.DateTimeReceived.ToShortDateString();
                    txtTimeArrival.Text = theArrival.DateTimeReceived.ToShortTimeString();
                }
                txtRemark.Text = theArrival.Remark;
                if (!theArrival.IsNonTruck)
                {
                    chkIsTruckInCompound.Checked = theArrival.IsTruckInCompound;
                    txtDriverName.Text = theArrival.DriverName;
                    txtLicenseNo.Text = theArrival.LicenseNumber;
                    txtPlaceIssued.Text = theArrival.LicenseIssuedPlace;
                    txtPlateNo.Text = theArrival.TruckPlateNumber;
                    txtTrailerPlateNo.Text = theArrival.TrailerPlateNumber;
                    txtNoPlomps.Text = theArrival.VoucherNumberOfPlomps.ToString() == "0" ? "" : theArrival.VoucherNumberOfPlomps.ToString();
                    txtTrailerNoPlomps.Text = theArrival.VoucherNumberOfPlompsTrailer.ToString() == "0" ? "" : theArrival.VoucherNumberOfPlompsTrailer.ToString();

                }
                txtVoucherNo.Text = theArrival.VoucherNumber;
                hdnHasVoucher.Value = theArrival.HasVoucher.ToString();
                //chkIsBiProduct.Checked = theArrival.IsBiProduct;
                if (Guid.Empty == theArrival.VoucherCommodityTypeID || string.IsNullOrEmpty(theArrival.VoucherCommodityTypeID.ToString()))
                {
                    cboCommodityType.SelectedIndex = 0;
                    cboCommodityType.Enabled = false;
                }
                else
                    cboCommodityType.SelectedValue = theArrival.VoucherCommodityTypeID.ToString();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = "Failed to populate existing data!";

                Messages.SetMessage("Existing data cannot be retrieved.  Please try again.", WarehouseApplication.Messages.MessageType.Error);
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static CascadingDropDownNameValue[] GetZone(string selectedRegion)
        {
            List<CascadingDropDownNameValue> lst;
            List<LookupValue> lus;
            Predicate<LookupValue> p = new Predicate<LookupValue>(l => l.RegionID == new Guid(selectedRegion));
            lus = SimpleLookup.Lookup(LookupTypeEnum.Zones).GetList(p);
            lst = new List<CascadingDropDownNameValue>();
            foreach (LookupValue l in lus)
            {
                lst.Add(new CascadingDropDownNameValue(l.Description, l.ID.ToString()));
            }
            return lst.ToArray();
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

        protected void chkIsSourceDetermined_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsSourceDetermined.Checked)
            {
                cboRegion.Enabled = true;
                cboZone.Enabled = true;
                cboWoreda.Enabled = true;
            }
            else
            {
                cboRegion.Enabled = false;
                cboZone.Enabled = false;
                cboWoreda.Enabled = false;
            }
        }

        protected void btnPrintPreArrival_Click(object sender, EventArgs e)
        {
            Nullable<Guid> Id;
            Id = theArrival.ID;
            if (Id != null)
            {
                Session["CommodityRequestId"] = Id;
                Session["ReportType"] = "PreArrival";
                ScriptManager.RegisterStartupScript(this,
                                    this.GetType(),
                                    "ShowReport",
                                    "<script type=\"text/javascript\">" +
                                    string.Format("javascript:window.open(\"ReportViewer.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                                    "</script>",
                                    false);
            }
        }

        protected void cboCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCommodityTypes();
            if (cboCommodityType.Items.Count == 1)
                cboCommodityType.Enabled = false;
            else
                cboCommodityType.Enabled = true;
            //cboCommodityType.Enabled = RFVCoffeeType.Enabled;
            if (cboCommodityType.Enabled == false)
                RFVCoffeeType.Enabled = false;
            else
                RFVCoffeeType.Enabled = true;
        }

        private void GetCommodityTypes()
        {
            if (cboCommodity.SelectedValue == "")
            {
                for (int i = cboCommodityType.Items.Count; i > 1; i--)
                    cboCommodityType.Items.RemoveAt(i - 1);
                return;
            }
            for (int i = cboCommodityType.Items.Count; i > 1; i--)
                cboCommodityType.Items.RemoveAt(i - 1);
            DataTable dt = GetCommodityTypesByCommodityId(new Guid(cboCommodity.SelectedValue), Request.QueryString["CommandType"].ToString());
            cboCommodityType.DataSource = dt;
            cboCommodityType.DataTextField = "Description";
            cboCommodityType.DataValueField = "ID";
            cboCommodityType.DataBind();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }

        protected void cboCommodity_PreRender(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlateNo.Text))
                txtNoPlomps.Enabled = false;
            else
                txtNoPlomps.Enabled = true;

            if (string.IsNullOrEmpty(txtTrailerPlateNo.Text))
                txtTrailerNoPlomps.Enabled = false;
            else
                txtTrailerNoPlomps.Enabled = true;
        }
    }
}