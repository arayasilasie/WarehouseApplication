using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class PageSwicther : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string transactionNo = Request.QueryString["TranNo"];
            string taskName = Request.QueryString["Task"];
            if (transactionNo == null)
                return;
            if (taskName.Trim().ToUpper() == "Full Arrival".ToUpper())
            {
                Response.Redirect("AddArrival.aspx");
                return;
            }
            else if ( transactionNo == "ConfirmTrucksForSamp")
            {
                Response.Redirect("ConfirmTrucksForSampling.aspx");
                return;
            }
            else if (transactionNo == "GetSampleTicket")
            {
                Response.Redirect("GetSampleTicket.aspx");
                return;
            }
 
            
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            ECXWF.CMessage[] mess = null;
            try
            {
                //eng.UnlockMessageByUser(Request.QueryString["TranNo"], "AddVoucherInformation", UserBLL.GetCurrentUser());
                //eng.RemoveTransactionFromStack(Request.QueryString["TranNo"]);
                mess = eng.Request(Request.QueryString["TranNo"], UserBLL.GetCurrentUser(), new string[] { WarehouseBLL.CurrentWarehouse.Location });
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (mess == null)
                return;
            if (mess.Length > 1)
            {
                this.lstPages.DataValueField = "Name";
                this.lstPages.DataTextField = "Name";
                this.lstPages.DataSource = mess;

            }
            else if (mess.Length == 1)
            {
                this.Session["msg"] = mess[0];
                this.Response.Redirect(this.PageFactory(mess[0], Request.QueryString["TranNo"], new object[] { Request.QueryString["Id"] }));
                return;
            }
            btnOpen.Enabled = (lstPages.SelectedIndex >= 0);
        }
        private string PageFactory(ECXWF.CMessage msg ,  string TranNo, object[] par)
        {
            if (Enum.GetNames(typeof(WorkflowTaskType)).Contains(msg.Name))
            {
                InitiateWorkflowTask(msg.Name);
            }
            if (msg.Name == "AddCommodityDepositeRequest")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddCommodityDepositRequest.aspx";
            }
            else if (msg.Name == "AddDriverInformation")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddDriverInformation.aspx?TranNo=" + TranNo ;
            }
            else if (msg.Name == "AddVoucherInfo")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddVoucherInformation.aspx?TranNo=" + TranNo ;
            }
            else if (msg.Name == "GetSampleTicket")
            {
                return "GetSampleTicket.aspx";
            }
            else if (msg.Name == "AddSamplingResult")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddSamplingResult.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "GenerateGradingCode")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "GenerateGradingCode.aspx";
            }
            else if (msg.Name == "AddGradingResult")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddGradingResult.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "GradingResultCA")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "GradingResultClientAcceptance.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "AddUnloadingInfo")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddUnloadingInformation.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "PreWeighTruck")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "PreWeighTruck.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "AddUnloadingInfo")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddUnloadingInformation.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "PostWeighTruck")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "PostWeighTruck.aspx?TranNo=" + TranNo;
            } 
            else if (msg.Name == "AddScalingInfo")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddScalingInformation.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "AddGRN")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "AddGRN.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "GRNAcceptance")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "GRNAcceptance.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "ClientAcceptance")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "GRNAcceptance.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "WarehouseManagerAppr")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "ViewGRN.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "WarehouseManagerApproval")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "ViewGRN.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "EditGRN")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "ViewGRN.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "EditGradeDispute")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "EditGradeDispute.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "ApproveReSampling")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "EditReSampling.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "OpenGRNForEdit")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "OpenGRNEdit.aspx";
            }
            else if (msg.Name == "EditWHR")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "ReCreateGRN.aspx";
            }
            else if (msg.Name == "CancelGRN" || msg.Name.Trim() == "ApproveCancelationRequest")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "CancelGRN.aspx";
            }
                
            else if (msg.Name == "GetTrucksReadyForSam")
            {
                
                return "GetTrucksReadyForSampling.aspx";
            }
            else if (msg.Name == "ConfirmTrucksForSamp")
            {
                return "ConfirmTrucksForSampling.aspx";
            }
            else if (msg.Name == "Test")
            {
                return "Test1.aspx?Tran=" + TranNo;
            }
            else if (msg.Name.Trim() == "CodeSampRec")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "ReceiveSampleCode.aspx";
            }
            else if(msg.Name.Trim() == "EditGradingResult")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "EditGradingReceived.aspx?TrackingNo=" + TranNo.ToString();
            }
            else if (msg.Name.Trim() == "WHAppGRNEdit")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "EditApprovedGRNEditRequest.aspx";
            }
            else if (msg.Name.Trim() == "OpenGRNForEdit")
            {
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "OpenGRNEdit.aspx";
            }            
            else if (msg.Name.Trim() == "UpdateGRNNo")
            {
                //GRN number edit
                WFTransaction.LoadVariables(msg.Name, TranNo);
                return "UpdateGRNNumber.aspx";
            }
            else if (msg.Name.Trim() == "UpdateClientNo")
            {
                //NoClient
                WFTransaction.LoadVariables(msg.Name, TranNo);
                string id = HttpContext.Current.Session["CommodityRequestId"].ToString();
                return "EditCommodityDepositRequest.aspx?id="+ id;
            }
            else
            {
                return "";
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path + "?TranNo=" + txtTranNo.Text.Trim(), true);
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedIndex < 0)
            {
                return;
            }
            InitiateWorkflowTask(lstPages.SelectedValue);
        }

        private void InitiateWorkflowTask(string message)
        {
            if (Enum.GetNames(typeof(WorkflowTaskType)).Contains(message))
            {
                WorkflowTaskInitiator.InitiateTask(message, Request.QueryString["TranNo"]);
            }
        }
        
    }
}
