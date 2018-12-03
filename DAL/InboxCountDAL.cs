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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public class InboxCountDAL
    {
        public static List<InboxContent> GetInboxItemsByWarehouseId(Guid Id)
        {
            List<InboxContent> list = null;

            SqlDataReader reader;
            string strSql = "spGetInboxCount";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<InboxContent>();
                    while (reader.Read())
                    {
                        InboxContent obj = new InboxContent();
                        obj.TaskName = reader["TaskName"].ToString();
                        obj.Count = int.Parse(reader["TotalCount"].ToString());
                        obj.InboxGeneratedTime = DateTime.Now;
                        list.Add(obj);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return list;
        }
    }
}
