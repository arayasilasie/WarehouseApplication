using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class EditSamplerAttendance : System.Web.UI.Page
    {
        bool? IsNew
        {
            get
            {
                return (bool)ViewState["IsNew"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindSamlerGridviewForEdit();
        }
        public void BindSamplersGridview()
        {
            grvSamplersAttenendance.DataSource = SamplerAttendaceModel.GetSamplers(new Guid(Session["CurrentWarehouse"].ToString()));
            grvSamplersAttenendance.DataBind();
        }
        public void BindSamlerGridviewForEdit()
        {
           DataTable dtbl= SamplerAttendaceModel.GetSamplersAttendance(new Guid(Session["CurrentWarehouse"].ToString()), DateTime.Now);
           
            if (dtbl.Rows.Count != 0) 
           {
               ViewState.Add("IsNew", false);
               grvSamplersAttenendance.DataSource = dtbl;
               grvSamplersAttenendance.DataBind();
               btnAdd.Visible = false;
               lblHeader.Text = "EDIT SAMPLERS DAILY ATTENDANCE";
           }
           else
           {
               ViewState.Add("IsNew", true);
               BindSamplersGridview();
               btnAdd.Visible = true;
               lblHeader.Text = "SAMPLERS DAILY ATTENDANCE";
           }
        }
     
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string SamplersAttedanceXML = "<SamplersAttendance>";
            foreach (GridViewRow grvRow in grvSamplersAttenendance.Rows)
            {
                SamplersAttedanceXML +=
                    "<SamplerAttendanceItem>" +
                        "<OperatorID>" + ((Label)grvRow.FindControl("lblID")).Text + "</OperatorID>" +
                       "<Status>" + ((CheckBox)grvRow.FindControl("chbIsAvailable")).Checked + "</Status>" +
                        "<Reason>" + "Daily Attendance" + "</Reason>" +
                        "<CreatedBy>" + UserBLL.CurrentUser.UserId + "</CreatedBy>" +
                        "<CreatedTimestamp>" + DateTime.Now + "</CreatedTimestamp>" +
                        "<OperationDate>" + DateTime.Now + "</OperationDate>" +
                        "<WarehouseID>"+Session["CurrentWarehouse"].ToString()+ "</WarehouseID>"+
                    "</SamplerAttendanceItem>";
            }
            SamplersAttedanceXML += "</SamplersAttendance>";

            try
            {
                SamplerAttendaceModel.AddSamplersAttendance(SamplersAttedanceXML);
                Messages1.SetMessage("Record added successfully.", WarehouseApplication.Messages.MessageType.Success);
                BindSamlerGridviewForEdit();
                btnAdd.Visible = false;
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);            
            }
        }
       
        protected void grvSamplersAttenendance_SelectedIndexChanged(object sender, EventArgs e)
        { 
            Guid ID = new Guid(((Label)grvSamplersAttenendance.SelectedRow.FindControl("lblID")).Text);
            bool Status = ((CheckBox)grvSamplersAttenendance.SelectedRow.FindControl("chbIsAvailable")).Checked;
            string reason = ((TextBox)grvSamplersAttenendance.SelectedRow.FindControl("txtReason")).Text;
            Guid LastModifiedBy = UserBLL.CurrentUser.UserId;
            DateTime LastModifiedDate = DateTime.Now;
            if (reason == "")
            {
                Messages1.SetMessage("Please enter reason", WarehouseApplication.Messages.MessageType.Warning);            
            }
            else
            {
                try
                {
                    SamplerAttendaceModel.UpdateSamplersAttendance(ID, Status, LastModifiedBy, LastModifiedDate, reason);
                    Messages1.SetMessage("Record updated successflly. ", WarehouseApplication.Messages.MessageType.Success);
                    BindSamlerGridviewForEdit();

                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                }
            }
        }

        protected void grvSamplersAttenendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // if it is new hide update and reason column
            if ((bool)ViewState["IsNew"])
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;  
 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //CheckBox childchk = (CheckBox)e.Row.Cells[1].FindControl("chkBxSelect");
                //CheckBox headerchk = (CheckBox)this.grvSamplersAttenendance.HeaderRow.FindControl("chkBxHeader");
                //childchk.Attributes.Add("onclick", "Selectchildcheckboxes('" + headerchk.ClientID + "')");
            }           
            }
            else
            {
                if(e.Row.RowType == DataControlRowType.Header)
                    ((CheckBox)e.Row.FindControl("chkSelectAll")).Visible = false;
            }           
        }

        protected void grvSamplersAttenendance_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

    }
}