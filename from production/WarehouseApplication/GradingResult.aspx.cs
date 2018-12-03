using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GradingBussiness;
using System.Configuration;
using System.Data;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class GradingResult : BasePage
    {
        Guid GradingCodeID;
        Guid? illegalCommodityTypeID
        {
            get
            {
                if (ViewState["illegalCommodityTypeID"] != null)
                    return new Guid(ViewState["illegalCommodityTypeID"].ToString());
                else
                    return null;
            }
        }
        Guid? CommodityTypeID
        {
            get
            {
                if (ViewState[GradingConstants.CommodityTypeID.ToString()] != null)
                    return new Guid(ViewState[GradingConstants.CommodityTypeID.ToString()].ToString());
                else
                    return null;
            }
        }
        Guid? CommodityID
        {
            get
            {
                if (ViewState[GradingConstants.CommodityId.ToString()] != null)
                    return new Guid(ViewState[GradingConstants.CommodityId.ToString()].ToString());
                else
                    return null;
            }
        }
        Guid? WeradaID
        {
            get
            {
                if (ViewState[GradingConstants.WeradaId.ToString()] != null)
                    return new Guid(ViewState[GradingConstants.WeradaId.ToString()].ToString());
                else
                    return null;
            }
        }
        bool? IsNonCoffee
        {
            get
            {
                if (ViewState[GradingConstants.IsNonCoffee.ToString()] != null)
                    return bool.Parse(ViewState[GradingConstants.IsNonCoffee.ToString()].ToString());
                else
                    return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FillPyear();
                string GradingCode = "";
                bool EditMode = false;
                ViewState["illegalCommodityTypeID"] = ConfigurationManager.AppSettings["illegalCoffee"].ToUpper().Trim();

                if (Request.QueryString["GradingCode"] == null)
                {
                    Messages1.SetMessage("Please log out and try again!", Messages.MessageType.Error);
                    return;
                }
                if (Request.QueryString["EditMode"] == null)
                {
                    Messages1.SetMessage("Please log out and try again!", Messages.MessageType.Error);
                    return;
                }
                else
                {
                    ViewState["EditMode"] = Request.QueryString["EditMode"];
                }

                GradingCode = Request.QueryString["GradingCode"];
                if (string.IsNullOrEmpty(GradingCode))
                {
                    Messages1.SetMessage("Please log out and try again!", Messages.MessageType.Error);
                    return;
                }

                if (ViewState["EditMode"] != null && bool.TryParse(ViewState["EditMode"].ToString(), out EditMode) && EditMode)
                {
                    PopulateGradeResult(GradingCode);
                }
                else
                {
                    chkChangeClass.Visible = false;
                    if (FillInfo(GradingCode))
                    {
                        //FillCommodityClass(drpCommodityClass, CommodityID , WeradaID, null);
                        FillCommodityClass(drpCommodityClass, CommodityID, null, null);
                        //assuming if woreda is not known the woredaID field in arrival will be null or empty guid
                        if (WeradaID != null && Guid.Empty != WeradaID && CommodityTypeID != illegalCommodityTypeID && CommodityID != new Guid("37d28859-5579-436b-98c8-2bf28bd413be"))
                        {
                            FillCommodityFactorGroup(drpCommodityFactorGroup, null, CommodityID.Value, WeradaID);
                        }
                        else
                        {
                            if (CommodityID == new Guid("37d28859-5579-436b-98c8-2bf28bd413be"))
                            {
                                FillCommodityFactorGroup(drpCommodityFactorGroup, Guid.Empty, CommodityID.Value, null);
                            }
                            else
                            {
                                FillCommodityFactorGroup(drpCommodityFactorGroup, Guid.Empty, CommodityID.Value, WeradaID);
                            }


                            lblComodityClass.Visible = drpCommodityClass.Visible = true;
                            lblComodityClass.Enabled = drpCommodityClass.Enabled = true;
                        }
                    }
                }
                string s = "";
                s = txtCommodity.Text.ToString();
                if (!(txtCommodity.Text.ToString() == "Coffee"))
                {
                    RdBtndeposit.Enabled = false;
                    RdBtnexport.Enabled = false;
                }
            }
            unBlockUI();
        }

        void unBlockUI()
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(),
                           "unBlockUI", "<script type='text/javascript'>$.unblockUI();</script>", false);
        }

        private void PopulateGradeResult(string gradeResultCode)
        {
            GradingModel gradingResult = GradingModel.GetGradingInformationByGradingCode(gradeResultCode);
            ViewState.Add("EditMode", true);
            ViewState["GradingResultID"] = gradingResult.ID;
            lblGradingCodeValue.Text = gradingResult.GradingCode;
            lblCodeGeneratedDateValue.Text = gradingResult.DateTimeCoded.ToShortDateString();
            lblCodeGeneratedTimeValue.Text = gradingResult.DateTimeCoded.ToShortTimeString();
            lblGraderCupperValue.Text = BLL.UserBLL.GetName(gradingResult.UserId);
            txtCommodity.Text = gradingResult.CommodityName;
            if (gradingResult.CommodityID == null)
            {
                Messages1.SetMessage("Please specify the commodity in Arrival form to continue!");
                return;
            }
            ViewState.Add(GradingConstants.CommodityId.ToString(), gradingResult.CommodityID.ToString());
            ViewState.Add(GradingConstants.IsNonCoffee.ToString(),
                !ConfigurationManager.AppSettings["CoffeeId"].ToUpper().Trim().Equals(gradingResult.CommodityID.ToString().ToUpper().Trim()));
            ViewState.Add(GradingConstants.WeradaId.ToString(), gradingResult.WoredaID.ToString());
            ViewState.Add(GradingConstants.GradingID.ToString(), gradingResult.ID);
            ViewState.Add(GradingConstants.CommodityTypeID.ToString(), gradingResult.VoucherCommodityTypeID);
            txtDateRecived.Text = gradingResult.GradeRecivedDateTime.ToShortDateString();
            txtTimeRecived.Text = gradingResult.GradeRecivedDateTime.ToShortTimeString();

            if (gradingResult.ClientAcceptanceTimeStamp >= gradingResult.GradeRecivedDateTime)
                ViewState.Add("ClientAcceptanceTimeStamp", gradingResult.ClientAcceptanceTimeStamp);

            if (gradingResult.ProductionYear > 0)
                drpProductionYear.SelectedValue = gradingResult.ProductionYear.ToString();

            //FillCommodityClass(drpCommodityClass, CommodityID, WeradaID, null);
            FillCommodityClass(drpCommodityClass, CommodityID, null, null);
            //assuming if woreda is not known the woredaID field in arrival will be null or empty guid
            if (WeradaID != null && Guid.Empty != WeradaID && CommodityTypeID != illegalCommodityTypeID)
            {
                FillCommodityFactorGroup(drpCommodityFactorGroup, null, CommodityID.Value, WeradaID);
                drpCommodityFactorGroup.SelectedValue = gradingResult.GradingFactorGroupID.ToString();
                //drpCommodityFactorGroup_SelectedIndexChanged("ForUpdate", null);
                onCommodityFactorGroupChange(true, gradingResult.CommodityClassID.ToString());
                drpCommodityClass.SelectedValue = gradingResult.CommodityClassID.ToString();
            }
            else
            {
                lblComodityClass.Visible = drpCommodityClass.Visible = true;
                lblComodityClass.Enabled = drpCommodityClass.Enabled = true;
                drpCommodityClass.SelectedValue = gradingResult.CommodityClassID.ToString();
                drpCommodityClass_SelectedIndexChanged("ForUpdate", null);
                drpCommodityFactorGroup.SelectedValue = gradingResult.GradingFactorGroupID.ToString();
                drpCommodityFactorGroup_SelectedIndexChanged(null, null);
            }
            if (gradingResult.GradeRecived != null)
                drpGrade.SelectedValue = gradingResult.GradeRecived.ToString();

            if (gradingResult.GRNCreated || gradingResult.NewSampleCodeGenerated ||
                (gradingResult.GradingResultStatusID == (int)GradingResultStatus.SegrigationRequested && gradingResult.GradingsStatusID > 0))
            {
                drpCommodityClass.Enabled = drpCommodityFactorGroup.Enabled = drpGrade.Enabled = false;
                chkChangeClass.Enabled = false;
                rfvdrpGrade.Enabled = false;
                gvGradingFactors1.Enabled = false;
                txtDateRecived.Enabled = txtTimeRecived.Enabled = false;
                drpProductionYear.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                string toolTip = "GRN processed grade result can't be edited!";
                btnSave.ToolTip = drpCommodityClass.ToolTip = drpCommodityFactorGroup.ToolTip = drpGrade.ToolTip =
                    gvGradingFactors1.ToolTip = toolTip;
            }
        }

        private bool FillInfo(string GradingCode)
        {
            List<GradingModel> listCode = GradingModel.GetCodeInformationByCode(GradingCode);

            if (listCode[0].GradingResultStatusID > 0)
            {
                PopulateGradeResult(GradingCode);
                return false;
            }

            ViewState["GradingResultID"] = listCode[0].ID;
            lblGradingCodeValue.Text = listCode[0].GradingCode;
            lblCodeGeneratedDateValue.Text = listCode[0].DateTimeCoded.ToShortDateString();
            lblCodeGeneratedTimeValue.Text = listCode[0].DateTimeCoded.ToShortTimeString();
            lblGraderCupperValue.Text = BLL.UserBLL.GetName(listCode[0].UserId);
            txtCommodity.Text = listCode[0].CommodityName;
            if (listCode[0].CommodityID == null)
            {
                Messages1.SetMessage("Please specify the commodity in Arrival form to continue!");
                return true;
            }
            ViewState.Add(GradingConstants.CommodityId.ToString(), listCode[0].CommodityID.ToString());
            ViewState.Add(GradingConstants.IsNonCoffee.ToString(),
                !ConfigurationManager.AppSettings["CoffeeId"].ToUpper().Trim().Equals(listCode[0].CommodityID.ToString().ToUpper().Trim()));
            ViewState.Add(GradingConstants.WeradaId.ToString(), listCode[0].WoredaID.ToString());
            ViewState.Add(GradingConstants.GradingID.ToString(), GradingCodeID);
            ViewState.Add(GradingConstants.CommodityTypeID.ToString(), listCode[0].VoucherCommodityTypeID);
            txtDateRecived.Text = DateTime.Now.ToShortDateString();
            txtTimeRecived.Text = DateTime.Now.ToShortTimeString();

            return true;
        }

        private void FillPyear()
        {
            int CurrentEthiopianYear = int.Parse(ConfigurationManager.AppSettings["CurrentEthiopianYear"]);
            drpProductionYear.Items.Add(new ListItem("Please Select Production Year.", ""));
            for (int i = CurrentEthiopianYear - 7; i <= CurrentEthiopianYear; i++)
                this.drpProductionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        private void FillCommodityClass(DropDownList ddl, Guid? CommodityId, Guid? WeradaId, Guid? FactorGroupId)
        {
            if (Guid.Empty == WeradaId) WeradaId = null;
            ddl.DataSource = null;
            ddl.Items.Clear();
            ddl.Items.Add("");
            ddl.DataSource = GradingModel.GetCommodityClass(CommodityId, WeradaId, FactorGroupId);
            ddl.DataTextField = "Class";
            ddl.DataValueField = "ClassID";
            ddl.DataBind();
        }

        private void FillCommodityFactorGroup(DropDownList ddl, Guid? commodityClassId, Guid CommodityId, Guid? weradaId)
        {
            ddl.DataSource = null;
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("- - Please Select - -", ""));
            DataTable dt = GradingModel.GetCommodityFactorGroup(commodityClassId, CommodityId, weradaId);
            ddl.DataSource = dt;
            ddl.DataTextField = "GradingFactorGroupName";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            Dictionary<string, int> factorGroups = new Dictionary<string, int>();
            foreach (DataRow dr in dt.Rows)
            {
                factorGroups.Add(dr["ID"].ToString(), int.Parse(dr["GradingResultStatusID"].ToString()));
            }
            ViewState.Add("FGs", factorGroups);
        }

        private void FillCommodityGrade(DropDownList ddl, Guid commodityFactorGroupId, Guid classId)
        {
            ddl.DataSource = null;
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("- - Please Select - -", ""));
            DataTable dt = GradingModel.GetCommodityGrade(commodityFactorGroupId, classId);
            ViewState.Add("GradeList", dt);
            ddl.DataSource = dt;
            ddl.DataTextField = "Grade";
            ddl.DataValueField = "Grade";
            ddl.DataBind();
        }

        private void FillCommodityFactor(Guid commodityFactorGroupId)
        {
            string isdeposit="";
            if (RdBtndeposit.Checked == true)
                isdeposit = "deposit";
            else if (RdBtnexport.Checked == true)
                isdeposit = "export";
            Guid? gradingsID = null; bool EditMode = false;
            if (ViewState["EditMode"] != null && bool.TryParse(ViewState["EditMode"].ToString(), out EditMode) && EditMode)
                gradingsID = new Guid(ViewState[GradingConstants.GradingID.ToString()].ToString());
            gvGradingFactors1.DataSource = null;
            gvGradingFactors1.DataSource = GradingModel.GetCommodityFactor(commodityFactorGroupId, gradingsID, isdeposit);
            gvGradingFactors1.DataBind();
        }

        private GradingModel SetGrading()
        {
            Boolean Isnormaldeposit = true;
            if (RdBtndeposit.Checked != true)
                Isnormaldeposit = false;
            int gradingResultStatus = ((Dictionary<string, int>)ViewState["FGs"])[drpCommodityFactorGroup.SelectedValue];
            //eg. General Requirement Fail Sesame status = 3

            GradingModel gm = new GradingModel();
            gm.ID = new Guid(ViewState["GradingResultID"].ToString()); // new Guid(ViewState[GradingConstants.GradingID.ToString()].ToString());
            Guid gFGID = new Guid(drpCommodityFactorGroup.SelectedValue);
            gm.GradingResultStatusID = gradingResultStatus;
            gm.GradingFactorGroupID = gFGID;
            if ((int)GradingResultStatus.AcceptableResult == gradingResultStatus)//for moisture fail
            {
                gm.GradingFactorGroupID = gFGID;
                gm.GradingResultStatusID = (int)GradingResultStatus.AcceptableResult;
                gm.TotalValue = 0;
                gm.GradeRecived = drpGrade.SelectedItem.Text;
                gm.ProductionYear = int.Parse(drpProductionYear.Text);
                gm.CommodityClassID = new Guid(drpCommodityClass.SelectedValue);
                gm.Isnormaldeposit = Isnormaldeposit;
            }
            gm.NumberofSeparations = 1;/*By default the separation(segrigation) number is one*/
            gm.GradeRecivedDateTime = DateTime.Parse(txtDateRecived.Text + " " + txtTimeRecived.Text);
            gm.ResultCreatedBy = BLL.UserBLL.CurrentUser.UserId;
            gm.LastModifiedBy = BLL.UserBLL.CurrentUser.UserId;

            GradingDetail gd;
            decimal gradingValue = 0;

            //bool ValueForsemi_wased =false;
            //bool ValueForUnderScreen =false;
            int? classificationNo = 1;
            ViewState["ClassificatonMsg"] = null;
            foreach (GridViewRow gr in gvGradingFactors1.Rows)
            {
                GradingResultControlNew grc = ((GradingResultControlNew)gr.FindControl("ResultInputControl"));
                if (!grc.validate())
                {
                    Messages1.SetMessage("The grading factor '" + ((Label)gr.FindControl("lblGradingFactorName")).Text + "' has invalid Value! " +
                                         grc.errorMessage,
                                        Messages.MessageType.Error);
                    return null;
                }
                gd = new GradingDetail(gm);
                gd.ID = Guid.NewGuid();
                gd.GradingsID = gm.ID;
                gd.GradingFactorID = new Guid(((Label)gr.FindControl("lblId")).Text);
                gd.ReceivedValue = grc.Value;
                gd.Status = 1;
                gm.addGradingDetail(gd);
                TextBox isInTotal = ((TextBox)gr.FindControl("txtIsInTotalValue"));

                gradingValue = 0;
                bool canAddToTotal = false;
                if (decimal.TryParse(grc.Value.ToString(), out gradingValue)/*we need the value after the loop even if not added to total value*/
                    && bool.TryParse(isInTotal.Text, out canAddToTotal) && canAddToTotal)
                {
                    gm.TotalValue += gradingValue;
                }

                if (!gd.IsValid())
                {
                    Messages1.SetMessage("The grading factor" + ((Label)gr.FindControl("lblGradingFactorName")).Text + " " + gd.ErrorMessage,
                                        Messages.MessageType.Error);
                    return null;
                }
                /*Check for semi-washed, under screen and Moisture Content factor values*/
                decimal limit = -1;
                string value = ((Label)gr.FindControl("lblIsSemiWashedFactor")).Text;
                if (bool.Parse(value) && grc.Value.ToString().ToLower() == "yes")//for semi-washed case
                {
                    classificationNo *= 3;
                    if (ViewState["ClassificatonMsg"] != null)
                        ViewState.Add("ClassificatonMsg", ViewState["ClassificatonMsg"].ToString() + ", Semi-washed");
                    else
                        ViewState.Add("ClassificatonMsg", "Semi-washed");
                }
                value = ((Label)gr.FindControl("lblIsUnderScreenFactor")).Text;
                if (bool.Parse(value) && decimal.TryParse(((Label)gr.FindControl("lblUnderScreenLimit")).Text, out limit)
                    && limit > 0 && gradingValue < limit)//for under screen case
                {
                    classificationNo *= 5;
                    if (ViewState["ClassificatonMsg"] != null)
                        ViewState.Add("ClassificatonMsg", ViewState["ClassificatonMsg"].ToString() + ", Under Screen");
                    else
                        ViewState.Add("ClassificatonMsg", "Under Screen");
                }
                value = ((Label)gr.FindControl("lblIsType")).Text;
                if (bool.Parse(value) && decimal.TryParse(grc.Value, out limit))//for non coffee case
                {
                    classificationNo *= int.Parse(grc.Value);
                    if (ViewState["ClassificatonMsg"] != null)
                        ViewState.Add("ClassificatonMsg", ViewState["ClassificatonMsg"].ToString() + ", " + grc.Text);
                    else
                        ViewState.Add("ClassificatonMsg", grc.Text);
                }
                //Moisture Content factor values
                value = ((Label)gr.FindControl("lblIsMoistureContentFactor")).Text;
                //string expvalue = "";
                //expvalue = ((Label)gr.FindControl("MoistureContentLimitexp")).Text;
                limit = -1;
                if (bool.Parse(value) && decimal.TryParse(((Label)gr.FindControl("lblMoistureContentLimit")).Text, out limit)
                    && limit > 0)//for moisture content case
                {
                    string MFFactorGroup = ((Label)gr.FindControl("lblMoistureFailFactorGroup")).Text;
                    if (gradingValue > limit && !gm.GradingFactorGroupID.ToString().Equals(MFFactorGroup.Trim()))
                    {
                        Messages1.SetMessage("Value entered for moisture content is above range " + limit.ToString() +
                            "! The grading must be considered with factor group 'Moisture Fail'!",
                                            Messages.MessageType.Error);
                        return null;
                    }
                    else if (gradingValue <= limit && gm.GradingFactorGroupID.ToString().Equals(MFFactorGroup.Trim()))
                    {
                        Messages1.SetMessage("The grading factor moisture content is set with in the limit " + limit.ToString() +
                            "! The grading MUST NOT be considered with factor group 'Moisture Fail'!",
                                            Messages.MessageType.Error);
                        return null;
                    }
                }
            }
            //gm.IsSemiWashed = ValueForsemi_wased;
            //gm.IsUnderScreen = ValueForUnderScreen;
            if (classificationNo > 1)
                gm.ClassificationNo = classificationNo;
            /*we expect only one factor for segrigation request 
             * i.e. number of segrigations the grading value will take this value from above*/
            if ((int)GradingResultStatus.SegrigationRequested == gradingResultStatus)
                gm.NumberofSeparations = Convert.ToInt32(gradingValue);

            if (!gm.IsValidForGradingResult())
            {
                Messages1.SetMessage(gm.ErrorMessage, Messages.MessageType.Error);
                return null;
            }

            return gm;
        }

        [System.Web.Services.WebMethod]
        public static object GetMaxMinOfGrade(string grade, string commodityFactorGroupId, string classId)
        {
            try
            {
                DataTable dt = GradingModel.GetCommodityGrade(new Guid(commodityFactorGroupId), new Guid(classId));
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select(" Grade = '" + grade + "'");
                    if (dr.Count() > 0)
                    {

                        dr[0]["MinimumTotalValue"].ToString();
                        return new
                        {
                            Max = dr[0]["MaximumTotalValue"].ToString(),
                            Min = dr[0]["MinimumTotalValue"].ToString()
                        };
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch { return null; }
        }

        private bool ValidateGradeValue(GradingModel gm)
        {
            //if the result status is not accept able the below validation is not wanted 
            if (gm.GradingResultStatusID != (int)GradingResultStatus.AcceptableResult) return true;
            DataTable dt = (DataTable)ViewState["GradeList"];
            DataRow[] dr = dt.Select(" Grade = '" + drpGrade.SelectedValue + "'");
            int max = 0, min = 0;
            if (!int.TryParse(dr[0]["MaximumTotalValue"].ToString(), out max) ||
            !int.TryParse(dr[0]["MinimumTotalValue"].ToString(), out min))
            {
                Messages1.SetMessage("Please first specify the min and max values of the grade '" + drpGrade.SelectedItem.Text + "' in the grading setting!", Messages.MessageType.Warning);
                return false;
            }
            if (gm.TotalValue >= max || gm.TotalValue < min)
            {
                Messages1.SetMessage("Total value for grade factor  (" + gm.TotalValue.ToString()
                    + ") is outside of range max(" + max.ToString() + ") and min(" + min.ToString()
                    + ")!. For max it is exclusive.", Messages.MessageType.Error);
                return false;
            }
            return true;
        }

        private bool ControlsValidated()
        {
            int gradingResultStatus = 0;
            if (drpCommodityFactorGroup.SelectedIndex > 0)
                gradingResultStatus = ((Dictionary<string, int>)ViewState["FGs"])[drpCommodityFactorGroup.SelectedValue];

            bool woradaUnknown = (WeradaID == null || Guid.Empty == WeradaID || CommodityTypeID == illegalCommodityTypeID);

            string errorMessage = "";

            if (drpCommodityFactorGroup.SelectedIndex <= 0)
                errorMessage = " Please select grading result! ";
            else if ((int)GradingResultStatus.AcceptableResult == gradingResultStatus)
            {
                if (woradaUnknown && drpCommodityClass.SelectedIndex <= 0)
                    errorMessage = " Please select commodity class! ";
                if (drpGrade.SelectedIndex <= 0) errorMessage = "Please select grading value!";
                //if (drpProductionYear.SelectedIndex <= 0) errorMessage = "You must select production year!";
            }

            if (ViewState["ClientAcceptanceTimeStamp"] != null &&
                       DateTime.Parse(ViewState["ClientAcceptanceTimeStamp"].ToString()) <
                                  DateTime.Parse(txtDateRecived.Text + " " + txtTimeRecived.Text))
            {
                errorMessage = " Please select date time less than Client Acceptance Time " +
                    ViewState["ClientAcceptanceTimeStamp"].ToString() + "! ";
            }

            if (errorMessage.Trim() != string.Empty)
            {
                Messages1.SetMessage(errorMessage, Messages.MessageType.Error);
                return false;
            }
            else
                return true;
        }

        private void Clear(bool afterSave)
        {
            if (afterSave)
            {
                lblGradingCodeValue.Text = string.Empty;
                lblGraderCupperValue.Text = string.Empty;
                txtCommodity.Text = string.Empty;
                drpCommodityClass.ClearSelection();
                drpProductionYear.ClearSelection();
                drpCommodityClass.DataSource = new DataTable();
                drpCommodityClass.DataBind();
                drpCommodityClass.Items.Clear();
                drpCommodityFactorGroup.DataSource = new DataTable();
                drpCommodityFactorGroup.DataBind();
                drpCommodityFactorGroup.Items.Clear();
                drpGrade.DataSource = new DataTable();
                drpGrade.DataBind();
                drpGrade.Items.Clear();
                ViewState.Clear();
            }
            drpCommodityFactorGroup.ClearSelection();
            drpGrade.ClearSelection();
            txtDateRecived.Text = DateTime.Now.ToShortDateString();
            txtTimeRecived.Text = DateTime.Now.ToShortTimeString();
            //drpProductionYear.ClearSelection();
            gvGradingFactors1.DataSource = new DataTable();
            gvGradingFactors1.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear(false);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            if (!ControlsValidated()) return;
            GradingModel gm = SetGrading();
            if (gm != null && ValidateGradeValue(gm))
            {
                try
                {
                    gm.SaveGradingResult();
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToUpper().Contains("GRADING ERROR".ToUpper()))
                    {
                        string msg = ex.Message.Split('|').First(s => s.ToUpper().Contains("GRADING ERROR".ToUpper()));
                        Messages1.SetMessage(msg, Messages.MessageType.Error);
                        // Messages1.SetMessage("GRADING ERROR. Error with the specified factor values does not exits!", Messages.MessageType.Error);
                        return;
                    }
                    else
                        throw;
                }
                string gradeInfo = "";
                //if (gm.IsSemiWashed && gm.IsUnderScreen)
                //{
                //    gradeInfo = " Since the Semi-washed is set to Yes and the Screen Size fields is set below 80 the grade will be " + 
                //        " underscreen semiwashed!";
                //}
                //else if (!gm.IsSemiWashed && gm.IsUnderScreen)
                //{ 
                //    gradeInfo = " Since the Screen Size fields is set below 80 the grade will be " +
                //        " underscreen!";
                //}
                //else if (gm.IsSemiWashed && !gm.IsUnderScreen)
                //{
                //    gradeInfo = " Since the Semi-washed is set to Yes the grade will be " +
                //        " semiwashed!";
                //}
                if (ViewState["ClassificatonMsg"] != null && ViewState["ClassificatonMsg"].ToString() != string.Empty)
                    gradeInfo = " From the factors specified the grade will be " + ViewState["ClassificatonMsg"].ToString() + ".";
                Messages1.SetMessage("Grading was succesfully saved! " + gradeInfo, Messages.MessageType.Success);
                // Clear(true);
                chkChangeClass.Visible = true;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void drpCommodityFactorGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            onCommodityFactorGroupChange(null, null);
        }

        private void onCommodityFactorGroupChange(bool? ForUpdate, string GclassValue)
        {
            Messages1.ClearMessage();
            gvGradingFactors1.DataSource = new DataTable();
            gvGradingFactors1.DataBind();
            drpGrade.Items.Clear();
            drpGrade.DataSource = new DataTable();
            drpGrade.DataBind();
            if (drpCommodityFactorGroup.SelectedIndex > 0)
            {
                bool woradaknown = !(WeradaID == null || Guid.Empty == WeradaID || CommodityTypeID == illegalCommodityTypeID);

                int gradingResultStatus = ((Dictionary<string, int>)ViewState["FGs"])[drpCommodityFactorGroup.SelectedValue];

                if ((int)GradingResultStatus.AcceptableResult != gradingResultStatus)
                {
                    drpGrade.Enabled = false;
                    rfvdrpGrade.Enabled = false;
                    drpProductionYear.Enabled = false;
                    rfvdrpProductionYear.Enabled = false;
                    drpCommodityClass.SelectedIndex = -1;
                    rfvdrpCommodityClass.Enabled = false;
                    //drpCommodityClass.Enabled = false;
                }
                else
                {
                    drpGrade.Enabled = true;
                    rfvdrpGrade.Enabled = true;
                    rfvdrpProductionYear.Enabled = true;
                    drpProductionYear.Enabled = true;
                    if (CommodityID == new Guid("37d28859-5579-436b-98c8-2bf28bd413be"))
                    {
                        woradaknown = false;
                    }
                    if (woradaknown)
                    {
                        //FillCommodityClass(drpCommodityClass, null,
                        //     WeradaID,
                        //     new Guid(drpCommodityFactorGroup.SelectedValue));
                        //if (((DataTable)drpCommodityClass.DataSource).Rows.Count > 0)
                        //    drpCommodityClass.SelectedIndex = 1;
                        if (!chkChangeClass.Checked)
                        {

                            //if (IsNonCoffee != null && IsNonCoffee.Value)
                            DataTable dt = GradingModel.GetCommodityClass(null, WeradaID, new Guid(drpCommodityFactorGroup.SelectedValue));
                            if (GclassValue == null || dt.Select("ClassID ='" + GclassValue + "'").Count() > 0)
                                FillCommodityClass(drpCommodityClass, null, WeradaID, new Guid(drpCommodityFactorGroup.SelectedValue));
                            if (dt.Rows.Count > 0)
                            {
                                //if (drpCommodityClass.Items.Count == 2)// || (IsNonCoffee == null || !IsNonCoffee.Value))//
                                if (dt.Rows.Count == 1)// || (IsNonCoffee == null || !IsNonCoffee.Value))
                                {
                                    if (GclassValue == null)
                                    {
                                        //DataTable dt = GradingModel.GetCommodityClass(null, WeradaID, new Guid(drpCommodityFactorGroup.SelectedValue));
                                        drpCommodityClass.SelectedValue = dt.Rows[0]["ClassID"].ToString();
                                    }
                                    else
                                        drpCommodityClass.SelectedValue = GclassValue;
                                    drpCommodityClass.Enabled = false;
                                    rfvdrpCommodityClass.Enabled = false;
                                }
                                else
                                {
                                    if (ForUpdate == null || !ForUpdate.Value)
                                        drpCommodityClass.SelectedIndex = 0;
                                    else if (GclassValue != null)
                                        drpCommodityClass.SelectedValue = GclassValue;
                                    drpCommodityClass.Enabled = true;
                                    rfvdrpCommodityClass.Enabled = true;
                                }
                            }
                            else
                            {
                                Messages1.SetMessage("Class with the factor group and woreda was not found");
                            }
                        }
                    }
                    if (drpCommodityClass.SelectedIndex > 0)
                        FillCommodityGrade(drpGrade, new Guid(drpCommodityFactorGroup.SelectedValue),
                         new Guid(drpCommodityClass.SelectedValue));
                }
                FillCommodityFactor(new Guid(drpCommodityFactorGroup.SelectedValue));
            }
        }

        protected void drpCommodityClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Messages1.ClearMessage();
            bool woradaknown = !(WeradaID == null || Guid.Empty == WeradaID || CommodityTypeID == illegalCommodityTypeID);
            if (drpCommodityClass.SelectedIndex > 0)
            {
                if (drpCommodityClass.Items.Count == 2 || chkChangeClass.Checked || !woradaknown//(IsNonCoffee == null || !IsNonCoffee.Value)
                    || (sender != null && sender.ToString() == "ForUpdate"))
                {
                    gvGradingFactors1.DataSource = new DataTable();
                    gvGradingFactors1.DataBind();
                    drpGrade.Items.Clear();
                    drpGrade.DataSource = new DataTable();
                    drpGrade.DataBind();
                    FillCommodityFactorGroup(drpCommodityFactorGroup, new Guid(drpCommodityClass.SelectedValue), CommodityID.Value, null);
                }
                else if ((sender == null || sender.ToString() != "ForUpdate") && woradaknown)
                {
                    FillCommodityGrade(drpGrade, new Guid(drpCommodityFactorGroup.SelectedValue),
                         new Guid(drpCommodityClass.SelectedValue));
                    rfvdrpCommodityClass.Enabled = true;
                }
            }
            else
            {
                if (drpCommodityClass.Items.Count == 2)// || (IsNonCoffee == null || !IsNonCoffee.Value))
                {
                    drpCommodityFactorGroup.Items.Clear();
                    FillCommodityFactorGroup(drpCommodityFactorGroup, Guid.Empty, CommodityID.Value, null);
                    drpCommodityFactorGroup_SelectedIndexChanged(null, null);
                }
                else
                {
                    drpGrade.Items.Clear();
                    drpGrade.DataSource = new DataTable();
                    drpGrade.DataBind();
                    rfvdrpCommodityClass.Enabled = true;
                }
            }
        }

        protected void gvGradingFactors1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GradingResultControlNew grc = ((GradingResultControlNew)e.Row.FindControl("ResultInputControl"));
            Label type = ((Label)e.Row.FindControl("lblType"));
            Label possibleValues = ((Label)e.Row.FindControl("lblPossibleValues"));
            Label result = ((Label)e.Row.FindControl("lblResult"));

            if (grc != null && type != null)
                grc.setType(type.Text, possibleValues.Text, result.Text);

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            bool EditMode = false;
            if (ViewState["EditMode"] != null && bool.TryParse(ViewState["EditMode"].ToString(), out EditMode) && EditMode)
            {
                Response.Redirect(@"~\SearchPage.aspx");
            }
            else
            {
                Response.Redirect(@"~\ListInboxNew.aspx");
            }
        }

        protected void chkChangeClass_CheckedChanged(object sender, EventArgs e)
        {
            if (!(WeradaID == null || Guid.Empty == WeradaID || CommodityTypeID == illegalCommodityTypeID))
                drpCommodityClass.Enabled = chkChangeClass.Checked;
            if (chkChangeClass.Checked)
            {
                FillCommodityClass(drpCommodityClass, CommodityID, null, null);
                rfvdrpCommodityClass.Enabled = true;
            }
            else
            {
                PopulateGradeResult(lblGradingCodeValue.Text);
                rfvdrpCommodityClass.Enabled = false;
            }
        }
    }
}