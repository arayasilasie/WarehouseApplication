using System;
using System.Text;
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

namespace WarehouseApplication
{
    public partial class GetSampleTicket : ECXWarehousePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("window.open('http://msdn.microsoft.com', '', '');");
            sb.Append("</scri");
            sb.Append("pt>");

            //Page.RegisterStartupScript("test", sb.ToString());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "pop", sb.ToString(), false);
        }
    }
}
