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
using System.Data.SqlClient;

namespace WarehouseApplication.DAL
{
    public class Connection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection Conn = null;
            string strConn ;
            strConn  = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;
            Conn = new SqlConnection(strConn);
            try
            {
                Conn.Open();
            }
            catch ( SqlException conn)
            {
                throw conn;
            }
            return Conn;
        }
    }
}
