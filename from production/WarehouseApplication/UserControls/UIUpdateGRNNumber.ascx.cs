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
    public partial class UIUpdateGRNNumber : System.Web.UI.UserControl ,ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GRNIDUpdateGRNNo"] != null)
            {
                if (Session["TrackingNoUpdateGRNNo"] != null)
                {
                    ViewState["TrackingNo"] = Session["TrackingNoUpdateGRNNo"].ToString();
                }
                else
                {
                    ViewState["TrackingNo"] = -1;
                }
                Session["TrackingNoUpdateGRNNo"] = null;
                Guid Id = new Guid(Session["GRNIDUpdateGRNNo"].ToString());
                Session["GRNIDUpdateGRNNo"] = null;
                GRNBLL objGRN = new GRNBLL();
                objGRN = objGRN.GetbyGRN_Number(Id);
                this.lblNewGRN.Text = objGRN.GRN_Number;
                ViewState["GRNId"] = objGRN.Id.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["GRNId"] != null)
            {
                GRNBLL obj = new GRNBLL();
                bool isSaved = false;
                isSaved = obj.UpdateGRNNumber(new Guid(ViewState["GRNId"].ToString()),
                    this.txtOldSystemGRNNo.Text, lblNewGRN.Text, ViewState["TrackingNo"].ToString());
                if (isSaved == true)
                {
                    this.lblMessage.Text = "Update Data Successfully.";
                    this.btnUpdate.Enabled = false;
                    return;
                }
                this.btnUpdate.Enabled = false;
            }
            else
            {
                this.lblMessage.Text = "Unable to update Data.Please try again";
                return;
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            cmd = new List<object>();
            cmd.Add(this.btnUpdate);
            return cmd;
        }

        #endregion
    }
}