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
using System.Data.SqlClient;
using System.Text;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIGetSamplingTicketById : System.Web.UI.UserControl, ISecurityConfiguration
    {
        public StringBuilder ss = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                EmployeeAttendanceBLL obj = new EmployeeAttendanceBLL();
                List<UserBLL> list = UserRightBLL.GetUsersWithRight("Sampler");
                this.cboSampler.Items.Add(new ListItem("Please Select Sampler", ""));
                this.cboSampler.AppendDataBoundItems = true;
                this.cboSampler.DataSource = list;
                this.cboSampler.DataTextField = "FullName";
                this.cboSampler.DataValueField = "UserId";
                this.cboSampler.DataBind();
            }

        }



        protected void btnGenerateSampleTicket_Click(object sender, EventArgs e)
        {
            // 

            Nullable<Guid> Id;
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            Guid Moistureid = Guid.Empty;
            bool isMoisture = false;
            string TransactionId = String.Empty;
            Nullable<Guid> RecievingRequestId = null;
            try
            {
                if (this.chkReSampling.Checked == false)
                {
                    RecievingRequestId = SamplingBLL.GetRandomSample(WarehouseId);
                    isMoisture = false;
                }
                else
                {
                    try
                    {
                        RecievingRequestId = SamplingBLL.GetRandomReSampling(WarehouseId, out Moistureid, out TransactionId);
                    }
                    catch (Exception ex)
                    {
                        this.lblMsg.Text = ex.Message;
                    }
                    isMoisture = true;
                }
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = ex.Message;
                return;
            }
            if (RecievingRequestId == null)
            {
                this.lblMsg.Text = "There are no records Pending Sampling";
                return;
            }

            Nullable<int> SerialNo = null;
            SamplingBLL objSample = new SamplingBLL();
            SerialNo = objSample.GetSerialNo(WarehouseId);
            if (SerialNo == null)
            {
                throw new Exception("Inavlid Serial No. exception");
            }
            // Create sampling obect 

            SamplingBLL objSampling = new SamplingBLL();
            objSampling.ReceivigRequestId = (Guid)RecievingRequestId;
            objSampling.SamplingStatusId = SamplingStatus.Active;
            objSampling.SerialNo = SamplingBLL.GetSerial(WarehouseId);
            objSampling.WarehouseId = WarehouseId;

            CommodityDepositeRequestBLL objCommDep = new CommodityDepositeRequestBLL();
            objCommDep = objCommDep.GetCommodityDepositeDetailById((Guid)RecievingRequestId);
            if (objCommDep != null)
            {
                if (string.IsNullOrEmpty(objCommDep.TrackingNo) == true)
                {
                    this.lblMsg.Text = "Can not get Tracking Number.Please try again.";
                    return;
                }
                else
                {
                    if (isMoisture == false)
                    {
                        objSampling.TrackingNo = objCommDep.TrackingNo;
                    }
                    else
                    {
                        objSampling.TrackingNo = TransactionId;
                    }
                }
            }
            else
            {
                throw new Exception("Unable to get Arrival Information CommDepId=" + RecievingRequestId.ToString());

            }

            //Create Sampler 
            SamplerBLL objSampler = new SamplerBLL();
            try
            {
                objSampler.SamplerId = new Guid(this.cboSampler.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please select Sampler";
                return;
            }

            objSampler.Status = SamplerStatus.Active;



            Id = objSampling.InsertSample(objSampling, objSampler, isMoisture);

            if (Id != null)
            {

                objSampling.Id = (Guid)Id;
                string tran = "";
                tran = SamplingBLL.GetTransactionNumber((Guid)RecievingRequestId);
                Session["Sampler"] = objSampler;
                Session["Sample"] = objSampling;



                ScriptManager.RegisterStartupScript(this,
                   this.GetType(),
                   "ShowReport",
                   "<script type=\"text/javascript\">" +
                       string.Format("javascript:window.open(\"ReportSampleTicket.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Id.ToString()) +
                   "</script>",
                   false);


            }
            else
            {
                this.lblMsg.Text = "Data can not be saved.Please try again if the error persists contact the administrator.";
            }



        }



        private string PageFactory(ECXWF.CMessage msg, string TranNo, object[] par)
        {
            if (msg.Name == "AddCommodityDepositeRequest")
            {
                return "AddCommodityDepositRequest.aspx";
            }
            else if (msg.Name == "AddDriverInformation")
            {
                return "AddDriverInformation.aspx?TranNo=" + TranNo + "&id=" + par[0].ToString();
            }
            else if (msg.Name == "AddVoucherInfo")
            {
                return "AddVoucherInformation.aspx?TranNo=" + TranNo + "&id=" + par[0].ToString();
            }
            else if (msg.Name == "GetSampleTicket")
            {
                return "GetSampleTicket.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "AddSamplingResult")
            {
                return "AddSamplingResult.aspx?TranNo=" + TranNo;
            }
            else if (msg.Name == "AddSampleCoding")
            {
                return "AddSampleCoding.aspx";
            }
            else if (msg.Name == "AddGradingResult")
            {
                return "AddGradingResult.aspx";
            }
            else
            {
                return "";
            }
        }



        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            cmd.Add(this.btnGenerateSampleTicket);
            return cmd;
        }

        #endregion

        protected void cboSampler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}