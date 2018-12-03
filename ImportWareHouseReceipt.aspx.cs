using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Data;

namespace WarehouseApplication
{
    public partial class ImportWareHouseReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ModelWRNO objImport = new ModelWRNO();
            if (DoValid(objImport))
            {
                set(objImport);
                save(objImport);
            }
        }
        public void set(ModelWRNO objImport)
        {            
            objImport.ID = Guid.NewGuid();
            objImport.WareHouseReceiptNo = Convert.ToInt32(txtWareHouseReceiptNo.Text);
            objImport.WarehouseID = UserBLL.GetCurrentWarehouse();
            objImport.Date =Convert.ToDateTime( txtDate.Text);
            objImport.Remark = txtRemark.Text;
            objImport.CreatedBy = UserBLL.GetCurrentUser();
            objImport.CreatedTimestamp = DateTime.Now;
        }
        public void save(ModelWRNO objImport)
        {
            objImport.Save();
            Messages.SetMessage("Successfully Saved...", WarehouseApplication.Messages.MessageType.Success);
        }
        public bool DoValid(ModelWRNO objImport)
        {
            DataTable dt = new DataTable();
            dt = objImport.CheckWRNo(Convert.ToInt32(txtWareHouseReceiptNo.Text));
            if (txtWareHouseReceiptNo.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("WareHouse Receipt No Required.", WarehouseApplication.Messages.MessageType.Error);
                return false;
            }
            else if(dt.Rows.Count>0)
            {
                Messages.SetMessage("Already Exist.", WarehouseApplication.Messages.MessageType.Error);
                return false;
            }          
            else if (txtDate.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Dsate Required.", WarehouseApplication.Messages.MessageType.Error);
                return false;
            }
            else if (txtRemark.Text.Trim() == string.Empty)
            {
                Messages.SetMessage("Remark Required.", WarehouseApplication.Messages.MessageType.Error);
                return false;
            }
            return true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }
    }
}