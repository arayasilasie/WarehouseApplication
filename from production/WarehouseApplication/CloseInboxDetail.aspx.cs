using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class CloseInboxDetail : System.Web.UI.Page
    {
        Guid WorkFlowID
        {
            get
            {
                return (Guid)ViewState["WorkFlowID"];
            }
        }

        Guid ArrivalID
        {
            get
            {
                return (Guid)ViewState["ArrivalID"];
            }
        }

         int StepID
        {
            get
            {
                return (int)ViewState["StepID"];
            }
        }

         Guid Id
         {
             get
             {
                 return (Guid)ViewState["Id"];
             }
         }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (Request.QueryString["StepID"] != null)
            {
                lblCloseDetail.Text = Session["Task"].ToString();
                BindInboxDetail();
            }
        }

        void BindInboxDetail()
        {
            /* in arrival WorkflowID and Id are the same 
              but in case of sampling and Grading Id is sampling or grading Id */
                   
            ViewState["StepID"] =  int.Parse(Request.QueryString["StepID"]); 
            ViewState["Id"] =  Guid.Parse(Request.QueryString["Id"]);
            ViewState["WorkFlowID"] = Guid.Parse(Request.QueryString["WorkFlowID"]);

            DataRow dr = InboxModel.GetInboxDetailForClosingModified(WorkFlowID, StepID, Id);
            if (dr != null)
            {
                lblClient.Text = dr["ClientName"].ToString();
                lblTrackingNo.Text = dr["TrackingNumber"].ToString();        
                lblSamplingCode.Text = dr["SampleCode"].ToString();
                lblNewTrackingNo.Text = dr["SamplingTrackingNumber"].ToString();
                lblGradingCode.Text = dr["GradingCode"].ToString();              
                //lblGRNNo.Text = dr["GRNNumber"].ToString();
               // ViewState["WorkFlowID"] = new Guid(dr["WorkFlowID"].ToString());
                ViewState["ArrivalID"] = new Guid(dr["ArrivalID"].ToString());

                // show SamplingCode and GradingCode 
                if (StepID > 1)
                {
                    if (lblTrackingNo.Text != lblNewTrackingNo.Text)
                        divNewTrackingNo.Visible = true;
                    divSamplingCode.Visible = true;
                    if (StepID > 4)
                    {
                        divGradingCode.Visible = true;
                    }
                }

            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (StepID > 1)
                    ViewState["ArrivalID"]= Id;             

                InboxModel.Close_WorkflowModified(WorkFlowID, UserBLL.CurrentUser.UserId, DateTime.Now, ArrivalID, txtReason.Text, StepID);
                Messages1.SetMessage("Closed successfully.", Messages.MessageType.Success);
               // btnClose.Visible = false;              
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, Messages.MessageType.Error);
            }
            btnNext.Visible = true;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew2.aspx");

        }
    }
}