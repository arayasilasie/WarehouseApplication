using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.SECManager;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddCommodityGradingFactor : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                GradingFactorGroupBLL obj = new GradingFactorGroupBLL();
                List<GradingFactorGroupBLL> list = obj.GetActive();
                foreach(GradingFactorGroupBLL o in list)
                {
                    this.cboGradingFactorName.Items.Add(new ListItem(o.GradingFactorGroupName, o.Id.ToString()));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            CommodityGradingFactorBLL obj = new CommodityGradingFactorBLL();
            obj.Id = Guid.NewGuid();
            if (this.chkIsCommodity.Checked == true)
            {
                obj.CommodityId = new Guid(this.cboCommodity.SelectedValue.ToString());
            }
            else
            {
                obj.CommodityId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
            }
            obj.GradingFactorGroupId = new Guid(this.cboGradingFactorName.SelectedValue.ToString());
            obj.Status = (CommodityGradingFactorStatus)(int.Parse(this.cboStatus.SelectedValue.ToString()));
            obj.CreatedBy = UserBLL.GetCurrentUser();
            obj.isForCommodity = this.chkIsCommodity.Checked;
            if (obj.Save() == true)
            {
                this.lblMessage.Text = "Data updated Successfully.";
            }
            else
            {
                this.lblMessage.Text = "Unable to update data.";
            }
        }
        protected void cboCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> list = null;
            if (name == "btnSave")
            {
                list = new List<object>();
                list.Add(this.btnSave);
            }
            return list;
        }

        #endregion

        protected void gvGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           
            

        }
        private void Search()
        {
            List<CommodityGradingFactorBLL> list = new List<CommodityGradingFactorBLL>();
            CommodityGradingFactorBLL obj = new CommodityGradingFactorBLL();
            if (this.cboSearchCommodityGrade.SelectedValue != "")
            {
                list = obj.GetByCommodityId(new Guid(this.cboSearchCommodityGrade.SelectedValue.ToString()));
            }
            else if (this.cboSearchCommodity.SelectedValue != "")
            {
                list = obj.GetByCommodityId(new Guid(this.cboSearchCommodity.SelectedValue.ToString()));
            }
            this.gvGroup.DataSource = list;
            this.gvGroup.DataBind();
        }

        protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdCancel")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvGroup.Rows[index];
                Label lblId = (Label)rw.FindControl("lblId");
                if (lblId != null)
                {
                    Guid Id = new Guid(lblId.Text);
                    CommodityGradingFactorBLL obj = new CommodityGradingFactorBLL();
                    if (obj.Inactive(Id) == true)
                    {
                        this.lblMessage.Text = "Data Updated Successfully";
                        this.gvGroup.EditIndex = -1;
                        Search();
                    }
                    else
                    {
                        this.lblMessage.Text = "Unable to Update Data";
                    }

                }
            }
        }
 
        
    }
}