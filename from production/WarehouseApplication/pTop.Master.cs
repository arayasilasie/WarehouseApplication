using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class pTop : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string lblLoggedUser = HttpContext.Current.User.Identity.Name.Remove(0, HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1);
            //if ((this.Session["LoggedUser"] == null && this.Session["LoggedUserName"] == null) ||
            //    (this.Session["LoggedUser"] != null && this.Session["LoggedUserName"] != null && this.Session["LoggedUserName"].ToString() != lblLoggedUser))
            //{
            //    Guid? UserGuid = UserBLL.GetUser(lblLoggedUser);
            //    if (UserGuid == null)
            //    {
            //        FormsAuthentication.SignOut();
            //        Response.Redirect(ConfigurationManager.AppSettings["lbLoggoffISAURL"]);
            //    }
            //    else
            //    {
            //        this.Session["LoggedUser"] = UserGuid;
            //        this.Session["LoggedUserName"] = lblLoggedUser;
            //    }
            //}
            this.Page.Title = "ECX Warehouse Application";
            //LOAD WAREHOUSE
            lblWarehouse.Text = WarehouseBLL.GetWarehouseNameById(new Guid(UserBLL.GetCurrentWarehouse().ToString())).ToUpper() + " Warehouse".ToUpper();
            lblUserName.Text = UserBLL.GetName(UserBLL.GetCurrentUser());
        }
    }
}
