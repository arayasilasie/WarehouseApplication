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
    public class WarehouseTrackingNoDAL
    {
        public static bool Insert(Guid Id , string TrackingNo, Guid WarehouseId, SqlTransaction trans)
        {
            string strSql = "spInsertWarehouseTrackingNo";

            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            arPar[1] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar,50);
            arPar[1].Value = TrackingNo;

            arPar[2] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = WarehouseId;

            int AffectedRows = 0;
            AffectedRows = SqlHelper.ExecuteNonQuery(trans, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<String> GetWarehouseTracking(string str, Guid WarehouseId )
        {
            string strSql = "Select TrackingNo from tblWarehouseTrackingNo where WarehouseId='" + WarehouseId.ToString() + "'  and TrackingNo in (" + str + ")";
            SqlConnection conn = null;
            SqlDataReader reader;
            List<String> list = null;
            try
            {

                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    list = new List<String>();
                    while (reader.Read())
                    {
                        list.Add(reader["TrackingNo"].ToString());

                    }

                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Tracking No.", ex);
            }
            return list;
        }
        public static List<WarehouseTrackingNoBLL> GetWarehouseForTrackingNos(string str)
        {
            string strSql = "Select TrackingNo,WarehouseId,DateTimeStatmp from tblWarehouseTrackingNo where TrackingNo in (" + str + ")";
            SqlConnection conn = null;
            SqlDataReader reader;
            List<WarehouseTrackingNoBLL> list = null;
            try
            {

                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    list = new List<WarehouseTrackingNoBLL>();
                    while (reader.Read())
                    {
                        WarehouseTrackingNoBLL o = new WarehouseTrackingNoBLL();
                        o.TrackingNo = reader["TrackingNo"].ToString();
                        o.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                        list.Add(o);

                    }

                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Tracking No.", ex);
            }
            return list;
        }
  
    }
}
