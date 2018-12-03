using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddGradeDispute : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                this.LoadData();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Guid GradingId = Guid.Empty;
            if (this.cboGradingCode.SelectedValue != "")
            {
                try
                {
                    GradingId = new Guid(this.cboGradingCode.SelectedValue.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Please select Grading code.";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Please select Grading code.";
                return;
            }
           
            Guid GradingResultId = Guid.Empty;
            if (this.hfGradingResultId.Value != "")
            {
                try
                {
                    GradingResultId = new Guid(hfGradingResultId.Value.ToString());
                }
                catch
                {
                
                    this.lblMsg.Text = "Please select Grading code.";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Please select Grading code.";
                return;
            }
            Guid PrevioudCommodityGradeId = Guid.Empty;
            if (this.hfPreviousGradingResult.Value != "")
            {
                try
                {
                    PrevioudCommodityGradeId = new Guid(this.hfPreviousGradingResult.Value.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Please select Grading code.";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Please select Grading code.";
                return;
            }

            Guid ExpectedCommodityGradeId = Guid.Empty;
            if (this.cboCommodityGrade.SelectedValue != "")
            {
                try
                {
                    ExpectedCommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                }
                catch
                {
                }
            }
            else
            {
                this.lblMsg.Text = "Please select Commodity Grade.";
                return;
            }
            DateTime DateTimeRequested;
            if (this.txtDateRecived.Text != "" && this.txtTimeRecived.Text != "")
            {
                try
                {
                    DateTimeRequested = Convert.ToDateTime(this.txtDateRecived.Text.Trim() + " " + this.txtTimeRecived.Text.Trim());
                }
                catch
                {
                    this.lblMsg.Text = "Date format is not correct.";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Please enter date time.";
                return;
            }
            string Remark;
            Remark = this.txtRemark.Text;
            int Status;
            Status = 1;// new
            //add
            bool isSaved = false;
            GradingDisputeBLL objGradeDispute = new GradingDisputeBLL();
            objGradeDispute.Id = Guid.NewGuid();
            objGradeDispute.GradingId = GradingId;
            objGradeDispute.GradingResultId = GradingResultId;
            objGradeDispute.PreviousCommodityGradeId = PrevioudCommodityGradeId;
            objGradeDispute.ExpectedCommodityGradeId = ExpectedCommodityGradeId;
            objGradeDispute.DateTimeRecived = DateTimeRequested;
            objGradeDispute.Remark = Remark;
            objGradeDispute.Status = Status;
            try
            {
                isSaved = objGradeDispute.Add();
                if (isSaved == true)
                {
                    this.lblMsg.Text = "Data added Successfully";
                    Clear();
                    if (Session["CoffeeRegradeTranNo"] != null)
                    {
                        Response.Redirect("PageSwicther.aspx?TranNo="+Session["CoffeeRegradeTranNo"].ToString());
                    }
                }
                else
                {
                    this.lblMsg.Text = "The record can not be added.";
                }
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = ex.Message;
            }



        }      
        protected void cboGradingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataAfterChange();
        }
        private void LoadData()
        {
            this.cboGradingCode.Items.Clear();
            List<GradingBLL> list = new List<GradingBLL>();
            GradingBLL objGrading = new GradingBLL();
            list = objGrading.GetGradingPenndingDispute();
            this.cboGradingCode.Items.Add(new ListItem("Please Select Grading code", ""));
            if (list != null)
            {
                if (list.Count > 0)
                {
                  
                    
                    foreach (GradingBLL i in list)
                    {
                        this.cboGradingCode.Items.Add(new ListItem(i.GradingCode.ToString(), i.Id.ToString()));
                    }
                }
            }
        }
        private void LoadDataAfterChange()
        {
            Guid Id;
            try
            {
                Id = new Guid(this.cboGradingCode.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "Please select Grading code";
                return;
            }
            // get previous Grading.
            GradingResultBLL obj = new GradingResultBLL();
            obj = obj.GetClientRejectedGradingResultByGradingId(Id);
            if (obj != null)
            {
                this.hfGradingResultId.Value = obj.ID.ToString();
                this.hfPreviousGradingResult.Value = obj.CommodityGradeId.ToString();
                this.lblPreviousGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
            }

        }
        private void Clear()
        {
            LoadData();
            this.lblPreviousGrade.Text = "";
            this.cboCommodity.SelectedIndex = -1;
            this.cboCommodityClass.SelectedIndex = -1;
            this.cboCommodityGrade.SelectedIndex = -1;
            this.txtDateRecived.Text = "";
            this.txtTimeRecived.Text = "";
            this.txtRemark.Text = "";
        }
        #region ISecurityConfiguration Members
        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnSave")
            {
                cmd = new List<object>();
                cmd.Add(this.btnSave);
            }
            return cmd;
        }
        #endregion
    }
}