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
    public partial class UIAbsentTrucks : System.Web.UI.UserControl ,  ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                LoadData();
            }
        }

        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvDetail.Rows[index];
                Label id = (Label)rw.FindControl("lblId");
                DropDownList cboSt = (DropDownList)rw.FindControl("cboRemark");
                TrucksMissingOnSamplingBLL obj = new TrucksMissingOnSamplingBLL();
                obj.Id = new Guid(id.Text);
                obj.Status = (TrucksMissingOnSamplingStatus)(int.Parse(cboSt.SelectedValue));
                if (obj.UpdateStatus() == true)
                {
                    this.lblMessage.Text = "Data Updated Successfully";
                    Response.Redirect("ConfirmTrucksForSampling.aspx");
                    

                }
                else
                {
                    this.lblMessage.Text = "Unable to update data please try agin";
                }


            }
        }
        public void LoadData()
        {
            Guid WarehouseId = Guid.Empty;
            WarehouseId = UserBLL.GetCurrentWarehouse();
            if (WarehouseId != Guid.Empty)
            {
                TrucksMissingOnSamplingBLL obj = new TrucksMissingOnSamplingBLL();
                List<TrucksMissingOnSamplingBLL> list = null;
                list = obj.GetAbsentTrucks(WarehouseId);
                this.gvDetail.DataSource = list;
                this.gvDetail.DataBind();
            }
        }
        

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "cmdEdit")
            {
                foreach (TableRow row in this.gvDetail.Rows)
                {
                    cmd = new List<object>();
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdEdit"));
                   
                }
            }
            return cmd;
        }

        #endregion
    }
}