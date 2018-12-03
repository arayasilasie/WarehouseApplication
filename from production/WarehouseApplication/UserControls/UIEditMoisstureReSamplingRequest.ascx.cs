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
using WarehouseApplication.BLL;
using System.Collections.Generic;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditMoisstureReSamplingRequest : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                Guid Id = Guid.Empty;
                if (Session["ResamplingEdit"] == null)
                {
                    this.lblmsg.Text = "Please Try Again.";
                    return;
                }
                if (Session["ResamplingEdit"].ToString() == "")
                {
                    this.lblmsg.Text = "Please Try Again.";
                    return;
                }
                else
                {
                    this.hfId.Value = Session["ResamplingEdit"].ToString();
                    try
                    {
                        Id = new Guid(Session["ResamplingEdit"].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.lblmsg.Text = ex.Message;
                    }
                    if (Id != Guid.Empty)
                    {
                        ReSamplingBLL objResampling = new ReSamplingBLL();
                        objResampling = objResampling.GetById(Id);
                        this.lblSamplingCode.Text = objResampling.SampleCode.ToString();
                        this.cboStatus.SelectedValue = ((int)objResampling.Status).ToString();
                        this.txtDate.Text = objResampling.DateTimeRequested.ToShortDateString();
                        this.txtTime.Text = objResampling.DateTimeRequested.ToLongTimeString();
                        this.hfTrackingNo.Value = objResampling.TrackingNo;
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            ReSamplingBLL obj = new ReSamplingBLL();
            obj.Id = new Guid(this.hfId.Value);
            try
            {
                obj.DateTimeRequested = DateTime.Parse(this.txtDate.Text + " " + this.txtTime.Text);
            }
            catch
            {
                this.lblmsg.Text = "Invalid Date.";
                return;
            }
          
           
            int intStatus = int.Parse(this.cboStatus.SelectedValue);
            if (intStatus == 1)
            {
                obj.Status = ReSamplingStatus.New;
            }
            else if (intStatus == 2)
            {
                obj.Status = ReSamplingStatus.Approved;
            }
            else
            {
                obj.Status = ReSamplingStatus.Cancelled;
            }
            
            obj.LastModifiedBy = UserBLL.GetCurrentUser();
            obj.TrackingNo = this.hfTrackingNo.Value.ToString();
            try
            {
                isSaved = obj.Update();
            }
            catch ( Exception ex)
            {
                this.lblmsg.Text = ex.Message;
            }
            if (isSaved == true)
            {
                Response.Redirect("ListInbox.aspx");
            }
            else
            {
                this.lblmsg.Text = "Unable to update Data";
            }

        }

        #region ISecurityConfiguration Members

       
        #endregion

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnSave")
            {
                cmd.Add(this.btnSave);
            }
            return cmd;
                 
        }

        #endregion
    }
}