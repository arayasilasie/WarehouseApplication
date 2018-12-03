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
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIAcceptGradingResult : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            if (IsPostBack != true)
            {
                LoadData();
               
            }

            
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            DateTime dt = Convert.ToDateTime(this.txtDateOfAcceptance.Text +" "  + this.txtTimeodAcceptance.Text );
            GradingResultBLL objR = new GradingResultBLL();
            Guid Id = new Guid(this.hfId.Value);
            int Status = -1; 
            //Grading recived Status.
            GradingResultStatus GradingRecivedStatus;
            try
            {
                GradingRecivedStatus = (GradingResultStatus) int.Parse(this.cboGradingRecivedStatus.SelectedValue.ToString()) ;
            }
            catch
            {
                this.lblMsg.Text = "Grading Recived status is empty.";
                return;
            }
            try
            {
                 Status = int.Parse(this.cboAcceptanceStatus.SelectedValue);
            }
            catch
            {
                this.lblMsg.Text = "Please Select Status.";
                return;
            }
            if (Status == -1)
            {
                this.lblMsg.Text = "Please Select Status.";
                return;
            }
            string preQueNo = "";
            isSaved = objR.ClientAcceptance(Id, Status, GradingRecivedStatus, dt, out preQueNo);
            if (isSaved == true)
            {
                //if (objR.Status == GradingResultStatus.ClientAccepted)
                //{
                if(preQueNo != "")
                {
                    this.lblMsg.Text = "Data Updated Successfully - Pre Weight Queue No. is:" + preQueNo;
                    return;
                }
                //}
                this.btnSave.Enabled = false;
                if (Session["CATranNo"] != null)
                {
                   // Response.Redirect("ListInbox.aspx");
                }
                //LoadData();

            }
            else
            {
                this.lblMsg.Text = "Unable to Updat Data.";
            }

        }
        private void LoadData()
        {
            string str = "";
            Guid Id;
            if (Session["GRID"] != null)
            {
                str = Session["GRID"].ToString();
            }
            if (str == "")
            {
                this.lblMsg.Text = "Unable to load data please try again.";
                return;
            }
            try
            {
                Id = new Guid(str);
                this.hfId.Value = Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception ("Unable to Update Data.", ex);
            }
            GradingResultBLL obj = new GradingResultBLL();
            obj = obj.GetGradingResultById(Id);
            
            if (obj != null)
            {
                this.cboGradingRecivedStatus.SelectedValue = ((int)obj.Status).ToString();
            }
            lblGradingReceivedDate.Text = obj.GradeRecivedTimeStamp.ToShortDateString();
            cmpSampGen.ValueToCompare = obj.GradeRecivedTimeStamp.ToShortDateString();
            if (obj.GradingCode != null)
            {
                this.lblGradeCode.Text = obj.GradingCode;
            }
            this.lblClient.Text = "";
            string cg = "";
            CommodityDepositeRequestBLL obCD = new CommodityDepositeRequestBLL();
            obCD = obCD.GetCommodityDepositeDetailById(obj.CommodityDepositRequestId);
            ClientBLL objClient = new ClientBLL();
            objClient = ClientBLL.GetClinet(obCD.ClientId);
            if (objClient != null)
            {
                this.lblClient.Text = objClient.ClientName;
            }
            else
            {
                this.lblMsg.Text = "Unable to load client Data.";
            }
           
            cg = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
            this.lblCommodityGrade.Text = cg;
            if (((int)obj.Status == 3 || (int)obj.Status == 4) && obj.ClientAcceptanceTimeStamp != null)
            {
                this.cboAcceptanceStatus.SelectedValue = ((int)obj.Status).ToString();
              
                DateTime dtCA = DateTime.Now ;
                if (obj.ClientAcceptanceTimeStamp == null)
                {
                    this.cboAcceptanceStatus.SelectedIndex = -1;
                }
                
                dtCA = (DateTime)obj.ClientAcceptanceTimeStamp;
                this.txtDateOfAcceptance.Text = dtCA.Date.ToShortDateString();
                this.txtTimeodAcceptance.Text = dtCA.ToLongTimeString();
                this.cboGradingRecivedStatus.SelectedValue = ((int)obj.GradingResult).ToString();
            }
            
        }



        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnSave")
            {
                cmd.Add(this.btnSave);
               
            }
            else if (name == "cboAcceptanceStatus")
            {
                cmd.Add(this.cboAcceptanceStatus);
               
            }
            return cmd;
        }

        #endregion

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["rptGradingResultId"] = this.hfId.Value;
            ScriptManager.RegisterStartupScript(this,
                   this.GetType(),
                   "ShowReport",
                   "<script type=\"text/javascript\">" +
                       string.Format("javascript:window.open(\"rptGradingResultReport.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                   "</script>",
                   false);
            
        }
    }
}