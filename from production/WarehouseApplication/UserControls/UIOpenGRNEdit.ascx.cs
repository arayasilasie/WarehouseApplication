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
    public partial class UIOpenGRNEdit : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["OpenGRNEditId"] == null)
                {
                    throw new Exception("Your session has expired");
                }
                else
                {
                    Guid Id = new Guid(Session["OpenGRNEditId"].ToString());
                    LoadData(Id);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            RequestforEditGRNBLL objOld = (RequestforEditGRNBLL)ViewState["oldObject"];
            if (objOld == null)
            {
                this.lblMessage.Text = "An error has occured please try agin";
                return;
            }
            RequestforEditGRNBLL obj = new RequestforEditGRNBLL();
            obj.Id = new Guid( hfId.Value.ToString());
            obj.GRNId = new Guid(hfGRNID.Value.ToString());
            obj.Status = (RequestforEditGRNStatus)(int.Parse(this.cboStatus.SelectedValue));
            obj.Remark = this.txtRemark.Text;
            obj.DateRequested = DateTime.Parse( this.txtDateRequested.Text);
            obj.TrackingNo = hfTrackingNo.Value.ToString();
            obj.GRN_Number = this.txtGRNNo.Text;

            RequestforEditGRNStatus oldStatus = (RequestforEditGRNStatus)(int.Parse(this.hfOriginalStatus.Value.ToString())) ;
            isSaved = obj.AllowGRNEdit(oldStatus,objOld);
            if (isSaved == true)
            {
                this.lblMessage.Text = "Data updated Successfully.";
                this.btnEdit.Enabled = false;
                return;
            }
            else
            {
                this.lblMessage.Text = "Unable to Update the record please try again.";           
                return;
            }

        }
        private void LoadData(Guid Id)
        {
            RequestforEditGRNBLL obj = new RequestforEditGRNBLL();
            obj = obj.GetById(Id);
            ViewState["oldObject"] = obj;
            if (obj != null)
            {
                obj.Id = Id;
                hfId.Value = Id.ToString();
                if (obj.GRNId != null)
                {
                    hfGRNID.Value = obj.GRNId.ToString();
                }
                if (obj.GRN_Number != null)
                {
                    this.txtGRNNo.Text = obj.GRN_Number;
                }
                if (obj.DateRequested != null)
                {
                    this.txtDateRequested.Text = obj.DateRequested.ToString();
                }
                if (obj.Remark != null)
                {
                    this.txtRemark.Text = obj.Remark;
                }
                hfTrackingNo.Value = obj.TrackingNo;
                hfOriginalStatus.Value = ((int)obj.Status).ToString();
                this.cboStatus.SelectedValue = ((int)obj.Status).ToString();

            }
        }
    
#region ISecurityConfiguration Members

public List<object>  GetSecuredResource(string scope, string name)
{
    List<object> cmd = null;
    if (name == "btnEdit")
    {
        cmd = new List<object>();
        cmd.Add(this.btnEdit);
        
    }
    return cmd;
}

#endregion
}
}