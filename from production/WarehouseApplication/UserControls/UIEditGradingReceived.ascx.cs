using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditGradingReceived : System.Web.UI.UserControl , ISecurityConfiguration
    {
        private bool isGradeChanged = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack != true)
            {
                LoadData();
              
               
            }

        }     
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Guid oldGradeId = new Guid(ViewState["OldCommodityGrade"].ToString());
            Guid SelectedGuid = Guid.Empty;
            string strTempCommGrade = this.cboCommodityGrade_CascadingDropDown.SelectedValue.ToString();
            string[] arrTemp = strTempCommGrade.Split(':');
            SelectedGuid = new Guid(arrTemp[0].ToString());
            int oldProdYear = -2;
            int curProdYear = -2;
            if(ViewState["vsProdYear"] != null)
            {
                oldProdYear = int.Parse(ViewState["vsProdYear"].ToString());
            }
            curProdYear = int.Parse( this.cboProductionYear.SelectedValue.ToString());

            if ((oldGradeId != SelectedGuid) || (curProdYear != oldProdYear))
            {
                try
                {
                    
                    AddNewGradingFactorValues();
                }
                catch 
                {
                    this.lblMsg.Text = "Unable to Complete this your Request.";
                    
                }
            }
            else
            {
                this.lblMsg.Text = "No Grade Change.";
            }
            
        }
        protected void gvGradingFactors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGradingFactors.EditIndex = e.NewEditIndex;
            GridDataBind();
            //update 



        }
        private void GridDataBind()
        {

            Guid Id = new Guid(this.hfId.Value.ToString());
            GradingResultDetailBLL objGRD = new GradingResultDetailBLL();
            List<GradingResultDetailBLL> list = new List<GradingResultDetailBLL>();
            list = objGRD.GetGradingResultDetailByGradingResultId(Id);

            this.gvGradingFactors.DataSource = list;
            this.gvGradingFactors.DataBind();
        }
        protected void gvGradingFactors_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGradingFactors.EditIndex = -1;
            GridDataBind();
        }
        protected void gvGradingFactors_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
        }
        protected void gvGradingFactors_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Guid Id = new Guid(this.hfId.Value.ToString());
            }
            catch 
            {
                this.lblmsg2.Text = "Unable to Update data,please check the data enterd and try again.If the error persists, contact IT Support";
            }
            //check if the GR is accpted by the Client if true then - Ignore changes.

            GridViewRow row = gvGradingFactors.Rows[e.RowIndex];
            if (row != null)
            {

                Label lblmessage;
                lblmessage = row.FindControl("lblEmpty") as Label;
                if (lblmessage != null)
                {
                    lblmessage.Text = "";
                    lblmessage.Visible = true;
                    lblmessage.ForeColor = System.Drawing.Color.Red;

                }
                Nullable<Guid> Id = null;
                Label lblId = row.FindControl("lblId") as Label;
                if (lblId != null)
                {
                    try
                    {
                        Id = new Guid(lblId.Text);
                    }
                    catch 
                    {
                        this.lblmsg2.Text = "An Error please try again.If the error persists conatact the administrator.";
                        return;
                    }

                }
                //get value Type
                string DataType = String.Empty;
                Label lblDataType = row.FindControl("lblDataType") as Label;
                if (lblDataType != null)
                {
                    DataType = lblDataType.Text;
                }

                // get Possible Values.
                string PossibleValues=String.Empty;
                Label lblPossibleValues = row.FindControl("lblPossibleTypes") as Label;
                if (lblPossibleValues != null)
                {
                    PossibleValues = lblPossibleValues.Text;
                }

                string value = String.Empty;
                TextBox txtValue = row.FindControl("txtGradingFactorValue") as TextBox;
                if (txtValue != null)
                {
                    value = txtValue.Text;
                }


                // check validations.
                //if (DataValidationBLL.isDataValidForDataType(value, DataType) == false)
                //{
                //    Label lblmessage2;
                //    lblmessage2 = row.FindControl("lblEmpty") as Label;
                //    if (lblmessage != null)
                //    {
                //        lblmessage2.Text = "ERROR in Data Type";
                //        lblmessage2.Visible = true;
                //        lblmessage2.ForeColor = System.Drawing.Color.Red;
                //    }
                //    return;
                //}
                if (DataValidationBLL.isExists(value, PossibleValues) == false)
                {
                    Label lblmessage3;
                    lblmessage3 = row.FindControl("lblEmpty") as Label;
                    if (lblmessage3 != null)
                    {
                        lblmessage3.Text = "Data Should Be one of the following: " + PossibleValues;
                        lblmessage3.Visible = true;
                        lblmessage3.ForeColor = System.Drawing.Color.Red;

                    }
                    return;
                }
                bool isSaved = false;
                GradingResultDetailBLL objGRD = new GradingResultDetailBLL();
                isSaved = objGRD.UpdateEach((Guid)Id, value);
                if (isSaved == true)
                {
                    this.lblmsg2.Text = "Data updated Successfully.";
                    this.gvGradingFactors.EditIndex = -1;
                    GridDataBind();

                }
                else
                {
                    this.lblmsg2.Text = "Data can not be updated.";
                    this.gvGradingFactors.EditIndex = -1;
                    GridDataBind();
                }
               
               


            }
            else
            {
                this.lblmsg2.Text = "You can not update this record.";
            }
        }
        private void LoadCommodityControls(Guid GradeId)
        {
            if (GradeId == Guid.Empty)
            {
                this.chkRecivedGrade.Checked = false;
            }
            else
            {
                this.chkRecivedGrade.Checked = true;
                //get Commodity Grade
                CommodityGradeBLL objGrade = new CommodityGradeBLL();
                objGrade = CommodityGradeBLL.GetCommodityGrade(GradeId);
                //get CommodityClass
                CommodityGradeBLL objClass = CommodityGradeBLL.GetCommodityClassById(objGrade.CommodityClassId);
                this.cboCommodityGrade_CascadingDropDown.SelectedValue = GradeId.ToString();
                this.cboCommodityClass_CascadingDropDown.SelectedValue = objGrade.CommodityClassId.ToString();
                this.cboCommodity_CascadingDropDown.SelectedValue = objClass.CommodityId.ToString();
                ViewState["OldCommodityGrade"] = GradeId.ToString();
            }
        }
        private bool isTotalValueCorrect()
        {
            bool isCorrect = false;
            bool CalculateTotalValue = false;
            float totalValue = 0;
            foreach (GridViewRow row in this.gvGradingFactors.Rows)
            {
                GradingResultDetailBLL objResult = new GradingResultDetailBLL();
                float value;
               
                
            
                try
                {
                    Label lblisTotalValue = new Label();
                    lblisTotalValue = (Label)row.FindControl("lblisTotalValue");
                    if (string.IsNullOrEmpty(lblisTotalValue.Text) != true)
                    {
                        try
                        {
                            if (lblisTotalValue.Text == "True")
                            {
                               
                                CalculateTotalValue = true;
                                // get Value.
                                try
                                {
                                    TextBox txtGradingFactorValue = new TextBox();
                                    txtGradingFactorValue = (TextBox)row.FindControl("txtGradingFactorValue");
                                    if (txtGradingFactorValue.Text == "" || txtGradingFactorValue.Text == null)
                                    {
                                        this.lblMsg.Text = "Please eneter Value";
                                        return false;
                                    }
                                    else
                                    {
                                        value = float.Parse(txtGradingFactorValue.Text);
                                        totalValue += value;
                                    }
                                    

                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
            if (CalculateTotalValue == true)
            {
                if (this.cboCommodityGrade.SelectedValue != "" || this.cboCommodityGrade.SelectedValue != null)
                {
                    string err = "";
                    GradingResultDetailBLL obj = new GradingResultDetailBLL();
                    return obj.PreInsertHasValidGradingResult(totalValue, new Guid(this.cboCommodityGrade.SelectedValue.ToString()),out err );
                }
                else
                {
                    return false;
                }
            }
            
            return isCorrect;
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnUpdate")
            {
                cmd.Add(this.btnUpdate);
            }
            else if (name == "btnEdit")
            {
                foreach (TableRow row in this.gvGradingFactors.Rows)
                {
                    cmd.Add(row.FindControl("btnEdit"));
                }
            }
            return cmd;

        }

        #endregion

        protected void cboCommodityGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGradingFactors();
        }
        //todo Change
        
        private void AddNewGradingFactorValues()
        {
            Nullable<float> totalValue = null;
            Nullable<Guid> GradingResultId = new Guid(ViewState["vsGradingResultId"].ToString());
            GradingResultStatus GenralReqiurmentStatus;
            GenralReqiurmentStatus = GradingResultStatus.Undertermined;
            List<GradingResultDetailBLL> list = new List<GradingResultDetailBLL>();
#region Checking GradingFactors
            foreach (GridViewRow row in this.gvGradingFactorsNew.Rows)
            {
                GradingResultDetailBLL objResult = new GradingResultDetailBLL();
                Guid GradingFactorId;
                string possibleValue;
                string dataType;
                string value;
                string FailPoint = string.Empty;
                int isMax = -1;
                // get the Id and the value.
                Label lblId = new Label();

                try
                {
                    lblId = row.FindControl("lblId") as Label;
                    GradingFactorId = new Guid(lblId.Text);
                }
                catch (InvalidCastException )
                {
                    this.lblMsg.Text = "An error has occured please try again.If the error persits contact the administrator.";
                    return;
                }

                //Get Possible Values 
                try
                {
                    Label lblPossibleTypes = new Label();
                    lblPossibleTypes = (Label)row.FindControl("lblPossibleTypes");
                    possibleValue = lblPossibleTypes.Text;

                }
                catch 
                {
                    this.lblMsg.Text = "An error has occured please try again.If the error persits contact the administrator.";
                    return;
                }
                // get DataType
                try
                {
                    Label lblDataType = new Label();
                    lblDataType = (Label)row.FindControl("lblDataType");
                    dataType = lblDataType.Text;

                }
                catch 
                {
                    this.lblMsg.Text = "An error has occured please try again.If the error persits contact the administrator.";
                    return;
                }

                // get Value.
                try
                {
                    TextBox txtGradingFactorValue = new TextBox();
                    txtGradingFactorValue = (TextBox)row.FindControl("txtGradingFactorValue");
                    value = txtGradingFactorValue.Text;
                    if (value == "" || value == String.Empty)
                    {
                        row.FindControl("lblEmpty").Visible = true;

                    }

                }
                catch 
                {
                    this.lblMsg.Text = "An error has occured please try again.If the error persits contact the administrator.";
                    return;
                }
                // if it is a Genral req.

                try
                {
                    Label lblFailPoint = new Label();
                    lblFailPoint = (Label)row.FindControl("lblFailPoint");
                    FailPoint = lblFailPoint.Text;
                }
                catch
                {
                }

                try
                {
                    Label lblisMax = new Label();
                    lblisMax = (Label)row.FindControl("lblisMax");
                    if (string.IsNullOrEmpty(lblisMax.Text) != true)
                    {
                        try
                        {
                            isMax = GradingFactorBLL.ParseTextGradingResultStatus(lblisMax.Text);
                        }
                        catch 
                        {
                            this.lblMsg.Text = "Invalid Grading Result Comparision.Please try again.";
                            return;
                        }
                    }
                }
                catch
                {
                }
                //IsInTotalValue
                bool IsInTotalValue = false;
                try
                {
                    Label lblisTotalValue = new Label();
                    lblisTotalValue = (Label)row.FindControl("lblisTotalValue");
                    if (string.IsNullOrEmpty(lblisTotalValue.Text) != true)
                    {
                        try
                        {
                            if (lblisTotalValue.Text == "True")
                            {
                                IsInTotalValue = true;
                            }

                        }
                        catch 
                        {
                            IsInTotalValue = false;
                        }
                    }
                }
                catch
                {
                }
                if (IsInTotalValue == true)
                {
                    if (totalValue == null)
                    {
                        totalValue = 0;
                    }
                    totalValue += float.Parse(value);

                }



                //Check Data Type.
                if (DataValidationBLL.isDataValidForDataType(value, dataType) == false)
                {
                    Label lblmessage;
                    lblmessage = row.FindControl("lblEmpty") as Label;
                    if (lblmessage != null)
                    {
                        lblmessage.Text = "ERROR in Data Type";
                        lblmessage.Visible = true;
                        lblmessage.ForeColor = System.Drawing.Color.Red;
                    }
                    return;
                }
                // check in possible Values
                if (DataValidationBLL.isExists(value, possibleValue) == false)
                {
                    Label lblmessage;
                    lblmessage = row.FindControl("lblEmpty") as Label;
                    if (lblmessage != null)
                    {
                        lblmessage.Text = "Data Should Be one of the following: " + possibleValue;
                        lblmessage.Visible = true;
                        lblmessage.ForeColor = System.Drawing.Color.Red;

                    }
                    return;
                }

                objResult.GradingFactorId = GradingFactorId;
                objResult.RecivedValue = value;
                objResult.Status = GradingResultDetailStatus.Active;

                list.Add(objResult);
                Label lblmessage1;
                lblmessage1 = row.FindControl("lblEmpty") as Label;
                if (lblmessage1 != null)
                {
                    lblmessage1.Text = "";
                    lblmessage1.Visible = false;


                }

                // Check if is a General requirment.
                GradingFactorBLL objGenralReqiurment = new GradingFactorBLL();
                if (GradingResultStatus.GeneralRequiementfail == GenralReqiurmentStatus)
                {
                    GenralReqiurmentStatus = GradingResultStatus.GeneralRequiementfail;
                }
                else
                {
                    GenralReqiurmentStatus = objGenralReqiurment.GetGradingResultStatus(value, (FailPointComparsion)(isMax), FailPoint, dataType);
                }


            }

            #endregion



            //Get Commodity Grade

            Guid CommodityGradeId = Guid.Empty;
            if (this.chkRecivedGrade.Checked == true)
            {
                try
                {
                    CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Please Select Commodity Grade.";
                    return;
                }
            }

            #region Checking Total Value
            //Check Grading is OK
                bool ValidGrade = false;
                CommodityGradeFactorValueBLL objGradeFactorValueBLL = new CommodityGradeFactorValueBLL();
                objGradeFactorValueBLL = objGradeFactorValueBLL.GetActiveValueByCommodoityGradeId(CommodityGradeId);
                GradingResultDetailBLL objGradingResultDetail = new GradingResultDetailBLL();
                if (totalValue != null)
                {
                    string err = "";
                    ValidGrade = objGradingResultDetail.PreInsertHasValidGradingResult((float)totalValue, CommodityGradeId, out err);
                    if (ValidGrade == false)
                    {
                        this.lblMsg.Text = err + "-total Value=" + totalValue.ToString(); ;
                        this.lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            
            #endregion

            

            GradingResultBLL obj = new GradingResultBLL();
            try
            {
                obj.GradingId = new Guid(ViewState["vsGradingResultId"].ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please select Grading Code";
            }
            obj.CommodityGradeId = CommodityGradeId;
            obj.IsSupervisor = this.isSupervisor.Checked;
            //obj.Status = (GradingResultStatus)Convert.ToInt32(this.cboStatus.SelectedValue);
            obj.Status = GenralReqiurmentStatus;
            obj.Remark = this.txtRemark.Text;
            try
            {
                obj.GradeRecivedTimeStamp = Convert.ToDateTime(this.txtDateRecived.Text + " " + this.txtTimeRecived.Text);
            }
            catch
            {
                this.lblMsg.Text = "Please check grade recieved date and try again";
                return;
            }

            obj.CreatedBy = UserBLL.GetCurrentUser();
            obj.ProductionYear = int.Parse(this.cboProductionYear.SelectedValue.ToString());
            string strTrackinNo = String.Empty;
            try
            {

                if (ViewState["vsTrackingNo"] != null)
                {
                    // Update WF
                    strTrackinNo = ViewState["vsTrackingNo"].ToString();

                }
                bool isUpdated = false;
                if(strTrackinNo != String.Empty )
                {
                    isUpdated = obj.UpdateGradingResult(list, GradingResultId, strTrackinNo);
                    Response.Redirect("ListInbox.aspx");
                }
                else
                {
                    isUpdated = obj.UpdateGradingResult(list, GradingResultId);
                }
                if ( isUpdated == true)
                {
                    Session["GRID"] = obj.GradingId;
                    LoadData();
                    this.lblMsg.Text = "Data updated Successfully";
                }
                else
                {
                    this.lblMsg.Text = "Data can not be added please check the form and try again.If the error persists contact the administrator.";

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
 
        }
        protected void chkRecivedGrade_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRecivedGrade.Checked == false)
            {
                this.cboCommodityGrade_CascadingDropDown.SelectedValue  = "";
                LoadGradingFactors();
            }
        }
        public void LoadGradingFactors()
        {
            Guid oldGradeId = new Guid(ViewState["OldCommodityGrade"].ToString());
            Guid SelectedGuid = Guid.Empty;
            try
            {
                string strTempCommGrade = this.cboCommodityGrade_CascadingDropDown.SelectedValue.ToString();
                string[] arrTemp = strTempCommGrade.Split(':');
                SelectedGuid = new Guid(arrTemp[0].ToString());
            }
            catch
            {
            }

            if (SelectedGuid != oldGradeId)
            {
                // we need to Remove the Grading factors
                this.pnlGradingFactorsOld.Visible = false;
                this.pnlGradingFactorsOld.GroupingText = "Old Grading Factor.";
                isGradeChanged = true;
            }
            else
            {
                this.pnlGradingFactorsOld.Visible = true;
                this.pnlGradingFactorsNew.Visible = false;
                isGradeChanged = false;
            }
            if (isGradeChanged == true)
            {
                this.pnlGradingFactorsNew.Visible = true;
                GradingFactorBLL obj = new GradingFactorBLL();
                List<GradingFactorBLL> list = new List<GradingFactorBLL>();
                if (cboStatus.SelectedValue.ToString().Trim() == "5" || cboStatus.SelectedValue.ToString().Trim() == "6")
                {
                    list = (obj.GetGradingFactors(new Guid(this.cboCommodity.SelectedValue.ToString())));
                }
                else
                {
                    try
                    {
                        // Coffee Grading Change
                        if(this.cboCommodity.SelectedItem.Text.Trim() != "Coffee" )
                        {
                            list = (obj.GetGradingFactors(new Guid(this.cboCommodity.SelectedValue.ToString())));
                        }
                        list.AddRange(obj.GetGradingFactors(new Guid(this.cboCommodityGrade.SelectedValue.ToString())));
                        this.lblmsg2.Text = "";
                    }
                    catch( Exception ex)
                    {
                        if (this.chkRecivedGrade.Checked == true)
                        {
                            this.lblmsg2.Text = "Please check if all Grading Factors for the grade are displayed.";
                        }
                    }
                }
                
                this.gvGradingFactorsNew.DataSource = list;
                this.gvGradingFactorsNew.DataBind();
            }

            this.UpdatePanel1.Update();
        }
        public void LoadGradingFactorsProductionYearChange()
        {
            Guid oldGradeId = new Guid(ViewState["OldCommodityGrade"].ToString());
            Guid SelectedGuid = Guid.Empty;
            try
            {
                string strTempCommGrade = this.cboCommodityGrade_CascadingDropDown.SelectedValue.ToString();
                string[] arrTemp = strTempCommGrade.Split(':');
                SelectedGuid = new Guid(arrTemp[0].ToString());
            }
            catch
            {
            }

        
            // we need to Remove the Grading factors
            this.pnlGradingFactorsOld.Visible = false;
            this.pnlGradingFactorsOld.GroupingText = "Old Grading Factor.";
            isGradeChanged = true;
            
  
            if (isGradeChanged == true)
            {
                this.pnlGradingFactorsNew.Visible = true;
                GradingFactorBLL obj = new GradingFactorBLL();
                List<GradingFactorBLL> list = new List<GradingFactorBLL>();
                list = (obj.GetGradingFactors(new Guid(this.cboCommodity.SelectedValue.ToString())));
                try
                {
                    list.AddRange(obj.GetGradingFactors(new Guid(this.cboCommodityGrade.SelectedValue.ToString())));
                    this.lblmsg2.Text = "";
                }
                catch
                {
                    if (this.chkRecivedGrade.Checked == true)
                    {
                      //  this.lblmsg2.Text = "Please check if all Grading Factors for the grade are displayed.";
                    }
                }

                this.gvGradingFactorsNew.DataSource = list;
                this.gvGradingFactorsNew.DataBind();
            }

            this.UpdatePanel1.Update();
        }
        public void LoadData()
        {
            string str = "";
            try
            {
                ViewState["vsTrackingNo"] = Request.QueryString["TrackingNo"].ToString();
            }
            catch
            {
            }
            if (Session["GRID"] != null)
            {
                str = Session["GRID"].ToString();
                Session["GRID"] = null;
            }
            if (str == "")
            {
                this.lblMsg.Text = "Unable to Load data try again.";
                return;
            }

            Guid Id;
            try
            {
                Id = new Guid(str);
                ViewState["vsGradingResultId"] = Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            if (Id != null)
            {
                GradingResultBLL obj = new GradingResultBLL();
                obj = obj.GetGradingResultById(Id);
                try
                {
                    this.hfId.Value = obj.ID.ToString();
                }
                catch (InvalidCastException )
                {
                    this.hfId.Value = "";
                    this.btnUpdate.Enabled = false;
                    this.lblMsg.Text = "An error occured ";
                }
                catch (NullReferenceException )
                {
                    this.hfId.Value = "";
                    this.btnUpdate.Enabled = false;
                    this.lblMsg.Text = "An error occured ";
                }

                if (obj.GradingCode != null)
                {
                    this.txtGradingCode.Text = obj.GradingCode.ToString();
                }
                //Commodity Grade Populating
                LoadCommodityControls(obj.CommodityGradeId);


                //this.cboCommodityGrade.SelectedValue = obj.CommodityGradeId.ToString();
                try
                {
                    this.txtDateRecived.Text = obj.GradeRecivedTimeStamp.ToShortDateString();
                }
                catch (NullReferenceException )
                {
                    this.txtDateRecived.Text = "";
                }
                catch (InvalidCastException )
                {
                    this.txtDateRecived.Text = "";
                }
                this.txtTimeRecived.Text = obj.GradeRecivedTimeStamp.ToLongTimeString();
                try
                {
                    this.isSupervisor.Checked = obj.IsSupervisor;
                }
                catch (NullReferenceException )
                {
                    this.isSupervisor.Checked = false;
                }
                try
                {
                    this.cboStatus.SelectedValue = obj.Status.ToString();
                    if (this.cboStatus.SelectedValue == GradingResultStatus.ClientAccepted.ToString())
                    {
                        this.btnUpdate.Enabled = false;
                        this.gvGradingFactors.Enabled = false;
                    }
                }
                catch (NullReferenceException )
                {
                   
                    this.cboStatus.SelectedValue = "";
                }

                this.txtRemark.Text = obj.Remark.ToString();
                int currYear;
                currYear = int.Parse(ConfigurationSettings.AppSettings["CurrentEthiopianYear"]);
                this.cboProductionYear.Items.Add(new ListItem("Please Select Production Year.", ""));
                this.cboProductionYear.AppendDataBoundItems = true;
                bool isEthYearFound = false;
                int procYear = obj.ProductionYear;
                
                for (int i = currYear - 2; i <= currYear; i++)
                {
                    
                    this.cboProductionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    if (isEthYearFound == false)
                    {
                        if (i == procYear)
                        {
                            this.cboProductionYear.SelectedValue = i.ToString();
                            isEthYearFound = true;
                        }
                        
                    }
                }
                ViewState["vsProdYear"] = null;
                if (procYear != -1)
                {
                    this.cboProductionYear.Items.Add(new ListItem(procYear.ToString(), procYear.ToString()));
                    this.cboProductionYear.SelectedValue = procYear.ToString();
                   
                }
                ViewState["vsProdYear"] = procYear;
                //Load dataGrid
                GridDataBind();
            }
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGradingFactors();
        }

        protected void cboProductionYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            isGradeChanged = true;
            LoadGradingFactorsProductionYearChange();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (ViewState["vsGradingResultId"] != null)
            {
                Session["GRID"] = ViewState["vsGradingResultId"].ToString();
                LoadData();
            }
            else
            {
                throw new Exception("An Error has occured please try Again");
            }
        }
    }
}