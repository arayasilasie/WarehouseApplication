using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditGradeDispute : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid Id = Guid.Empty;
            if (Session["EditGradeDisputeId"] != null)
            {
                try
                {
                    Id = new Guid(Session["EditGradeDisputeId"].ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Unable to get Grade Dispute Id.Please Try Again.";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Unable to get Grade Dispute Id.Please Try Again.";
                return;
            }
           
            if (IsPostBack != true)
            {
                LoadData(Id);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Guid Id = Guid.Empty;
            Guid ExpectedCommodityGradeId = Guid.Empty;
            DateTime DateTimeRequested;
            string Remark;
            int Status;
#region AcceptingData
            if (hfId.Value != null)
            {
                try
                {
                    Id = new Guid(hfId.Value.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Unable to load all data please try Again";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Unable to load all data please try Again";
                return;
            }
            if (hfGradingResultId.Value != "")
            {
                try
                {
                    Id = new Guid(hfGradingResultId.Value.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Unable to load all data please try Again";
                    return;
                }
            }
            if (this.cboCommodityGrade.SelectedValue  != "")
            {
                try
                {
                    ExpectedCommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Unable to load all data please try Again";
                    return;
                }
            }
            if (this.txtDateRecived.Text != "" && this.txtTimeRecived.Text != "")
            {
                try
                {
                    DateTimeRequested = Convert.ToDateTime(this.txtDateRecived.Text + " " + this.txtTimeRecived.Text);
                }
                catch
                {
                    this.lblMsg.Text = "Please check date Time recived";
                    return;
                }
            }
            else
            {
                    this.lblMsg.Text = "Please check date Time recived.";
                    return;
            }
            Remark = this.txtRemark.Text;
            string x;
            x =this.cboStatus.SelectedItem.Text;
            if( this.cboStatus.SelectedValue != "")
            {
                try
                {
                    Status = Convert.ToInt32(this.cboStatus.SelectedValue.ToString());
                }
                catch
                {
                    this.lblMsg.Text = "Please check Status.";
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "Please check Status.";
                return;
            }
            
#endregion
            GradingDisputeBLL objGradeDispute = new GradingDisputeBLL();
            GradingDisputeBLL objOld = (GradingDisputeBLL)ViewState["OldGradingDisputeBLL"];
            objGradeDispute.Id = Id;
            objGradeDispute.ExpectedCommodityGradeId = ExpectedCommodityGradeId;
            objGradeDispute.DateTimeRecived = DateTimeRequested;
            objGradeDispute.Remark = Remark;
            objGradeDispute.Status = Status;
            objGradeDispute.TrackingNo = this.hfTrackingNo.Value;
            bool isSaved = false;
            try
            {
                isSaved = objGradeDispute.Edit(objOld);
                if(isSaved == true)
                {
                    this.lblMsg.Text = "Update Sucessfull";
                    if (Session["EditGradeDisputeTranNo"] != null)
                    {
                        Response.Redirect("PageSwicther.aspx?TranNo=" + Session["EditGradeDisputeTranNo"].ToString());
                    }
                    else
                    {
                        if (objGradeDispute.Status == 2)
                        {
                            this.lblMsg.Text = "Unable to update data.";

                        }
                        else
                        {
                            this.lblMsg.Text = "Data Updated Successfully";
                            
                        }
                    }
                }
            }
            catch( Exception exc)
            {
                this.lblMsg.Text = exc.Message;
            }
        }
        private void LoadData(Guid Id)
        {
            Guid GradingId = Guid.Empty;
            GradingDisputeBLL obj = new GradingDisputeBLL();
            hfId.Value = Id.ToString();
            obj = obj.GetById(Id);
            ViewState["OldGradingDisputeBLL"] = obj;
            if (obj != null)
            {
                if (obj.GradingId != null)
                {
                    GradingId = obj.GradingId;
                    GradingBLL objgrading = new GradingBLL();
                    objgrading = objgrading.GetById(GradingId);
                    this.lblGradeCode.Text = objgrading.GradingCode.ToString();
                   
                }
                if (obj.TrackingNo != null)
                {
                    this.hfTrackingNo.Value = obj.TrackingNo;
                }
                if (obj.DateTimeRecived != null)
                {
                    this.txtDateRecived.Text = obj.DateTimeRecived.ToShortDateString();
                    this.txtTimeRecived.Text = obj.DateTimeRecived.ToLongTimeString();
                }
                
                this.cboStatus.SelectedValue = obj.Status.ToString();
                if (obj.Remark != null)
                {
                    this.txtRemark.Text = obj.Remark;
                }
                if (obj.PreviousCommodityGradeId != null)
                {
                    this.lblPreviousGrade.Text = CommodityGradeBLL.GetCommodityGradeNameById(obj.PreviousCommodityGradeId);
                }

                if (obj.ExpectedCommodityGradeId != null)
                {

                    
                    CommodityGradeBLL objCG = CommodityGradeBLL.GetCommodityGrade(obj.ExpectedCommodityGradeId);
                    CommodityGradeBLL objCC = CommodityGradeBLL.GetCommodityClassById(objCG.CommodityClassId);
                    CommodityGradeBLL objC = CommodityGradeBLL.GetCommodityById(objCC.CommodityId);
                    this.cboCommodity_CascadingDropDown.SelectedValue = objC.CommodityId.ToString();
                    this.cboCommodityClass_CascadingDropDown.SelectedValue = objCC.CommodityClassId.ToString();
                    this.cboCommodityGrade_CascadingDropDown.SelectedValue = obj.ExpectedCommodityGradeId.ToString();
                }
            }
            
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