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
    public partial class UIEditSamplingResult : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                LoadData();
                this.btnSave.Visible = false;
            }
           
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            
            // get from the Database.
            SamplingResultBLL obj = new SamplingResultBLL();
           
            // Load Controls 
            obj.Id = new Guid(this.txtId.Value.ToString());
            obj.EmployeeId = new Guid(this.cboSampler.SelectedValue.ToString());
            obj.NumberOfBags = Convert.ToInt32( this.txtNumberofbags.Text);
            //obj.NumberOfSeparations = Convert.ToInt32(this.txtNumberOfSeparations.Text);
            obj.SamplerComments = this.txtSamplerCommment.Text;
            obj.Remark = this.txtRemark.Text;
            obj.IsSupervisor = this.chkisSupervisor.Checked;
            obj.Status = (SamplingResultStatus)Convert.ToInt32( this.cboStatus.SelectedValue.ToString());
            isSaved = obj.Update();
            if (isSaved == true)
            {
                this.lblMsg.Text = "Data updated Successfully.";
                LoadData();
            }
            else
            {
                this.lblMsg.Text = "Unable to update data.";
            }
        }
        protected void txtSampler_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadSampler(Guid Id)
        {
            List<SamplerBLL> list = new List<SamplerBLL>();
            SamplerBLL obj = new SamplerBLL();
            list = obj.GetSamplerBySamplingId(Id);
            if (list != null )
            {
                this.cboSampler.Items.Clear();


                List<UserBLL> listE = UserRightBLL.GetUsersWithRight("Sampler");
                if (listE != null)
                {
                    foreach(UserBLL b in listE )
                    {
                        this.cboSampler.Items.Add(new ListItem(b.UserName, b.UserId.ToString()));
                    }
                    if (list != null)
                    {
                        this.cboSampler.SelectedValue = list[0].SamplerId.ToString();
                    }

                }
                else
                {
                    if (list != null)
                    {
                        foreach (SamplerBLL i in list)
                        {
                            this.cboSampler.Items.Add(new ListItem(UserBLL.GetName(i.SamplerId), i.SamplerId.ToString()));
                        }
                    }
                }

                


            }
        }
        private void LoadData()
        {
            // from Query String or smt
            if (Session["SamplingResultId"] != null)
            {
                this.txtId.Value = Session["SamplingResultId"].ToString();
            }

            // get from the Database.
            SamplingResultBLL obj = new SamplingResultBLL();
            obj = obj.GetSamplingResultById(new Guid(this.txtId.Value.ToString()));
            LoadSampler(obj.SamplingId);
            // Load Controls 
            this.txtSampleCode.Text = obj.SamplingResultCode.ToString();
            this.cboSampler.SelectedValue = obj.EmployeeId.ToString();
            this.txtNumberofbags.Text = obj.NumberOfBags.ToString();
            //this.txtNumberOfSeparations.Text = obj.NumberOfSeparations.ToString();
            this.txtSamplerCommment.Text = obj.SamplerComments;
            this.txtRemark.Text = obj.Remark;
            this.chkisSupervisor.Checked = obj.IsSupervisor;
            this.cboStatus.SelectedValue = ((int)obj.Status).ToString();
            List<SamplingResultBLL> listSR = new List<SamplingResultBLL>();
            SamplingResultBLL oSr = new SamplingResultBLL();
          


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