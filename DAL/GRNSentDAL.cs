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
using System.Collections;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class GRNSentDAL
    {
        public static List<GRNSentBLL> getCountApprovedGRNSentbyDate( DateTime datesent)
        {
            List<GRNSentBLL> list = null;
            string strSql = "spSentGRN";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Approveddate", SqlDbType.DateTime);
            arPar[0].Value = datesent;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GRNSentBLL>();
                    while (reader.Read())
                    {
                        GRNSentBLL obj = new GRNSentBLL();
                        if (reader["NoofGRN"] != DBNull.Value)
                        {
                            obj.count = int.Parse(reader["NoofGRN"].ToString());
                        }
                        else
                        {
                            throw new Exception("Invalid Count");
                        }
                        if (reader["WarehouseId"] != DBNull.Value)
                        {
                            obj.warehouseId = new Guid(reader["WarehouseId"].ToString());
                        }
                        else
                        {
                            throw new Exception("Invalid Warehouse Id");
                        }
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
            return null;
            return list;
        }
    }
}
