using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WarehouseApplication.DAL
{

    public class DatabaseHelper
    {
        public static SqlConnection  getConnection (int Type )
        {

            System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString =
                    rootWebConfig.ConnectionStrings.ConnectionStrings["WarehouseApplicationConnectionLocal"];
                if (null != connString)
                {
                    SqlConnection conn = new SqlConnection(connString.ToString());
                    try
                    {
                       conn.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return conn;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }

        }
        public static void closeConnection(SqlConnection Conn)
        {
            try
            {
                Conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
