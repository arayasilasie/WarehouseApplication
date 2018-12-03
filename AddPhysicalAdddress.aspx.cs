using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class AddPhysicalAdddress : System.Web.UI.Page
    {
        static PhysicalAdddressModel phsicalAddress = new PhysicalAdddressModel();
        static DataTable dtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindShed();
        }

        public void BindShed()
        {
            DataTable sheds = StackModel.GetWarehouseSheds(new Guid(Session["CurrentWarehouse"].ToString()));
            ddlShed.DataSource = sheds;
            ddlShed.DataTextField = "ShedNumber";
            ddlShed.DataValueField = "ID";
            ddlShed.DataBind();

            ddlShedSearch.DataSource = sheds;
            ddlShedSearch.DataTextField = "ShedNumber";
            ddlShedSearch.DataValueField = "ID";
            ddlShedSearch.DataBind();
        }

        public void BindPhyscalAddressGrisview()
        {
            dtbl = StackModel.GetPhysicalAddresses(new Guid(ddlShedSearch.SelectedValue));
            grvPhysicalAddress.DataSource = dtbl;
            grvPhysicalAddress.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (InitializePhysicalAddress())
            {
                try
                {
                    phsicalAddress.InsertPhysicalAdddress();
                    Messages1.SetMessage("Record added successfully.", WarehouseApplication.Messages.MessageType.Success);

                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                }
            }
        }

        bool IsValidAddress()
        {
            int num;
            float fnum;
            if (txtMaximumSize.Text == "" ||txtColumn.Text == "" || txtAddress.Text == "" || 
                txtWidth.Text == "" || txtHeight.Text == "" || ddlShed.SelectedValue == "")
            {
                Messages1.SetMessage("Please enter all values.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(int.TryParse(txtMaximumSize.Text, out  num)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }

            else if (!(float.TryParse(txtWidth.Text, out fnum)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(float.TryParse(txtLength.Text, out fnum)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(float.TryParse(txtHeight.Text, out fnum)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool InitializePhysicalAddress()
        {
            if (IsValidAddress())
            {
                phsicalAddress.ShedID = new Guid(ddlShed.SelectedValue);
                phsicalAddress.Row = txtRow.Text;
                phsicalAddress.Columun = txtColumn.Text;
                phsicalAddress.AddressName = txtAddress.Text;
                phsicalAddress.Width = float.Parse(txtWidth.Text);
                phsicalAddress.Height = float.Parse(txtHeight.Text);
                phsicalAddress.Length = float.Parse(txtLength.Text);
                phsicalAddress.Status = 1;
                phsicalAddress.MaximumSize = float.Parse(txtMaximumSize.Text); ////Need some calculation here 

                phsicalAddress.CreatedBy = UserBLL.CurrentUser.UserId;
                phsicalAddress.CreatedTimestamp = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPhyscalAddressGrisview();
        }
         
        protected void grvPhysicalAddress_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            grvPhysicalAddress.PageIndex = e.NewPageIndex;
            grvPhysicalAddress.DataSource = dtbl;
            int c=dtbl.Rows.Count;
            grvPhysicalAddress.DataBind();
        }

        protected void grvPhysicalAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PhysicalAdddressModel.CancelPhysicalAddress(new Guid(grvPhysicalAddress.SelectedDataKey[0].ToString()), UserBLL.CurrentUser.UserId, DateTime.Now);
                Messages1.SetMessage("Record cancelled successfully.", WarehouseApplication.Messages.MessageType.Success);
                BindPhyscalAddressGrisview();
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            }
        }
   
    }
}