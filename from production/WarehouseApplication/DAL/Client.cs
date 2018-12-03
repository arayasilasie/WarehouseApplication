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
using Microsoft.ApplicationBlocks.Data;

namespace WarehouseApplication.DAL
{
    public class Client
    {
        public string getClientName(int ClientId)
        {
            string ClientName = "";
            string strSql = "select ClientName from tblClient where ClientID=" + ClientId;
            SqlConnection Conn = Connection.getConnection();
            ClientName = SqlHelper.ExecuteScalar(Conn, CommandType.Text, strSql).ToString();
            Conn.Close();
            return ClientName;
        }
    }
}
