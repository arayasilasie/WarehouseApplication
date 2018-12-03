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
                    lblUnit1.Text = "Kg.";
                    lblUnit2.Text = "Kg.";
                }
                else
                {
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

                hdnTare.Value = dt.Rows[0]["Tare"].ToString();
                ViewState["CommodityGradeId"] = dt.Rows[0]["CommodityGradeId"].ToString();
                ViewState["TrackingNumber"] = dt.Rows[0]["TrackingNumber"].ToString();
                ViewState["GRNID"] = dt.Rows[0]["GRNID"].ToString();

                BagTypeId = new Guid(dt.Rows[0]["BagTypeId"].ToString());
                txtClientAcceptanceDate.Text = DateTime.Parse(dt.Rows[0]["ClientAcceptanceDate"].ToString()).ToString("g");
                hdnClientAcceptanceDate.Value = DateTime.Parse(dt.Rows[0]["ClientAcceptanceDate"].ToString()).ToShortDateString();
                string TrackingNumber = ViewState["TrackingNumber"].ToString();
                PopulateSameGradingGrid(TrackingNumber);
            }
            catch 
            {
                Messages.SetMessage("Failed to load existing information!");
            }
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
            LoadStacksByShadeId(new Guid(Session["ShedId_GRN"].ToString()));

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
                    LoadStacksByShadeId(new Guid(cboShed.SelectedValue.ToString()));
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

                objGrnSetter.TrackingNumber = ViewState["TrackingNumber"].ToString();
                objGrnSetter.DateDeposited = DateTime.Parse(txtDateDeposited.Text + " " + txtTimeDeposited.Text);
                objGrnSetter.UnloadingTicketNumber = txtDepositTicketNumber.Text;
                objGrnSetter.TotalNumberOfBags = int.Parse(txtNumberOfBags.Text);
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
                objGrnSetter.Remark = txtRemark.Text;
                objGrnSetter.ArrivalId = new Guid(Session["ArrivalId_GRN"].ToString());
                objGrnSetter.IsScaleTicketSigned = chkScaleTicketSigned.Checked;
                objGrnSetter.Tolerance = float.Parse(ConfigurationSettings.AppSettings["WeightTolerance"].ToString());
                objGrnSetter.TruckTypeID = int.Parse(cboTruckType.SelectedValue.ToString());
                //objGrnSetter.CommodityGradeId = new Guid(Session["CommodityGradeId_GRN"].ToString());

                objGrnSetter.HalfBagWeight = Math.Round(Convert.ToDecimal((objGrnSetter.NoOfBags * objGrnSetter.Tare) / 2), 4);
                objGrnSetter.NetWeightWithHalfBagWeight = Math.Round(Convert.ToDecimal(objGrnSetter.NetWeight) + objGrnSetter.HalfBagWeight, 4);
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
            cboStackNo.Items.Add(new ListItem("Select Stack No.", ""));
            //dt = objGRN_BL.GetAllAvailableStacks(ShadeId, new Guid("6b5852b4-15a2-41ac-aad6-89f1c2955bc1"), int.Parse(Session["ProductionYear"].ToString()));
            dt = objGRN_BL.GetAllAvailableStacks(ShadeId, new Guid(ViewState["CommodityGradeId"].ToString()), int.Parse(ViewState["ProductionYear"].ToString()));
            cboStackNo.DataSource = dt;
            cboStackNo.DataValueField = "Id";
            cboStackNo.DataTextField = "Number";
            cboStackNo.DataBind();   
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
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GRN_BL objGrnSetter = new BLL.GRN_BL();
            string GradesToBeClosedQuery = "UPDATE tblworkflow SET UpdatedBy = '" + UserBLL.GetCurrentUser() + "', UpdateTimeStamp = '" + DateTime.Now + "' WHERE TrackingNo IN(";
            string closingGradesTrackingNumbers = "";
            int counter=0;
            try
            {
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
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;

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
                    "</script>",
                    false);
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
                    {
                        decimalNetWeight = decimalNetWeight - ((moiscontodeduct / 100) * decimalNetWeight);
                    }
                    lblNetWeight.Text = (Math.Round(decimalNetWeight * Math.Pow(10, 2)) / Math.Pow(10, 2)).ToString();
                    //HalfBagWeght = Math.Round(Convert.ToDecimal((noofbag * tare) / 2), 4);
                    //NetWeightWithHalfbagWeight = Math.Round(Convert.ToDecimal(lblNetWeight.Text) + HalfBagWeght,4);

                    //ViewState["HalfBagWeght"] = Math.Round(Convert.ToDecimal((noofbag * tare) / 2), 4);
                    //ViewState["NetWeightWithHalfbagWeight"] = Math.Round(Convert.ToDecimal(lblNetWeight.Text) + HalfBagWeght, 4);

                    //to bebadded           
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
                    {
                        decimalNetWeight = decimalNetWeight - ((moiscontodeduct / 100) * decimalNetWeight);
                    }
                    lblNetWeight.Text = (Math.Round(decimalNetWeight * Math.Pow(10, 2)) / Math.Pow(10, 2)).ToString();
                    //HalfBagWeght = Math.Round(Convert.ToDecimal( (noofbag * tare) / 2),4);
                    //NetWeightWithHalfbagWeight = Math.Round(Convert.ToDecimal(lblNetWeight.Text) + HalfBagWeght,4);

                    //ViewState["HalfBagWeght"] = Math.Round(Convert.ToDecimal((noofbag * tare) / 2), 4);
                    //ViewState["NetWeightWithHalfbagWeight"] = Math.Round(Convert.ToDecimal(lblNetWeight.Text) + HalfBagWeght, 4);

           
                }
                catch (Exception ex)
                {
                }


            }
        }

    }
}

