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
    public class Role
    {
        public static DataSet  GetEmployeesByRole()
        {
            string strSql = "GetEmployeesByRole";
            SqlParameter[] arPar = new SqlParameter[1];
            DataSet dsResult = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsResult = SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dsResult;
        }


    }
}
