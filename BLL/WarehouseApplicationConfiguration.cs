using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace WarehouseApplication.BLL
{
    public class WarehouseApplicationConfiguration
    {
        public static Guid GetWorkingLanguage()
        {
            return new Guid(ConfigurationSettings.AppSettings["WorkingLanguage"].ToString());
        }
    }
}
