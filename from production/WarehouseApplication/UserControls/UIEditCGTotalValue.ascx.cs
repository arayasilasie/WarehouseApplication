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
    public partial class UIEditCGTotalValue : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["old"] == null)// new entry
            {
                CommodityGradeTotalValueBLL obj = new CommodityGradeTotalValueBLL();
                obj.Id = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                obj.CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                obj.MaxValue = float.Parse(this.txtMaxValue.Text);
                obj.MinValue = float.Parse(this.txtMinimumValue.Text);
                obj.Status = (CGTotalValueStatus)int.Parse(this.cboStatus.SelectedValue.ToString());
                if (obj.Update(null))
                {
                    ViewState["old"] = obj;
                    this.lblMsg.Text = "Data Updated Successfully";
                    this.btnSave.Enabled = false;
                }
                else
                {
                    this.lblMsg.Text = "Unable to update data please try again.";
                }

            }
            else
            {
                CommodityGradeTotalValueBLL obj = (CommodityGradeTotalValueBLL)ViewState["old"];
                CommodityGradeTotalValueBLL objOld = (CommodityGradeTotalValueBLL)ViewState["old"];
                obj.CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
                obj.MaxValue = float.Parse(this.txtMaxValue.Text);
                obj.MinValue = float.Parse(this.txtMinimumValue.Text);
                obj.Status = (CGTotalValueStatus)int.Parse(this.cboStatus.SelectedValue.ToString());
                if (obj.Update(objOld) == true)
                {
                    ViewState["old"] = null;
                    this.btnSave.Enabled = false;
                    this.lblMsg.Text = "Data updated Successfully";
                }
                else
                {
                    this.lblMsg.Text = "Unable to update data please try again.";
                }
            }
        }

        protected void cboCommodityGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid CommodityGradeId = Guid.Empty;
            CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
            if (CommodityGradeId == Guid.Empty)
            {
                this.btnSave.Enabled = false;
                this.lblMsg.Text = "Please Select commodity Grade";
                return;
            }
            CommodityGradeTotalValueBLL obj = new CommodityGradeTotalValueBLL();
            obj = obj.GetByCommodityGradeId(CommodityGradeId);
            if (obj == null)
            {
                this.lblMsg.Text = "No Data entered for this record.";
                ViewState["old"] = null;
                
            }
            else
            {
                ViewState["old"] = obj;
                this.txtMinimumValue.Text = obj.MinValue.ToString();
                this.txtMaxValue.Text = obj.MaxValue.ToString();
                this.cboStatus.SelectedValue = ((int)(obj.Status)).ToString();
            }

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            cmd.Add(this.btnSave);
            return cmd;
        }

        #endregion
    }
}