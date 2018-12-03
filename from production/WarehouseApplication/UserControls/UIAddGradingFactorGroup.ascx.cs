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
    public partial class UIAddGradingFactorGroup : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GradingFactorGroupBLL objGFG = new GradingFactorGroupBLL();
            objGFG.Id = Guid.NewGuid();
            if (this.txtGradingFactorName.Text == "")
            {
                this.lblMessage.Text = "Please enter Grading factor Group Name.";
                return;
            }
            else
            {
                objGFG.GradingFactorGroupName = txtGradingFactorName.Text;
            }
            if (cboStatus.SelectedValue == "")
            {
                this.lblMessage.Text = "Please Select Status.";
                return;
            }
            else
            {
                objGFG.Status = (GradingFactorGroupStatus)(int.Parse(cboStatus.SelectedValue.ToString()));
            }



            List<GradingFactorGroupDetailBLL> list = null;
            list = new List<GradingFactorGroupDetailBLL>();
            foreach (GridViewRow rw in this.gvGF.Rows)
            {
                //get the contorls  
                CheckBox chkSelected = (CheckBox)rw.FindControl("chkSelected");
                if (chkSelected.Checked == true)
                {
                    Label lblGradingFactorId = (Label)rw.FindControl("lblId");
                    Label lblGradingTypeId = (Label)rw.FindControl("lblGradingTypeId");
                    TextBox txtMaxValue = (TextBox)rw.FindControl("txtMaxValue");
                    TextBox txtMinValue = (TextBox)rw.FindControl("txtMinValue");
                    TextBox txtFailPoint = (TextBox)rw.FindControl("txtFailPoint");
                    DropDownList cboIsMax = (DropDownList)rw.FindControl("cboIsMax");
                    CheckBox chkInTotalValue = (CheckBox)rw.FindControl("chkInTotalValue");
                   
                    GradingFactorGroupDetailBLL o = new GradingFactorGroupDetailBLL();
                    o.Id = Guid.NewGuid();
                    o.GradingFactorId = new Guid(lblGradingFactorId.Text);
                    o.GradingTypeId = new Guid(lblGradingTypeId.Text);
                    try
                    {
                        o.MaximumValue = float.Parse(txtMaxValue.Text);
                    }
                    catch
                    {
                        o.MaximumValue = null;
                    }
                    try
                    {
                        o.MinimumValue = float.Parse(txtMinValue.Text);
                    }
                    catch
                    {
                        o.MinimumValue = null;
                    }
                    o.FailPoint = txtFailPoint.Text;
                    o.isMax = int.Parse(cboIsMax.SelectedValue.ToString());
                    o.isInTotalValue = chkInTotalValue.Checked;
                    o.CreatedBy = UserBLL.GetCurrentUser();
                    list.Add(o);
                }
            }
            bool issaved = false;
            issaved = objGFG.Save(list);
            if (issaved == true)
            {
                this.lblMessage.Text = "Data Updated Successfully.";
            }
            else
            {
                this.lblMessage.Text = "unable to  Updated Successfully.";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
            this.upGFGrids.Update();
        }
        private void Search()
        {
            List<GradingFactorBLL> list = null;
            this.gvGF.DataSource = list;
            this.DataBind();
            GradingFactorBLL obj = new GradingFactorBLL();
            Nullable<Guid> GFTID = null;
            if (this.cboSearchGradingFactorTypeId.SelectedValue != "")
            {
                GFTID = new Guid(this.cboSearchGradingFactorTypeId.SelectedValue);
            }
            Nullable<GradingFactorStatus> Status = null;
            if (this.cboSearchStatus.SelectedValue != "")
            {
                Status = (GradingFactorStatus)(int.Parse(this.cboSearchStatus.SelectedValue));
            }
            list = obj.Search(this.txtSearchGradingFactorName.Text, GFTID, Status);
            if (list != null)
            {
                ViewState["listGF"] = list;
                this.gvGF.DataSource = list;
                this.DataBind();
            }

        }

        protected void gvGF_PageIndexChanged(object sender, EventArgs e)
        {
            List<GradingFactorBLL> list = null;
         
            if (ViewState["listGF"] != null)
            {
                list = (List<GradingFactorBLL>)  ViewState["listGF"];
                this.gvGF.DataSource = list;
                this.DataBind();
            }
        }

        protected void gvGF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvGF.PageIndex = e.NewPageIndex;
        }

    }
}