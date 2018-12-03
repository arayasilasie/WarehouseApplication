using System;
using System.Text;
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
    public partial class UIGetGradingCode : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RangeValidator1.MaximumValue = DateTime.Now.ToShortDateString();
           
            if (IsPostBack != true)
            {
                //GradingDate
                 Page.DataBind();
                this.txtDateCodeGenrated.Text = DateTime.Now.ToShortDateString();
               
                Session["Graders"] = null;

                if (Session["GradingCodeId"] != null && Session["GradingCodeTrackingNo"]!= null)
                {
                    hfTrackingNo.Value = Session["GradingCodeTrackingNo"].ToString();
                    BindData();
                    
                }
                else
                {
                    this.cboSampleCode.Enabled = false;
                    this.btnGenerateCode.Enabled = false;
                    this.lblMsg.Text = "Please try Again";
                    return;
                }
              
                if (Session["GenerateCodeSampleId"] != null)
                {
                    this.cboSampleCode.SelectedValue = Session["GenerateCodeSampleId"].ToString();
                }
                this.cboSampleCode.Enabled = false;
            }
        }

        protected void btnGenerateCode_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            this.lblMsg.Text = "";
            string WarehouseNo;
            try
            {
                 WarehouseNo = UserBLL.GetCurrentWarehouseCode();
            }
            catch
            {
                this.lblMsg.Text = "No warehouse code,please try agian.";
                return;
            }
            if (string.IsNullOrEmpty(WarehouseNo) == true)
            {
                this.lblMsg.Text = "No warehouse code,please try agian.";
                return;
            }
            if (string.IsNullOrEmpty(this.cboSampleCode.SelectedItem.Text) == true)
            {
                this.lblMsg.Text = "Please if the sample code exists";
                return;
            }
            
            string[] IdCollection = new string[3];
            IdCollection = this.cboSampleCode.SelectedValue.Split('/');
            Guid SamplingResultId = Guid.Empty;
            Guid ReceivigRequestId = Guid.Empty;
            try
            {
                 SamplingResultId = new Guid(IdCollection[0].ToString());
                 ReceivigRequestId = new Guid(IdCollection[1].ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please if the sample code exists";
                return;
            }
            string TrackingNo = IdCollection[2].ToString();
            DateTime DateCoded = DateTime.Parse(this.txtDateCodeGenrated.Text + " " + txtTimeArrival.Text);
            GradingBLL objGrading = new GradingBLL();
            if (SamplingResultId != Guid.Empty)
            {
                objGrading.SamplingResultId = SamplingResultId;
            }
            else
            {
                this.lblMsg.Text = "Please if the sample code exists";
                return;
            }
            if (ReceivigRequestId != Guid.Empty)
            {
                objGrading.CommodityRecivingId = ReceivigRequestId;
            }
            else
            {
                this.lblMsg.Text = "Please if the sample code exists";
                return;
            }
            objGrading.Status = GradingStatus.Coded;
            objGrading.TrackingNo = TrackingNo;
            objGrading.CreatedBy = UserBLL.GetCurrentUser();
            objGrading.DateCoded = DateCoded;
            List<GradingByBLL> list = new List<GradingByBLL>();
            list = (List<GradingByBLL>)Session["Graders"];
            if (list == null)
            {
                this.lblMsg.Text = "Please provide graders.";
                return;
            }
            if (list.Count <= 0)
            {
                this.lblMsg.Text = "Please provide graders.";
                return;
            }
            if (isSingleSupervisorGrader(list) == false)
            {
               
                return;
            }
            Guid CommodityId = Guid.Empty;
            CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
            objCDR = objCDR.GetCommodityDepositeDetailById(ReceivigRequestId);
            if (GradingByBLL.IsNumberofGraderAcceptable(list.Count, objCDR.CommodityId) == false)
            {
                this.lblMsg.Text = "The number of graders selected is less than the minimum required.";
                return;
            }

            if (list == null || list.Count <= 0)
            {
                this.lblMsg.Text = "Please add graders before generating code.";
                return;
            }
            if (this.hfTrackingNo.Value == "" || this.hfTrackingNo.Value == null)
            {
                this.lblMsg.Text = "An error has occured.";
                return;
            }
            isSaved = objGrading.Add(WarehouseNo,this.hfTrackingNo.Value.ToString(), list);
            if (isSaved == true)
            {
                // TODO Update a GridView dataBind and print the Coding Ticket.
                this.lblMsg.Text = "Data Added Successfully.";
                CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
                Session["Graders"] = null;
                this.gvGrader.DataSource = null;
                this.gvGrader.DataBind();
                this.pnlGradingDetail.Visible = false;
                this.btnGenerateCode.Visible = false;
                LoadCode(objGrading.SamplingResultId);
            }
            else
            {
                this.lblMsg.Text = "Data can not be saved. Please check the data entered and try again.If the error persists, conatact the IT support.";
            }


        }

        protected void cboSampleCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BindData()
        {

            this.cboSampleCode.Items.Clear();
            // Load Combo with samplesPendingCoding 
            List<SamplingResultBLL> list = new List<SamplingResultBLL>();
            SamplingResultBLL obj = new SamplingResultBLL();
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            string strSC = Session["GradingCodeId"].ToString();
            list = obj.GetSamplesResultsPendingCoding(WarehouseId, strSC);
            this.cboSampleCode.Items.Add(new ListItem("Please Select Sample Code", String.Empty));
            this.cboSampleCode.AppendDataBoundItems = true;
            //GradingDate
            
            if (list != null)
            {
               int flag = 0;
                foreach (SamplingResultBLL i in list)
                {
                    
                    string TempId = i.Id.ToString() + "/" + i.ReceivigRequestId.ToString()+ "/" + i.TrackingNo;
                    this.cboSampleCode.Items.Add(new ListItem(i.SamplingResultCode.ToString(), TempId));
                    if (Session["GradingCodeId"] == null)
                    {
                        this.lblMsg.Text = "your Session has expired";
                        return;
                    }
                    if (Session["GradingCodeId"].ToString().ToUpper() == i.SamplingResultCode.ToString().ToUpper())
                    {
                        this.cboSampleCode.SelectedValue = TempId;
                        //GradingDate
                       this.txtSamplingResultDate.Text =  i.ResultReceivedDateTime.ToShortDateString();
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    this.cboSampleCode.SelectedIndex = -1;
                }
               
            }
            else
            {
                this.lblMsg.Text = "There are no samples Pending coding.";
                return;
            }
            List<UserBLL> listEmp = new List<UserBLL>();
            try
            {
                listEmp = UserRightBLL.GetUsersWithRight("Grader");
            }
            catch
            {
                this.lblMsg.Text = "No Graders for the warehouse";
                return;
            }
            

            if (listEmp != null && listEmp.Count > 0)
            {
                this.cboGrader.Items.Clear();
                this.cboGrader.AppendDataBoundItems = true;
                this.cboGrader.Items.Add(new ListItem("Please Select Grader.", ""));
                listEmp.Sort((x, y) => string.Compare(x.UserName, y.UserName));
                foreach (UserBLL o in listEmp)
                {

                    this.cboGrader.Items.Add(new ListItem(o.FullName, o.UserId.ToString()));
                }
                this.cboGrader.AppendDataBoundItems = false;
            }
            else
            {
                throw new Exception("No Graders Avalaible for the warehouse");
            }


        }

        
        protected void btnAddGrader_Click(object sender, EventArgs e)
        {
            this.lblMsg.Text = "";
           
                List<GradingByBLL> list = new List<GradingByBLL>();
                if (Session["Graders"] != null)
                {
                list = (List<GradingByBLL>)Session["Graders"];
                }
            //check Count Number of graders required.
                GradingByBLL objgrader = new GradingByBLL();
                objgrader.Id = Guid.NewGuid();
                try
                {
                    int index;
                    objgrader.GraderName = this.cboGrader.SelectedItem.ToString();
                    objgrader.UserId = new Guid(this.cboGrader.SelectedValue.ToString());
                    objgrader.IsSupervisor = this.chkIsSupervisor.Checked;
                    index = int.Parse(this.cboGrader.SelectedIndex.ToString());
                    list.Add(objgrader);
                    Session["Graders"] = list;
                    this.gvGrader.DataSource = list;
                    this.gvGrader.DataBind();
                    this.cboGrader.Items.RemoveAt(index);
                    this.cboGrader.SelectedIndex = -1;
                    if (this.chkIsSupervisor.Checked == true)
                    {
                        this.chkIsSupervisor.Checked = false;
                    }
                   
                }
                catch
                {
                    this.lblMsg.Text = "Please select Grader.";
                }

              

            
        }

        protected void gvGrader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.lblMsg.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rw = this.gvGrader.Rows[index];
            if (e.CommandName == "Remove")
            {
                List<GradingByBLL> list = new List<GradingByBLL>();
                list = (List<GradingByBLL>)Session["Graders"];
                GradingByBLL obj = new GradingByBLL();
                obj = list[index];
                this.cboGrader.AppendDataBoundItems = true;
                this.cboGrader.Items.Add(new ListItem(obj.GraderName,obj.UserId.ToString()));
                this.cboGrader.AppendDataBoundItems = false;
                list.RemoveAt(index);
                this.gvGrader.DataSource = list;
                this.gvGrader.DataBind();
                Session["Graders"] = list;
            }
        }

        public bool isSingleSupervisorGrader(List<GradingByBLL> list)
        {
           
            int SupCount = 0;
            foreach(GradingByBLL sup in list)
            {
                if (sup.IsSupervisor == true)
                {
                    SupCount++;
                }
            }
            if (SupCount > 1)
            {
                this.lblMsg.Text = "Only one Supervisor is allowed";
                return false;
            }
            else if (SupCount < 1)
            {
                this.lblMsg.Text = "Atleast one Supervisor is required";
                return false;
            }
            return true;
        }

        protected void gvCodeGenerated_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            this.lblMsg.Text = "";
            if (e.CommandName == "Print")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvCodeGenerated.Rows[index];
                Label lblId = (Label) rw.FindControl("lblId");
                Guid Id = Guid.Empty;
                try
                {
                    Id = new Guid(lblId.Text);
                    if (Id != Guid.Empty)
                    {
                         GradingBLL objCodeReport = new GradingBLL();
                         objCodeReport = objCodeReport.GetById(Id);
                         Session["CodeReport"] = objCodeReport;
                         List<GradingByBLL> list = new List<GradingByBLL>();
                         GradingByBLL objGraders = new GradingByBLL();
                         list = objGraders.GetByGradingId(objCodeReport.Id);
                         GradingByCollection objGraderCollection = new GradingByCollection(list);
                         Session["GradersCodeReport"] = objGraderCollection;
                         StringBuilder sb = new StringBuilder();
                         sb.Append("<script>");
                         sb.Append("window.open('rptCoffeeCode.aspx");
                         sb.Append("', '', 'toolbar=0');");
                         sb.Append("</scri");
                         sb.Append("pt>");
                         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowReport",
                            sb.ToString(), false);
                    }
                }
                catch( Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void LoadCode(Guid SamplingResultId)
        {
            List<GradingBLL> list = new List<GradingBLL>();
            GradingBLL obj = new GradingBLL();
            list = obj.GetGradingBySamplingResultId(SamplingResultId);
            if (list == null || list.Count < 0)
            {
                this.lblMsg.Text = "No codes generated.";
            }
            else
            {
                this.gvCodeGenerated.DataSource = list;
                this.gvCodeGenerated.DataBind();
            }
        }



        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            cmd.Add(this.btnGenerateCode);
            return cmd;
        }

        #endregion

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
    }
}