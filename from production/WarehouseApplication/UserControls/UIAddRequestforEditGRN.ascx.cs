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
    public partial class UIAddRequestforEditGRN : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["GRNIDRequestCD"] != null)
                {
                    Guid GRNId = new Guid(Session["GRNIDRequestCD"].ToString());
                    hfGRNID.Value = Session["GRNIDRequestCD"].ToString();
                    LoadData(GRNId);
                }
            }
        }
        private void LoadData(Guid GRNId)
        {
            GRNBLL objGRN = new GRNBLL();
            objGRN = objGRN.GetbyGRN_Number(GRNId);
            if (objGRN != null)
            {
                this.txtGRNNo.Text = objGRN.GRN_Number.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            RequestforEditGRNBLL obj = new RequestforEditGRNBLL();
            obj.GRNId = new Guid(this.hfGRNID.Value.ToString());
            obj.RequestedBy = UserBLL.GetCurrentUser();
            obj.DateRequested = DateTime.Parse(this.txtDateRequested.Text);
            obj.Remark = this.txtRemark.Text;
            obj.Status = RequestforEditGRNStatus.New;
            isSaved = obj.Add();
            if (isSaved == true)
            {
                this.lblMessage.Text = "Data Added Successfully.";
                this.btnAdd.Enabled = false;
            }
            else
            {
                this.lblMessage.Text = "Data Can't be Added.please check the data and try again";
                return;
            }

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnAdd")
            {
                cmd = new List<object>();
                cmd.Add(this.btnAdd);
            }
            return cmd;
        }

        #endregion
    }
}