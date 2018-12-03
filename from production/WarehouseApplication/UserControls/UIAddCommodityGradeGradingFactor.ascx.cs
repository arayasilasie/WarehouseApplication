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
    public partial class UIAddCommodityGradeGradingFactor : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cboCommodityGrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GradingFactorBLL obj = new GradingFactorBLL();
            obj.CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
            
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