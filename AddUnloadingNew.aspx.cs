using System;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Data;
using System.Web;
using System.Configuration;
using System.Web.UI;


namespace WarehouseApplication
{
    public partial class AddUnloadingNew : System.Web.UI.Page
    {
        private static Guid BagTypeId;
        protected void Page_Load(object sender, EventArgs e)
        {
            Messages.ClearMessage();          
            if (!IsPostBack)
            {
                Page.DataBind();
                if (Request.QueryString["CommandName"] == null)
                {
                    //change to zaid.
                    Messages.ClearMessage();
                    return;
                }
                ViewState["CommandName"] = Request.QueryString["CommandName"];

                if (ViewState["CommandName"].ToString() == "Insert")
                    ViewState["GradingCode"] = Request.QueryString["GradingCode"].ToString();
                else if (ViewState["CommandName"].ToString() == "Update")
                    ViewState["GradingCode"] = Session["GradingCode_Search"].ToString();
                else
                    return;

                if (string.IsNullOrEmpty(ViewState["GradingCode"].ToString()))
                {
                    //change to zaid.
                    Messages.ClearMessage();
                    return;
                }

                //HttpContext.Current.Session["TrackingNumber_GRN"] = Request.QueryString["TranNo"];
                HttpContext.Current.Session["ArrivalId_GRN"] = null; //Initialized at LoadDisplayInfo() function
                HttpContext.Current.Session["GradeId_GRN"] = null;
                HttpContext.Current.Session["CommodityGradeId_GRN"] = null;
                HttpContext.Current.Session["ShedId_GRN"] = null;
                HttpContext.Current.Session["LICID_GRN"] = null;

                HttpContext.Current.Session["GRNId_GRN"] = null;//Setted at InitializeInstance() function
                ViewState["GRNID"] = null; //setted at displayInfo() function

                string GradingCode = ViewState["GradingCode"].ToString();
                LoadDisplayInfo(GradingCode);
                SetRawCupValues(GradingCode);
                
                cboShed.Enabled = false;

                if (ViewState["CommandName"].ToString() == "Insert")
                {
                    //Make submit button to Save
                    btnPrintGrn.Enabled = false;
                    btnSave.Text = "Save";
                    btnNext.Enabled = false;
                    LoadControls();
                }
                else if (ViewState["CommandName"].ToString() == "Update")
                {
                    //Populate data for update
                    //Change Save button to Update 
                    LoadControls();
                    PopulateInformation();
                    btnPrintGrn.Enabled = true;
                    btnNext.Enabled = true;
                    Session["CommandType_GRN"] = null;
                }
                else
                {
                    LoadControls();
                    //Redirect to Error page and 
                    //Display session expired message                
                }

            }
        }

        /// <summary>
        /// Populate information inserted at previous stages like cdr and Grading stages
        /// and display it as a table inside gridview. It uses on stored procedure to populate data
        /// </summary>
        private void LoadDisplayInfo(string GradingCode)
        {
            try
            {
                GRN_BL obj = new BLL.GRN_BL();

                DataTable dt = obj.GetDisplayInfoByGradingCode(GradingCode);
                grdDisplayInfo.DataSource = dt;
                grdDisplayInfo.DataBind();
                //If coffee then change lable to Kg. Else change label to Qtl.
                if (dt.Rows[0]["CommodityId"].ToString() == "71604275-df23-4449-9dae-36501b14cc3b")
                {
                    cboStackNo.Enabled = false;
                    cboStackNo.SelectedValue = Guid.Empty.ToString();
                    Session["IsTracable"] = true;
                    Session["CommodityId"] = dt.Rows[0]["CommodityId"].ToString();
                    lblUnit1.Text = "Kg.";
                    lblUnit2.Text = "Kg.";
                }
                else
                {
                    Session["IsTracable"] = false;
                    ChkBondedYard.Enabled = false;
                    Session["CommodityId"] = dt.Rows[0]["CommodityId"].ToString();
                    lblUnit1.Text = "Qtl.";
                    lblUnit2.Text = "Qtl.";
                }
                GRNStatusNew status;
                GRNStatusNew.TryParse((dt.Rows[0]["status"].ToString()), out status);

                if (status == GRNStatusNew.New || status == GRNStatusNew.NotCreated)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnSave.ToolTip = "Can't Update GRN because it is at " + status.ToString() + " Stage";

                }
                ViewState["WarehouseId"] = dt.Rows[0]["WarehouseId"].ToString();
                ViewState["ProductionYear"] = dt.Rows[0]["Pro_Year"].ToString();

                Session["ArrivalId_GRN"] = new Guid(dt.Rows[0]["ArrivalId"].ToString());
                Session["GradeId_GRN"] = new Guid(dt.Rows[0]["GradeId"].ToString());
                Session["CommodityGradeId_GRN"] = new Guid(dt.Rows[0]["CommodityGradeId"].ToString());
                Session["ShedId_GRN"] = new Guid(dt.Rows[0]["ShedId"].ToString());
                Session["LICID_GRN"] = new Guid(dt.Rows[0]["LICID"].ToString());
                Session["TruckPlateNumber"] = dt.Rows[0]["TruckPlate"].ToString();
                Session["TrailerPlateNumber"] = dt.Rows[0]["TrailerPlate"].ToString();
                Session["ProcessingCenter"] = dt.Rows[0]["ProcessingCenter"].ToString();
                Session["WoredaID"] = dt.Rows[0]["WoredaID"].ToString();

                hdnTare.Value = dt.Rows[0]["Tare"].ToString();
                ViewState["CommodityGradeId"] = dt.Rows[0]["CommodityGradeId"].ToString();
                ViewState["TrackingNumber"] = dt.Rows[0]["TrackingNumber"].ToString();
                ViewState["GRNID"] = dt.Rows[0]["GRNID"].ToString();

                BagTypeId = new Guid(dt.Rows[0]["BagTypeId"].ToString());
                txtClientAcceptanceDate.Text = DateTime.Parse(dt.Rows[0]["ClientAcceptanceDate"].ToString()).ToString("g");
                hdnClientAcceptanceDate.Value = DateTime.Parse(dt.Rows[0]["ClientAcceptanceDate"].ToString()).ToShortDateString();
                Session["ArrivalCert"] = getArrivalCert(new Guid(Session["ArrivalId_GRN"].ToString()));
                string TrackingNumber = ViewState["TrackingNumber"].ToString();
                PopulateSameGradingGrid(TrackingNumber);
            }
            catch 
            {
                Messages.SetMessage("Failed to load existing information!");
            }
        }

        private string getArrivalCert(Guid arrivalid)
        {
            GRN_BL arr = new GRN_BL();
            string str = "";
            DataTable dt = arr.getArrivalCert(arrivalid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i < dt.Rows.Count - 1)
                    str = str + dt.Rows[i]["CertificateName"].ToString() + ", ";
                else
                    str = str + dt.Rows[i]["CertificateName"].ToString();
            }
            return str;
        }

        /// <summary>
        /// this function filled dropdowns with possible alternatives each time the page is loaded
        /// </summary>
        private void LoadControls()
        {
            GRN_BL objGRN_BL = new BLL.GRN_BL();
            DataTable dt;
            //string a = Session["WarehouseId"].ToString();
            //string b = Session["ProductionYear"].ToString();
            //Guid x = new Guid(Session["WarehouseId"].ToString());
            //int y = int.Parse(Session["ProductionYear"].ToString());
            //Load Sheds
            if (ViewState["CommodityGradeId"] == null)
            {
                //zaid 
                return;
            }
            Guid commodityGradeID = new Guid (ViewState["CommodityGradeId"].ToString());

            dt = objGRN_BL.GetAllAvailableSheds(new Guid(ViewState["WarehouseId"].ToString()),
                commodityGradeID, int.Parse(ViewState["ProductionYear"].ToString()));   
                                                                                                    
            cboShed.DataSource = dt;
            cboShed.DataValueField = "Id";
            cboShed.DataTextField = "Number";
            cboShed.DataBind();
            cboShed.SelectedValue = Session["ShedId_GRN"].ToString();

            if (ViewState["CommandName"].ToString() == "Insert")
                cboShed.SelectedValue = Session["ShedId_GRN"].ToString();

            //load stack on page lode time
            if (ViewState["CommandName"].ToString() == "Insert" && Session["CommodityId"].ToString() != "71604275-df23-4449-9dae-36501b14cc3b")
            {
                LoadStacksByShadeId(new Guid(Session["ShedId_GRN"].ToString()));
            }

            if (cboShed.SelectedIndex != 0)
                LoadLICByShed(new Guid(cboShed.SelectedValue.ToString()));

            //Load Weighers
            dt = objGRN_BL.GetAllWeighers(new Guid(ViewState["WarehouseId"].ToString()));
            cboWeigher.DataSource = dt;
            cboWeigher.DataValueField = "Id";
            cboWeigher.DataTextField = "Name";
            cboWeigher.DataBind();

            //Load WB Service Provider
            dt = objGRN_BL.GetAllWBServiceProviders(new Guid(ViewState["WarehouseId"].ToString()));
            cboWBServiceProvider.DataSource = dt;
            cboWBServiceProvider.DataValueField = "Id";
            cboWBServiceProvider.DataTextField = "ServiceProviderName";
            cboWBServiceProvider.DataBind();

            //Load Labourer Group
            dt = objGRN_BL.GetAllLabourerGroup();
            cboLabourerGroup.DataSource = dt;
            cboLabourerGroup.DataValueField = "Id";
            cboLabourerGroup.DataTextField = "GroupName";
            cboLabourerGroup.DataBind();

            //Load Trucks
            dt = objGRN_BL.GetAllTrucks();
            cboTruckType.DataSource = dt;
            cboTruckType.DataValueField = "Id";
            cboTruckType.DataTextField = "Name";
            cboTruckType.DataBind();
        }

        /// <summary>
        /// Populate existing unloading information, if the page is opened for update
        /// </summary>
        private void PopulateInformation()
        {
            try
            {
                GRN_BL objGrn = new GRN_BL();
                DataTable dt = objGrn.PopulateGrnById(new Guid(Session["GRNID_Search"].ToString()));
                txtDepositTicketNumber.Text = dt.Rows[0]["UnloadingTicketNumber"].ToString();
                txtNumberOfBags.Text = dt.Rows[0]["TotalNumberOfBags"].ToString();
                txtRebagging.Text = dt.Rows[0]["RebagingQuantity"].ToString();
                txtDateDeposited.Text = DateTime.Parse(dt.Rows[0]["DateDeposited"].ToString()).ToShortDateString();
                txtTimeDeposited.Text = DateTime.Parse(dt.Rows[0]["DateDeposited"].ToString()).ToShortTimeString();
                cboShed.SelectedValue = dt.Rows[0]["ShedID"].ToString();
                if (cboShed.SelectedIndex != 0 && Request.QueryString["CommandName"].ToString() == "Update")
                {
                    LoadLICByShed(new Guid(cboShed.SelectedValue.ToString()));
                    if (cboStackNo.SelectedValue.ToString() != "-1")
                    {
                        LoadStacksByShadeId(new Guid(cboShed.SelectedValue.ToString()));
                    }
                }
                cboInventoryController.SelectedValue = dt.Rows[0]["LeadInventoryControllerID"].ToString();
                cboStackNo.SelectedValue = dt.Rows[0]["StackID"].ToString();
                cboLabourerGroup.SelectedValue = dt.Rows[0]["LabourerGroup"].ToString();
                chkScaleTicketSigned.Checked = bool.Parse(dt.Rows[0]["ScaleTicketSigned"].ToString());
                txtScaleTicket.Text = dt.Rows[0]["ScaleTicketNumber"].ToString();
                txtDateWeighed.Text = DateTime.Parse(dt.Rows[0]["DateTimeWeighed"].ToString()).ToShortDateString();
                txtTimeWeighed.Text = DateTime.Parse(dt.Rows[0]["DateTimeWeighed"].ToString()).ToShortTimeString();
                txtGrossTruckWeight.Text = dt.Rows[0]["GrossWeight"].ToString();
                txtTruckWeight.Text = dt.Rows[0]["TruckWeight"].ToString();
                lblNetWeight.Text = dt.Rows[0]["NetWeight"].ToString();
                cboTruckType.SelectedValue = dt.Rows[0]["TruckTypeId"].ToString();
                cboWeigher.SelectedValue = dt.Rows[0]["WeigherID"].ToString();
                cboWBServiceProvider.SelectedValue = dt.Rows[0]["WBServiceProviderID"].ToString();
                txtRemark.Text = dt.Rows[0]["Remark"].ToString();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Validated user inputes
        /// </summary>
        private void ValidateInputs()
        {
        }

        /// <summary>
        /// Sets all user inputs to BLL instance object
        /// </summary>
        /// <param name="objGrnSetter">An instance of BLL created and send to this method, 
        /// NB the instance is not created inside this method</param>
        private void InitializeInstance(GRN_BL objGrnSetter)
        {
            try
            {
                if (ViewState["CommandName"].ToString() == "Update" && Session["GRNID_Search"] != null)
                    objGrnSetter.ID = new Guid(Session["GRNID_Search"].ToString());
                else if (ViewState["CommandName"].ToString() == "Insert")
                    objGrnSetter.ID = Guid.NewGuid();
                else
                {
                    objGrnSetter = null;
                    return;
                }
                Session["GRNId_GRN"] = objGrnSetter.ID;
                if (ChkBondedYard.Checked)
                    objGrnSetter.ConsignmentType = 1;
                else if (ChkWHMode.Checked)
                    objGrnSetter.ConsignmentType = 3;
                else
                    objGrnSetter.ConsignmentType = 2;


                objGrnSetter.TrackingNumber = ViewState["TrackingNumber"].ToString();
                objGrnSetter.ArrivalCert = Session["ArrivalCert"].ToString();
                objGrnSetter.DateDeposited = DateTime.Parse(txtDateDeposited.Text + " " + txtTimeDeposited.Text);
                objGrnSetter.UnloadingTicketNumber = txtDepositTicketNumber.Text;
                objGrnSetter.TotalNumberOfBags = int.Parse(txtNumberOfBags.Text);
                objGrnSetter.IsTracable = Convert.ToBoolean(Session["IsTracable"]);
                //objGrnSetter.Quantity = 0;//Not actual value
                objGrnSetter.UserID = UserBLL.GetCurrentUser();
                if (!(string.IsNullOrEmpty(txtRebagging.Text.Trim())))
                {
                    objGrnSetter.RebagingQuantity = int.Parse(txtRebagging.Text);
                }
                else
                {
                    objGrnSetter.RebagingQuantity = 0;

                }


                //StackTransaction related fields
                objGrnSetter.StackTransactionID = Guid.NewGuid();

                objGrnSetter.ShedID = new Guid(cboShed.SelectedValue);
                objGrnSetter.StackID = Guid.Empty;
                if (Session["CommodityId"].ToString() != "71604275-df23-4449-9dae-36501b14cc3b")
                    if (!(string.IsNullOrEmpty(cboStackNo.SelectedValue)))
                        objGrnSetter.StackID = new Guid(cboStackNo.SelectedValue);
               
                objGrnSetter.LeadInventoryController = new Guid(cboInventoryController.SelectedValue);
                objGrnSetter.WeigherID = new Guid(cboWeigher.SelectedValue);
                objGrnSetter.WBServiceProviderID = int.Parse(cboWBServiceProvider.SelectedValue);
                objGrnSetter.LabourerGroup = int.Parse(cboLabourerGroup.SelectedValue);
                objGrnSetter.DateTimeWeighed = DateTime.Parse(txtDateWeighed.Text + " " + txtTimeWeighed.Text);
                objGrnSetter.ScaleTicketNumber = txtScaleTicket.Text;
                objGrnSetter.GrossWeight = float.Parse(txtGrossTruckWeight.Text);
                objGrnSetter.TruckWeight = float.Parse(txtTruckWeight.Text);
                objGrnSetter.NetWeight = float.Parse(lblNetWeight.Text);
                objGrnSetter.BagTypeID = BagTypeId;
                objGrnSetter.NoOfBags = int.Parse(txtNumberOfBags.Text);
                objGrnSetter.Tare = float.Parse(hdnTare.Value);
                if (!ChkBondedYard.Checked)
                {
                    objGrnSetter.Quadrant = TxtQuadrant.Text;
                    objGrnSetter.Shade = cboShed.SelectedItem.Text;
                }
                else
                {
                    objGrnSetter.Quadrant = TxtQuadrant.Text;
                    objGrnSetter.Shade = cboShed.SelectedItem.Text;
                }
                objGrnSetter.WoredaID = new Guid(Session["WoredaID"].ToString());
                objGrnSetter.ProcessingCenter = Session["ProcessingCenter"].ToString();
                objGrnSetter.RawValue = Convert.ToDecimal(ViewState["RawValue"]);
                objGrnSetter.CupValue = Convert.ToDecimal(ViewState["CupValue"]);
                if (ViewState["isSpeciality"] != null)
                {
                    if (!ViewState["isSpeciality"].ToString().Equals("true"))
                    {
                        objGrnSetter.TotalValue = Convert.ToDecimal(ViewState["RawValue"]) + Convert.ToDecimal(ViewState["CupValue"]);
                        if (objGrnSetter.TotalValue > 100)
                        {
                            objGrnSetter.TotalValue = Convert.ToDecimal(ViewState["CupValue"]);
                        }
                    }
                    else
                    {
                        objGrnSetter.TotalValue = Convert.ToDecimal(ViewState["CupValue"]);
                    }
                }
                objGrnSetter.TrailerPlateNumber = Session["TrailerPlateNumber"].ToString();
                objGrnSetter.TruckPlateNumber = Session["TruckPlateNumber"].ToString();
                objGrnSetter.Remark = txtRemark.Text;
                objGrnSetter.ArrivalId = new Guid(Session["ArrivalId_GRN"].ToString());
                objGrnSetter.IsScaleTicketSigned = chkScaleTicketSigned.Checked;
                objGrnSetter.Tolerance = float.Parse(ConfigurationSettings.AppSettings["WeightTolerance"].ToString());
                objGrnSetter.TruckTypeID = int.Parse(cboTruckType.SelectedValue.ToString());
                //objGrnSetter.CommodityGradeId = new Guid(Session["CommodityGradeId_GRN"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error on initializing object!");
            }
        }

        private void ClearForm()
        {
        }

        private void PopulateSameGradingGrid(string TrackingNo)
        {
            GRN_BL obj2 = new BLL.GRN_BL();
            DataTable dt2 = obj2.GetGradingsWithSameResults(TrackingNo);
            grdSameGradingResults.DataSource = dt2;
            grdSameGradingResults.DataBind();
        }

        private void LoadStacksByShadeId(Guid ShadeId)
        {
            //Load Stacks
            DataTable dt;
            GRN_BL objGRN_BL = new GRN_BL();
            cboStackNo.Items.Clear();
            //if (cboStackNo.SelectedValue.ToString() != "")
            //{
                cboStackNo.Items.Add(new ListItem("Select Stack No.", ""));
                //dt = objGRN_BL.GetAllAvailableStacks(ShadeId, new Guid("6b5852b4-15a2-41ac-aad6-89f1c2955bc1"), int.Parse(Session["ProductionYear"].ToString()));
                dt = objGRN_BL.GetAllAvailableStacks(ShadeId, new Guid(ViewState["CommodityGradeId"].ToString()), int.Parse(ViewState["ProductionYear"].ToString()));
                cboStackNo.DataSource = dt;
                cboStackNo.DataValueField = "Id";
                cboStackNo.DataTextField = "Number";
                cboStackNo.DataBind();
            //}
        }

        private void LoadLICByShed(Guid ShedId)
        {
            if (cboShed.SelectedIndex == 0)
                return;
            for (int i = cboInventoryController.Items.Count - 1; i > 0; i--)
                cboInventoryController.Items.RemoveAt(i);
            GRN_BL objGRN_BL = new BLL.GRN_BL();
            DataTable dt;
            dt = objGRN_BL.GetInventoryControllersByShed(ShedId);
            cboInventoryController.DataSource = dt;
            cboInventoryController.DataValueField = "Id";
            cboInventoryController.DataTextField = "Name";
            cboInventoryController.DataBind();
            if (cboShed.SelectedValue.ToString() == Session["ShedId_GRN"].ToString())
                if (ViewState["CommandName"].ToString() == "Insert")
                    cboInventoryController.SelectedValue = Session["LICID_GRN"].ToString();
        }

        protected void cboShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboShed.SelectedIndex != 0)
            {
                LoadStacksByShadeId(new Guid(cboShed.SelectedValue));
                LoadLICByShed(new Guid(cboShed.SelectedValue.ToString()));
            }
        }

        public bool checkConsigmentSelection()
        {
            if (ChkBondedYard.Checked == false && ChkWHMode.Checked == false)
            {
                Messages.SetMessage("Please Select Consignment Type . . . ", WarehouseApplication.Messages.MessageType.Error);
                return true;
            }
            if (ChkBondedYard.Checked == true && ChkWHMode.Checked == true)
            {
                Messages.SetMessage("Please Select Only One Consignment Type . . . ", WarehouseApplication.Messages.MessageType.Error);
                return true;
            }
            return false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GRN_BL objGrnSetter = new BLL.GRN_BL();
            string GradesToBeClosedQuery = "UPDATE tblworkflow SET UpdatedBy = '" + UserBLL.GetCurrentUser() + "', UpdateTimeStamp = '" + DateTime.Now + "' WHERE TrackingNo IN(";
            string closingGradesTrackingNumbers = "";
            int counter=0;
            try
            {
                if (checkConsigmentSelection())
                    return;
                if (Session["CommodityId"].ToString() != "71604275-df23-4449-9dae-36501b14cc3b" && cboStackNo.SelectedIndex==-1)
                {
                    Messages.SetMessage("Please Select Stack !!", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                       
                InitializeInstance(objGrnSetter);
                if (objGrnSetter == null)
                    throw new Exception("Initialization has not been successfull!");
                
                string GradingIdsWithSameGradeResult = "";
                foreach (GridViewRow grdRow in grdSameGradingResults.Rows)
                {
                    CheckBox chkItem = (CheckBox)grdRow.FindControl("chkChoice");
                    if (chkItem.Checked == true)
                    {
                        GradingIdsWithSameGradeResult += grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["GradeId"].ToString() + ",";

                        if (chkItem.Enabled == true)
                        {
                            counter++;
                            if (counter > 1)
                                closingGradesTrackingNumbers += "," + "'" + grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["TrackingNumber"].ToString() + "'";
                            else
                                closingGradesTrackingNumbers += "'" + grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["TrackingNumber"].ToString() + "'";
                        }
                    }
                }
                objGrnSetter.GradingIdsWithSameGradeResult = GradingIdsWithSameGradeResult;
                GradesToBeClosedQuery += closingGradesTrackingNumbers + ") AND (UpdatedBy IS NULL AND UpdateTimeStamp IS NULL)";
                if (!string.IsNullOrEmpty(closingGradesTrackingNumbers))
                    objGrnSetter.GradesToBeClosedQuery = GradesToBeClosedQuery;
                else
                    objGrnSetter.GradesToBeClosedQuery = "";
                if (objGrnSetter.NetWeight < 0)
                {
                    Messages.SetMessage("Net Weight should not be less than zero !! Please correct Gross Weight / NetWeight . . . ", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                
                objGrnSetter.SaveGRN();

                string TrackingNumber = ViewState["TrackingNumber"].ToString();
                PopulateSameGradingGrid(TrackingNumber);
                Messages.SetMessage("Record added successfully.", WarehouseApplication.Messages.MessageType.Success);
                btnPrintGrn.Enabled = true;
                btnNext.Enabled = true;
            }
            catch (Exception ex)
            {
                Messages.SetMessage("Failed to insert data!" + ex.Message.ToString(), WarehouseApplication.Messages.MessageType.Error);
                //lblmsg.Text = "Failed to insert data! " + ex.Message.ToString();
            }
        }

        protected void grdDisplayInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;
            //e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;

        }

        protected void grdSameGradingResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string CommandName = Request.QueryString["CommandName"].ToString();
            string OpenedGRN = ViewState["GRNID"].ToString();
            foreach (GridViewRow grdRow in grdSameGradingResults.Rows)
            {
                string GradeId = Session["GradeId_GRN"].ToString();
                string CommodityGradeId = Session["CommodityGradeId_GRN"].ToString();

                CheckBox chkItem = (CheckBox)grdRow.FindControl("chkChoice");
                if (CommandName == "Insert" && !string.IsNullOrEmpty( grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["GRNID"].ToString()))
                {
                    grdRow.ForeColor = System.Drawing.Color.Green;
                    chkItem.Checked = false;
                    chkItem.Enabled = false;
                    continue;
                }
                if (CommandName == "Update" && OpenedGRN == grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["GRNID"].ToString())
                {
                    grdRow.ForeColor = System.Drawing.Color.Green;
                    chkItem.Checked = true;
                    chkItem.Enabled = false;
                    continue;
                }
                if (CommandName == "Update" && GradeId == grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["GradeId"].ToString())
                {
                    chkItem.Checked = false;
                    chkItem.Enabled = false;
                    grdRow.ForeColor = System.Drawing.Color.Green;
                    continue;
                }


                if (GradeId == grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["GradeId"].ToString())
                {
                    chkItem.Checked = true;
                    chkItem.Enabled = false;
                    grdRow.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    if (CommodityGradeId == grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["CommodityGradeID"].ToString())
                    {
                        if (CommandName == "Update" && OpenedGRN == grdSameGradingResults.DataKeys[grdRow.RowIndex].Values["GRNID"].ToString())
                        {
                            grdRow.ForeColor = System.Drawing.Color.Green;
                            chkItem.Checked = true;
                            chkItem.Enabled = false;
                        }
                        else
                        {
                            if (CommandName == "Update")
                            {
                                grdRow.ForeColor = System.Drawing.Color.Green;
                                chkItem.Enabled = false;
                            }
                            else
                            grdRow.ForeColor = System.Drawing.Color.Green;                        
                        }
                    }
                    else
                    {
                        chkItem.Enabled = false;
                        grdRow.ForeColor = System.Drawing.Color.Gray;
                    }
                }
            }
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        protected void chkChangeShade_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChangeShade.Checked == true)
            {
                cboShed.Enabled = true;
                cboInventoryController.Enabled = true;
            }
            else
            {
                cboShed.Enabled = false;
                cboInventoryController.Enabled = false;
            }
        }

        protected void btnPrintGrn_Click(object sender, EventArgs e)
        {
            if (ViewState["CommandName"].ToString() == "Update" && Session["GRNID_Search"] != null)
                Session["GRNId_GRN"] = Session["GRNID_Search"].ToString();
            Session["ReportType"] = "GRN";

            ScriptManager.RegisterStartupScript(this,
                    this.GetType(),
                    "ShowReport",
                    "<script type=\"text/javascript\">" +
                    string.Format("javascript:window.open(\"ReportViewer.aspx?\", \"_blank\",\"height=750px,width=900px,top=0,left=0,resizable=yes,scrollbars=yes\");") +
                    "</script>", false);
           //Response.Redirect("ReportViewer.aspx");
        }

        protected void chkIsManuallyScaled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsManuallyScaled.Checked == true)
            {
                this.xsety1.Enabled = false;
                txtTruckWeight.Text = "0";
                txtTruckWeight.Enabled = false;
                
            }
            else
            {
                
                this.xsety1.Enabled = true;
                txtTruckWeight.Enabled = true;
                txtTruckWeight.Text = "";
            }

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }

        public void SetRawCupValues(string gradingcode)
        {
            DataTable dt= new DataTable();
            GRN_BL grn=new GRN_BL();
            decimal rawvalue = 0, cupvalue = 0; ViewState["isSpeciality"] = "false";
            dt = grn.getGradingsDetailbyGRCode(gradingcode);
            foreach(DataRow dr in dt.Rows)
            {
                if(dr["GradingFactorGroupID"].ToString().ToUpper() == "DCF40E67-16C9-4444-B62E-0E46410D0129".ToUpper()) // for washed coffee grading factor group
                {
                        if (dr["GradingFactorID"].ToString().ToUpper() == "44F51F10-A698-4F1F-B37E-6CD8EF8858F4")   // Secondary Defect Distribution grading factor
                            rawvalue=rawvalue+Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "B6146DBE-B688-4CE7-9D1E-5DF76170CA63")   // Primary Defect Distribution grading factor
                            rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "3FBA3370-D4CD-48A5-8F6D-39366E31977D")   // Odour grading factor
                            rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "505E5EF4-FBD4-4BD5-935E-632EAC33928D")   // Color grading factor
                            rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "576854AE-1339-4554-A856-C3CEC7F36172")   // Make and Shape grading factor
                            rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());

                        if (dr["GradingFactorID"].ToString().ToUpper() == "2BE965C6-8784-49CA-BCDB-AA0764C97C4D")   // Cup Cleanness grading factor
                            cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "5AAB471D-A5B0-42BF-8988-13A2F06AE8A2")   // Acidity grading factor
                            cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "FF892F2A-EE12-4243-8572-43963DABE533")   // Body grading factor
                            cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                        if (dr["GradingFactorID"].ToString().ToUpper() == "C1AE5DF7-CD28-4201-A1B6-CC5A840618AA")   // Flavour grading factor
                            cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    }
                else if (dr["GradingFactorGroupID"].ToString().ToUpper() == "cfb768b4-80a6-4c9e-91e4-c53e9d3087a5".ToUpper())   //Unwashed Coffee Grading Factor
                {
                    if (dr["GradingFactorID"].ToString().ToUpper() == "F5307987-94A0-462C-A210-EAD62B9F83A2")   // Secondary Defect Distribution grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "BF1EE2B2-DB3B-477F-96D5-8FC076A2B6C8")   // Primary Defect Distribution grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "D33E5941-B2F3-4DA3-AA49-BDAF1287CAB4")   // Odour grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                   
                    if (dr["GradingFactorID"].ToString().ToUpper() == "F56638BB-F148-4D87-95EC-E53BC37968C9")   //Cup Cleanness grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "31F2A2D5-842B-417E-8D65-DBA195E8A059")   //Acidity grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "FCDCCABE-4348-45A8-A351-FF660671F500")   //Body grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "08B5D0A5-9DE1-45F7-BE90-CA19086EB811")   //Flavour grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());

                }

                else if (dr["GradingFactorGroupID"].ToString().ToUpper() == "CB0CA7A0-B581-402B-8756-B1CC83C1C4CF")   //UNWashed Specilaty Coffee Grading Factor
                {
                    if (dr["GradingFactorID"].ToString().ToUpper() == "9B22B658-D723-426E-8FAB-052C779EDC7D")   // Primary Defect
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "5AEDB582-A87C-4BC4-BBB0-A29EE6BCCEAF")   // Secondary Defect grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "BBA5B2CB-F17C-4A5E-A0A2-E3DBD9B9FD5D")   // Odour grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());


                    cupvalue = Convert.ToDecimal(dr["TotalValue"].ToString());
                    ViewState["isSpeciality"] = "true";

                    //if (dr["GradingFactorID"].ToString().ToUpper() == "DF7A10BF-921F-4977-8E35-0DFCE53FA082")   // Cup Cleanness grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                    //if (dr["GradingFactorID"].ToString().ToUpper() == "0E0C46BE-8151-4407-B6DB-6C6556299879")   // Acidity grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                    //if (dr["GradingFactorID"].ToString().ToUpper() == "8601818D-4261-4063-BABE-8822AB522DAB")   // Body grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                    //if (dr["GradingFactorID"].ToString().ToUpper() == "67B3F90A-9921-4005-A4DC-A349DF6A7AE3")   // Flavour grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                }

                else if (dr["GradingFactorGroupID"].ToString().ToUpper() == "3F95040F-52EE-4C09-93AD-7F37C5BCE8C2")   //Washed Specilaty Coffee Grading Factor
                {
                    if (dr["GradingFactorID"].ToString().ToUpper() == "F5307987-94A0-462C-A210-EAD62B9F83A2")   // Primary Defect
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "5AEDB582-A87C-4BC4-BBB0-A29EE6BCCEAF")   // Secondary Defect grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "576854AE-1339-4554-A856-C3CEC7F36172")   // Make&Shape Defect
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "6FF4971E-6044-491A-A008-619B2D438EDB")   // Colour grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "BCC8A72D-D8A8-4CE5-AE7A-A6765CD2384D")   // Odour grading factor
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());



                    cupvalue = Convert.ToDecimal(dr["TotalValue"].ToString());
                    ViewState["isSpeciality"] = "true";

                    //if (dr["GradingFactorID"].ToString().ToUpper() == "55FFB644-BE8A-4113-9CFC-6A27EBEC17E9")   // Cup Cleanness grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                    //if (dr["GradingFactorID"].ToString().ToUpper() == "C7E85755-101A-48AB-B283-6376343452CB")   // Acidity grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                    //if (dr["GradingFactorID"].ToString().ToUpper() == "FCDCCABE-4348-45A8-A351-FF660671F500")   // Body grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                    //if (dr["GradingFactorID"].ToString().ToUpper() == "C1AE5DF7-CD28-4201-A1B6-CC5A840618AA")   // Flavour grading factor
                    //    cupvalue = cupvalue + Convert.ToDouble(dr["ReceivedValue"].ToString());
                }


                else if (dr["GradingFactorGroupID"].ToString().ToUpper() == "73E97E70-65D7-42B6-B9EE-C71832E8E0BB".ToUpper())   // Local Washed Coffee Grading Factor
                {
                    // to do . . .

                    if (dr["GradingFactorID"].ToString().ToUpper() == "EE1C0FC1-1F44-4354-A4BF-62633EA0267D")   // Primary Defect Distribution
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "D69A32BB-A4D1-4CA3-9EB7-A2584A8AC403")   // Secondary Defect Distribution
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "F53CE65F-0652-4BC3-9078-9C7978F710AC")   // Make and Shape
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "6FF4971E-6044-491A-A008-619B2D438EDB")   // Color
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "517260E8-AF05-44F4-8E23-560A9F4C1F0B")   // Odour
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());


                    if (dr["GradingFactorID"].ToString().ToUpper() == "9C57B532-765A-4455-8389-5754075CF1A9")   // Cup Cleaness grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "074DC7EF-2272-47C8-9B57-7FF79D2F6E4A")   // Acidity grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "B0D6B14D-E277-45BA-985F-AE30A641B1A3")   // Body grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "A5FF841A-1F15-41D0-B04B-BF54B5DDE3BD")   // Flavour grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());

                }

                else if (dr["GradingFactorGroupID"].ToString().ToUpper() == "A1562345-5A55-408E-A994-74EB27FECB09".ToUpper())   // Local UnWashed Coffee Grading Factor
                {
                    // to do . . .

                    if (dr["GradingFactorID"].ToString().ToUpper() == "AC0D6B72-E011-4A11-9E23-550C957D33F8")   // Primary Defect Distribution
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "EC2E447A-F591-40AD-B310-4A2ECEC28455")   // Secondary Defect Distribution
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "BCC8A72D-D8A8-4CE5-AE7A-A6765CD2384D")   // Odour
                        rawvalue = rawvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());


                    if (dr["GradingFactorID"].ToString().ToUpper() == "695DD7A6-3A8F-443E-B0AD-2FF918FE6772")   // Cup Cleaness grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "C7E85755-101A-48AB-B283-6376343452CB")   // Acidity grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "CC3E4651-A5FE-4AC5-AD19-4D08E160F206")   // Body grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());
                    if (dr["GradingFactorID"].ToString().ToUpper() == "9E1A6745-A220-420E-90AF-3BF3B2B85021")   // Flavour grading factor
                        cupvalue = cupvalue + Convert.ToDecimal(dr["ReceivedValue"].ToString());

                }
            }

            ViewState["RawValue"] = rawvalue.ToString();
            ViewState["CupValue"] = cupvalue.ToString();

        }

        protected void txtTruckWeight_TextChanged(object sender, EventArgs e)
        {
            GRN_BL objGrn = new GRN_BL();
            double decimalNetWeight;
            int noofbag = 0;
            double tare = 0;
            if (txtNumberOfBags.Text != "" && txtGrossTruckWeight.Text!="")
            {
                double Moistcont = objGrn.getmoisturecontentbytrNo(ViewState["GradingCode"].ToString());
                double moiscontodeduct = 0;
                if (Moistcont > 11.5)
                    moiscontodeduct = (Moistcont - 11.5) * 1.3;

                double grossTruckWeightdec = Convert.ToDouble(txtGrossTruckWeight.Text);
                double truckweightdouble = Convert.ToDouble(txtTruckWeight.Text);
                tare = Convert.ToDouble(hdnTare.Value);
                try
                {
                    noofbag = Convert.ToInt16(txtNumberOfBags.Text);
                    decimalNetWeight = ((grossTruckWeightdec - truckweightdouble) - (noofbag * tare));
                    if (Moistcont > 11.5)
                        decimalNetWeight = decimalNetWeight - ((moiscontodeduct / 100) * decimalNetWeight);
                    lblNetWeight.Text = (Math.Round(decimalNetWeight * Math.Pow(10, 2)) / Math.Pow(10, 2)).ToString();
                    //objGrn.NetWeight = Convert.ToDecimal(Math.Round(decimalNetWeight * Math.Pow(10, 2)) / Math.Pow(10, 2));
                }
                catch (Exception ex)
                {
                }


            }
        }

        protected void txtNumberOfBags_TextChanged(object sender, EventArgs e)
        {
            GRN_BL objGrn = new GRN_BL();
            double decimalNetWeight;
            int noofbag = 0;
            double tare = 0;
            if (txtTruckWeight.Text != "" && txtGrossTruckWeight.Text != "")
            {
                double Moistcont = objGrn.getmoisturecontentbytrNo(ViewState["GradingCode"].ToString());
                double moiscontodeduct = 0;
                if (Moistcont > 11.5)
                    moiscontodeduct = (Moistcont - 11.5) * 1.3;

                double grossTruckWeightdec = Convert.ToDouble(txtGrossTruckWeight.Text);
                double truckweightdouble = Convert.ToDouble(txtTruckWeight.Text);
                tare = Convert.ToDouble(hdnTare.Value);
                try
                {
                    noofbag = Convert.ToInt16(txtNumberOfBags.Text);
                    decimalNetWeight = ((grossTruckWeightdec - truckweightdouble) - (noofbag * tare));
                    if (Moistcont > 11.5)
                        decimalNetWeight = decimalNetWeight - ((moiscontodeduct / 100) * decimalNetWeight);
                    lblNetWeight.Text = (Math.Round(decimalNetWeight * Math.Pow(10, 2)) / Math.Pow(10, 2)).ToString();
                }
                catch (Exception ex)
                {
                }


            }
        }

        protected void ChkBondedYard_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBondedYard.Checked)
            {
                TxtQuadrant.Enabled = true;
                chkChangeShade.Enabled = true;
            }
            else
            {
                TxtQuadrant.Enabled = true;
                chkChangeShade.Enabled = true;
            }
        }

    }
}

