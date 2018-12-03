using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.BLL;
using WarehouseApplication.GINLogic;
using WarehouseApplication.SECManager;


namespace WarehouseApplication
{
    public partial class SelectWarehouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserBLL.Expire(UserBLL.CurrentUser.UserId);
            if (!IsPostBack)
            {
               
                ddlWarehouse.Items.Clear();
                ddlWarehouse.Items.AddRange(
                    (from warehouse in WarehouseBLL.GetAllActiveWarehouse()
                     select new ListItem() { Text = warehouse.WarehouseName, Value = warehouse.WarehouseId.ToString() }).ToArray());
                this.Page.Title = "ECX Warehouse Application";
            }
             
                
                    
               
            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != string.Empty)
            {
                Session["CurrentWarehouse"] = new Guid(ddlWarehouse.SelectedValue);
                ExpireWarehouseData();
                //Response.Redirect("TestPage.aspx" , true);
                Response.Redirect(string.Format("ListInboxNew2.aspx?CurrentWarehouse={0}", ddlWarehouse.SelectedValue), true);
            }
            else
            {
                if (Master is pUtility)
                {
                    ((pUtility)Master).ErrorMessage.Text = "Please select a warehouse";
                }
            }
        }

        private void ExpireWarehouseData()
        {
            SystemLookup.ExpireWarehouseData();
        }
    }
}
