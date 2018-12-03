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
    public partial class UIAddApprovedGRNCancelationRequest : System.Web.UI.UserControl,ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["ApprovedGRNCancelationID"] != null)
                {
                    Guid GRNId = new Guid(Session["ApprovedGRNCancelationID"].ToString());
                    hfGRNID.Value = Session["ApprovedGRNCancelationID"].ToString();
                    LoadData(GRNId);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
           
            obj.GRNId= new Guid(this.hfGRNID.Value.ToString());
            obj.RequestedBy = UserBLL.GetCurrentUser();
            obj.DateRequested = DateTime.Parse(this.txtDateRequested.Text);
            obj.Remark = this.txtRemark.Text;
            obj.Status = RequestforApprovedGRNCancelationStatus.New;
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
        private void LoadData(Guid GRNId)
        {
            GRNBLL objGRN = new GRNBLL();
            objGRN = objGRN.GetbyGRN_Number(GRNId);
            if (objGRN != null)
            {
                this.txtGRNNo.Text = objGRN.GRN_Number.ToString();
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