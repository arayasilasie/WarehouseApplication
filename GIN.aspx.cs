using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;
using System.Data;
using WarehouseApplication.BLL;
///
/// The Application are devloped in 2012
///
namespace WarehouseApplication
{
    public partial class GIN : System.Web.UI.Page
    {
        protected GINModel CurrentGINModel
        {
            get
            {
                return (GINModel)Session["GINMODEL"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Session.Remove("remainingBalance");
            gvPUNInformation.DataSource = CurrentGINModel.PickupNoticesList;
            gvPUNInformation.DataBind();
            bool editMode = (bool)Session["EditMode"];
            if(CurrentGINModel.PickupNoticesList[0].ConsignmentType != null)
                Session["ConsType"] = CurrentGINModel.PickupNoticesList[0].ConsignmentType.ToString();

            if(CurrentGINModel.PickupNoticesList[0].Commodity==new Guid("71604275-df23-4449-9dae-36501b14cc3b") && CurrentGINModel.PickupNoticesList[0].ConsignmentType=="Bonded Yard")
            {
                BondedYardControls();
            }

            if (CurrentGINModel.PickupNoticesList[0].Commodity == new Guid("71604275-df23-4449-9dae-36501b14cc3b")
                && (CurrentGINModel.PickupNoticesList[0].ConsignmentType == "Truck To Warehouse" || CurrentGINModel.PickupNoticesList[0].ConsignmentType == "Direct To Warehouse"))
            {
                CoffeeWarehouseControls();
                if (editMode)
                    populateControls();
            }

            lblGrossWeight.Text = lblGrossWeight.Text + "(" + CurrentGINModel.PickupNoticesList[0].Measurement + ")";
            lblTruckWeight.Text = lblTruckWeight.Text + "(" + CurrentGINModel.PickupNoticesList[0].Measurement + ")";
            lblNetWeight.Text = lblNetWeight.Text + "(" + CurrentGINModel.PickupNoticesList[0].Measurement + ")";
            FillLIC(drpInventoryCoordinatorLoad, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator           
            FillLabourersAssociation();
            FillWeigher(drpWeigherSupervisor, WareHouseOperatorTypeEnum.Weigher);//Weigher Supervisor             
            gvStack.DataSource = CurrentGINModel.StackInfosList;
            gvStack.DataBind();
            FillWBServiceProvider(drpWBServiceProvider);
            FillCarType(drpTruckType);
            if (CurrentGINModel.GINStatusID == Convert.ToInt32(GINStatusTypeEnum.Start))
                btnSave.Visible = true;
            else if (CurrentGINModel.GINStatusID == Convert.ToInt32(GINStatusTypeEnum.New))
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
           
            Session["EditModePrint"] = Session["EditMode"];
            if (editMode)
                RePopulateGINForm(CurrentGINModel);
            else
            {
                drpWeigherSupervisor.Items.Insert(0, new ListItem("Select", string.Empty));
                drpInventoryCoordinatorLoad.Items.Insert(0, new ListItem("Select", string.Empty));
                drpWBServiceProvider.Items.Insert(0, new ListItem("Select", string.Empty));
                if (drpWBServiceProvider.Items.Count == 2)
                    drpWBServiceProvider.SelectedIndex = 1;
                drpTruckType.Items.Insert(0, new ListItem("Select", string.Empty));
                drpDailyLabourersAssociation.Items.Insert(0, new ListItem("Select", string.Empty));
                Session.Remove("remainingBalance");
            }
            Session["EditMode"] = false;
            unBlockUI();
        }

        private void CoffeeWarehouseControls()
        {
            //txtQuadrant.Enabled = false;
            drpStackNo.Enabled = false;
            if (CurrentGINModel.PickupNoticesList[0].Quadrant != null)
                txtQuadrant.Text = CurrentGINModel.PickupNoticesList[0].Quadrant.ToString();

            HiddenFieldTare.Value = "0.93";
        }

        private void BondedYardControls()
        {

            txtNetWeight.Text = Total.ToString();
            populateControls();
            txtNoOfBags.Enabled = false;
            //txtScaleTicketNo.Enabled = false;
            //txtTrailerPlateNo.Enabled = false;
            //txtTruckPlateNo.Enabled = false;
            txtTruckWeight.Enabled = false;
            txtGrossWeight.Enabled = false;
            //txtLoadingTicket.Enabled = false;
            txtNetWeight.Enabled = false;
            drpStackNo.Enabled = false;
            txtQuadrant.Enabled = false;
            txtAvailableTime.Enabled = false; 
            txtAvailableTimeOnly.Enabled = false;
            txtDateLoaded.Enabled = false;
            txtDateLoadedTime.Enabled = false;
            txtDateWeighted.Enabled = false;
            txtDateWeightedTime.Enabled = false;
            //txtScaleTicketNo.Enabled=false;
            //drpWBServiceProvider.Enabled = false;
            //drpWeigherSupervisor.Enabled = false;
            drpInventoryCoordinatorLoad.Enabled = false;
            
        }

        private void populateControls()
        {
            drpInventoryCoordinatorLoad.SelectedValue = CurrentGINModel.PickupNoticesList[0].LeadInventoryControllerID.ToString();
           if(CurrentGINModel.PickupNoticesList[0].WBServiceProviderID != 0)
               drpWBServiceProvider.SelectedValue = CurrentGINModel.PickupNoticesList[0].WBServiceProviderID.ToString();
            if (CurrentGINModel.PickupNoticesList[0].WeigherID != Guid.Empty)
                drpWeigherSupervisor.SelectedValue = CurrentGINModel.PickupNoticesList[0].WeigherID.ToString();
            if (CurrentGINModel.PickupNoticesList[0].ScaleTicketNumber != null)
                txtScaleTicketNo.Text = CurrentGINModel.PickupNoticesList[0].ScaleTicketNumber.ToString();
            txtGrossWeight.Text = CurrentGINModel.PickupNoticesList[0].GrossWeight.ToString();
            txtTruckWeight.Text = CurrentGINModel.PickupNoticesList[0].TruckWeight.ToString();
            txtNoOfBags.Text = CurrentGINModel.PickupNoticesList[0].TotalNumberOfBags.ToString();
            if(CurrentGINModel.PickupNoticesList[0].Quadrant != null)
                txtQuadrant.Text = CurrentGINModel.PickupNoticesList[0].Quadrant.ToString();
            HiddenFieldTare.Value = "0.93";
        }

        void unBlockUI()
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //               "unBlockUI", "<script type='text/javascript'>$.unblockUI();</script>", false);
        }
        private void RePopulateGINForm(GINModel gm)
        {
            txtDateLoaded.Text = gm.DateTimeLoaded.ToShortDateString();
            txtDateLoadedTime.Text = gm.DateTimeLoaded.ToShortTimeString();
            drpAdjustmentType.SelectedValue = gm.AdjustmentTypeID.ToString();
            txtDriver.Text = gm.DriverName;
            txtLicenseNo.Text = gm.LicenseNumber;
            txtIssuedBy.Text = gm.LicenseIssuedBy;
            txtTruckPlateNo.Text = gm.PlateNumber;
            txtTrailerPlateNo.Text = gm.TrailerPlateNumber;
            txtDNSubmitedTime.Text = gm.TruckRequestTime.ToShortDateString();
            txtDNSubmitedTimeOnly.Text = gm.TruckRequestTime.ToShortTimeString();
            txtAvailableTime.Text = gm.TruckRegisterTime.ToShortDateString();
            txtAvailableTimeOnly.Text = gm.TruckRegisterTime.ToShortTimeString();
            txtDateIssued.Text = gm.DateIssued.ToShortDateString();
            txtDateIssuedTime.Text = gm.DateIssued.ToShortTimeString();
            txtWeightAdjustment.Text = gm.WeightAdjustment.ToString();
            txtBagAdjustment.Text = gm.BagAdjustment.ToString();
            drpDailyLabourersAssociation.SelectedValue = gm.DailyLabourersAssociationID.ToString();
            drpInventoryCoordinatorLoad.SelectedValue = gm.LeadInventoryControllerID.ToString();

            if(drpInventoryCoordinatorLoad.SelectedValue !="")
                PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));

            drpWeigherSupervisor.Items.Insert(0, new ListItem("Select", string.Empty));
            drpInventoryCoordinatorLoad.Items.Insert(0, new ListItem("Select", string.Empty));
            drpWBServiceProvider.Items.Insert(0, new ListItem("Select", string.Empty));
            drpTruckType.Items.Insert(0, new ListItem("Select", string.Empty));
           
            drpDailyLabourersAssociation.Items.Insert(0, new ListItem("Select", string.Empty));
            txtRebag.Text = gm.NoOfRebags.ToString();
            Session["remainingBalance"] = CurrentGINModel.PickupNoticesList.Sum(p => p.RemainingWeight) - CurrentGINModel.StackInfosList.Sum(s => s.NetWeight);
            if (CurrentGINModel.StackInfosList.Count > 0)
                btnAddStack.Visible = false;
            //gvPUNInformation.DataSource = CurrentGINModel.PickupNoticesList;
            //gvPUNInformation.DataBind();
            btnPrint.Visible = true;
        }
        public Guid GINID
        {
            get { return CurrentGINModel.ID; }
        }
        private void FillLIC(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICs(CurrentGINModel);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        private void FillLabourersAssociation()
        {
            drpDailyLabourersAssociation.DataSource = null;
            drpDailyLabourersAssociation.DataSource = DailyLabourersAssociation.DailyLabourersAssociations(UserBLL.GetCurrentWarehouse());
            drpDailyLabourersAssociation.DataTextField = "AssociationName";
            drpDailyLabourersAssociation.DataValueField = "ID";
            drpDailyLabourersAssociation.DataBind();
        }
        private void FillWBServiceProvider(DropDownList ddl)
        {
            ddl.DataSource = null;
            List<GINBussiness.StackTransactionModel> lst = StackTransactionModel.GetWBServiceProvider(CurrentGINModel);
            ddl.DataSource = lst;
            ddl.DataTextField = "ServiceProviderName";
            ddl.DataValueField = "WBServiceProviderID";
            ddl.DataBind();
        }
        private void FillCarType(DropDownList ddl)
        {
            ddl.DataSource = null;
            ddl.DataSource = StackTransactionModel.GetTruckType(CurrentGINModel);
            ddl.DataTextField = "TruckTypeName";
            ddl.DataValueField = "TruckTypeID";
            ddl.DataBind();
        }
        private void FillWeigher(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.WarehouseOperators(UserBLL.GetCurrentWarehouse()).FindAll(p => p.Type % (int)type == 0);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        private bool SateGIN()
        {
            GINModel ginModel = CurrentGINModel;
            ginModel.ID = Guid.NewGuid();
            if (CurrentGINModel.PickupNoticesList[0].Commodity == new Guid("71604275-df23-4449-9dae-36501b14cc3b") && CurrentGINModel.PickupNoticesList[0].ConsignmentType == "Bonded Yard")
            {
                ginModel.DateTimeLoaded = DateTime.Now;
                ginModel.TruckRegisterTime = DateTime.Now;
            }
            else
            {
                ginModel.TruckRegisterTime = DateTime.Parse(Convert.ToDateTime(txtAvailableTime.Text).ToShortDateString() + " " + Convert.ToDateTime(txtAvailableTimeOnly.Text).ToShortTimeString());
                ginModel.DateTimeLoaded = DateTime.Parse(Convert.ToDateTime(txtDateLoaded.Text).ToShortDateString() + " " + Convert.ToDateTime(txtDateLoadedTime.Text).ToShortTimeString());
            }
            ginModel.AdjustmentTypeID = Convert.ToInt32(drpAdjustmentType.SelectedValue);
            try
            {
                string driverFullName = txtDriver.Text.ToString().Trim();
                string[] tokenNames = driverFullName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (int.Parse(tokenNames.Length.ToString()) > 3 || int.Parse(tokenNames.Length.ToString()) < 3)
                {
                    throw new Exception("Invalid Driver Name!");
                }
            }
            catch (Exception ex)
            {
                Messages.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                return false;
            }
            ginModel.DriverName = txtDriver.Text;
            ginModel.LicenseNumber = txtLicenseNo.Text;
            ginModel.LicenseIssuedBy = txtIssuedBy.Text;
            ginModel.PlateNumber = txtTruckPlateNo.Text;
            ginModel.TrailerPlateNumber = txtTrailerPlateNo.Text;
            ginModel.TruckRequestTime = DateTime.Parse(Convert.ToDateTime(txtDNSubmitedTime.Text).ToShortDateString() + " " + Convert.ToDateTime(txtDNSubmitedTimeOnly.Text).ToShortTimeString());
            ginModel.DateIssued = DateTime.Parse(Convert.ToDateTime(txtDateIssued.Text).ToShortDateString() + " " + Convert.ToDateTime(txtDateIssuedTime.Text).ToShortTimeString());
            ginModel.WeightAdjustment = Convert.ToDouble(txtWeightAdjustment.Text);
            ginModel.BagAdjustment = Convert.ToDouble(txtBagAdjustment.Text);
            ginModel.WarehouseID = UserBLL.GetCurrentWarehouse();
            ginModel.LeadInventoryControllerID = new Guid(drpInventoryCoordinatorLoad.SelectedValue);
            ginModel.IsPSA = false;
            ginModel.DailyLabourersAssociationID = Convert.ToInt32(drpDailyLabourersAssociation.SelectedValue);
            ginModel.LeadInventoryController = drpInventoryCoordinatorLoad.SelectedItem.Text;
            ginModel.CreatedBy = BLL.UserBLL.CurrentUser.UserId;
            ginModel.NetWeight = ginModel.PickupNoticesList.Sum(s => s.RemainingWeight) - Convert.ToDouble(Session["remainingBalance"]);
            ginModel.ScaleTicketNumber = txtScaleTicketNo.Text;
            ginModel.LoadUnloadTicketNO = txtLoadingTicket.Text;
            ginModel.GINStatusID = Convert.ToInt32(GINStatusTypeEnum.New);
            try
            {
                ginModel.NoOfBags = Convert.ToInt32(txtNoOfBags.Text);
            }
            catch (Exception ex)
            {
            }

            ginModel.NoOfRebags = Convert.ToDouble(txtRebag.Text);
            btnPrint.Visible = true;
            return true;
        }
        private void SaveGIN()
        {
            GINModel ginModel = CurrentGINModel;
            ginModel.Save();
            Messages.SetMessage("GIN with GIN Number " + ginModel.GINNumber + " is successfully saved " + Session["RemainingMsg"], WarehouseApplication.Messages.MessageType.Success);
            btnSave.Visible = false;
        }

        private void SaveBondedYardGIN()
        {
            GINModel ginmodel = CurrentGINModel;
            ginmodel.SaveBondedYard();
            Messages.SetMessage("GIN with GIN Number " + ginmodel.GINNumber + " is successfully saved " + Session["RemainingMsg"], WarehouseApplication.Messages.MessageType.Success);
            btnSave.Visible = false;
        }
        private void DoNew()
        {
            txtDriver.Text = string.Empty;
            txtLicenseNo.Text = string.Empty;
            txtIssuedBy.Text = string.Empty;
            txtTruckPlateNo.Text = string.Empty;
            txtTrailerPlateNo.Text = string.Empty;          
            txtDNSubmitedTime.Text = string.Empty;
            txtAvailableTime.Text = string.Empty;
            drpInventoryCoordinatorLoad.SelectedIndex = 0;
            txtDateIssued.Text = string.Empty;
            gvStack.DataSource = null;
            gvStack.DataBind();
        }
        private void DoNewStack()
        {
            drpStackNo.SelectedIndex = 0;
            txtScaleTicketNo.Text = string.Empty;
            txtDateWeighted.Text = string.Empty;
            drpWeigherSupervisor.SelectedIndex = 0;
            drpWBServiceProvider.SelectedIndex = 0;
            txtDateWeightedTime.Text = string.Empty;
            drpTruckType.SelectedIndex = 0;
            txtTruckWeight.Text = string.Empty;
            txtGrossWeight.Text = string.Empty;
            txtNoOfBags.Text = string.Empty;
            txtLoadingTicket.Text = string.Empty;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["ConsType"] = CurrentGINModel.PickupNoticesList[0].ConsignmentType;
            Session["ReportType"] = "GIN";
            ScriptManager.RegisterStartupScript(this,
                                                          this.GetType(),
                                                          "ShowReport",
                                                          "<script type=\"text/javascript\">" +
                                                          string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                          "</script>",
                                                          false);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isCoffee = false;
            StackTransactionModel sm = GetStack();
            GINModel gm = CurrentGINModel;
            if (CurrentGINModel.PickupNoticesList[0].Commodity == new Guid("71604275-df23-4449-9dae-36501b14cc3b"))
                isCoffee = true;
            string errorMessage = string.Empty;
            try
            {
                if (RemainingWeight())
                {
                    if (SateGIN())
                    {
                        if (sm.IsValid(isCoffee))
                        {
                            if (isCoffee)
                                SaveBondedYardGIN();
                            else
                            {
                                gm.AddStack(sm);
                                if (gm.IsValid())
                                {

                                    SaveGIN();
                                }
                                else
                                {
                                    errorMessage = gm.ErrorMessage;
                                    Messages.SetMessage(gm.ErrorMessage, WarehouseApplication.Messages.MessageType.Error);
                                }
                            }
                        }
                        else
                        {
                            errorMessage = gm.ErrorMessage;
                            Messages.SetMessage(gm.ErrorMessage, WarehouseApplication.Messages.MessageType.Error);
                        }
                    }
                    else
                    {

                        Session["remainingBalance"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["remainingBalance"] = Convert.ToDouble(Session["remainingBalance"]) + Convert.ToDouble(Session["netWeight"]);
                Messages.SetMessage(ex.Message.ToString(), WarehouseApplication.Messages.MessageType.Error);
            }
        }
        protected void btnAddStack_Click(object sender, EventArgs e)
        {
            StackTransactionModel sm = GetStack();
            //if (sm.IsValid())
            //{
              if (!RemainingWeight())
                {
                    Messages.SetMessage("Requested Net Weight is greater than total available PUN weight. Please check the Loading Ticket.", WarehouseApplication.Messages.MessageType.Warning);
                }
                else
                {
                    GINModel ginModel = new GINModel();
                    ginModel = (GINModel)Session["GINMODEL"];
                    try
                    {
                        DateTime dtm = DateTime.Parse((Convert.ToDateTime(txtDateWeighted.Text).ToShortDateString()) + " " + Convert.ToDateTime(txtDateWeightedTime.Text).ToShortTimeString());
                        DateTime dtm2 = DateTime.Parse((Convert.ToDateTime(txtDateLoaded.Text).ToShortDateString()) + " " + Convert.ToDateTime(txtDateLoadedTime.Text).ToShortTimeString());
                        ginModel.AddStack(new Guid(drpStackNo.SelectedValue), new Guid(drpWeigherSupervisor.SelectedValue), txtScaleTicketNo.Text, txtLoadingTicket.Text, dtm, Convert.ToDouble(txtTruckWeight.Text), Convert.ToDouble(txtGrossWeight.Text), Convert.ToDouble(txtNoOfBags.Text), dtm2, Convert.ToInt32(drpTruckType.SelectedValue), Convert.ToInt32(drpWBServiceProvider.SelectedValue));
                        btnAddStack.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        Messages.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Warning);
                    }
                    gvStack.DataSource = ginModel.StackInfosList;
                    gvStack.DataBind();
                    Session["GINMODEL"] = ginModel;
                    DoNewStack();
                }
            //}
        }
        private bool RemainingWeight()
        {
            GINModel ginModel = (GINModel)Session["GINMODEL"];
            double remainingBalance;
            if (Session["remainingBalance"] == null)
            {
                if (Session["EditMode"] == null)
                    Session["remainingBalance"] = ginModel.PickupNoticesList.Sum(s => s.RemainingWeight);
                else
                {
                    Session["remainingBalance"] = ginModel.PickupNoticesList.Sum(s => s.RemainingWeight);
                }
            }
            Guid commodityGradeId = ((GINModel)Session["GINMODEL"]).PickupNoticesList[0].CommodityGradeID;
            List<BagTypeBLL> bagTypeList = BagTypeBLL.GetCommodityGradeBagTypes(commodityGradeId);

                double netWeight=0;
                if (CurrentGINModel.PickupNoticesList[0].ConsignmentType != "Bonded Yard")
                {

                    netWeight = double.Parse(txtGrossWeight.Text) - double.Parse(txtTruckWeight.Text) -
                                double.Parse(txtNoOfBags.Text) * double.Parse(HiddenFieldTare.Value) +
                                double.Parse(drpAdjustmentType.SelectedValue) * double.Parse(txtWeightAdjustment.Text);
                }
                else if (CurrentGINModel.PickupNoticesList[0].ConsignmentType == "Bonded Yard")
                {

                    HiddenFieldTare.Value = "0.93";
                    netWeight = double.Parse(txtNetWeight.Text);
                }
                Session["netWeight"] = netWeight;
                remainingBalance = Math.Round(double.Parse(Session["remainingBalance"].ToString()), 2) - Math.Round(netWeight, 2);
                if (CurrentGINModel.PickupNoticesList[0].Commodity != new Guid("71604275-df23-4449-9dae-36501b14cc3b"))
                {
                    if (remainingBalance < 0)
                    {
                        Messages.SetMessage("Requested Net Weight is greater than total available PUN weight. Please check the Loading Ticket.", WarehouseApplication.Messages.MessageType.Warning);
                        return false;
                    }
                }
                if (remainingBalance >= 0)
                {
                    Session["RemainingMsg"] = "Selected PUNs have Total Remaining weights to be issued of = " + Convert.ToDouble(remainingBalance) + " Kg";
                }
                Session["remainingBalance"] = remainingBalance;
            
            return true;
        }
        private bool ValidateStack()
        {
            if (drpStackNo.SelectedItem.Value == string.Empty || drpStackNo.SelectedIndex == 0)
            {
                Messages.SetMessage("Please enter Stack No.It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtScaleTicketNo.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Please enter Scale Ticket Number.It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtDateWeighted.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Please enter Date Weighted.It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (drpWeigherSupervisor.SelectedItem.Value == string.Empty || drpWeigherSupervisor.SelectedIndex == 0)
            {
                Messages.SetMessage("Please enter Weigher Name.It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtTruckWeight.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Please enter Truck Weight. It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtGrossWeight.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Please enter Gross Weight.It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (Convert.ToInt32(txtGrossWeight.Text) - Convert.ToInt32(txtTruckWeight.Text) <= 0)
            {
                Messages.SetMessage("Please enter correct the Truck Weight it is more than gross waight.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtNoOfBags.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Please enter No Of Bags. It is a required field.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (Convert.ToInt32(((txtNoOfBags.Text.Trim() == string.Empty ? "0" : txtNoOfBags.Text.Trim()))) < 0)
            {
                Messages.SetMessage("Gin No Of Bags should be greater than zero.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (((GINModel)Session["GINMODEL"]).StackInfosList.Exists(s => s.StackID == new Guid(drpStackNo.SelectedValue)))
            {
                Messages.SetMessage("Cannot add more than once from the same stack for one delivery. Please check the Loading Ticket.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }

            if (Convert.ToDateTime(txtDNSubmitedTime.Text) > Convert.ToDateTime(txtAvailableTime.Text))
            {
                Messages.SetMessage("DN Submitted Time must be less than or equal to Truck Provided Time.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (Convert.ToDateTime(txtAvailableTime.Text) > Convert.ToDateTime(txtDateWeighted.Text))
            {
                Messages.SetMessage("Truck Provided Time must be less than or equal to Date Weighted.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (Convert.ToDateTime(txtDateWeighted.Text) > Convert.ToDateTime(txtDateLoaded.Text))
            {
                Messages.SetMessage("Date Weighted must be less than or equal to Date Loaded.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            return true;
        }
        private StackTransactionModel GetStack()
        {
            StackTransactionModel sm = new StackTransactionModel(CurrentGINModel);
            sm.StackID = Guid.Empty;
            sm.ScaleTicketNumber = txtScaleTicketNo.Text.Trim();
            sm.LoadUnloadTicketNO = txtLoadingTicket.Text;
            sm.DateTimeWeighed = DateTime.Now;
            sm.DateTimeLoaded = DateTime.Now;
            sm.WeigherID = Guid.Empty;
            sm.TruckWeight = TextToDouble(txtTruckWeight.Text);
            sm.GrossWeight = TextToDouble(txtGrossWeight.Text);
            sm.NoOfBags = TextToDouble(txtNoOfBags.Text);
            sm.WBServiceProviderID = 1;
            if (CurrentGINModel.PickupNoticesList[0].Commodity != new Guid("71604275-df23-4449-9dae-36501b14cc3b"))
            {
                if (new Guid(drpStackNo.SelectedValue) != Guid.Empty)
                    sm.StackID = new Guid(drpStackNo.SelectedValue);
                if (!string.IsNullOrEmpty(txtScaleTicketNo.Text.Trim()))
                    sm.ScaleTicketNumber = txtScaleTicketNo.Text.Trim();
                if (!string.IsNullOrEmpty(txtLoadingTicket.Text.Trim()))
                    sm.LoadUnloadTicketNO = txtLoadingTicket.Text.Trim();
                DateTime dtm = DateTime.Parse((Convert.ToDateTime(txtDateWeighted.Text).ToShortDateString()) + " " + Convert.ToDateTime(txtDateWeightedTime.Text).ToShortTimeString());
                DateTime dtm2 = DateTime.Parse((Convert.ToDateTime(txtDateLoaded.Text).ToShortDateString()) + " " + Convert.ToDateTime(txtDateLoadedTime.Text).ToShortTimeString());
                sm.DateTimeWeighed = dtm;
                sm.DateTimeLoaded = dtm2;
            }
            if (drpWeigherSupervisor.SelectedIndex>0)
                sm.WeigherID = new Guid(drpWeigherSupervisor.SelectedValue);
            if (drpTruckType.SelectedIndex > 0)
                sm.TruckTypeID = Convert.ToInt32(drpTruckType.SelectedValue);
            if (drpWBServiceProvider.SelectedIndex > 0)
                sm.WBServiceProviderID = Convert.ToInt32(drpWBServiceProvider.SelectedValue);
            sm.TruckWeight = TextToDouble(txtTruckWeight.Text);
            sm.GrossWeight = TextToDouble(txtGrossWeight.Text);
            sm.NoOfBags = TextToDouble(txtNoOfBags.Text);
            return sm;
        }
        private double TextToDouble(string text)
        {
            double w;
            double.TryParse(text, out w);
            return w;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPickupNotice.aspx");
        }
        protected void drpInventoryCoordinatorLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            System.Threading.Thread.Sleep(2000);
           
            if (drpInventoryCoordinatorLoad.SelectedIndex == 0)
            {
                ModalPopupExtender.Hide();
            }
            else if (new Guid(drpInventoryCoordinatorLoad.SelectedValue) == Guid.Empty)
            {
                PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
                drpStackNo.Items.Insert(0, new ListItem("Select", string.Empty));
            }
            else if (CurrentGINModel.StackInfosList != null && CurrentGINModel.StackInfosList.Exists(s => s.StackID != new Guid(drpInventoryCoordinatorLoad.SelectedValue)))
            {
                ModalPopupExtender.TargetControlID = "drpInventoryCoordinatorLoad";
                ModalPopupExtender.Show();
            }
            else
            {
                PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
                drpStackNo.Items.Insert(0, new ListItem("Select", string.Empty));
            }            
        }
        private void PopulateStack(Guid LIC)
        {           
            WarehouseOperator ob = new WarehouseOperator();
            CurrentGINModel.LICShedID = LIC;
            List<StackTransactionModel> list = null;
            list = StackTransactionModel.GetStackInShed(CurrentGINModel);
            if (list != null)
            {
                drpStackNo.DataSource = list;
                drpStackNo.DataTextField = "StackNumber";
                drpStackNo.DataValueField = "ID";
                drpStackNo.DataBind();
            }
               
        }
        protected void drpStackNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            StackTransactionModel sm = new StackTransactionModel(CurrentGINModel);
            sm.StackID = new Guid(drpStackNo.SelectedValue);
            HiddenFieldTare.Value = sm.Tare.ToString();
        }
        protected void gvStack_SelectedIndexChanged(object sender, EventArgs e)
        {
            GINModel gm = (GINModel)Session["GINMODEL"];
            Guid stackLoadedId = new Guid(gvStack.SelectedDataKey["StackID"].ToString());
            if (gm.StackInfosList.Count != 0)
            {
                StackTransactionModel stm = gm.StackInfosList.First(s => s.StackID == stackLoadedId);
                Guid commodityGradeId = ((GINModel)Session["GINMODEL"]).PickupNoticesList[0].CommodityGradeID;
                List<BagTypeBLL> bagTypeList = BagTypeBLL.GetCommodityGradeBagTypes(commodityGradeId);

                if (Session["remainingBalance"] == null)
                {
                    Session["remainingBalance"] = gm.PickupNoticesList.Sum(s => s.RemainingWeight);//+ (stm.GrossWeight - stm.TruckWeight - bagTypeList[0].Tare * stm.NoOfBags);
                }
                else
                {
                    Session["remainingBalance"] = (double)Session["remainingBalance"] + (stm.GrossWeight - stm.TruckWeight - stm.Tare * stm.NoOfBags);
                }
                txtScaleTicketNo.Text = stm.ScaleTicketNumber;
                txtDateWeighted.Text = stm.DateTimeWeighed.ToShortDateString();
                txtDateWeightedTime.Text = stm.DateTimeWeighed.ToShortTimeString();
                txtTruckWeight.Text = stm.TruckWeight.ToString();
                txtGrossWeight.Text = stm.GrossWeight.ToString();
                txtNoOfBags.Text = stm.NoOfBags.ToString();
                txtLoadingTicket.Text = stm.LoadUnloadTicketNO;
                PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
                drpStackNo.SelectedValue = stm.StackID.ToString();
                //stm.StackID = new Guid(drpStackNo.SelectedValue);
                HiddenFieldTare.Value = stm.Tare.ToString();
                drpWeigherSupervisor.SelectedValue = stm.WeigherID.ToString();// drpWeigherSupervisor.Items.Cast<ListItem>().First(s => new Guid(s.Value) == stm.WeigherID).Value;
                gm.StackInfosList.Remove(stm);
                gm.RemoveStack(stm.StackID);
                btnAddStack.Visible = true;
                FillWBServiceProvider(drpWBServiceProvider);
                drpWBServiceProvider.SelectedValue = stm.WBServiceProviderID.ToString();
                //drp
                FillCarType(drpTruckType);
                drpTruckType.SelectedValue = stm.TruckTypeID.ToString();
                // gvStack.DataSource = gm.StackInfosList;
                // gvStack.DataBind();
            }
            else
            {
                Messages.SetMessage("The stack is already selected or Empty Stack.", WarehouseApplication.Messages.MessageType.Warning);
            }
        }
        protected void OkButton_Click(object sender, EventArgs e)
        {
            GINModel ginModel = (GINModel)Session["GINMODEL"];
            StackTransactionModel sm = new StackTransactionModel(CurrentGINModel);
            CurrentGINModel.stackList = null;
            CurrentGINModel.StackInfosList = new List<StackTransactionModel>();
            Session.Remove("remainingBalance");

            gvStack.DataSource = null;
            gvStack.DataBind();
            PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
            drpStackNo.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
        decimal Total = 0;       
        protected void gvPUNInformation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Total to the running total variables
                Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RemainingWeight"));               
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Totals:";
                // for the Footer, display the running totals
                e.Row.Cells[3].Text = Total.ToString();              
                e.Row.Cells[3].HorizontalAlign = e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

            }
        }
        protected void btnStackRemove_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void gvStack_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ImageButton buttonSelect = (ImageButton)e.Row.Cells[0].FindControl("btnStackRemove");
            //    buttonSelect.CommandArgument = e.Row.RowIndex.ToString();
            //}
        }

        protected void gvStack_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // // If multiple buttons are used, use the
            //// CommandName property to determine if "Delete Stack" button was clicked. 
            //if (e.CommandName.Equals("DeleteStack"))
            //{
            //    // Convert the row index stored in the CommandArgument
            //    // property to an Integer.
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = gvStack.Rows[index];

            //    //fetch product attributes from current row
            //    string productID = gvStack.DataKeys[index].Value.ToString();
            //    string productName = row.Cells[1].Text;

            //    //delete product
            //   // RemoveProduct(productID, productName);
            //}
        }

        protected void txtGrossWeight_TextChanged(object sender, EventArgs e)
        {
                 //var txtTruckWeight = txtTruckWeight.Text;//document.getElementById('<%= txtTruckWeight.ClientID%>').value;
                 //var txtGrossWeight = txtGrossWeight.Text;//document.getElementById('<%= txtGrossWeight.ClientID%>').value;
                 //var txtAddWeight = txtWeightAdjustment.Text;//document.getElementById('<%= txtWeightAdjustment.ClientID%>').value;
                 //var txtNoOfBags = txtNoOfBags.Text;//tedocument.getElementById('<%= txtNoOfBags.ClientID%>').value;
                 //var cal = drpAdjustmentType.Text;//document.getElementById('<%= drpAdjustmentType.ClientID%>').value;
                 //var tare = HiddenFieldTare.ToString();//Text//document.getElementById('<%= HiddenFieldTare.ClientID%>').value;
                 
                 //if (txtTruckWeight != "" && txtGrossWeight != "") {
                     
                 //    //var amt = eval((parseFloat(txtGrossWeight) - parseFloat(txtTruckWeight)) - ((parseFloat(txtNoOfBags) * parseFloat(tare)) / 100));
                 //    var amt = eval((parseFloat(txtGrossWeight) - parseFloat(txtTruckWeight)) - ((parseFloat(txtNoOfBags) * parseFloat(tare))));
                 //    amt = roundNumber(amt, 4);
                 //    document.getElementById('<%= txtNetWeight.ClientID%>').value = amt;
                 //if (cal == 1) {
                 //   var netweight = eval(parseFloat(document.getElementById('<%= txtNetWeight.ClientID%>').value) + parseFloat(txtAddWeight));
                 //   netweight = roundNumber(netweight, 4);
                 //    document.getElementById('<%= txtNetWeight.ClientID%>').value = netweight;
                 //   }
                 //      else if (cal == -1) {
                 //      var netweight = eval(parseFloat(document.getElementById('<%= txtNetWeight.ClientID%>').value) - parseFloat(txtAddWeight));
                 //      netweight = roundNumber(netweight, 4);
                 //      document.getElementById('<%= txtNetWeight.ClientID%>').value = netweight;
                 // }
               
          }

        protected void drpTruckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Trucktype = drpTruckType.SelectedItem.ToString();
        }

        
      
    }
}
