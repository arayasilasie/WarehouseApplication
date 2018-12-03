using System;
using System.Collections;
using System.Collections.Generic;
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
using WarehouseApplication.BLL;
using AjaxControlToolkit;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddGradingRecived : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadData();
                LoadGrader();
                LoadCommodity();
                this.cboStatus.Items.Clear();
                this.cboStatus.Items.Add(new ListItem("Please Select Status", ""));
                this.cboStatus.Items.Add(new ListItem("Approved", "1"));
                this.cboStatus.Items.Add(new ListItem("Cancelled", "2"));
                // this.cboCommodity_CascadingDropDown.ContextKey = "4f05667f-f6a8-47c1-8d26-c3cbe185b4f7";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            this.lblMsg.Text = "";
            if (this.cboCommodityGrade.SelectedValue != "")
            {
                if (this.chkRecivedGrade.Checked == false)
                {
                    this.lblMsg.Text = "Grade Received is Not Checked.";
                    this.lblMsg.ForeColor = System.Drawing.Color.Red;
                    this.cboCommodityGrade.SelectedIndex = -1;
                    this.cboCommodityClass.SelectedIndex = -1;
                    loadData();
                    this.cboCommodityGrade.SelectedIndex = -1;
                    this.cboCommodityGrade_CascadingDropDown.SelectedValue = "";
                    return;
                }
            }

            Nullable<float> totalValue = null;
            GradingResultStatus GenralReqiurmentStatus;
            GenralReqiurmentStatus = GradingResultStatus.Undertermined;
            List<GradingResultDetailBLL> list = new List<GradingResultDetailBLL>();
            #region Checking GradingFactors
            foreach (GridViewRow row in this.gvGradingFactors.Rows)
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
                catch (InvalidCastException)
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
                catch (Exception ex)
                {
                    throw ex;
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
                catch (Exception ex)
                {
                    throw new Exception("An error has occured please try again.If the error persits contact the administrator.", ex);

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
                if (value == "")
                {
                    Label lblmessage;
                    lblmessage = row.FindControl("lblEmpty") as Label;
                    if (lblmessage != null)
                    {
                        lblmessage.Text = "*";
                        lblmessage.Visible = true;
                        lblmessage.ForeColor = System.Drawing.Color.Red;
                    }
                    return;
                }
                else
                {
                    if (DataValidationBLL.isDataValidForDataType(value, dataType) == false)
                    {
                        Label lblmessage;
                        lblmessage = row.FindControl("lblEmpty") as Label;
                        if (lblmessage != null)
                        {
                            lblmessage.Text = "Error in Data Type";
                            lblmessage.Visible = true;
                            lblmessage.ForeColor = System.Drawing.Color.Red;
                        }
                        return;
                    }
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
                if (((GradingResultStatus)int.Parse(this.cboStatus.SelectedValue)) == GradingResultStatus.New || ((GradingResultStatus)int.Parse(this.cboStatus.SelectedValue)) == GradingResultStatus.Approved)
                {

                    GenralReqiurmentStatus = objGenralReqiurment.GetGradingResultStatus(value, (FailPointComparsion)(isMax), FailPoint, dataType);

                }
                else
                {
                    GenralReqiurmentStatus = (GradingResultStatus)int.Parse(this.cboStatus.SelectedValue);
                }


            }

            #endregion

            // Check if is a General requirment.

            if (((GradingResultStatus)int.Parse(this.cboStatus.SelectedValue)) == GradingResultStatus.New || ((GradingResultStatus)int.Parse(this.cboStatus.SelectedValue)) == GradingResultStatus.Approved)
            {

                GenralReqiurmentStatus = GradingResultStatus.Approved;

            }
            else
            {
                GenralReqiurmentStatus = (GradingResultStatus)int.Parse(this.cboStatus.SelectedValue);
            }



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

            //#region Checking Total Value
            ////Check Grading is OK
            //if (this.chkRecivedGrade.Checked == true)
            //{
            //    bool ValidGrade = false;
            //    CommodityGradeFactorValueBLL objGradeFactorValueBLL = new CommodityGradeFactorValueBLL();
            //    objGradeFactorValueBLL = objGradeFactorValueBLL.GetActiveValueByCommodoityGradeId(CommodityGradeId);
            //    GradingResultDetailBLL objGradingResultDetail = new GradingResultDetailBLL();
            //    if (totalValue != null)
            //    {
            //        string err = "";
            //        ValidGrade = objGradingResultDetail.PreInsertHasValidGradingResult((float)totalValue, CommodityGradeId, out err);
            //        if (ValidGrade == false)
            //        {
            //            this.lblMsg.Text = err + " Total Value : " + totalValue.ToString();
            //            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            //            return;
            //        }
            //    }
            //}
            //#endregion

            Nullable<Guid> Id = null;

            GradingResultBLL obj = new GradingResultBLL();
            try
            {
                obj.GradingId = new Guid(this.cboGradingCode.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please select Grading Code";
                return;
            }
            obj.CommodityGradeId = CommodityGradeId;
            obj.IsSupervisor = this.isSupervisor.Checked;
            //obj.Status = (GradingResultStatus)Convert.ToInt32(this.cboStatus.SelectedValue);
            obj.Status = GenralReqiurmentStatus;
            obj.Remark = this.txtRemark.Text;
            if (this.cboProductionYear.SelectedValue.ToString() != "")
            {
                obj.ProductionYear = int.Parse(this.cboProductionYear.SelectedValue.ToString());
            }
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
            try
            {
                Id = obj.Add(list, obj.TrackingNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (Id != null)
            {
                this.lblMsg.Text = "Result Added Successfully.";
                Session["GRID"] = Id.ToString();
                string strTranNo = Session["AddGradingRecivedTranNo"].ToString();
                Session["AddGradingRecivedTranNo"] = null;
                Response.Redirect("ListInbox.aspx");
            }
            else
            {
                this.lblMsg.Text = "Data can not be added please check the form and try again.If the error persists contact the administrator.";

            }

        }
        private void BindData()
        {
            this.cboGradingCode.Items.Clear();
            this.cboGradingCode.Items.Add(new ListItem("Please Select Grading Code.", ""));
            this.cboGradingCode.AppendDataBoundItems = true;
            //Get Codes pending results.
            GradingBLL obj = new GradingBLL();
            List<GradingBLL> list = new List<GradingBLL>();
            //TODO Warehouse Id 
            Guid warhouseId = UserBLL.GetCurrentWarehouse();
            
            if (Session["GradingRecivedGradingId"] != null)
            {
                list = obj.GetGradingsPendingResultByGradingId(warhouseId, new Guid(Session["GradingRecivedGradingId"].ToString()));
                this.cboGradingCode.DataSource = list;
                this.cboGradingCode.DataValueField = "Id";
                this.cboGradingCode.DataTextField = "GradingCode";
                this.cboGradingCode.DataBind();


                if (Session["GradingRecivedGradingId"].ToString() != "")
                {
                    this.cboGradingCode.SelectedValue = Session["GradingRecivedGradingId"].ToString();

                    //get Code Generated date.

                    GradingBLL objGrading = new GradingBLL();
                    objGrading = objGrading.GetById(new Guid(this.cboGradingCode.SelectedValue.ToString()));
                    if (objGrading != null)
                    {
                        this.lblCodeGeneratedDate.Text = objGrading.DateCoded.ToShortDateString();
                        //elias to remove.
                        // this.cmpCodeGen.ValueToCompare = objGrading.DateCoded.ToShortDateString();
                    }

                    LoadGrader();
                    LoadCommodity();
                }
            }
            else
            {
                this.cboGradingCode.SelectedIndex = -1;
                return;
            }
            this.cboGradingCode.Enabled = false;
        }
        protected void cboGradingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrader();
            LoadCommodity();

        }
        protected void cboCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid CommodityGradeId = Guid.Empty;
            try
            {
                CommodityGradeId = new Guid(this.cboCommodity.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please Select commodity Grade.";
                return;
            }
            if (CommodityGradeId != Guid.Empty)
            {
                GradingFactorBLL obj = new GradingFactorBLL();
                List<GradingFactorBLL> list = new List<GradingFactorBLL>();
                if (this.chkRecivedGrade.Checked == false)
                {
                    list = obj.GetGradingFactors(CommodityGradeId);
                }
                else
                {
                    list = null;
                }
                this.gvGradingFactors.DataSource = list;
                this.gvGradingFactors.DataBind();
            }
            else
            {
                this.lblMsg.Text = "Please Select commodity Grade.";
                return;
            }
            this.UpdatePanel1.Update();
        }
        protected void cboCommodityGrade_SelectedIndexChanged(object sender, EventArgs e)
        {


            this.lblMsg.Text = "";
            this.gvGradingFactors.DataSource = null;
            this.gvGradingFactors.DataBind();
            Guid CommodityGradeId = Guid.Empty;
            Guid CommodityId = Guid.Empty;
            try
            {
                CommodityId = new Guid(this.cboCommodity.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please Select commodity Grade.";
                this.cboCommodityGrade.SelectedIndex = -1;
                this.cboCommodityGrade_CascadingDropDown.SelectedValue = "";
                return;
            }
            if (this.chkRecivedGrade.Checked == true)
            {
                try
                {
                    CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Please Select commodity Grade.";
                    return;
                }
            }
            else
            {
                if (this.cboCommodityGrade.SelectedValue.ToString() != "")
                {
                    this.lblMsg.Text = "Grade Received is Not Checked.";
                    this.lblMsg.ForeColor = System.Drawing.Color.Red;
                    loadData();
                    return;
                }
            }
            if (this.chkRecivedGrade.Checked == true)
            {

                LoadGradingFactors(CommodityId, CommodityGradeId);
            }
        }
        //protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //Guid CommodityGradeId = Guid.Empty;
        //try
        //{
        //    CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
        //}
        //catch
        //{
        //    this.lblMsg.Text = "Please Select commodity Grade.";
        //    return;
        //}
        //if (CommodityGradeId != Guid.Empty)
        //{
        //    GradingFactorBLL obj = new GradingFactorBLL();
        //    List<GradingFactorBLL> list = new List<GradingFactorBLL>();
        //    list = obj.GetGradingFactors(CommodityGradeId);
        //    this.gvGradingFactors.DataSource = list;
        //    this.gvGradingFactors.DataBind();
        //}
        //else
        //{
        //    this.lblMsg.Text = "Please Select commodity Grade.";
        //    return;
        //}
        // }
        private void LoadGrader()
        {
            Guid GradingId = Guid.Empty;
            try
            {
                GradingId = new Guid(this.cboGradingCode.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please Select Grading Code.";
                return;
            }
            if (GradingId == Guid.Empty)
            {
                this.lblMsg.Text = "Please Select Grading Code.";
                return;
            }
            GradingByBLL objGradeByBLL = new GradingByBLL();
            List<GradingByBLL> list = new List<GradingByBLL>();
            list = objGradeByBLL.GetByGradingId(GradingId);
            this.cboGrader.Items.Clear();
            this.cboGrader.Items.Add(new ListItem("Please Select Grader", ""));
            if (list.Count > 0)
            {
                foreach (GradingByBLL i in list)
                {
                    this.cboGrader.Items.Add(new ListItem(i.GraderName, i.Id.ToString()));
                }
            }
        }
        private void LoadCommodity()
        {
            Guid GradingId = Guid.Empty;
            try
            {
                GradingId = new Guid(this.cboGradingCode.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please Select Grading Code.";
                return;
            }
            if (GradingId == Guid.Empty)
            {
                this.lblMsg.Text = "Please Select Grading Code.";
                return;
            }
            GradingBLL objGrading = new GradingBLL();
            objGrading = objGrading.GetById(GradingId);
            Guid CommodityReciveingRequestId = Guid.Empty;
            if (objGrading != null)
            {
                CommodityReciveingRequestId = objGrading.CommodityRecivingId;
            }
            else
            {
                this.lblMsg.Text = "An error has occured please Try again.";
                return;
            }
            if (CommodityReciveingRequestId != Guid.Empty)
            {
                CommodityDepositeRequestBLL objCommDepReq = new CommodityDepositeRequestBLL();
                objCommDepReq = objCommDepReq.GetCommodityDepositeDetailById(CommodityReciveingRequestId);
                if (objCommDepReq != null)
                {
                    if (objCommDepReq.CommodityId != null)
                    {
                        this.cboCommodity_CascadingDropDown.ContextKey = objCommDepReq.CommodityId.ToString();
                        this.cboCommodity_CascadingDropDown.SelectedValue = objCommDepReq.CommodityId.ToString();
                        this.cboCommodity.SelectedValue = objCommDepReq.CommodityId.ToString();
                        LoadGradingFactors(objCommDepReq.CommodityId);
                    }
                }
            }

        }
        private void LoadGradingFactors(Guid CommodityGradeId)
        {



            if (CommodityGradeId != Guid.Empty)
            {
                GradingFactorBLL obj = new GradingFactorBLL();
                List<GradingFactorBLL> list = new List<GradingFactorBLL>();
                list = obj.GetGradingFactors(CommodityGradeId);
                this.gvGradingFactors.DataSource = list;
                this.gvGradingFactors.DataBind();
            }
            else
            {
                this.lblMsg.Text = "Please Select commodity Grade.";
                return;
            }
            this.UpdatePanel1.Update();
        }
        private void LoadGradingFactors(Guid Commodity, Guid CommodityGradeId)
        {

            List<GradingFactorBLL> list = new List<GradingFactorBLL>();
            GradingFactorBLL obj = new GradingFactorBLL();


            if (Commodity != Guid.Empty)
            {

                if (this.chkRecivedGrade.Checked == false || Commodity.ToString().ToUpper().Trim() != ConfigurationSettings.AppSettings["CoffeeId"].ToUpper().Trim())
                {
                    list = obj.GetGradingFactors(Commodity);
                }
                else
                {
                    if (Commodity.ToString().ToUpper().Trim() == ConfigurationSettings.AppSettings["CoffeeId"].ToUpper().Trim())
                    {
                        list = new List<GradingFactorBLL>();
                    }
                }

            }
            if (Commodity.ToString().ToUpper().Trim() == ConfigurationSettings.AppSettings["CoffeeId"].ToUpper().Trim())
            {
                if (CommodityGradeId != Guid.Empty)
                {
                    List<GradingFactorBLL> objList = null;
                    objList = obj.GetGradingFactors(CommodityGradeId);
                    if (objList != null)
                    {
                        if (list == null)
                        {
                            list = new List<GradingFactorBLL>();
                        }
                        foreach (GradingFactorBLL o in objList)
                        {
                            list.Add(o);
                        }
                    }

                }
            }

            if (list != null)
            {
                if (list.Count > 0)
                {
                    list = (from w in list
                            orderby w.Rank ascending
                            select w).ToList();
                    this.gvGradingFactors.DataSource = list;
                    this.gvGradingFactors.DataBind();
                    this.UpdatePanel1.Update();
                }
            }
        }
        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            cmd.Add(this.btnSave);
            return cmd;
        }
        private void loadData()
        {
            this.Page.DataBind();
            this.isSupervisor.Checked = true;

            BindData();
            int currYear;
            currYear = int.Parse(ConfigurationSettings.AppSettings["CurrentEthiopianYear"]);
            this.cboProductionYear.Items.Clear();
            this.cboProductionYear.Items.Add(new ListItem("Please Select Production Year.", ""));
            this.cboProductionYear.AppendDataBoundItems = true;
            for (int i = currYear - 2; i <= currYear; i++)
            {
                this.cboProductionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        #endregion

        protected void gvGradingFactors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblPossibleTypes = new Label();
                //lblPossibleTypes = (Label)e.Row.FindControl("lblPossibleTypes");



                //Label lblText = new Label();
                //lblText.Text = "hi";
                //e.Row.Cells[7].Controls.Add(lblText);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }

        protected void chkRecivedGrade_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRecivedGrade.Checked == true)
            {
                Guid CommodityGradeId = Guid.Empty;
                try
                {
                    CommodityGradeId = new Guid(this.cboCommodity.SelectedValue.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Please Select commodity Grade.";
                    return;
                }
                if (CommodityGradeId != Guid.Empty)
                {
                    GradingFactorBLL obj = new GradingFactorBLL();
                    List<GradingFactorBLL> list = new List<GradingFactorBLL>();
                    list = obj.GetGradingFactors(CommodityGradeId);
                    this.gvGradingFactors.DataSource = list;
                    this.gvGradingFactors.DataBind();
                }
                else
                {
                    this.lblMsg.Text = "Please Select commodity Grade.";
                    return;
                }

                this.UpdatePanel1.Update();
                this.cboStatus.Items.Clear();
                this.cboStatus.Items.Add(new ListItem("Please Select Status", ""));
                this.cboStatus.Items.Add(new ListItem("Approved", "1"));
                this.cboStatus.Items.Add(new ListItem("Cancelled", "2"));
            }
            else
            {
                this.gvGradingFactors.DataSource = null;
                this.gvGradingFactors.DataBind();
                this.cboCommodityGrade_CascadingDropDown.SelectedValue = "";
                if (this.cboCommodity.SelectedValue != "")
                {
                    LoadGradingFactors(new Guid(this.cboCommodity.SelectedValue.ToString()));
                }
                // remove Status 
                this.cboStatus.Items.Clear();
                this.cboStatus.Items.Add(new ListItem("Please Select Status", ""));
                this.cboStatus.Items.Add(new ListItem("Moisture Fail", "5"));
                this.cboStatus.Items.Add(new ListItem("General Requierment fail", "6"));


            }

        }


        //protected void chkReceivedGrade_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chkReceivedGrade.Checked == false)
        //    {
        //        Guid CommodityGradeId = Guid.Empty;
        //        try
        //        {
        //            CommodityGradeId = new Guid(this.cboCommodity.SelectedValue.ToString());
        //            LoadGradingFactors(CommodityGradeId);
        //        }
        //        catch
        //        {
        //            this.lblMsg.Text = "Please Select commodity Grade.";
        //            return;
        //        }
        //    }
        //}




    }
}