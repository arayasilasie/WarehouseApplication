using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GradingBussiness;

namespace WarehouseApplication
{
    public partial class SampleCodeReceive : System.Web.UI.Page
    {
        private Guid GradingCodeID;
        private string GradingCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Elias Getachew
                //To integrate with Inbox
                this.GradingCode = string.Empty;
                this.GradingCode = Request.QueryString["GradingCode"].ToString();
                if (string.IsNullOrEmpty(this.GradingCode))
                {
                    //display error message
                    Messages.SetMessage("Please Log out and try again.", WarehouseApplication.Messages.MessageType.Error);
                    return;

                }
                //Get data from 
                GradingModel o = GradingModel.GetGradingInformationByGradingCode(GradingCode);
                if (o == null)
                {
                    Messages.SetMessage("Unable to find data with the selected Code.", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }


                
                lblDateCodedValue.Text = DateTime.Now.ToShortDateString(); //o.DateTimeCoded.ToShortDateString();
                lblGradingCodeValue.Text = o.GradingCode;
                ViewState["GradingID"] =  o.ID;
                this.GradingCodeID =  o.ID;
                this.Page.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(DoValid())
            ReciveCode();
        }
        public bool DoValid()
        {
            if (Convert.ToDateTime(txtDateRecived.Text.Trim()) < Convert.ToDateTime(lblDateCodedValue.Text))
            {
                Messages.SetMessage("Date Received cannot be less than Date Coded.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtDateRecived.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Date is required.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            if (txtTimeRecived.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Time is required.", WarehouseApplication.Messages.MessageType.Warning);
                return false;
            }
            return true;
        }
        private void ReciveCode()
        {
            GradingModel objcode = new GradingModel();
            if (ViewState["GradingID"] != null)
            {
                GradingCodeID = new Guid(ViewState["GradingID"].ToString());
            }
            objcode.ID = GradingCodeID;
            objcode.CodeReceivedBy = BLL.UserBLL.GetCurrentUser();
            try
            {
                objcode.CodeReceivedDateTime = DateTime.Parse(Convert.ToDateTime(txtDateRecived.Text).ToShortDateString() + " " + Convert.ToDateTime(txtTimeRecived.Text).ToShortTimeString());
            }
            catch (Exception ex)
            {
                Messages.SetMessage("Please enter Valid Date or Time", WarehouseApplication.Messages.MessageType.Error);
                return;
            }
            objcode.WarehouseId = BLL.UserBLL.GetCurrentWarehouse();
            objcode.UserId = BLL.UserBLL.GetCurrentUser();
            objcode.UpdateRecivedCode();
            btnNext.Visible = true;
            Messages.SetMessage("Data update Successfull.", WarehouseApplication.Messages.MessageType.Success);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }
    }
}