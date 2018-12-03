using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using GradingBussiness;
using GINBussiness;
using System.Data;
using System.Configuration;
//using System.Web.UI.MobileControls;

namespace WarehouseApplication
{
    public partial class GenerateGrading : System.Web.UI.Page
    {
        private string SamplingCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (IsPostBack) return;
            {
               
                Session.Remove("gModel");
              
                if (Request.QueryString["sampleCode"] == null)
                {
                    Messages.SetMessage("An error has occurred Please log out and Try Agian.", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                ViewState["sampleCode"] = Request.QueryString["sampleCode"];
                SamplingCode = Request.QueryString["sampleCode"];
                if (string.IsNullOrEmpty(SamplingCode))
                {
                    Messages.SetMessage("An error has occurred Please log out and Try Agian.", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                lblSampleCodeValue.Text = SamplingCode;
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt = GradingModel.GetSampleDate(SamplingCode);
                List<GradingModel> samplingList = GradingModel.getSamplingInfo(SamplingCode);
                dt1 = GradingModel.GetWoredaName(samplingList[0].WoredaID);
                dt2 = GradingModel.GetCommodityClass(samplingList[0].CommodityID, samplingList[0].WoredaID, null, samplingList[0].VoucherCommodityTypeID);
                pnlGradingClass.Visible = false;
                Session["VoucherCommodityTypeID"] = samplingList[0].VoucherCommodityTypeID;
                Session["CommodityID"] = samplingList[0].CommodityID;
                if (samplingList[0].CommodityID == new Guid(ConfigurationManager.AppSettings["CoffeeId"].ToUpper().Trim()))
                {
                    pnlGradingClass.Visible = true;
                    if(samplingList[0].VoucherCommodityTypeID == new Guid(ConfigurationManager.AppSettings["illegalCoffee"].ToUpper().Trim())
                        || samplingList[0].VoucherCommodityTypeID == new Guid ("30d25321-5037-48e1-be0f-5b66ce0330eb")
                        
                        )
                            if (samplingList[0].VoucherCommodityTypeID == new Guid("30d25321-5037-48e1-be0f-5b66ce0330eb"))
                            {
                                lblClassValue.Text = "Bi-Product Coffee";
                            }
                            else
                            {
                                 lblClassValue.Text = "Illegal Coffee";
                            }
                        else if (dt2.Rows.Count > 0)
                        {
                            for (int i = dt2.Rows.Count; i > 0; i--)
                            {
                                if (!dt2.Rows[i - 1]["Class"].ToString().EndsWith("Q"))
                                    if (i == dt2.Rows.Count)
                                        lblClassValue.Text = dt2.Rows[i - 1]["Class"].ToString();
                                    else if (lblClassValue.Text == string.Empty)
                                        lblClassValue.Text = dt2.Rows[i - 1]["Class"].ToString();
                                    else
                                        lblClassValue.Text = lblClassValue.Text + " ; " + dt2.Rows[i - 1]["Class"].ToString();
                            }
                        }
                        else
                        {
                            Messages.SetMessage("Class must be assigned to generate a code.", WarehouseApplication.Messages.MessageType.Error);
                            btnAdd.Visible = false;
                            btnGenerateCode.Visible = false;
                        }
                }
                if (dt1.Rows.Count > 0)
                    lblWoredaValue.Text = dt1.Rows[0]["Description"].ToString();
                //else
                //  Messages.SetMessage("There is no Woreda with this parameter.", WarehouseApplication.Messages.MessageType.Error);
                if (dt.Rows.Count > 0)
                    lblSampleDateValue.Text = dt.Rows[0]["SampleCodeGeneratedTimeStamp"].ToString();
                else
                    Messages.SetMessage("Please provide Sample Date.", WarehouseApplication.Messages.MessageType.Error);
                FillDrop(drpGrader, WareHouseOperatorTypeEnum.Grader);
            }
        }
        private void FillDrop(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.WarehouseOperators(UserBLL.GetCurrentWarehouse()).FindAll(p => p.Type % (int)type == 0);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        public static bool RemovePredicate(TransactionDetail o)
        {
            return true;
        }
        protected void btnGenerateCode_Click(object sender, EventArgs e)
        {
            GradingModel gm = (GradingModel)Session["gModel"];            
            string errorMessage = string.Empty;
            try
            {
                //gm.AddGrader(sm.UserId, sm.isSupervisor);
                if (gm==null)                     
                    Messages.SetMessage("Empty Data.", WarehouseApplication.Messages.MessageType.Error);
                else
                {
                    if (GenerateCode(gm))
                    {
                        Session["GraderID"] = gm.ID;
                        PrintCode();
                        Messages.SetMessage("Code Generated Successfully.", WarehouseApplication.Messages.MessageType.Success);
                        btnGenerateCode.Visible = false;
                        btnNext.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.SetMessage(gm.ErrorMessage, WarehouseApplication.Messages.MessageType.Error);
            }
        }
        private void PrintCode()
        {
            Session["ReportType"] = "GradingCode";
            ScriptManager.RegisterStartupScript(this,
                                                          this.GetType(),
                                                          "ShowReport",
                                                          "<script type=\"text/javascript\">" +
                                                          string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                          "</script>",
                                                          false);
        }
        private bool GenerateCode(GradingModel objcode)
        {
            if (ViewState["sampleCode"] == null)
            {
                Messages.SetMessage("An error has occurred Please log out and Try Agian.", WarehouseApplication.Messages.MessageType.Error);
                return false;
            }
            SamplingCode = ViewState["sampleCode"].ToString();
            List<GradingModel> samplingList = GradingModel.getSamplingInfo(SamplingCode);
            if (samplingList[0].CodeGenerated != true)
            {
                WarehouseApplication.BLL.GradingBLL objrandom = new WarehouseApplication.BLL.GradingBLL();
                objcode.ID = Guid.NewGuid();
                Session["GenerateCode"] = objcode.ID;
                objcode.SamplingsID = samplingList[0].SamplingsID;
                objcode.ArrivalID = samplingList[0].ArrivalID;
                objcode.TrackingNumber = samplingList[0].TrackingNumber;
                objcode.GradingCode = objcode.GetRandomCode(UserBLL.GetCurrentWarehouseCode());
                objcode.DateTimeCoded = DateTime.Now;
                objcode.WarehouseId = UserBLL.GetCurrentWarehouse();
                objcode.CreatedBy = UserBLL.GetCurrentUser();
                objcode.CreatedTimestamp = DateTime.Now;
                GraderModel value = objcode.gradinginfoList.Find(s => (s.isSupervisor == true));
                if (value == null)
                {
                    Messages.SetMessage("At least one supervisor required.", WarehouseApplication.Messages.MessageType.Error);
                    return false;
                }
                try
                {

                    objcode.Save();
                    return true;
                }
                catch 
                {
                    Messages.SetMessage("Unable to save record. Please try again.", WarehouseApplication.Messages.MessageType.Error);
                    return false;
                }
            }
            else
            {
                Messages.SetMessage("Code Already Generated.", WarehouseApplication.Messages.MessageType.Error);
                return false;
            }
        }
        protected void gvDetail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();
            GradingModel gModel;
            if (Session["gModel"] == null)
            {
                gModel = new GradingModel();
                Session.Add("gModel", gModel);
            }
            else
                gModel = (GradingModel)Session["gModel"];
            try
            {
                gModel.AddGrader(new Guid(drpGrader.SelectedValue), chkIsSupervisor.Checked);
            }
            catch (Exception ex)
            {
                Messages.SetMessage(ex.Message.ToString(), WarehouseApplication.Messages.MessageType.Error);

            }

            gvGradingBy.DataSource = gModel.gradinginfoList;
            gvGradingBy.DataBind();

        }
        private GraderModel GetGradingBy(GradingModel gModel)
        {
            GraderModel gm = new GraderModel(gModel);
            gm.UserId = new Guid(drpGrader.SelectedValue);
            gm.isSupervisor = chkIsSupervisor.Checked;
            return gm;
        }
        protected void gvGradingBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            GradingModel gm = (GradingModel)Session["gModel"];
            Guid UserId = new Guid(gvGradingBy.SelectedDataKey["UserId"].ToString());
            gm.gradinginfoList.Remove(gm.gradinginfoList.FindLast(s => s.UserId == UserId));
            gvGradingBy.DataSource = gm.gradinginfoList;
            gvGradingBy.DataBind();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }
    }
}