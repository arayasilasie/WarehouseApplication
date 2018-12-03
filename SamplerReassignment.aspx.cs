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
    public partial class SamplerReassignment : System.Web.UI.Page
    {
        static Guid CurrentWarehouse;
        static Guid OldSampler;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            CurrentWarehouse = new Guid(Session["CurrentWarehouse"].ToString());
            BindSampleCode();
        }

        public void BindSampleCode()
        {
            ddlSampleCode.DataSource = SamplerReassignmentModel.GetSamplingCodes(CurrentWarehouse);
            ddlSampleCode.DataTextField = "SampleCode";
            ddlSampleCode.DataValueField = "SampleCode";
            ddlSampleCode.DataBind();
        }

        public void BindOldSampler()
        {
            DataRow row = SamplerReassignmentModel.GetSamplersByCode(ddlSampleCode.SelectedValue);
            lblSampler.Text = row["Name"].ToString();
            OldSampler = new Guid(row["SamplerID"].ToString());
        }
        public void BindNewSamplers()
        {
            ddlNewSampler.Items.Clear();
            ddlNewSampler.Items.Add(new ListItem("Select New Sampler", ""));
            ddlNewSampler.DataSource = SamplerReassignmentModel.GetSamplersForReassingment(CurrentWarehouse, OldSampler,DateTime.Now);
            ddlNewSampler.DataTextField = "Name";
            ddlNewSampler.DataValueField = "ID";
            ddlNewSampler.DataBind();
        }

        protected void ddlSampleCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSampleCode.SelectedValue != string.Empty)
            {
                BindOldSampler();
                BindNewSamplers();
            }
            else
            {
                lblSampler.Text = "";
                ddlNewSampler.Items.Clear();
                ddlNewSampler.Items.Add(new ListItem("Select New Sampler", ""));
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SamplerReassignmentModel samplerReassimnt = new SamplerReassignmentModel();
            samplerReassimnt.ID = new Guid();
            samplerReassimnt.OldSampler = OldSampler;
            samplerReassimnt.NewSampler = new Guid(ddlNewSampler.SelectedValue);
            samplerReassimnt.SamplerName = ddlNewSampler.SelectedItem.Text;
            samplerReassimnt.SampleCode = ddlSampleCode.SelectedValue;
            samplerReassimnt.Remark = txtReason.Text;
            samplerReassimnt.CreatedBy = UserBLL.CurrentUser.UserId;
            samplerReassimnt.CreatedTimestamp = DateTime.Now;

            try
            {
                samplerReassimnt.InsertSamplerReassignment();
                Messages1.SetMessage("Record added successflly.", Messages.MessageType.Success);             
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, Messages.MessageType.Success);
            }
        }
    }
}